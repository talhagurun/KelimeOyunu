using System;

namespace KelimeOyunu
{
    partial class GameForm
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
            this.btn_KelimeEkle = new System.Windows.Forms.Button();
            this.btn_Sinav = new System.Windows.Forms.Button();
            this.Analiz = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_KelimeEkle
            // 
            this.btn_KelimeEkle.Location = new System.Drawing.Point(41, 27);
            this.btn_KelimeEkle.Name = "btn_KelimeEkle";
            this.btn_KelimeEkle.Size = new System.Drawing.Size(196, 104);
            this.btn_KelimeEkle.TabIndex = 0;
            this.btn_KelimeEkle.Text = "Kelime Ekle";
            this.btn_KelimeEkle.UseVisualStyleBackColor = true;
            this.btn_KelimeEkle.Click += new System.EventHandler(this.btn_KelimeEkle_Click);
            // 
            // btn_Sinav
            // 
            this.btn_Sinav.Location = new System.Drawing.Point(41, 187);
            this.btn_Sinav.Name = "btn_Sinav";
            this.btn_Sinav.Size = new System.Drawing.Size(196, 104);
            this.btn_Sinav.TabIndex = 1;
            this.btn_Sinav.Text = "Sınav";
            this.btn_Sinav.UseVisualStyleBackColor = true;
            this.btn_Sinav.Click += new System.EventHandler(this.btn_Sinav_Click);
            // 
            // Analiz
            // 
            this.Analiz.Location = new System.Drawing.Point(550, 187);
            this.Analiz.Name = "Analiz";
            this.Analiz.Size = new System.Drawing.Size(196, 104);
            this.Analiz.TabIndex = 2;
            this.Analiz.Text = "Analiz";
            this.Analiz.UseVisualStyleBackColor = true;
            this.Analiz.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Image = global::KelimeOyunu.Properties.Resources.icons8_setting_50;
            this.button1.Location = new System.Drawing.Point(645, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 95);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Analiz);
            this.Controls.Add(this.btn_Sinav);
            this.Controls.Add(this.btn_KelimeEkle);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.ResumeLayout(false);

        }

    

     

        #endregion

        private System.Windows.Forms.Button btn_KelimeEkle;
        private System.Windows.Forms.Button btn_Sinav;
        private System.Windows.Forms.Button Analiz;
        private System.Windows.Forms.Button button1;
    }
}