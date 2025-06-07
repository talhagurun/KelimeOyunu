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
            changeTheme();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void changeTheme()
        {
            if(Settings.darkMode)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
            }

            foreach(Control control in this.Controls)
            {
                control.BackColor = this.BackColor;
                control.ForeColor = this.ForeColor;
            }
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

                int result = (int)signInCmd.ExecuteScalar();


                try
                {
                    if (result > 0)
                    {
                        SqlCommand getIDCommand = new SqlCommand("SELECT UserID , UserName FROM Users WHERE UserName = @uname", conn);
                        getIDCommand.Parameters.AddWithValue("@uname", txtLoginUsername.Text);

                        int userID = -1;
                        string userName = "";

                        using (SqlDataReader read = getIDCommand.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                userID = read.GetInt32(0);
                                userName = read.GetString(1);
                            }
                        }

                        if (userID != -1)
                        {
                            Session.userID = userID;
                            Session.userName = userName;

                            SqlCommand darkModeCommand = new SqlCommand("SELECT darkMode FROM UserSettings WHERE UserID = @id", conn);
                            darkModeCommand.Parameters.AddWithValue("@id", userID);

                            object darkModeResult = darkModeCommand.ExecuteScalar();

                            if (darkModeResult != null && darkModeResult != DBNull.Value)
                            {
                                Settings.darkMode = Convert.ToBoolean(darkModeResult);
                            }
                            else
                            {
                                Settings.darkMode = false;
                            }

                            MessageBox.Show("Giriş Başarılı");

                            GameForm gameForm = new GameForm();
                            this.Hide();
                            gameForm.ShowDialog();
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı bilgileri alınamadı.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    clearTextbox(this);
                }




            }
        }

        private void txtLoginUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
           
            if(txtSigninPassword.Text != txtSigninPassword2.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor");
                return;
            }

            using (SqlConnection conn = DatabaseConnect.BaglantiOlustur())
            {
                conn.Open();
                
                String usernameQuery = "SELECT COUNT(*) FROM Users WHERE username = @uname";
                SqlCommand usernameCmd = new SqlCommand(usernameQuery , conn);
                usernameCmd.Parameters.AddWithValue("@uname", txtSigninUserName.Text);

                int isUsernameTaken = (int)usernameCmd.ExecuteScalar();

                if(isUsernameTaken > 0)
                {
                    MessageBox.Show("Kullanıcı adı kullanılıyor");
                    return;
                }

                if(txtSigninPassword.Text != txtSigninPassword2.Text)
                {
                    MessageBox.Show("Şifreler uyuşmuyor");
                    return;
                }

                int newUserID;

                using (SqlCommand addUserCommand = new SqlCommand("SignUp", conn))
                {
                    addUserCommand.CommandType = CommandType.StoredProcedure;

                    addUserCommand.Parameters.AddWithValue("@uname", txtSigninUserName.Text);
                    addUserCommand.Parameters.AddWithValue("@password", txtSigninPassword.Text);
                    newUserID = Convert.ToInt32(addUserCommand.ExecuteScalar());
                }

                using (SqlCommand addWordsCommand = new SqlCommand("InsertWordsForNewUser", conn))
                {
                    addWordsCommand.CommandType = CommandType.StoredProcedure;
                    addWordsCommand.Parameters.AddWithValue("@UserID", newUserID);
                    addWordsCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Kayıt başarılı giriş yapabilirsiniz");

                clearTextbox(this);
            }
        }

        private void txtSigninPassword2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLoginPassword_TextChanged(object sender, EventArgs e)
        {

        }
        private void newUserAddWords(int userID)
        {
            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"INSERT INTO UserWordProgress (UserID , WordID , CorrectCount , LastCorrectDate
                                                        SELECT @UserID , WordID , 0 , NULL
                                                        FROM Words 
                                                        WHERE WordID NOT IN (
                                                            SELECT WordID FROM UserWordProgress WHERE UserID = @userID)" , connection);

                command.Parameters.AddWithValue("@userID", userID);
                command.ExecuteNonQuery();
            }
        }
    }
}
