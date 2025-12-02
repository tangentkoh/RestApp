using System;
using System.Drawing;
using System.Windows.Forms;
using EngineerRestApp.Models;
using EngineerRestApp.Forms;
using System.IO;

namespace EngineerRestApp.Forms
{
    public partial class MainForm : Form
    {
        // Null非許容フィールドは、Loadイベントで初期化されるため、Null免除演算子(!)を使用
        private SettingModel _settings = null!;
        private Random _random = new Random();
        private Image _blockImage = null!;
        private const int MaxBlockCount = 200; // 画像を停止させる目安の枚数

        public MainForm()
        {
            InitializeComponent();

            // フォームのプロパティをコードで設定
            this.FormBorderStyle = FormBorderStyle.None; // 枠線を消す
            this.TopMost = true;                        // 常に最前面に表示
            this.TransparencyKey = Color.Magenta;       // 背景色のMagentaを透明にする
            this.BackColor = Color.Magenta;             // 背景色を透明化キーと同じ色に設定
            this.Opacity = 0.01;                        // フォームをほぼ透明にする

            // イベントハンドラの登録 (デザイナーで連結できない場合に備えコードで強制連結)
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;

            // NotifyIconのイベント連結（Designerで設定済みの場合でも再確認として有効）
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            // タスクトレイアイコンの初期設定
            notifyIcon.Text = "休憩促しアプリ - 待機中";
            //notifyIcon.Icon = SystemIcons.Application; // Windowsの標準アイコンを使用
            notifyIcon.Visible = false; // 最初は非表示
        }

        private void MainForm_Load(object? sender, EventArgs e) // Null許容対応
        {
            // フォームを全画面に設定
            this.SetBounds(0, 0, Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen!.Bounds.Height);

            // 初期設定のロード
            _settings = SettingModel.Load();

            // 画像ファイルをロード
            LoadBlockImage();

            // 初期タイマーを起動
            StartInitialTimer();

            // アプリ起動時は最小化（タスクトレイに格納） -> Resizeイベントが呼ばれる
            this.WindowState = FormWindowState.Minimized;
        }

        private void LoadBlockImage()
        {
            if (_blockImage != null)
            {
                _blockImage.Dispose(); // 以前の画像を破棄
            }

            try
            {
                // 設定パスから画像をロード
                _blockImage = Image.FromFile(_settings.ImageFilePath);
            }
            catch (FileNotFoundException)
            {
                // エラーメッセージを表示し、アプリが停止しないように_blockImageをnullのままにする
                MessageBox.Show($"指定された画像ファイルが見つかりません: {_settings.ImageFilePath}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像のロード中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartInitialTimer()
        {
            initialTimer.Stop();
            incrementTimer.Stop();

            // 既存の画像をすべてクリア
            this.Controls.Clear();
            this.Opacity = 0.01;
            this.Hide();

            // 設定時間（分）をミリ秒に変換
            initialTimer.Interval = _settings.InitialDelayMinutes * 60000;
            initialTimer.Tick -= InitialTimer_Tick;
            initialTimer.Tick += InitialTimer_Tick;

            initialTimer.Start();
            notifyIcon.Text = $"休憩促しアプリ - 次の休憩まで {_settings.InitialDelayMinutes} 分";
        }

        private void InitialTimer_Tick(object? sender, EventArgs e) // Null許容対応
        {
            initialTimer.Stop();
            StartImageIncrementTimer();
        }

        private void StartImageIncrementTimer()
        {
            incrementTimer.Stop();

            // フォームを最前面・全画面表示に戻す
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Opacity = 1.0;

            // 画像を増加させる間隔 (秒) をミリ秒に変換
            incrementTimer.Interval = _settings.ImageIntervalSeconds * 1000;
            incrementTimer.Tick -= IncrementTimer_Tick;
            incrementTimer.Tick += IncrementTimer_Tick;
            incrementTimer.Start();
            notifyIcon.Text = "休憩促しアプリ - 休憩を促しています";
        }

        private void IncrementTimer_Tick(object? sender, EventArgs e) // Null許容対応
        {
            AddRandomImage();

            // 画面を埋め尽くす目安に達したらタイマーを停止
            if (this.Controls.Count >= MaxBlockCount)
            {
                incrementTimer.Stop();
                notifyIcon.Text = "休憩促しアプリ - 最大画像数に到達";
            }
        }

        private void AddRandomImage()
        {
            if (_blockImage == null) return;

            // PictureBoxコントロールを作成
            var pb = new PictureBox();
            pb.Image = _blockImage;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            // サイズは AddRandomImage() の項で調整
            pb.Width = 150;
            pb.Height = 150;

            // 画面全体上のランダムな座標を計算
            int x = _random.Next(0, this.Width - pb.Width);
            int y = _random.Next(0, this.Height - pb.Height);

            pb.Location = new Point(x, y);

            // フォームに追加
            this.Controls.Add(pb);
            pb.BringToFront();
        }

        // フォームが最小化されたときの処理
        private void MainForm_Resize(object? sender, EventArgs e) // Null許容対応
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        // タスクトレイアイコンをダブルクリックしたときの処理（フォームの再表示）
        private void notifyIcon_DoubleClick(object? sender, EventArgs e) // Null許容対応
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        // タスクトレイメニュー: 設定を開く (デザイナーで settingsToolStripMenuItem_Click に連結)
        private void settingsToolStripMenuItem_Click(object? sender, EventArgs e) // Null許容対応
        {
            var settingsForm = new SettingsForm(_settings);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // 設定が変更された場合は、最新の設定をロードし、タイマーをリセットして再起動
                _settings = SettingModel.Load();
                LoadBlockImage(); // 画像も再ロード

                // 画像が表示されているかによって、再起動するタイマーを切り替える
                if (this.Controls.Count > 0)
                {
                    StartImageIncrementTimer();
                }
                else
                {
                    StartInitialTimer();
                }
            }
        }

        // タスクトレイメニュー: 終了 (デザイナーで exitToolStripMenuItem_Click に連結)
        private void exitToolStripMenuItem_Click(object? sender, EventArgs e) // Null許容対応
        {
            Application.Exit();
        }

        // フォーム終了時の処理（全て消える）
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            initialTimer.Stop();
            incrementTimer.Stop();

            notifyIcon.Visible = false;

            base.OnFormClosing(e);
        }
    }
}