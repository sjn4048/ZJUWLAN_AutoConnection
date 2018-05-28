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
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net.NetworkInformation;

namespace ZJUWLAN_Connection
{
    public partial class MainForm : Form
    {
        NetworkMonitor networkMonitor;

        WIFIRequest wifiRequest = new WIFIRequest();
        string wifiSSID = "ZJUWLAN";
        int pingTime, signalQuality;

        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false; //解决跨窗体传参的问题
            InitializeComponent();
            SystemEvents.PowerModeChanged += OnPowerChange; //如果从睡眠中恢复，则自动连接
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChangedCallback); //如果ip地址改变
            timerCounter.Interval = 1000;
            timerCounter.Start();
        }

        private void NetworkChangedCallback(object sender, EventArgs e) //改变ip或网络条件后的行为
        {

            ConnectWifi(showFailReason: false);//后台自动连接时，不弹出失败原因，失败就失败了吧
        }

        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    ConnectWifi(showFailReason: false);
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
                    new SettingForm()
                    {
                        TopMost = true
                    }.ShowDialog();

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

        private void TestSpeed()
        {
            networkMonitor = new NetworkMonitor();
            if (networkMonitor.Adapters.Length == 0)
            {
                throw new Exception("No network adapters found on this computer.");
            }
            //networkMonitor.StopMonitoring();
            networkMonitor.StartMonitoring();
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
                    this.checkLabel.Text = $"ping: {pingTime}ms, 信号: {WIFISSID.WlanSsid.wlanSignalQuality}";
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
            .ShowDialog();
        }

        private void ConnectWifi(string wifiSSID = "ZJUWLAN", bool showFailReason = true)
        {
            try
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
                        this.WindowState = FormWindowState.Minimized;
                        this.ShowInTaskbar = false;
                        this.NotifyIcon.ShowBalloonTip(2000, "连接成功", "已经帮主人连好Wifi啦~", ToolTipIcon.Info);
                    }
                    TestSpeed();
                    DisplayResult(wifiRequest);
                    return;
                }

                if (!showFailReason) //如果不显示失败原因
                {
                    NotifyIcon.ShowBalloonTip(2000, "重连失败", "自动重连wifi失败，点击查看详情", ToolTipIcon.Warning);
                    NotifyIcon.BalloonTipClicked += (s, arg) =>
                    {
                        if (MessageBox.Show(text: $"连接失败，错误码：{result}，请尝试点击“重试”。如仍无法连接，请检查①用户名/密码是否正确，②是否{wifiSSID}信号过弱，③是否已经连接到了其他网络。如仍不能解决，请联系作者 1176827825@qq.com", caption: "Oops", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                            ConnectWifi(wifiSSID);
                        else DisplayResult(wifiRequest);
                    };
                }
                else if (showFailReason) //如果显示失败原因
                {
                    if (MessageBox.Show(text: $"连接失败，错误码：{result}，请尝试点击“重试”。如仍无法连接，请检查①用户名/密码是否正确，②是否{wifiSSID}信号过弱，③是否已经连接到了其他网络。如仍不能解决，请联系作者 1176827825@qq.com", caption: "Oops", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                        ConnectWifi(wifiSSID);
                    else
                        DisplayResult(wifiRequest);
                }
            }
            catch (Exception ex)
            {
                FileStream fs = new FileStream("error.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("-------\n" + ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace + "\n" + DateTime.Now.ToShortTimeString() + "\n-------\n\n");
            }
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
                if (Config.isFirstUse)
                {
                    this.NotifyIcon.ShowBalloonTip(2000, "", "我在这里~需要的时候双击打开我噢~", ToolTipIcon.Info);
                    Config.SetConfig(firstUse: false);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingForm() { TopMost = true }
            .ShowDialog();
        }

        private void 打开日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("error.log"))
                File.Create("error.log").Close();
            Process.Start("error.log");
        }

        private void timerCounter_Tick(object sender, EventArgs e)
        {
            if (networkMonitor == null || networkMonitor.Adapters.Length == 0)
            {
                speedLabel.Text = "当前速度(Kbps): 未联网";
                speedLabel.ForeColor = Color.DarkRed;
            }
            else
            {
                speedLabel.Text = "当前速度(Kbps): " + networkMonitor.Adapters.Sum(x => x.DownloadSpeedKbps).ToString("0.0") + " / " + networkMonitor.Adapters.Sum(x => x.UploadSpeedKbps).ToString("0.0");
                speedLabel.ForeColor = Color.DarkGreen;
            }
            DisplayResult(wifiRequest);
        }

        private void testSpeedButton_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => // 将阻塞线程的操作在另外一个线程中执行，这样就不会堵塞UI线程。     
            {
                double result = wifiRequest.TestMaxSpeed(); //运行时间5s左右  
                MessageBox.Show("Speed: " + result.ToString("0.0") + "kb/s");
            });
        }

        protected override void WndProc(ref Message msg) //右上角关闭窗体后，不自动关闭程序
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE)) //点击winform右上关闭按钮 
            {
                if (Config.isNotClose)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    //this.NotifyIcon.ShowBalloonTip(2000, "", "我在这里~需要的时候双击打开我噢~", ToolTipIcon.Info);
                }
                // 加入不关闭的逻辑处理

                return;//阻止了窗体关闭
            }
            base.WndProc(ref msg);
        }
    }
}