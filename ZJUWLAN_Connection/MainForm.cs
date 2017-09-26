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

namespace ZJUWLAN_Connection
{
    public partial class MainForm : Form
    {
        WIFIRequest wifiRequest = new WIFIRequest();

        public MainForm()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            connectLabel.Text = checkLabel.Text = "";
            findLabel.Text = "检查WIFI中...";
            Task.Run(() =>
            {
                findLabel.ForeColor = Color.Lime;
                wifiRequest.CheckWifiState(out int signalQuality, out int pingTime);
                try
                {
                    Config.ReadConfig();
                }
                catch
                {
                    MessageBox.Show(text: "进入程序后请先进行设置", caption: "尚未设置", icon: MessageBoxIcon.Information, buttons: MessageBoxButtons.OK);
                }
                if (Config.isAutoConnection)
                {
                    Task.Run(() =>
                    {
                        ConnectWifi();
                    }).Wait();
                }
                DisplayResult(wifiRequest);
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
                ConnectWifi();
            });
        }

        private void DisplayResult(WIFIRequest wifiRequest)
        {
            wifiRequest.CheckWifiState(out int signalQuality, out int pingTime);

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

        private void ConnectWifi()
        {
            findLabel.Text = "已发现ZJUWLAN";
            findLabel.ForeColor = Color.DarkGreen;
            connectLabel.Text = "连接中...";
            connectLabel.ForeColor = Color.MediumBlue;

            var result = wifiRequest.OverallConnection();

            for (int i = 0; i < 3; i++, Thread.Sleep(200))
            {
                wifiRequest.CheckWifiState(out int signalQuality, out int pingTime);
                if (wifiRequest.IsNetAvailable)
                {
                    if (Config.isAutoHide)
                        this.Close();
                    return;
                }
            }
            if (MessageBox.Show(text: $"连接失败，错误码：{result}，请尝试点击“重试”。如仍无法连接，请检查①用户名/密码是否正确，②是否ZJUWLAN信号过弱，③是否已经连接到了其他网络。如仍不能解决，请联系作者 1176827825@qq.com", caption: "Oops", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.RetryCancel) == DialogResult.Retry)
            {
                ConnectWifi();
            }
            DisplayResult(wifiRequest);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }
    }
}
