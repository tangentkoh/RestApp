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
            nudInitialDelay = new NumericUpDown();
            nudInterval = new NumericUpDown();
            txtImagePath = new TextBox();
            btnBrowse = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            ofdImageFile = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)nudInitialDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval).BeginInit();
            SuspendLayout();
            // 
            // nudInitialDelay
            // 
            nudInitialDelay.Location = new Point(434, 102);
            nudInitialDelay.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudInitialDelay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudInitialDelay.Name = "nudInitialDelay";
            nudInitialDelay.Size = new Size(180, 31);
            nudInitialDelay.TabIndex = 0;
            nudInitialDelay.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // nudInterval
            // 
            nudInterval.Location = new Point(434, 181);
            nudInterval.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudInterval.Name = "nudInterval";
            nudInterval.Size = new Size(180, 31);
            nudInterval.TabIndex = 1;
            nudInterval.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(434, 260);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(138, 31);
            txtImagePath.TabIndex = 2;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(602, 260);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(112, 34);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "参照";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(206, 108);
            label1.Name = "label1";
            label1.Size = new Size(112, 25);
            label1.TabIndex = 4;
            label1.Text = "遅延時間(分)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(206, 187);
            label2.Name = "label2";
            label2.Size = new Size(112, 25);
            label2.TabIndex = 5;
            label2.Text = "増加時間(秒)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(206, 266);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 6;
            label3.Text = "画像(.png)";
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(241, 366);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 7;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(434, 366);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.UseWaitCursor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBrowse);
            Controls.Add(txtImagePath);
            Controls.Add(nudInterval);
            Controls.Add(nudInitialDelay);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)nudInitialDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudInitialDelay;
        private NumericUpDown nudInterval;
        private TextBox txtImagePath;
        private Button btnBrowse;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnOK;
        private Button btnCancel;
        private OpenFileDialog ofdImageFile;
    }
}