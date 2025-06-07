using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;

namespace KelimeOyunu
{

    public partial class AnalysisForm : Form
    {
        private List<String> topicNames = new List<string>();

        public AnalysisForm()
        {
            InitializeComponent();
            
        }

        private void AnalysisForm_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.CustomLabels.Clear();

            Series series = new Series("Başarı");

            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            chart1.Series.Add(series);

            topicNames.Clear();

            using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
            {
                connection.Open();

                SqlCommand successRateCommand = new SqlCommand("GetSuccessRate", connection);
                successRateCommand.CommandType = CommandType.StoredProcedure;
                successRateCommand.Parameters.AddWithValue("@UserID", Session.userID);

                using (SqlDataReader reader = successRateCommand.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        string topicName = reader["Topic"].ToString();
                        double successRate = Convert.ToDouble(reader["SuccessRate"]);

                        topicNames.Add(topicName);
                        chart1.ChartAreas[0].AxisX.CustomLabels.Add(
                            new CustomLabel(i + 0.5, i + 1.5, topicName, 0, LabelMarkStyle.None));

                        series.Points.AddXY(i + 1, successRate);
                        i++;
                    }
                }

            }


        }

        private void ExportPdf()
        {
            string imagePath = Path.Combine(Path.GetTempPath(), "chart_temp.png");
            chart1.SaveImage(imagePath, ChartImageFormat.Png);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "PDF Olarak Kaydet";
            saveFileDialog.FileName = "BaşarıRaporu.pdf";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                using (SqlConnection connection = DatabaseConnect.BaglantiOlustur())
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("GetSuccessRate", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", Session.userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        Document doc = new Document();
                        PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        doc.Open();

                        Paragraph title = new Paragraph("Konu Bazli Basari Raporu");
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;
                        doc.Add(title);

                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;
                        table.AddCell("Konu");
                        table.AddCell("Dogru");
                        table.AddCell("Yanlis");
                        table.AddCell("Basari Orani %");

                        while (reader.Read())
                        {
                            string topic = reader["Topic"].ToString();
                            string correct = reader["TotalCorrect"].ToString();
                            string wrong = reader["TotalWrong"].ToString();
                            string rate = Convert.ToDouble(reader["SuccessRate"]).ToString("0.00");

                            table.AddCell(topic);
                            table.AddCell(correct);
                            table.AddCell(wrong);
                            table.AddCell(rate);
                        }

                        iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(imagePath);
                        chartImage.Alignment = Element.ALIGN_CENTER;
                        chartImage.ScaleToFit(500f, 400f);


                        doc.Add(chartImage);
                        doc.Add(table);
                        doc.Close();

                        MessageBox.Show("PDF Başarıyla oluşturuldu.", "Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportPdf();
        }
    }
}
