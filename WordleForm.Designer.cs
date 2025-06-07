namespace KelimeOyunu
{
    partial class WordleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnWordle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGuess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWordle
            // 
            this.btnWordle.Location = new System.Drawing.Point(290, 357);
            this.btnWordle.Name = "btnWordle";
            this.btnWordle.Size = new System.Drawing.Size(231, 81);
            this.btnWordle.TabIndex = 0;
            this.btnWordle.Text = "Oyunu Başlat";
            this.btnWordle.UseVisualStyleBackColor = true;
            this.btnWordle.Click += new System.EventHandler(this.btnWordle_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 285);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(326, 12);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(136, 48);
            this.btnGuess.TabIndex = 2;
            this.btnGuess.Text = "Tahmin Et";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // WordleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGuess);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnWordle);
            this.Name = "WordleForm";
            this.Text = "WordleForm";
            this.Load += new System.EventHandler(this.WordleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWordle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGuess;
    }
}