namespace ADNPlugin.Revit.FileUpgrader
{
    partial class UpgraderForm
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
          this.btnSource = new System.Windows.Forms.Button();
          this.txtSrcPath = new System.Windows.Forms.TextBox();
          this.grpSrc = new System.Windows.Forms.GroupBox();
          this.grpDestination = new System.Windows.Forms.GroupBox();
          this.btnDestination = new System.Windows.Forms.Button();
          this.txtDestPath = new System.Windows.Forms.TextBox();
          this.btnUpgrade = new System.Windows.Forms.Button();
          this.btnCancel = new System.Windows.Forms.Button();
          this.lstBxUpdates = new System.Windows.Forms.ListBox();
          this.bar = new System.Windows.Forms.ProgressBar();
          this.grpFileType = new System.Windows.Forms.GroupBox();
          this.chkBoxRTE = new System.Windows.Forms.CheckBox();
          this.chkBoxRFA = new System.Windows.Forms.CheckBox();
          this.chkBoxRVT = new System.Windows.Forms.CheckBox();
          this.chkBoxRFT = new System.Windows.Forms.CheckBox();
          this.grpSrc.SuspendLayout();
          this.grpDestination.SuspendLayout();
          this.grpFileType.SuspendLayout();
          this.SuspendLayout();
          // 
          // btnSource
          // 
          this.btnSource.Location = new System.Drawing.Point(441, 21);
          this.btnSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.btnSource.Name = "btnSource";
          this.btnSource.Size = new System.Drawing.Size(100, 30);
          this.btnSource.TabIndex = 0;
          this.btnSource.Text = "Browse";
          this.btnSource.UseVisualStyleBackColor = true;
          this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
          // 
          // txtSrcPath
          // 
          this.txtSrcPath.Location = new System.Drawing.Point(8, 23);
          this.txtSrcPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtSrcPath.Name = "txtSrcPath";
          this.txtSrcPath.Size = new System.Drawing.Size(425, 22);
          this.txtSrcPath.TabIndex = 1;
          // 
          // grpSrc
          // 
          this.grpSrc.Controls.Add(this.txtSrcPath);
          this.grpSrc.Controls.Add(this.btnSource);
          this.grpSrc.Location = new System.Drawing.Point(13, 14);
          this.grpSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpSrc.Name = "grpSrc";
          this.grpSrc.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpSrc.Size = new System.Drawing.Size(553, 65);
          this.grpSrc.TabIndex = 2;
          this.grpSrc.TabStop = false;
          this.grpSrc.Text = "Source";
          // 
          // grpDestination
          // 
          this.grpDestination.Controls.Add(this.btnDestination);
          this.grpDestination.Controls.Add(this.txtDestPath);
          this.grpDestination.Location = new System.Drawing.Point(13, 86);
          this.grpDestination.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpDestination.Name = "grpDestination";
          this.grpDestination.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpDestination.Size = new System.Drawing.Size(553, 71);
          this.grpDestination.TabIndex = 3;
          this.grpDestination.TabStop = false;
          this.grpDestination.Text = "Destination";
          // 
          // btnDestination
          // 
          this.btnDestination.Location = new System.Drawing.Point(443, 28);
          this.btnDestination.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.btnDestination.Name = "btnDestination";
          this.btnDestination.Size = new System.Drawing.Size(100, 30);
          this.btnDestination.TabIndex = 1;
          this.btnDestination.Text = "Browse";
          this.btnDestination.UseVisualStyleBackColor = true;
          this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
          // 
          // txtDestPath
          // 
          this.txtDestPath.Location = new System.Drawing.Point(8, 30);
          this.txtDestPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtDestPath.Name = "txtDestPath";
          this.txtDestPath.Size = new System.Drawing.Size(425, 22);
          this.txtDestPath.TabIndex = 0;
          // 
          // btnUpgrade
          // 
          this.btnUpgrade.Location = new System.Drawing.Point(347, 183);
          this.btnUpgrade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.btnUpgrade.Name = "btnUpgrade";
          this.btnUpgrade.Size = new System.Drawing.Size(100, 30);
          this.btnUpgrade.TabIndex = 4;
          this.btnUpgrade.Text = "Upgrade";
          this.btnUpgrade.UseVisualStyleBackColor = true;
          this.btnUpgrade.Click += new System.EventHandler(this.btnUpgrade_Click);
          // 
          // btnCancel
          // 
          this.btnCancel.Location = new System.Drawing.Point(467, 182);
          this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.btnCancel.Name = "btnCancel";
          this.btnCancel.Size = new System.Drawing.Size(100, 30);
          this.btnCancel.TabIndex = 5;
          this.btnCancel.Text = "Cancel";
          this.btnCancel.UseVisualStyleBackColor = true;
          this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
          // 
          // lstBxUpdates
          // 
          this.lstBxUpdates.FormattingEnabled = true;
          this.lstBxUpdates.HorizontalScrollbar = true;
          this.lstBxUpdates.ItemHeight = 16;
          this.lstBxUpdates.Location = new System.Drawing.Point(13, 231);
          this.lstBxUpdates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.lstBxUpdates.Name = "lstBxUpdates";
          this.lstBxUpdates.ScrollAlwaysVisible = true;
          this.lstBxUpdates.Size = new System.Drawing.Size(553, 180);
          this.lstBxUpdates.TabIndex = 6;
          // 
          // bar
          // 
          this.bar.Location = new System.Drawing.Point(13, 420);
          this.bar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.bar.Name = "bar";
          this.bar.Size = new System.Drawing.Size(553, 28);
          this.bar.TabIndex = 7;
          // 
          // grpFileType
          // 
          this.grpFileType.Controls.Add(this.chkBoxRFT);
          this.grpFileType.Controls.Add(this.chkBoxRTE);
          this.grpFileType.Controls.Add(this.chkBoxRFA);
          this.grpFileType.Controls.Add(this.chkBoxRVT);
          this.grpFileType.Location = new System.Drawing.Point(13, 165);
          this.grpFileType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpFileType.Name = "grpFileType";
          this.grpFileType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.grpFileType.Size = new System.Drawing.Size(313, 59);
          this.grpFileType.TabIndex = 8;
          this.grpFileType.TabStop = false;
          this.grpFileType.Text = "File Type";
          // 
          // chkBoxRTE
          // 
          this.chkBoxRTE.AutoSize = true;
          this.chkBoxRTE.Location = new System.Drawing.Point(163, 28);
          this.chkBoxRTE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.chkBoxRTE.Name = "chkBoxRTE";
          this.chkBoxRTE.Size = new System.Drawing.Size(58, 21);
          this.chkBoxRTE.TabIndex = 2;
          this.chkBoxRTE.Text = "RTE";
          this.chkBoxRTE.UseVisualStyleBackColor = true;
          // 
          // chkBoxRFA
          // 
          this.chkBoxRFA.AutoSize = true;
          this.chkBoxRFA.Checked = true;
          this.chkBoxRFA.CheckState = System.Windows.Forms.CheckState.Checked;
          this.chkBoxRFA.Location = new System.Drawing.Point(84, 28);
          this.chkBoxRFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.chkBoxRFA.Name = "chkBoxRFA";
          this.chkBoxRFA.Size = new System.Drawing.Size(57, 21);
          this.chkBoxRFA.TabIndex = 1;
          this.chkBoxRFA.Text = "RFA";
          this.chkBoxRFA.UseVisualStyleBackColor = true;
          // 
          // chkBoxRVT
          // 
          this.chkBoxRVT.AutoSize = true;
          this.chkBoxRVT.Checked = true;
          this.chkBoxRVT.CheckState = System.Windows.Forms.CheckState.Checked;
          this.chkBoxRVT.Location = new System.Drawing.Point(8, 28);
          this.chkBoxRVT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.chkBoxRVT.Name = "chkBoxRVT";
          this.chkBoxRVT.Size = new System.Drawing.Size(58, 21);
          this.chkBoxRVT.TabIndex = 0;
          this.chkBoxRVT.Text = "RVT";
          this.chkBoxRVT.UseVisualStyleBackColor = true;
          // 
          // chkBoxRFT
          // 
          this.chkBoxRFT.AutoSize = true;
          this.chkBoxRFT.Location = new System.Drawing.Point(238, 28);
          this.chkBoxRFT.Margin = new System.Windows.Forms.Padding(4);
          this.chkBoxRFT.Name = "chkBoxRFT";
          this.chkBoxRFT.Size = new System.Drawing.Size(57, 21);
          this.chkBoxRFT.TabIndex = 3;
          this.chkBoxRFT.Text = "RFT";
          this.chkBoxRFT.UseVisualStyleBackColor = true;
          // 
          // UpgraderForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
          this.ClientSize = new System.Drawing.Size(581, 462);
          this.Controls.Add(this.grpFileType);
          this.Controls.Add(this.bar);
          this.Controls.Add(this.lstBxUpdates);
          this.Controls.Add(this.btnCancel);
          this.Controls.Add(this.btnUpgrade);
          this.Controls.Add(this.grpDestination);
          this.Controls.Add(this.grpSrc);
          this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "UpgraderForm";
          this.ShowIcon = false;
          this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
          this.Text = "Revit File Upgrader";
          this.grpSrc.ResumeLayout(false);
          this.grpSrc.PerformLayout();
          this.grpDestination.ResumeLayout(false);
          this.grpDestination.PerformLayout();
          this.grpFileType.ResumeLayout(false);
          this.grpFileType.PerformLayout();
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox txtSrcPath;
        private System.Windows.Forms.GroupBox grpSrc;
        private System.Windows.Forms.GroupBox grpDestination;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.TextBox txtDestPath;
        private System.Windows.Forms.Button btnUpgrade;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstBxUpdates;
        private System.Windows.Forms.ProgressBar bar;
        private System.Windows.Forms.GroupBox grpFileType;
        private System.Windows.Forms.CheckBox chkBoxRVT;
        private System.Windows.Forms.CheckBox chkBoxRTE;
        private System.Windows.Forms.CheckBox chkBoxRFA;
        private System.Windows.Forms.CheckBox chkBoxRFT;
    }
}