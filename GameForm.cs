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
    public partial class GameForm : Form
    {
        public GameForm()
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
        private void btn_Sinav_Click(object sender, EventArgs e)
        {

        }
        public void btn_Analysis()
        {

        }

        private void btn_KelimeEkle_Click(object sender, EventArgs e)
        {
            AddWord addWord = new AddWord();
            this.Hide();
            addWord.ShowDialog();
            this.Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void settingsButton_Click_1(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            this.Hide();
            settings.ShowDialog();
            this.Show();
            

        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {

        }
    }
}
    