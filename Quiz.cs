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
    public partial class Quiz : Form
    {

        private List<Words> wordList = new List<Words> ();
        private int questionIndex;

        
        public Quiz()
        {
            InitializeComponent();
            UploadWords();
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
        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblQuestion_Click(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Text.Trim().ToLower();
            string correctAnswer = wordList[questionIndex].wordTextTurkish.ToLower();

            if(answer == correctAnswer)
            {
                MessageBox.Show("Doğru cevap!!");

                using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("UpdateProgressOnCorrectAnswer", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", Session.userID);
                    command.Parameters.AddWithValue("@WordID", wordList[questionIndex].WordID);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show($"Yanlış cevap! Doğru cevap : {correctAnswer}");

                using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
                {
                    connection.Open();

                    SqlCommand resetCorrectCountCommand = new SqlCommand(@"
                    UPDATE UserWordProgress
                    SET CorrectCount = 0,
                        LastCorrectDate = NULL,
                        NextRepeatDate = NULL
                    WHERE UserID = @UserID AND WordID = @WordID" , connection);

                    resetCorrectCountCommand.Parameters.AddWithValue("@UserID", Session.userID);
                    resetCorrectCountCommand.Parameters.AddWithValue("@WordID", wordList[questionIndex].WordID);

                    resetCorrectCountCommand.ExecuteNonQuery();
                }

            }

            questionIndex++;
            AskQuestion();
        }

        private void btnNextQuestion(object sender, EventArgs e)
        {
            if(questionIndex < wordList.Count - 1)
            {
                questionIndex++;
                AskQuestion();
            }

        }

        private void btnPreviousQuestion(object sender, EventArgs e)
        {
            if(questionIndex > 0)
            {
                questionIndex--;
                AskQuestion();
            }
        }

        private void UploadWords()
        {
            int userID = Session.userID;
            int quizWordCount = 10;

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand settingsCommand = new SqlCommand("SELECT QuizWordCount FROM UserSettings WHERE UserID = @id", connection);
                settingsCommand.Parameters.AddWithValue("@id", userID);

                object result = settingsCommand.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    quizWordCount = Convert.ToInt32(result);
                }

                SqlCommand quizCommand = new SqlCommand("GetQuizWords", connection);
                quizCommand.CommandType = CommandType.StoredProcedure;
                quizCommand.Parameters.AddWithValue("@userID", userID);
                quizCommand.Parameters.AddWithValue("@QuizWordCount", quizWordCount);

                SqlDataReader reader = quizCommand.ExecuteReader();
                if (!reader.HasRows)
                {
                    GameForm gameForm = new GameForm();
                    MessageBox.Show("Sınav için uygun kelime bulunamadı. Veritabanını kontrol et.");
                    this.Close();
                    gameForm.Show();

                    return;
                }


                while (reader.Read())
                {
                    Words word = new Words()
                    {
                        WordID = Convert.ToInt32(reader["WordID"]),
                        wordTextEnglish = reader["EnglishWordName"].ToString(),
                        wordTextTurkish = reader["TurkishWordName"].ToString()
                    };
                    wordList.Add(word);
                }

                if (wordList.Count > 0)
                {
                    questionIndex = 0;
                    AskQuestion();
                }
            }
        }

        private void AskQuestion()
        {
            if (questionIndex < wordList.Count)
            {
                lblQuestion.Text = $"Bu kelimenin Türkçesi nedir : {wordList[questionIndex].wordTextEnglish}";
                txtAnswer.Text = "";

                txtAnswer.Focus();
            }
            else
            {
                GameForm gameForm = new GameForm();
                MessageBox.Show("Tüm sorular bitti");
                this.Close();
                
            }
        }
    }
}
