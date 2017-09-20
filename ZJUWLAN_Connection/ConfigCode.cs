using System;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace ZJUWLAN_Connection
{
    public static class Config
    {
        public static bool autoConnection;
        public static bool autoHide;
        public static bool zjuFirst;
        public static string username;
        public static string password;

        public static void ReadConfig()
        {
            if (!File.Exists("Config.ini"))
            {
                throw new Exception();
            }
            using (StreamReader configSr = new StreamReader(new FileStream("Config.ini", FileMode.Open)))
            {
                var configPart = configSr.ReadToEnd().Split(',');
                autoConnection = bool.Parse(configPart[0]);
                autoHide = bool.Parse(configPart[1]);
                zjuFirst = bool.Parse(configPart[2]);
                username = configPart[3];
                password = configPart[4];
            }
        }
        public static void SetConfig(bool autoConnection, bool autoHide, string username, string password)
        {
            using (var configSr = new StreamWriter(new FileStream("Config.ini", FileMode.Create, FileAccess.ReadWrite)))
            {
                string configString = $"{autoConnection},{autoHide},{zjuFirst},{username},{password}";
                configSr.Write(configString);
            }
            ReadConfig();
        }
    }
}

