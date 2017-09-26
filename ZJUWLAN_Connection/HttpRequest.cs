using System;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NativeWifi;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZJUWLAN_Connection
{
    class WIFIRequest
    {
        public bool IsZJUWlanDetected;//是否能检测到ZJUWLAN
        public bool IsNetAvailable;//是否能连接到网络
        public enum WlanStatusEnum
        {
            ZJUWlan,
            OtherWlan,
            Unconnected
        };//是否连接上了ZJUWLAN
        public WlanStatusEnum WlanStatus = WlanStatusEnum.Unconnected;

        public enum ConnectionResult
        {
            Success,
            AlreadyConnected,
            AlteredToZJUWLAN,
            Unfound,
            FailToConnect,
            FailToWork,
        };

        public void CheckWifiState(out int signalQuality, out int pingTime, string wifiSSID = "ZJUWLAN")//负责检测当前WIFI是否能Ping百度，返回pingTime
        {
            signalQuality = WIFISSID.WlanSsid.wlanSignalQuality;
            pingTime = -1;

            string currentWifiName = GetCurrentConnection(wifiSSID);
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
            for (int i = 0; i < 2; i++)
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
                    if (i == 2)
                    {
                        IsNetAvailable = false;
                    }
                }
                catch
                {
                    IsNetAvailable = false;
                    break;
                }
            }
        }

        public ConnectionResult OverallConnection(string wifiSSID = "ZJUWLAN")//联网行为的总入口，默认连ZJUWLAN
        {
            ConnectionResult connectionResult = ConnectionResult.Success;

            if (IsNetAvailable && !Config.isZJUWLANFirst)
            {
                return ConnectionResult.AlreadyConnected;
            }
            else if (IsNetAvailable && Config.isZJUWLANFirst)
            {
                connectionResult = ConnectionResult.AlteredToZJUWLAN;
            }

            if (!IsZJUWlanDetected)
            {
                return ConnectionResult.Unfound;
            }

            if (WlanStatus != WlanStatusEnum.ZJUWlan)
            {
                if (wifiSSID == "ZJUWLAN")
                    ConnectWifi(wifiSSID);
            }

            CheckWifiState(out int signalQuality, out int pingTime);
            if (IsNetAvailable)
            {
                return ConnectionResult.Success;
            }

            int i = 0;
            while (true)
            {
                try
                {
                    PostZJUWLAN(Config.username, Config.password);
                    break;
                }
                catch
                {
                    if (i++ >= 15)
                        return ConnectionResult.FailToWork;
                }
            }
            return connectionResult;
        }

        private void ConnectWifi(string wifiSSID)//只负责连接WIFI，不负责验证
        {
            string profileName = WIFISSID.WlanSsid.SSID;
            string mac = StringToHex(profileName);
            string myProfileXML = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>manual</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName, mac);
            WIFISSID.WlanSsid.wlanInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, myProfileXML, true);
            WIFISSID.WlanSsid.wlanInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        private void PostZJUTV(string username, string password)//连接All Hail ZJUTV
        {
            var data = "ToBeFilled";//
            string url = "net.zjubtv.com/login";//这个也确认一下

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(data);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(data);
            myStreamWriter.Close();
        }

        private void PostZJUWLAN(string username, string password)//只负责POST用户名与密码，不负责判断WIFI是否连上
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

        public string GetCurrentConnection(string WlanToBeChecked)//负责获取当前连接的WIFI的名字，别的不关心
        {
            WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                if (networks.Any(n => n.profileName == WlanToBeChecked))
                {
                    IsZJUWlanDetected = true;
                    var ZJUWlanNetwork = networks.Where(n => n.profileName == WlanToBeChecked).First();
                    WIFISSID.WlanSsid.wlanInterface = wlanIface;
                    WIFISSID.WlanSsid.wlanSignalQuality = (int)ZJUWlanNetwork.wlanSignalQuality;
                    WIFISSID.WlanSsid.SSID = GetStringForSSID(ZJUWlanNetwork.dot11Ssid);
                    WIFISSID.WlanSsid.dot11DefaultAuthAlgorithm = ZJUWlanNetwork.dot11DefaultAuthAlgorithm.ToString();
                }

                if (wlanIface.InterfaceState == Wlan.WlanInterfaceState.Connected && wlanIface.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected)
                {
                    return wlanIface.CurrentConnection.profileName;
                }
            }
            return string.Empty;
        }

        public string StringToHex(string str)//字符串转换工具。不用管它。
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)    
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString().ToUpper());
        }

        public string GetStringForSSID(Wlan.Dot11Ssid ssid)//字符串转换工具，不用管它。
        {
            return Encoding.UTF8.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        public int JudgeSystemWifiState()//判断系统wifi开关是否打开，还没写好
        {
            


            return 0;
        }

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

        public static WIFISSID WlanSsid = new WIFISSID();
    }
}