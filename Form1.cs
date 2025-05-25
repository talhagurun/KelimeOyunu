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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void clearTextbox(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if(c.HasChildren)
                {
                    clearTextbox(c);
                }
            }
        }
        private void btnGiris_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DatabaseConnect.BaglantiOlustur())
            {
                conn.Open();
                SqlCommand signInCmd = new SqlCommand("UserLoginControl", conn);
                signInCmd.CommandType = CommandType.StoredProcedure;

                signInCmd.Parameters.Add("@uname" , txtLoginUsername.Text);
                signInCmd.Parameters.Add("@password", txtLoginPassword.Text);

                int sonuc = (int)signInCmd.ExecuteScalar();

                if (sonuc > 0)
                {
                    MessageBox.Show("Giriş Başarılı");
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış");
                }
                clearTextbox(this);

                GameForm gameForm = new GameForm();
                this.Hide();
                gameForm.ShowDialog();
                this.Show();
            }
        }

        private void txtLoginUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            //Şifrelerin uyuşup uyuşmadığını kontrol et
            if(txtSigninPassword.Text != txtSigninPassword2.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor");
                return;
            }

            using (SqlConnection conn = DatabaseConnect.BaglantiOlustur())
            {
                conn.Open();

                //Kullanıcı adı kullanılıyor mu kontrol et
                String usernameQuery = "SELECT COUNT(*) FROM Users WHERE username = @uname";
                SqlCommand usernameCmd = new SqlCommand(usernameQuery , conn);
                usernameCmd.Parameters.AddWithValue("@uname", txtSigninUserName.Text);

                int isUsernameTaken = (int)usernameCmd.ExecuteScalar();

                if(isUsernameTaken > 0)
                {
                    MessageBox.Show("Kullanıcı adı kullanılıyor");
                    return;
                }

                SqlCommand addUserCmd = new SqlCommand("SignUp", conn);
                addUserCmd.CommandType = CommandType.StoredProcedure;
                if(txtSigninPassword.Text != txtSigninPassword2.Text)
                {
                    MessageBox.Show("Şifreler uyuşmuyor");
                    return;
                }

                addUserCmd.Parameters.AddWithValue("@uname", txtSigninUserName.Text);
                addUserCmd.Parameters.AddWithValue("@password", txtSigninPassword.Text);
                
                addUserCmd.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı Giriş Yapabilirsiniz");

                clearTextbox(this);

                GameForm gameForm = new GameForm();
            }
        }

        private void txtSigninPassword2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLoginPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
