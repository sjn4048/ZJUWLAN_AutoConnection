using System;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NativeWifi;
using System.Linq;
using System.Text;

namespace ZJUWLAN_Connection
{
    class WIFIRequest
    {
        public bool IsZJUWlanDetected = false;//是否能检测到ZJUWLAN
        public bool IsNetAvailable = false;//是否能连接到网络
        public enum WlanStatusEnum
        {
            ZJUWlan,
            OtherWlan,
            Unconnected
        };//是否连接上了ZJUWLAN
        public WlanStatusEnum WlanStatus = WlanStatusEnum.Unconnected;

        public string GetCurrentConnection()//负责获取当前连接的WIFI的名字，别的不关心
        {
            try
            {
                WlanClient client = new WlanClient();
                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {
                    Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                    var query = networks.Where(a => a.profileName == "ZJUWLAN");
                    if (query != null)
                    {
                        IsZJUWlanDetected = true;
                    }
                    else
                    {
                        IsZJUWlanDetected = false;
                    }
                    foreach (Wlan.WlanAvailableNetwork network in networks)
                    {
                        if (wlanIface.InterfaceState == Wlan.WlanInterfaceState.Connected && wlanIface.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected)
                        {
                            return wlanIface.CurrentConnection.profileName;
                        }
                    }
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void CheckWifiState(out int signalQuality, out int pingTime)//负责检测当前WIFI是否能Ping百度，返回pingTime
        {
            string currentWifiName;
           
            ScanSSID();
            signalQuality = WIFISSID.zjuWlanSsid.wlanSignalQuality;
            pingTime = -1;

            currentWifiName = GetCurrentConnection();
            if (currentWifiName == string.Empty)
            {
                WlanStatus = WlanStatusEnum.Unconnected;
            }
            else if (currentWifiName != string.Empty && currentWifiName != "ZJUWLAN")
            {
                WlanStatus = WlanStatusEnum.OtherWlan;
            }
            else if (currentWifiName == "ZJUWLAN")
            {
                WlanStatus = WlanStatusEnum.ZJUWlan;
            }

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions { DontFragment = true };
            string data = "ping test data";
            byte[] buf = Encoding.ASCII.GetBytes(data);
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    PingReply reply = pingSender.Send("www.baidu.com", 1000, buf, options);
                    pingTime = (int)reply.RoundtripTime;
                    if (reply.Status == IPStatus.Success)
                    {
                        IsNetAvailable = true;
                        break;
                    }
                    if (i == 5)
                    {
                        IsNetAvailable = false;
                    }
                }
                catch
                {
                    IsNetAvailable = false;
                }
            }
        }

        public bool OverallConnection()//联网行为的总入口
        {
            if (IsNetAvailable && !Config.zjuFirst)
                return true;
            ScanSSID();
            if (!IsZJUWlanDetected)
                return false;
            if (WlanStatus != WlanStatusEnum.ZJUWlan)
                ConnectWifi();
            if (IsNetAvailable)
                return true;

            int i = 0;
            while (true)
            {
                try
                {
                    PostUserData(Config.username, Config.password);
                    break;
                }
                catch
                {
                    i++;
                    if (i >= 5)
                        return false;
                }
            }
            return true;
        }

        private void ConnectWifi()//只负责连接WIFI，不负责验证
        {
            string profileName = WIFISSID.zjuWlanSsid.SSID;
            string mac = StringToHex(profileName);
            string myProfileXML = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>manual</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName, mac);
            WIFISSID.zjuWlanSsid.wlanInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, myProfileXML, true);
            WIFISSID.zjuWlanSsid.wlanInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        private void PostUserData(string username, string password)//只负责POST用户名与密码，不负责判断WIFI是否连上
        {
            var data = $"action=login&username={username}&password={password}&ac_id=3&user_ip=&nas_ip=&user_mac=&save_me=1&ajax=1";
            string url = "https://net.zju.edu.cn/include/auth_action.php";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(data);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(data);
            myStreamWriter.Close();
        }

        public void ScanSSID()
        {
            WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                var ZJUWlanNetwork = networks.Where(n => n.profileName == "ZJUWLAN").First();
                WIFISSID.zjuWlanSsid.wlanInterface = wlanIface;
                WIFISSID.zjuWlanSsid.wlanSignalQuality = (int)ZJUWlanNetwork.wlanSignalQuality;
                WIFISSID.zjuWlanSsid.SSID = GetStringForSSID(ZJUWlanNetwork.dot11Ssid);
                WIFISSID.zjuWlanSsid.dot11DefaultAuthAlgorithm = ZJUWlanNetwork.dot11DefaultAuthAlgorithm.ToString();
            }
        }//扫描所有的网络获取ZJUWLAN

        public string StringToHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)    
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString().ToUpper());
        }//字符串转换工具。不用管它。

        public string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.UTF8.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }//字符串转换工具，不用管它。
    }

    class WIFISSID
    {
        public string SSID = "NONE";
        public string dot11DefaultAuthAlgorithm = "";
        public string dot11DefaultCipherAlgorithm = "";
        public bool networkConnectable = true;
        public string wlanNotConnectableReason = "";
        public int wlanSignalQuality = 0;
        public WlanClient.WlanInterface wlanInterface = null;

        public static WIFISSID zjuWlanSsid = new WIFISSID()
        {
            SSID = "ZJUWLAN",
        };
    }
}