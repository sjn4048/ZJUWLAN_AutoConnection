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
            textBox2.PasswordChar = '*';
            ShowConfig();
        }

        private void ShowConfig()
        {
            autoConnectionCheckBox.Checked = Config.isAutoConnection;
            autoHideCheckBox.Checked = Config.isAutoHide;
            zjuFirst.Checked = Config.isZJUWLANFirst;
            autoBootCheckBox.Checked = Config.isAutoBoot;
            closeCheckBox.Checked = Config.isNotClose;
            textBox1.Text = Config.username;
            textBox2.Text = Config.password;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            var result = Config.SetConfig(autoConnectionCheckBox.Checked, autoHideCheckBox.Checked, zjuFirst.Checked, autoBootCheckBox.Checked, closeCheckBox.Checked, textBox1.Text, textBox2.Text);
            if (result == false)
                return;
            else
            {
                ShowConfig();
                MessageBox.Show(text: "已成功更改设置。", caption: "成功", icon: MessageBoxIcon.Asterisk, buttons: MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
