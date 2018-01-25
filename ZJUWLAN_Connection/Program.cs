using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ZJUWLAN_Connection
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //以下为保证只开启一个实例
            Mutex instance = new Mutex(true, "ZJUWLAN_AutoConnection_Mutex", out bool createdNew);
            if (createdNew)
            {
                instance.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show(text: "我已经打开了呢~在右下角任务栏找到我~", caption: "已打开本程序", icon: MessageBoxIcon.Asterisk, buttons: MessageBoxButtons.OK);
                Application.Exit();
            }
        }
    }
}
