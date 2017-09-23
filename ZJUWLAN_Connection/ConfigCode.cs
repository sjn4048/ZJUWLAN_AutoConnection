using System;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Principal;

namespace ZJUWLAN_Connection
{
    public static class Config
    {
        public static bool isAutoConnection;//打开后自动连接
        public static bool isAutoHide;//连接后自动关闭
        public static bool isZJUWLANFirst;//总是优先连接zjuwlan
        public static bool isAutoBoot;//开机自动启动
        public static string username;//用户名
        public static string password;//密码

        public static string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}config.ini";


        public static void ReadConfig()
        {
            if (!File.Exists(configPath))
            {
                throw new Exception("无法读取文件");
            }
            using (StreamReader configSr = new StreamReader(new FileStream(configPath, FileMode.Open)))
            {
                var configPart = configSr.ReadToEnd().Split(',');
                isAutoConnection = bool.Parse(configPart[0]);
                isAutoHide = bool.Parse(configPart[1]);
                isZJUWLANFirst = bool.Parse(configPart[2]);
                isAutoBoot = bool.Parse(configPart[3]);
                username = configPart[4];
                password = configPart[5];
            }
            if (isAutoBoot && !SetAutoBootStatus(isAutoBoot))
            {
                MessageBox.Show(text: "开机自启动需要管理员权限。请退出程序，并右键以管理员身份重新打开程序进行设置。", caption: "自启动设置失败", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
                isAutoBoot = false;
            }
        }
        public static void SetConfig(bool autoConnection, bool autoHide, bool zjuFirst, bool autoBoot, string username, string password)
        {
            if (!IsAdministrator() && (autoBoot != isAutoBoot))
            {
                MessageBox.Show(text: "开机自启动需要管理员权限。请退出程序，并右键以管理员身份重新打开程序进行设置。", caption: "自启动设置失败", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
                autoBoot = isAutoBoot;
            }
            using (var configSr = new StreamWriter(new FileStream(configPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                string configString = $"{autoConnection},{autoHide},{zjuFirst},{autoBoot},{username},{password}";
                configSr.Write(configString);
            }
            ReadConfig();
        }

        public static bool SetAutoBootStatus(bool isAutoBoot)//设置开机自启动
        {
            try
            {
                string exePath = $"\"{Application.ExecutablePath.Replace('/', '\\')}\"";//程序exe文件位置
                var a = Application.ExecutablePath;
                string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";//自启动注册表目录

                string value = "ZJUWLANAutoConnector";//写入注册表的值
                RegistryKey registryKey = Registry.CurrentUser;//总的注册表变量
                RegistryKey registryKey_Read = registryKey.OpenSubKey(registryKeyPath);//读取注册表

                if (isAutoBoot)
                {
                    var valueInRegistry = registryKey_Read.GetValue(value);
                    if (valueInRegistry == null || valueInRegistry.ToString() != exePath)
                    {
                        using (RegistryKey registryKey_Write = registryKey.CreateSubKey(registryKeyPath))//写入注册表
                            registryKey_Write.SetValue(value, exePath);
                    }
                }
                else
                {
                    using (RegistryKey registryKey_Write = registryKey.CreateSubKey(registryKeyPath))//删除注册表
                        registryKey_Write.DeleteValue(value, false);
                }
                registryKey.Close();
                registryKey_Read.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}

