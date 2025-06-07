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
    public partial class WordleForm : Form
    {
        private List<TextBox[]> allGuess = new List<TextBox[]>();
        string word = "";
        int guessCount = 0;
        int maxGuessCount = 5;

        private void getWord()
        {
            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand getWordCommand = new SqlCommand(@"SELECT TOP 1 w.EnglishWordName
                                                             FROM LearnedWords lw
                                                             INNER JOIN Words w ON w.WordID = lw.WordID
                                                             WHERE lw.UserID = @UserID
                                                             ORDER BY NEWID();" , connection);

                getWordCommand.Parameters.AddWithValue("@UserID", Session.userID);

                SqlDataReader reader = getWordCommand.ExecuteReader();

                if (reader.Read())
                {
                    word = reader.GetString(0).ToLower();
                }
            }
        }
        public WordleForm()
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

        private void WordleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnWordle_Click(object sender, EventArgs e)
        {

            getWord();
            btnWordle.Visible = false;
            panel1.Controls.Clear();

            int wordLenght = word.Length;

            List<TextBox> guessBox = new List<TextBox>();

            for (int i = 0; i < wordLenght; i++)
            {
                TextBox box = new TextBox();
                box.Width = 40;
                box.Height = 40;
                box.Font = new Font("Arial", 20, FontStyle.Bold);
                box.MaxLength = 1;
                box.TextAlign = HorizontalAlignment.Center;
                box.Location = new Point(10 + i * 45, 10);

                panel1.Controls.Add(box);
                guessBox.Add(box);
            }
            allGuess.Add(guessBox.ToArray());
            guessCount = 1;

            
        }

        private void addGuessRow()
        {
            if(guessCount >= maxGuessCount)
            {
                MessageBox.Show($"Hakkınız Bitti. Doğru Kelime : {word}");
                return;
            }

            int wordLenght = word.Length;
            TextBox[] boxes = new TextBox[wordLenght];
            int y = 10 + guessCount * 50;

            for(int i = 0; i < wordLenght; i++)
            {
                TextBox textBox = new TextBox();
                textBox.Width = 40;
                textBox.Height = 40;
                textBox.Font = new Font("Arial", 20, FontStyle.Bold);
                textBox.MaxLength = 1;
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.Location = new Point(10 + i * 45, y);
                panel1.Controls.Add (textBox);
                boxes[i] = textBox;
            }

            allGuess.Add(boxes);
            guessCount++;
        }

        private void CheckGuess(TextBox[] boxes)
        {

            if (boxes.Any(b => string.IsNullOrWhiteSpace(b.Text)))
            {
                MessageBox.Show("Tüm kutuları doldur.");
                return;
            }

            string guess = string.Concat(boxes.Select(b => b.Text.ToLower()));
            if(guess.Length != word.Length)
            {
                MessageBox.Show("Tüm kutuları doldur.");
                return;
            }

            char[] wordChar = word.ToCharArray();
            bool[] matched = new bool[word.Length];

            for(int i = 0; i < word.Length; i++)
            {
                if (guess[i] == word[i])
                {
                    boxes[i].BackColor = Color.LightGreen;
                    matched[i] = true;
                    wordChar[i] = '*';
                }
            }

            for(int i = 0; i < word.Length; i++)
            {
                if (boxes[i].BackColor == Color.LightGreen)
                    continue;

                int index = Array.IndexOf(wordChar, guess[i]);
                if(index >= 0)
                {
                    boxes[i].BackColor = Color.Gold;
                    wordChar[index] = '*';
                }
                else
                {
                    boxes[i].BackColor = Color.Red;
                }

            }
            if (guess == word)
            {
                MessageBox.Show("Tahmin Doğru. Tebrikler!!");
            }
            else if (guessCount < maxGuessCount)
            {
                addGuessRow();
            }
            else
            {
                MessageBox.Show($"Tahmin Hakkın Bitti. Doğru Cevap : {word} ");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            TextBox[] currentGuess = allGuess.Last();
            CheckGuess(currentGuess);
        }
    }
}
