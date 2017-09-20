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
            autoConnectionCheckBox.Checked = Config.autoConnection;
            autoHideCheckBox.Checked = Config.autoHide;
            zjuFirst.Checked = Config.zjuFirst;
            textBox1.Text = Config.username;
            textBox2.Text = Config.password;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (autoConnectionCheckBox.Checked && autoHideCheckBox.Checked)
            {
                MessageBox.Show(text: "你同时勾选了自动连接与自动隐藏，这是一个非常“酷炫”的设定，因为你只需双击程序，程序就会在静默中自动帮你搞定一切。但请务必注意，这也意味着你将再也不会见到程序界面，如果之后你想再次更改设置，请手动删除文件夹中的“config.ini”。如果这个效果并非你所希望的，请返回并取消其中至少一项的勾选。",caption:"啰嗦但重要的须知",icon:MessageBoxIcon.Warning, buttons:MessageBoxButtons.OK);
            }
            Config.SetConfig(autoConnectionCheckBox.Checked, autoHideCheckBox.Checked, textBox1.Text, textBox2.Text);
            MessageBox.Show(text:"已保存设置，建议你再次核对用户名与密码是否填写正确，并在之后关闭设置窗口。",caption:"成功",icon: MessageBoxIcon.Asterisk, buttons:MessageBoxButtons.OK);
        }
    }
}
