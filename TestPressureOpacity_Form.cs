using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Configuration;
using System.Data.SQLite;
using Dapper;

namespace Project_Product_List
{


    public partial class TestPressureOpacity_Form : Form
    {
        string SavePath = Paths.Paths.TEST_PRESSURE_OPACITY_PATH;

        public TestPressureOpacity_Form()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        public void Load_Test_Pressure_Opacity_Name_on_Load()/// 
        {
            int TPOyear = dateTimePicker1.Value.Year;
            int counter = Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length;
            textBoxItemDescription.Text = "TPO_" + textBox8.Text  + "_R" + (counter + 1);
        }


        private void TestPressureOpacity_Form_Load(object sender, EventArgs e)
        {
            int TPOyear = DateTime.Now.Year;


            if (!Directory.Exists(SavePath))
            {
                try
                {
                    Directory.CreateDirectory(SavePath);
                }
                catch
                {
                    // Bring up a dialog to chose a folder path in which to open or save a file.
                    var folderBrowserDialog1 = new FolderBrowserDialog();

                    // Show the FolderBrowserDialog.
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        SavePath = folderBrowserDialog1.SelectedPath;
                    }
                }
            }

            Load_Test_Pressure_Opacity_Name_on_Load();
            

        }


        
        private void button3_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public void InsertToDataBase()
        {
            try
            {

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string result;

                    int TPOyear = dateTimePicker1.Value.Year;



                    int TPOcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


                    string numTest = "TPO_" + textBox8.Text + "_R" + TPOcounter;


                    if (checkBox1.Checked)
                    {
                        result = "PASS";
                    }
                    else
                    {
                        result = "FAIL";
                    }

                    cnn.Execute("insert into TEST_PRESSURE_OPACITY (DateCreate, DateEnd, NumOfTest, ValueUnderPressure, InitialTime, FinalTime, TotalTestTime, ItemDescription, Status, Comments, TesterName) values ('" + dateTimePicker1.Text.Trim() + "', '" + dateTimePicker2.Text.Trim() + "', '" + numTest + "', '" + textBox2.Text.Trim() + "', '" + textBox3.Text.Trim() + "', '" + textBox4.Text.Trim() + "', '" + textBox5.Text.Trim() + "', '" + textBox9.Text.Trim() + "', '" + result + "', '" + textBox6.Text.Trim() + "', '" + textBox10.Text.Trim() + "' )");


                    MessageBox.Show("Successfully inserted into database");
                }

            }

            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }


        public void clearFieldsAfterDone()
        {
            foreach (Control c in Controls)
            {
                //clean all Fields again for new test
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).Text = "";
                }
            }
        }

        public void PDFCreator()
        {

            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();

            int CURRENT_YEAR = DateTime.Now.Year;
            int TPOyear = dateTimePicker1.Value.Year;


            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            int TPOcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            string SavePathNew = SavePath + "TPO_" + textBox8.Text + "_R" + TPOcounter + ".pdf";


            while (File.Exists(SavePathNew))
            {
                TPOcounter += 1;
                SavePathNew = SavePath + "TPO_" + textBox8.Text + "_R" + TPOcounter + ".pdf";
            }

            Document doc = new Document(iTextSharp.text.PageSize.A4);

            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePathNew, FileMode.Create));


            doc.Open();


            ///////////////////////////////////////////////////////

            // UTC LOGO 
            iTextSharp.text.Image logoPNG = iTextSharp.text.Image.GetInstance("UTC_LOGO.png");
            logoPNG.ScalePercent(10f);
            logoPNG.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);

            ///////////////////////////////////////////////////////


            // Signature logo
            iTextSharp.text.Image SignaturePNG = iTextSharp.text.Image.GetInstance("SignatureutcPNG.png");
            SignaturePNG.ScalePercent(89f);
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 250f - 450f);


            /////////////////////////////////////////////////////




            // Down View

            iTextSharp.text.Image DownPNG = iTextSharp.text.Image.GetInstance("Down.png");
            DownPNG.ScalePercent(55f);
            DownPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 930f - 50f);

            ///////////////////////////////////////////////////////


            // Top View

            iTextSharp.text.Image TopPNG = iTextSharp.text.Image.GetInstance("Top.png");
            TopPNG.ScalePercent(55f);
            TopPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 270f - 50f);

            ///////////////////////////////////////////////////////



            Chunk headLine = new Chunk("Underwater Technologies Center Ltd", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLUE));
            //headLine.SetUnderline(0.5f, -1.5f);



            Paragraph CNParagraph = new Paragraph("C/N 513369199");
            Chunk AddressWithLine = new Chunk("Address:\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            AddressWithLine.SetUnderline(0.5f, -1.5f);

            Paragraph Note = new Paragraph("The Value is for customs purposes only\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));


            Chunk AddressParagraph = new Chunk("\n\n\n\n\n\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 84965.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
                                                                
            Paragraph line = new Paragraph("-------------------------------------------------------------------------------------------------------------------");

            Chunk TOP = new Chunk("Test Pressure Opacity\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.RED));
            TOP.SetUnderline(0.5f, -1.5f);

            Paragraph lineDown = new Paragraph("\n");


            Chunk Name = new Chunk("Tester Name: " + textBox10.Text + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));

            Chunk TesENDtDate = new Chunk("Test End Date: " + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));

            Chunk TestDate = new Chunk("Test Start Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "\n" + TesENDtDate + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));

            Chunk TestNUm = new Chunk("Test Number: " + SavePath + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));

            Chunk TestData = new Chunk("Test Data: \n" , FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            Chunk Data_for_the_item_being_tested = new Chunk("Data for the item being tested: \n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));



            Chunk Comments = new Chunk("Comments: \n" + textBox6.Text  + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));

            
            //////////////////////   table1   ///////////////////////////////

            PdfPTable table1 = new PdfPTable(4);
            PdfPCell cell1 = new PdfPCell();
            cell1.Colspan = 5;
            cell1.HorizontalAlignment = 1;
            table1.AddCell(cell1);
            table1.SetWidths(new int[] {5, 5, 5, 5 });



            table1.AddCell("Value Under Pressure");
            table1.AddCell("Initial Time");
            table1.AddCell("Final Time");
            table1.AddCell("Total Test Time");


            table1.AddCell(textBox2.Text);
            table1.AddCell(textBox3.Text);
            table1.AddCell(textBox4.Text);
            table1.AddCell("  " + textBox5.Text);



            ////////////////////// END table1   ///////////////////////////////

            //////////////////////   table2   ///////////////////////////////

            PdfPTable table2 = new PdfPTable(3);
            PdfPCell cell2 = new PdfPCell();
            cell2.Colspan = 5;
            cell2.HorizontalAlignment = 1;
            table2.AddCell(cell2);
            table2.SetWidths(new int[] {5, 5, 5 });


            table2.AddCell("Item Description");
            table2.AddCell("Serial"); 
            table2.AddCell("Status");

            table2.AddCell(textBox9.Text);
            table2.AddCell(textBox8.Text);

            if (checkBox1.Checked == true)
            {

                table2.AddCell("Pass");
                
            }
            else
            { 
                table2.AddCell("Fail");
            }


            ////////////////////// END table2   ///////////////////////////////


            ////////////////////// Page design  //////////////////////
            
            doc.Add(headLine);
            doc.Add(lineDown);
            doc.Add(TOP);
            doc.Add(lineDown);
            doc.Add(Name);
            doc.Add(TestDate);
            doc.Add(lineDown);
            doc.Add(line);
            doc.Add(lineDown);
            doc.Add(TestData);
            doc.Add(lineDown);
            doc.Add(table1);
            doc.Add(lineDown);
            doc.Add(Data_for_the_item_being_tested);
            doc.Add(table2);
            doc.Add(lineDown);
            doc.Add(Comments);
            doc.Add(DownPNG);
            doc.Add(TopPNG);
            doc.Add(logoPNG);
            doc.Add(SignaturePNG);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(AddressParagraph);

            ////////////////////// END Page design  //////////////////////


            doc.Close();
            MessageBox.Show("PDF file created successfully");
        }

        private void SendToPrinter_TPO()
        {
            int TPOyear = dateTimePicker1.Value.Year;

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";

            
            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry

            info.FileName = SavePath + "TPO_" + textBox8.Text + "_R" + PDFcounter + ".pdf";

            //MessageBox.Show(SavePath + "\n" + info.FileName);


            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            long ticks = -1;
            while (ticks != p.TotalProcessorTime.Ticks)
            {
                ticks = p.TotalProcessorTime.Ticks;
                Thread.Sleep(1000);
            }

            if (false == p.CloseMainWindow())
            {
                p.Kill();
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Please Fill the Status checkBox");
            }
            else
            {
                try
                {

                    InsertToDataBase();
                    PDFCreator();
                    
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_TPO();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                    }
                    clearFieldsAfterDone();
                    Load_Test_Pressure_Opacity_Name_on_Load();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Test Pressure Opacity document will be created, but it will not be stored in the Base database");
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_TPO();
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string MyDirectory()
        {
            //MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = Paths.Paths.TEST_PRESSURE_OPACITY_HELP_FILE;


            p.StartInfo = pi;

            try
            {
                p.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Please Fill the Status checkBox");

            }
            else
            {
                try
                {
                    InsertToDataBase();

                    PDFCreator();

                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        try
                        {
                            SendToPrinter_TPO();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);


                        }
                    }
                    //clearFieldsAfterDone();
                    Load_Test_Pressure_Opacity_Name_on_Load();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Test Pressure Opacity document will be created, but it will not be stored in the Base database");
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_TPO();
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }
    }
}
