using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace ZJUWLAN_Connection
{
    public partial class MainForm : Form
    {
        WIFIRequest wifiRequest = new WIFIRequest();
        string wifiSSID = "ZJUWLAN";
        int pingTime, signalQuality;

        public MainForm()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            SystemEvents.PowerModeChanged += OnPowerChange;
        }

        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    ConnectWifi();
                    break;
                default:
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            connectLabel.Text = checkLabel.Text = "";
            findLabel.ForeColor = Color.DarkGreen;
            findLabel.Text = "检查WIFI中...";
            Task.Run(() =>
            {
                try
                {
                    Config.ReadConfig();
                }
                catch
                {
                    MessageBox.Show(text: "进入程序后请先进行设置", caption: "尚未设置", icon: MessageBoxIcon.Information, buttons: MessageBoxButtons.OK);
                }
                DisplayResult(wifiRequest);
                if (Config.isAutoConnection)
                {
                    Task.Run(() =>
                    {
                        ConnectWifi();
                    }).Wait();
                }
            });
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            DisplayResult(wifiRequest);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                wifiSSID = "ZJUWLAN";
                ConnectWifi();
            });
        }

        private void DisplayResult(WIFIRequest wifiRequest)
        {
            wifiRequest.CheckWifiState(out signalQuality, out pingTime, wifiSSID);

            switch (wifiRequest.WlanStatus)
            {
                case (WIFIRequest.WlanStatusEnum.ZJUWlan):
                    this.connectLabel.Text = "已连接ZJUWLAN";
                    this.connectLabel.ForeColor = Color.DarkGreen;
                    break;
                case (WIFIRequest.WlanStatusEnum.OtherWlan):
                    this.connectLabel.Text = "已连接其他WIFI";
                    this.connectLabel.ForeColor = Color.DarkGreen;
                    break;
                case (WIFIRequest.WlanStatusEnum.Unconnected):
                    this.connectLabel.Text = "未连接WIFI";
                    this.connectLabel.ForeColor = Color.DarkRed;
                    break;
                default:
                    this.connectLabel.Text = "未连接WIFI";
                    break;
            }
            switch (wifiRequest.IsNetAvailable)
            {
                case (true):
                    this.checkLabel.Text = $"已联网, ping:{pingTime}ms, 信号:{WIFISSID.WlanSsid.wlanSignalQuality}";
                    this.checkLabel.ForeColor = Color.DarkGreen;
                    break;
                case (false):
                    this.checkLabel.Text = "未联网";
                    this.checkLabel.ForeColor = Color.DarkRed;
                    break;
            }

            switch (wifiRequest.IsZJUWlanDetected)
            {
                case (true):
                    this.findLabel.Text = "已发现ZJUWLAN";
                    this.findLabel.ForeColor = Color.DarkGreen;
                    break;
                case (false):
                    this.findLabel.Text = "未发现ZJUWLAN";
                    this.findLabel.ForeColor = Color.DarkRed;
                    break;
            }
        }

        private void connectLabel_Click(object sender, EventArgs e)
        {

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            new SettingForm() { TopMost = true }
            .Show();
        }

        private void ConnectWifi(string wifiSSID = "ZJUWLAN")
        {
            findLabel.Text = $"尝试连接{wifiSSID}";
            findLabel.ForeColor = Color.DarkGreen;
            connectLabel.Text = "连接中...";
            connectLabel.ForeColor = Color.MediumBlue;

            var result = wifiRequest.OverallConnection(out signalQuality, out pingTime, wifiSSID);
            if (wifiRequest.IsNetAvailable)
            {
                if (Config.isAutoHide)
                {
                    this.Hide();
                    this.ShowInTaskbar = false;
                    this.NotifyIcon.ShowBalloonTip(2000, "", "已经帮主人连好Wifi啦~需要的时候双击打开我噢~", ToolTipIcon.Info);
                }
                DisplayResult(wifiRequest);
                Process autoProcess = new Process();
                autoProcess.StartInfo.CreateNoWindow = true;
                autoProcess.StartInfo.UseShellExecute = false;
                autoProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                autoProcess.StartInfo.FileName = @"D:\Code\python\PyCharmProjects\AutoLoginToZJUTV.com\Autologin.bat";
                autoProcess.Start();//可改成执行任务部分
                return;
            }

            if (MessageBox.Show(text: $"连接失败，错误码：{result}，请尝试点击“重试”。如仍无法连接，请检查①用户名/密码是否正确，②是否{wifiSSID}信号过弱，③是否已经连接到了其他网络。如仍不能解决，请联系作者 1176827825@qq.com", caption: "Oops", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.RetryCancel) == DialogResult.Retry)
            {
                ConnectWifi(wifiSSID);
            }
            else
                DisplayResult(wifiRequest);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void 打开主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyIcon.Visible = false;
            this.Close();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.NotifyIcon.ShowBalloonTip(2000, "", "我在这里~需要的时候双击打开我噢~", ToolTipIcon.Info);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }
    }
}
