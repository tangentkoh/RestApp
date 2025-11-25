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
        private SettingModel _settings = null!;
        private Random _random = new Random();
        private Image _blockImage = null!; // 読み込んだ画像
        private const int MaxBlockCount = 100; // 画像を停止させる目安の枚数

        public MainForm()
        {
            InitializeComponent();

            // フォームのプロパティをコードで設定
            this.FormBorderStyle = FormBorderStyle.None; // 枠線を消す
            this.TopMost = true;                        // 常に最前面に表示
            this.TransparencyKey = Color.Magenta;       // 背景色のMagentaを透明にする
            this.BackColor = Color.Magenta;             // 背景色を透明化キーと同じ色に設定
            this.Opacity = 0.01;                        // フォームをほぼ透明にする (クリックイベントを拾うため)

            // イベントハンドラの登録
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;

            // タスクトレイアイコンの設定（デザイナーでContextMenuStripを設定している前提）
            notifyIcon.Text = "休憩促しアプリ - 待機中";
            notifyIcon.Icon = SystemIcons.Application; // Windowsの標準アイコンを使用
            notifyIcon.Visible = false; // 最初は非表示
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            // フォームを全画面に設定
            this.SetBounds(0, 0, Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            // 初期設定のロード
            _settings = SettingModel.Load();

            // 画像ファイルをロード
            LoadBlockImage();

            // 初期タイマーを起動
            StartInitialTimer();

            // アプリ起動時は最小化（タスクトレイに格納）
            this.WindowState = FormWindowState.Minimized;
        }

        // --- 省略 --- (以下に各メソッドの実装)
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
                MessageBox.Show($"指定された画像ファイルが見つかりません: {_settings.ImageFilePath}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像のロード中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartInitialTimer()
        {
            // 既存のタイマーがあれば停止
            initialTimer.Stop();
            incrementTimer.Stop();

            // 設定時間（分）をミリ秒に変換
            initialTimer.Interval = _settings.InitialDelayMinutes * 60000;
            initialTimer.Tick -= InitialTimer_Tick; // 多重登録を防ぐ
            initialTimer.Tick += InitialTimer_Tick;

            // 準備が整ったらタイマーをスタート
            initialTimer.Start();
            notifyIcon.Text = $"休憩促しアプリ - 次の休憩まで {_settings.InitialDelayMinutes} 分";
        }

        private void InitialTimer_Tick(object? sender, EventArgs e)
        {
            // 休憩時間到達
            initialTimer.Stop();

            // 画像増加タイマーを起動
            StartImageIncrementTimer();
        }
        private void StartImageIncrementTimer()
        {
            // 既存のタイマーがあれば停止
            incrementTimer.Stop();

            // フォームを表示状態にする
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Opacity = 1.0; // フォームを不透明にして、画像が表示されていることを明確にする

            // 画像を増加させる間隔 (秒) をミリ秒に変換
            incrementTimer.Interval = _settings.ImageIntervalSeconds * 1000;
            incrementTimer.Tick -= IncrementTimer_Tick;
            incrementTimer.Tick += IncrementTimer_Tick;
            incrementTimer.Start();
            notifyIcon.Text = "休憩促しアプリ - 休憩を促しています";
        }

        private void IncrementTimer_Tick(object? sender, EventArgs e)
        {
            // 画像をランダムに追加
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

            // サイズ調整モード (画像がPicture Boxのサイズに合わせて拡大縮小される)
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            // 画像サイズは固定とする (調整可能)
            pb.Width = 120;
            pb.Height = 120;

            // 画面全体上のランダムな座標を計算
            // FormBorderStyle=Noneで全画面設定しているため、this.Width/Heightが画面サイズとなる
            int x = _random.Next(0, this.Width - pb.Width);
            int y = _random.Next(0, this.Height - pb.Height);

            // 位置を設定
            pb.Location = new Point(x, y);

            // フォームに追加
            this.Controls.Add(pb);

            // 画像を最前面に移動 (他のコントロール（もしあれば）の上に表示させるため)
            pb.BringToFront();
        }
        // フォームが最小化されたときの処理
        private void MainForm_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); // フォームを非表示にし
                notifyIcon.Visible = true; // タスクトレイアイコンを表示
            }
        }

        // タスクトレイアイコンをダブルクリックしたときの処理（フォームの再表示）
        private void notifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        // タスクトレイメニュー: 設定を開く
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 設定フォームを開く
            var settingsForm = new SettingsForm(_settings);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // 設定が変更された場合は、最新の設定をロードし、タイマーをリセットして再起動
                _settings = SettingModel.Load();
                LoadBlockImage(); // 画像も再ロード

                // 画像が既に表示中の場合は、タイマーだけリセットして再起動
                if (this.Controls.Count > 0)
                {
                    StartImageIncrementTimer();
                }
                else
                {
                    // 待機中の場合は、初期タイマーを再起動
                    StartInitialTimer();
                }
            }
        }

        // タスクトレイメニュー: 終了
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // アプリケーションを終了
            Application.Exit();
        }

        // フォーム終了時の処理（全て消える）
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // タイマーを確実に停止
            initialTimer.Stop();
            incrementTimer.Stop();

            // タスクトレイアイコンを非表示にして破棄
            notifyIcon.Visible = false;

            // フォームが閉じられると、その上の PictureBox コントロールも自動的に破棄され、画像は消えます。
            base.OnFormClosing(e);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}