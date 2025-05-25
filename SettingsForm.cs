using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            changeTheme();
        }
        public void changeTheme()
        {
            if (Settings.darkMode)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
            }

            foreach (Control control in this.Controls)
            {
                control.BackColor = this.BackColor;
                control.ForeColor = this.ForeColor;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxDarkMode.Checked)
            {
                this.BackColor = Color.FromArgb(30,30,30);
                this.ForeColor = Color.White;

                checkBoxDarkMode.BackColor = Color.FromArgb(30, 30, 30);
                checkBoxDarkMode.ForeColor = Color.White;
            }
        }

        private void checkBoxDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            Settings.darkMode = checkBoxDarkMode.Checked;

            changeTheme();

            foreach(Form control in Application.OpenForms)
            {
                if(control != this)
                {
                    var changeThemeMethod = control.GetType().GetMethod("changeTheme");
                    if(changeThemeMethod != null)
                    {
                        changeThemeMethod.Invoke(control, null);
                    }
                }
            }




        }
    }
}
