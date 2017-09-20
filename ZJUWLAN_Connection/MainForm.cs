using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJUWLAN_Connection
{
    public partial class MainForm : Form
    {
        WIFIRequest wifiRequest = new WIFIRequest();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Config.ReadConfig();
                CheckWifiStateInWinForm(wifiRequest);
                if (Config.autoConnection)
                {
                    ConnectButton.PerformClick();
                }
            }
            catch
            {
                MessageBox.Show(text:"进入程序后请先进行设置", caption:"尚未设置",icon:MessageBoxIcon.Information, buttons:MessageBoxButtons.OK);
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            CheckWifiStateInWinForm(wifiRequest);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!wifiRequest.OverallConnection())
            {
                MessageBox.Show(text: "连接失败，请重试。如仍无法连接，请检查①WLAN是否连接，②用户名/密码是否正确，③是否已经连接到了其他网络。如仍不能解决，请联系作者 1176827825@qq.com", caption: "Oops", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
            else
            {
                CheckWifiStateInWinForm(wifiRequest);
                if (Config.autoHide)
                {
                    this.Close();
                }
            }
        }

        private void CheckWifiStateInWinForm(WIFIRequest wifiRequest)
        {
            int pingTime = -1, signalQuality = 0;
            wifiRequest.CheckWifiState(out signalQuality ,out pingTime);

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
                    this.checkLabel.Text = $"已联网, ping:{pingTime}ms, 信号:{WIFISSID.zjuWlanSsid.wlanSignalQuality}";
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
    }
}
