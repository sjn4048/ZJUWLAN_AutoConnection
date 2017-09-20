namespace ZJUWLAN_Connection
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connectLabel = new System.Windows.Forms.Label();
            this.checkLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CheckButton = new System.Windows.Forms.Button();
            this.findLabel = new System.Windows.Forms.Label();
            this.SettingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectLabel
            // 
            this.connectLabel.AutoSize = true;
            this.connectLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectLabel.Location = new System.Drawing.Point(41, 62);
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Size = new System.Drawing.Size(95, 27);
            this.connectLabel.TabIndex = 1;
            this.connectLabel.Text = "WIFI连接";
            this.connectLabel.Click += new System.EventHandler(this.connectLabel_Click);
            // 
            // checkLabel
            // 
            this.checkLabel.AutoSize = true;
            this.checkLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkLabel.Location = new System.Drawing.Point(41, 100);
            this.checkLabel.Name = "checkLabel";
            this.checkLabel.Size = new System.Drawing.Size(92, 27);
            this.checkLabel.TabIndex = 1;
            this.checkLabel.Text = "联网状态";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConnectButton.Location = new System.Drawing.Point(150, 145);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 40);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "一键连接";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CheckButton
            // 
            this.CheckButton.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckButton.Location = new System.Drawing.Point(34, 145);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(110, 40);
            this.CheckButton.TabIndex = 2;
            this.CheckButton.Text = "检查网络";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // findLabel
            // 
            this.findLabel.AutoSize = true;
            this.findLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.findLabel.Location = new System.Drawing.Point(41, 25);
            this.findLabel.Name = "findLabel";
            this.findLabel.Size = new System.Drawing.Size(95, 27);
            this.findLabel.TabIndex = 1;
            this.findLabel.Text = "检测WIFI";
            // 
            // SettingButton
            // 
            this.SettingButton.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SettingButton.Location = new System.Drawing.Point(256, 145);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(80, 40);
            this.SettingButton.TabIndex = 2;
            this.SettingButton.Text = "设置";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 210);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.checkLabel);
            this.Controls.Add(this.findLabel);
            this.Controls.Add(this.connectLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "一键联网";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label connectLabel;
        public System.Windows.Forms.Label checkLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CheckButton;
        public System.Windows.Forms.Label findLabel;
        private System.Windows.Forms.Button SettingButton;
    }
}

