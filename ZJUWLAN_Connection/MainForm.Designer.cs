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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connectLabel = new System.Windows.Forms.Label();
            this.checkLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CheckButton = new System.Windows.Forms.Button();
            this.findLabel = new System.Windows.Forms.Label();
            this.SettingButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenu_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键连接AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCounter = new System.Windows.Forms.Timer(this.components);
            this.speedLabel = new System.Windows.Forms.Label();
            this.testSpeedButton = new System.Windows.Forms.Button();
            this.ContextMenu_NotifyIcon.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.ConnectButton.Location = new System.Drawing.Point(126, 189);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(87, 40);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "一键连接";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CheckButton
            // 
            this.CheckButton.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckButton.Location = new System.Drawing.Point(31, 189);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(89, 40);
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
            this.SettingButton.Location = new System.Drawing.Point(219, 189);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(68, 40);
            this.SettingButton.TabIndex = 2;
            this.SettingButton.Text = "设置";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.ContextMenuStrip = this.ContextMenu_NotifyIcon;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "ZJUWLAN自动连接";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // ContextMenu_NotifyIcon
            // 
            this.ContextMenu_NotifyIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenu_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开主界面ToolStripMenuItem,
            this.一键连接AToolStripMenuItem,
            this.toolStripMenuItem2,
            this.设置ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.退出ToolStripMenuItem});
            this.ContextMenu_NotifyIcon.Name = "ContextMenu_NotifyIcon";
            this.ContextMenu_NotifyIcon.Size = new System.Drawing.Size(176, 112);
            // 
            // 打开主界面ToolStripMenuItem
            // 
            this.打开主界面ToolStripMenuItem.Name = "打开主界面ToolStripMenuItem";
            this.打开主界面ToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.打开主界面ToolStripMenuItem.Text = "打开主界面(&O)";
            this.打开主界面ToolStripMenuItem.Click += new System.EventHandler(this.打开主界面ToolStripMenuItem_Click);
            // 
            // 一键连接AToolStripMenuItem
            // 
            this.一键连接AToolStripMenuItem.Name = "一键连接AToolStripMenuItem";
            this.一键连接AToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.一键连接AToolStripMenuItem.Text = "一键连接(&C)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(172, 6);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.设置ToolStripMenuItem.Text = "设置(&S)";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.退出ToolStripMenuItem.Text = "退出(&X)";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开日志ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 28);
            // 
            // 打开日志ToolStripMenuItem
            // 
            this.打开日志ToolStripMenuItem.Name = "打开日志ToolStripMenuItem";
            this.打开日志ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.打开日志ToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.打开日志ToolStripMenuItem.Text = "打开日志";
            this.打开日志ToolStripMenuItem.Click += new System.EventHandler(this.打开日志ToolStripMenuItem_Click);
            // 
            // timerCounter
            // 
            this.timerCounter.Tick += new System.EventHandler(this.timerCounter_Tick);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.speedLabel.Location = new System.Drawing.Point(41, 140);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(92, 27);
            this.speedLabel.TabIndex = 1;
            this.speedLabel.Text = "当前网速";
            // 
            // testSpeedButton
            // 
            this.testSpeedButton.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.testSpeedButton.Location = new System.Drawing.Point(293, 189);
            this.testSpeedButton.Name = "testSpeedButton";
            this.testSpeedButton.Size = new System.Drawing.Size(68, 40);
            this.testSpeedButton.TabIndex = 2;
            this.testSpeedButton.Text = "测速";
            this.testSpeedButton.UseVisualStyleBackColor = true;
            this.testSpeedButton.Click += new System.EventHandler(this.testSpeedButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 244);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.testSpeedButton);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.checkLabel);
            this.Controls.Add(this.findLabel);
            this.Controls.Add(this.connectLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "一键联网";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ContextMenu_NotifyIcon.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem 打开主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一键连接AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开日志ToolStripMenuItem;
        private System.Windows.Forms.Timer timerCounter;
        public System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Button testSpeedButton;
    }
}

