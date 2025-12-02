using System;
using System.Windows.Forms;
using EngineerRestApp.Models;

namespace EngineerRestApp.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingModel CurrentSettings { get; private set; }

        public SettingsForm(SettingModel initialSettings)
        {
            InitializeComponent();
            CurrentSettings = new SettingModel
            { /* initialSettingsから値をコピー */ };

            // イベントハンドラの登録と初期値の反映
            this.Load += SettingsForm_Load;
            btnBrowse.Click += BtnBrowse_Click;
            btnOK.Click += BtnOK_Click;
            ReflectModelToControls();
        }

        // モデルの値をコントロールに反映するメソッド
        private void ReflectModelToControls() { /* ... 実装 ... */ }

        private void SettingsForm_Load(object? sender, EventArgs e) { /* ... */ }

        private void BtnBrowse_Click(object? sender, EventArgs e) // 💡 object? に修正
        {
            if (ofdImageFile.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofdImageFile.FileName;
            }
        }

        private void BtnOK_Click(object? sender, EventArgs e) // 💡 object? に修正
        {
            // コントロールから値を CurrentSettings に取得し、
            CurrentSettings.InitialDelayMinutes = (int)nudInitialDelay.Value;
            CurrentSettings.Save(); // JSONファイルに保存
            this.DialogResult = DialogResult.OK;
        }
    }
}