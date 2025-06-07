using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{
    public partial class AddWord : Form
    {
        public AddWord()
        {
            InitializeComponent();
            LoadTopic();
            changeTheme();
        }

        private void LoadTopic()
        {
            cmbTopic.Items.Clear();

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                string query = "SELECT DISTINCT Topic FROM Words WHERE Topic IS NOT NULL AND Topic <> ''";
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbTopic.Items.Add(reader["Topic"].ToString());
                }

                reader.Close();
            }

            if (cmbTopic.Items.Count > 0)
                cmbTopic.SelectedIndex = 0;
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
        private void clearTextbox(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c.HasChildren)
                {
                    clearTextbox(c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DatabaseConnect.BaglantiOlustur())
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Words WHERE LOWER(EnglishWordName) = LOWER(@eng)";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@eng", txtEnglishWord.Text.Trim());

                int ısWordExists = (int)checkCmd.ExecuteScalar();

                if(ısWordExists > 0)
                {
                    MessageBox.Show("Bu kelime zaten var");
                    return;
                }


                SqlCommand addWordCmd = new SqlCommand("AddWord", conn);
                addWordCmd.CommandType = CommandType.StoredProcedure;

                addWordCmd.Parameters.AddWithValue("@english", txtEnglishWord.Text);
                addWordCmd.Parameters.AddWithValue("@turkish" , txtTurkishWord.Text);
                addWordCmd.Parameters.AddWithValue("@topic", cmbTopic.SelectedItem.ToString());

                addWordCmd.ExecuteNonQuery();

                MessageBox.Show("Kelime başarıyla eklendi");

                clearTextbox(this);
            }
            

            
        }

        private void txtTurkishWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEnglishWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
