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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            ShowConfig();
        }

        private void ShowConfig()
        {
            autoConnectionCheckBox.Checked = Config.isAutoConnection;
            autoHideCheckBox.Checked = Config.isAutoHide;
            zjuFirst.Checked = Config.isZJUWLANFirst;
            autoBootCheckBox.Checked = Config.isAutoBoot;
            textBox1.Text = Config.username;
            textBox2.Text = Config.password;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Config.SetConfig(autoConnectionCheckBox.Checked, autoHideCheckBox.Checked, zjuFirst.Checked, autoBootCheckBox.Checked, textBox1.Text, textBox2.Text);
            ShowConfig();
            if (autoConnectionCheckBox.Checked && autoHideCheckBox.Checked)
            {
                MessageBox.Show(text: "你同时勾选了自动连接与自动隐藏，这是一个非常“酷炫”的设定，因为你只需双击程序，程序就会在静默中自动帮你搞定一切。但请务必注意，程序界面将自动隐藏，请在右下角托盘处找到程序。",caption:"啰嗦但重要的须知",icon:MessageBoxIcon.Warning, buttons:MessageBoxButtons.OK);
            }
            MessageBox.Show(text:"已成功更改设置。",caption:"成功",icon: MessageBoxIcon.Asterisk, buttons:MessageBoxButtons.OK);
            this.Close();
        }
    }
}
