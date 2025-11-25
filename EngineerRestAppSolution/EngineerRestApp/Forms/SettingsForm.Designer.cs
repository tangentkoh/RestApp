namespace EngineerRestApp.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            nudInitialDelay = new NumericUpDown();
            label2 = new Label();
            nudInterval = new NumericUpDown();
            label3 = new Label();
            txtImagePath = new TextBox();
            btnBrowse = new Button();
            btnOK = new Button();
            btnCancel = new Button();
            ofdImageFile = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)nudInitialDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(60, 117);
            label1.Name = "label1";
            label1.Size = new Size(191, 25);
            label1.TabIndex = 0;
            label1.Text = "休憩開始までの時間(分)";
            // 
            // nudInitialDelay
            // 
            nudInitialDelay.Location = new Point(430, 117);
            nudInitialDelay.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudInitialDelay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudInitialDelay.Name = "nudInitialDelay";
            nudInitialDelay.Size = new Size(180, 31);
            nudInitialDelay.TabIndex = 1;
            nudInitialDelay.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 177);
            label2.Name = "label2";
            label2.Size = new Size(163, 25);
            label2.TabIndex = 2;
            label2.Text = "画像増加の時間(秒)";
            // 
            // nudInterval
            // 
            nudInterval.Location = new Point(430, 177);
            nudInterval.Name = "nudInterval";
            nudInterval.Size = new Size(180, 31);
            nudInterval.TabIndex = 3;
            nudInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 236);
            label3.Name = "label3";
            label3.Size = new Size(242, 25);
            label3.TabIndex = 4;
            label3.Text = "使用する画像ファイル(png推奨)";
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(430, 236);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(150, 31);
            txtImagePath.TabIndex = 5;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(637, 236);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(112, 34);
            btnBrowse.TabIndex = 6;
            btnBrowse.Text = "参照...";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(242, 353);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 7;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(430, 353);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ofdImageFile
            // 
            ofdImageFile.FileName = "openFileDialog1";
            ofdImageFile.Filter = "\"PNGファイル (*.png)|*.png|すべてのファイル (*.*)|*.*\"";
            ofdImageFile.FileOk += ofdImageFile_FileOk;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(btnBrowse);
            Controls.Add(txtImagePath);
            Controls.Add(label3);
            Controls.Add(nudInterval);
            Controls.Add(label2);
            Controls.Add(nudInitialDelay);
            Controls.Add(label1);
            Name = "SettingsForm";
            Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)nudInitialDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown nudInitialDelay;
        private Label label2;
        private NumericUpDown nudInterval;
        private Label label3;
        private TextBox txtImagePath;
        private Button btnBrowse;
        private Button btnOK;
        private Button btnCancel;
        private OpenFileDialog ofdImageFile;
    }
}