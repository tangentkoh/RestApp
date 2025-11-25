using System;
using System.IO;
using System.Windows.Forms;
using EngineerRestApp.Models; // SettingModelを参照するために必要

namespace EngineerRestApp.Forms
{
    // クラス名が SettingsForm であることを確認してください
    public partial class SettingsForm : Form
    {
        // フォームが管理する設定モデル
        public SettingModel CurrentSettings { get; private set; }

        /// <summary>
        /// SettingsFormのコンストラクタ
        /// </summary>
        /// <param name="initialSettings">メインフォームから渡される、現在の設定モデル</param>
        public SettingsForm(SettingModel initialSettings)
        {
            InitializeComponent();

            // 渡された初期設定をコピーして、フォーム内で一時的に編集する。
            CurrentSettings = new SettingModel
            {
                InitialDelayMinutes = initialSettings.InitialDelayMinutes,
                ImageIntervalSeconds = initialSettings.ImageIntervalSeconds,
                ImageFilePath = initialSettings.ImageFilePath
            };

            // フォームが読み込まれたときのイベントハンドラを登録
            this.Load += SettingsForm_Load;

            // コントロールのイベントハンドラを登録
            // これらのボタン名は、デザイナーで設定した名前に対応している必要があります
            btnBrowse.Click += BtnBrowse_Click;
            btnOK.Click += BtnOK_Click;

            // 初期化時に全てのコントロールの値を反映する
            ReflectModelToControls();
        }

        /// <summary>
        /// モデル（CurrentSettings）の値をフォーム上のコントロールに反映する
        /// </summary>
        private void ReflectModelToControls()
        {
            // 時間設定
            // nudInitialDelay, nudInterval はデザイナーで配置した NumericUpDown の Name です
            nudInitialDelay.Value = CurrentSettings.InitialDelayMinutes;
            nudInterval.Value = CurrentSettings.ImageIntervalSeconds;

            // 画像パス設定
            // txtImagePath はデザイナーで配置した TextBox の Name です
            txtImagePath.Text = CurrentSettings.ImageFilePath;
        }

        // フォームロード時のイベントハンドラ
        private void SettingsForm_Load(object? sender, EventArgs e)
        {
            // コンストラクタで既に反映済みのため、ここでは特に処理は不要
        }

        /// <summary>
        /// 「参照...」ボタンクリック時：ファイル選択ダイアログを開く
        /// </summary>
        private void BtnBrowse_Click(object? sender, EventArgs e)
        {
            // ofdImageFile はデザイナーで配置した OpenFileDialog の Name です
            if (ofdImageFile.ShowDialog() == DialogResult.OK)
            {
                // 選択されたファイルパスをテキストボックスに反映
                txtImagePath.Text = ofdImageFile.FileName;
            }
        }

        /// <summary>
        /// 「OK」ボタンクリック時：コントロールの値をモデルに反映し、保存
        /// </summary>
        private void BtnOK_Click(object? sender, EventArgs e)
        {
            // 1. コントロールから新しい値を取得し、モデルを更新
            CurrentSettings.InitialDelayMinutes = (int)nudInitialDelay.Value;
            CurrentSettings.ImageIntervalSeconds = (int)nudInterval.Value;
            CurrentSettings.ImageFilePath = txtImagePath.Text;

            // 2. 永続化処理（settings.jsonに書き出す）
            try
            {
                CurrentSettings.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定の保存中にエラーが発生しました: {ex.Message}", "保存エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 保存に失敗した場合はダイアログを閉じないようにキャンセル扱いにする
                this.DialogResult = DialogResult.None;
                return;
            }

            // 3. フォームを閉じる
            this.DialogResult = DialogResult.OK;
        }

        private void ofdImageFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}