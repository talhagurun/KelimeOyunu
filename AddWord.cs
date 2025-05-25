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

                addWordCmd.ExecuteNonQuery();

                MessageBox.Show("Kelime başarıyla eklendi");

                clearTextbox(this);
            }
            

            
        }
    }
}
