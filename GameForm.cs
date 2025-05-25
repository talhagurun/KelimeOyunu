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
        }

        private void btn_Sinav_Click(object sender, EventArgs e)
        {

        }
        public void btn_Analysis()
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
    