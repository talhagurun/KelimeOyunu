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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            changeTheme();
            LoadUserSettings();
        }

        private void LoadUserSettings()
        {
            int userID = Session.userID;

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT QuizWordCount , darkMode FROM UserSettings WHERE UserID = @id", connection);

                command.Parameters.AddWithValue("@id", userID);

                object result = command.ExecuteScalar();


                SqlDataReader read = command.ExecuteReader();

                if(read.Read())
                {

                    
                    int quizWordCount = 10;

                    if (read["QuizWordCount"] != DBNull.Value)
                    {
                        quizWordCount = Convert.ToInt32(read["QuizWordCount"]);
                    }

                    numericUpDownQuizWordCount.Value = quizWordCount;

                    //Karanlık tema

                    bool darkMode = false;

                    if (read["darkMode"] != DBNull.Value)
                    {
                        darkMode = Convert.ToBoolean(read["darkMode"]);

                    }
                    checkBoxDarkMode.Checked = darkMode;

                    if(darkMode)
                    {
                        changeTheme();
                    }
                    else
                    {

                    }


                }
            }
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

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserSettings WHERE UserID = @id", connection);
                command.Parameters.AddWithValue("@id", Session.userID);

                int isDarkMode = (int)command.ExecuteScalar();

                if(isDarkMode > 0)
                {
                    SqlCommand updateCommand = new SqlCommand("UPDATE UserSettings SET darkMode = @dark WHERE UserID = @id ", connection);
                    updateCommand.Parameters.AddWithValue("@id ", Session.userID);
                    updateCommand.Parameters.AddWithValue("@dark", Settings.darkMode);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand addCommand = new SqlCommand("INSERT INTO UserSettings (UserID , darkMode) VALUES(@id , @dark)", connection);
                    addCommand.Parameters.AddWithValue("@id", Session.userID);
                    addCommand.Parameters.AddWithValue("@dark", Settings.darkMode);
                    addCommand.ExecuteNonQuery();
                }
            }



        }

        private void QuizWordCount(object sender, EventArgs e)
        {
            int userID = Session.userID;
            int quizCountNew = (int)numericUpDownQuizWordCount.Value;

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM UserSettings WHERE UserID = @id", connection);
                checkCommand.Parameters.AddWithValue("@id", userID);

                int count = (int)checkCommand.ExecuteScalar();
                

                if(count > 0)
                {
                    SqlCommand updateCommand = new SqlCommand("UPDATE UserSettings SET QuizWordCount = @count WHERE UserID = @id" , connection);

                    updateCommand.Parameters.AddWithValue("@count", quizCountNew);
                    updateCommand.Parameters.AddWithValue("@id", userID);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO UserSettings (UserID , QuizWordCount) VALUES (@id , @count)" , connection);

                    insertCommand.Parameters.AddWithValue("@count", quizCountNew);
                    insertCommand.Parameters.AddWithValue("@id", userID);
                    insertCommand.ExecuteNonQuery();
                }
            }

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
