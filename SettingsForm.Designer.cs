namespace KelimeOyunu
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxDarkMode = new System.Windows.Forms.CheckBox();
            this.numericUpDownQuizWordCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuizWordCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kelime Sayısı";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBoxDarkMode
            // 
            this.checkBoxDarkMode.AutoSize = true;
            this.checkBoxDarkMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDarkMode.Location = new System.Drawing.Point(124, 193);
            this.checkBoxDarkMode.Name = "checkBoxDarkMode";
            this.checkBoxDarkMode.Size = new System.Drawing.Size(202, 36);
            this.checkBoxDarkMode.TabIndex = 2;
            this.checkBoxDarkMode.Text = "Karanlık Mod";
            this.checkBoxDarkMode.UseVisualStyleBackColor = true;
            this.checkBoxDarkMode.CheckedChanged += new System.EventHandler(this.checkBoxDarkMode_CheckedChanged);
            // 
            // numericUpDownQuizWordCount
            // 
            this.numericUpDownQuizWordCount.Location = new System.Drawing.Point(324, 139);
            this.numericUpDownQuizWordCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQuizWordCount.Name = "numericUpDownQuizWordCount";
            this.numericUpDownQuizWordCount.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownQuizWordCount.TabIndex = 4;
            this.numericUpDownQuizWordCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownQuizWordCount.ValueChanged += new System.EventHandler(this.QuizWordCount);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericUpDownQuizWordCount);
            this.Controls.Add(this.checkBoxDarkMode);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuizWordCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDarkMode;
        private System.Windows.Forms.NumericUpDown numericUpDownQuizWordCount;
    }
}