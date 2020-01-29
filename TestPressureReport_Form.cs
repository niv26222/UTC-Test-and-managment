using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlServerCe;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Project_Product_List
{
    public partial class TestPressureReport_Form : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        public TestPressureReport_Form()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void TestPressureReport_Form_Load(object sender, EventArgs e)
        {
            int TPRyear = TestDate1.Value.Year;

            string SavePath = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            LOAD_SERIAL_NUMBER_TO_TEXT_BOX();
            counterTestNumbers();
        }
        public void insertToDataBase()
        {

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("TestPressureReport_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@itemDescription", textBoxItemDescription.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@serialNumber", textBoxSerialNumber.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@TestDate1", TestDate1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue1", textBoxPressureValue1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime1", textBoxInitialTime1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime1", textBoxFinalTime1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime1", textBoxTotalTestTime1.Text.Trim());
                    if (checkBoxStatus1.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status1", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status1", "FAIL");
                    }

                    sqlCmd.Parameters.AddWithValue("@TestDate2", TestDate2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue2", textBoxPressureValue2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime2", textBoxInitialTime2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime2", textBoxFinalTime2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime2", textBoxTotalTestTime2.Text.Trim());
                    if (checkBoxStatus2.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status2", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status2", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate3", TestDate3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue3", textBoxPressureValue3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime3", textBoxInitialTime3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime3", textBoxFinalTime1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime3", textBoxTotalTestTime3.Text.Trim());
                    if (checkBoxStatus3.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status3", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status3", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate4", TestDate4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue4", textBoxPressureValue4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime4", textBoxInitialTime4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime4", textBoxFinalTime4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime4", textBoxTotalTestTime4.Text.Trim());
                    if (checkBoxStatus4.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status4", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status4", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate5", TestDate5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue5", textBoxPressureValue5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime5", textBoxInitialTime5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime5", textBoxFinalTime5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime5", textBoxTotalTestTime5.Text.Trim());
                    if (checkBoxStatus5.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status5", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status5", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@Comment", textBoxCOMMENT.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TesterName", textBoxTesterName.Text.Trim());



                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    MessageBox.Show("Successfully inserted into database");

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }




        }

        public void insertToTemporaryDataBase()
        {


            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("TestPressureReport_TEMPORARY_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@itemDescription", textBoxItemDescription.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@serialNumber", textBoxSerialNumber.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@TestDate1", TestDate1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue1", textBoxPressureValue1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime1", textBoxInitialTime1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime1", textBoxFinalTime1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime1", textBoxTotalTestTime1.Text.Trim());
                    if (checkBoxStatus1.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status1", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status1", "FAIL");
                    }

                    sqlCmd.Parameters.AddWithValue("@TestDate2", TestDate2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue2", textBoxPressureValue2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime2", textBoxInitialTime2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime2", textBoxFinalTime2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime2", textBoxTotalTestTime2.Text.Trim());
                    if (checkBoxStatus2.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status2", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status2", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate3", TestDate3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue3", textBoxPressureValue3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime3", textBoxInitialTime3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime3", textBoxFinalTime3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime3", textBoxTotalTestTime3.Text.Trim());
                    if (checkBoxStatus3.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status3", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status3", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate4", TestDate4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue4", textBoxPressureValue4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime4", textBoxInitialTime4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime4", textBoxFinalTime4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime4", textBoxTotalTestTime4.Text.Trim());
                    if (checkBoxStatus4.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status4", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status4", "FAIL");
                    }


                    sqlCmd.Parameters.AddWithValue("@TestDate5", TestDate5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PressureValue5", textBoxPressureValue5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InitialTime5", textBoxInitialTime5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FinalTime5", textBoxFinalTime5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TotalTestTime5", textBoxTotalTestTime5.Text.Trim());
                    if (checkBoxStatus5.Checked == true)
                    {
                        sqlCmd.Parameters.AddWithValue("@status5", "PASS");
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@status5", "FAIL");
                    }




                    sqlCmd.Parameters.AddWithValue("@Comment", textBoxCOMMENT.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TesterName", textBoxTesterName.Text.Trim());



                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }



        }


        public void counterTestNumbers()
        {
            textBoxTestNumber1.Text = " 1";
            textBoxTestNumber2.Text = " 2";
            textBoxTestNumber3.Text = " 3";
            textBoxTestNumber4.Text = " 4";
            textBoxTestNumber5.Text = " 5";
        }

        void clearFieldsAfterDone()
        {
            //clean all Fields again for new test
            foreach (Control c in Controls)
            {
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

        void FIX_PDF()
        {
            //fix all in the table
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (((TextBox)c).Text == "")
                    {
                        ((TextBox)c).Text = " ";
                    }
                }

                if (c is CheckBox)
                {
                    if (checkBoxStatus5.Checked == false && checkBox10.Checked == false)
                    {
                        checkBoxStatus5.Checked = false;
                        checkBox10.Checked = false;
                        
                    }
                    if (checkBoxStatus4.Checked == false && checkBox8.Checked == false)
                    {
                        checkBoxStatus4.Checked = false;
                        checkBox8.Checked = false;
                    }
                    if (checkBoxStatus3.Checked == false && checkBox6.Checked == false)
                    {
                        checkBoxStatus3.Checked = false;
                        checkBox6.Checked = false;
                    }
                    if (checkBoxStatus2.Checked == false && checkBox4.Checked == false)
                    {
                        checkBoxStatus2.Checked = false;
                        checkBox4.Checked = false;
                    }
                    if (checkBoxStatus1.Checked == false && checkBox2.Checked == false)
                    {
                        checkBoxStatus1.Checked = false;
                        checkBox2.Checked = false;
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }


        private void SendToPrinter_TPR()
        {
            int TPRyear = TestDate1.Value.Year;

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";

            string SavePath = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\";

            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry

            
            info.FileName = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\" + "TPR_" + textBoxSerialNumber.Text + "_R" + PDFcounter + ".pdf";
            //info.FileName = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + "RMA " + RMAyear + "_R" + PDFcounter + ".pdf";

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



        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "")
            {
                MessageBox.Show("Serial number must be writed ! ! ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    insertToTemporaryDataBase();
                    FIX_PDF();
                    insertToDataBase();
                    TPR_TO_PDF();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {
                            SendToPrinter_TPR();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }


                        
                    }
                    clearFieldsAfterDone();
                    MessageBox.Show(" Done successfully !");
                    clearFieldsAfterDone();
                    counterTestNumbers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Test Pressure Report document will be created, but it will not be stored in the Base database");
                    TPR_TO_PDF();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_TPR();
                        }
                        catch (Exception Exe)
                        {
                            MessageBox.Show(Exe.Message);
                        }
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }

        public void TPR_TO_PDF()
        {

            ///////////// Creating the document  - Designe only /////////////

            FontFactory.RegisterDirectories();


            int TPRyear = TestDate1.Value.Year;

            string SavePath = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            int TPRcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            SavePath = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\" + "TPR_" + textBoxSerialNumber.Text + "_R" + TPRcounter + ".pdf";

            while (File.Exists(SavePath))
            {
                TPRcounter += 1;
                SavePath = "U:" + @"\" + "Test Pressure Report" + @"\" + TPRyear + @"\" + "TPR_" + textBoxSerialNumber.Text + "_R" + TPRcounter + ".pdf";
            }


            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePath, FileMode.Create));


            doc.Open();


            /////////////////////////////////////////////////////


            // UTC LOGO 
            iTextSharp.text.Image logoJPG = iTextSharp.text.Image.GetInstance("UTC_LOGO.png");
            logoJPG.ScalePercent(10f);
            logoJPG.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);

            /////////////////////////////////////////////////////


            // Signature logo
            iTextSharp.text.Image SignaturePNG = iTextSharp.text.Image.GetInstance("SignatureutcPNG.png");
            SignaturePNG.ScalePercent(89f);
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 220f - 450f);


            /////////////////////////////////////////////////////




            // Down View

            iTextSharp.text.Image DownJPG = iTextSharp.text.Image.GetInstance("Down.png");
            DownJPG.ScalePercent(55f);
            DownJPG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 930f - 50f);

            ///////////////////////////////////////////////////////


            // Top View

            iTextSharp.text.Image TopJPG = iTextSharp.text.Image.GetInstance("Top.png");
            TopJPG.ScalePercent(55f);
            TopJPG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 270f - 50f);

            ///////////////////////////////////////////////////////



            Chunk headLine = new Chunk("Underwater Technologies Center Ltd", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLUE));
            Chunk Comment = new Chunk("Comment:\n" + textBoxCOMMENT.Text + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK));
            Chunk TesterName = new Chunk("Tester Name: " + textBoxTesterName.Text + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK));


            Chunk AddressParagraph = new Chunk("\n\n\n\n\n\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 84965.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            Paragraph line = new Paragraph("-------------------------------------------------------------------------------------------------------------------");

            Chunk rma = new Chunk("Test Pressure Report\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));


            Paragraph lineDown = new Paragraph("\n");

            //////////////////////   table1   ///////////////////////////////

            PdfPTable table1 = new PdfPTable(1);
            PdfPCell cell1 = new PdfPCell();
            cell1.Colspan = 1;
            cell1.HorizontalAlignment = 1;
            table1.AddCell(cell1);


            table1.AddCell("Item description: " + textBoxItemDescription.Text);

            table1.AddCell("Serial number: " + textBoxSerialNumber.Text);

            table1.WidthPercentage = 100f;

            ////////////////////// END table1   ///////////////////////////////




            //////////////////////   table2   ///////////////////////////////

            PdfPTable table2 = new PdfPTable(7);
            PdfPCell cell2 = new PdfPCell();
            cell2.BackgroundColor = new BaseColor(Color.Blue);///////////
            cell2.Colspan = 7;
            cell2.HorizontalAlignment = 7;
            table2.AddCell(cell2);

            table2.AddCell(" ");
            table2.AddCell("Test Date");
            table2.AddCell("Pressure Value");
            table2.AddCell("Initial Time");
            table2.AddCell("Final Time");
            table2.AddCell("Total Test Time");
            table2.AddCell("Status");

            

            if (textBoxPressureValue1.Text != " ")
            {
                table2.AddCell(textBoxTestNumber1.Text);
                table2.AddCell(TestDate1.Value.ToString("dd-MM-yyyy"));
                table2.AddCell(textBoxPressureValue1.Text);
                table2.AddCell(textBoxInitialTime1.Text);
                table2.AddCell(textBoxFinalTime1.Text);
                table2.AddCell(textBoxTotalTestTime1.Text);
                bool Status1 = checkBoxStatus1.Checked;
                if (Status1 == true)
                {
                    table2.AddCell("PASS");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }

            
            /////////////////////
            if (textBoxPressureValue2.Text != " ")
            {
                table2.AddCell(textBoxTestNumber2.Text);
                table2.AddCell(TestDate2.Value.ToString("dd-MM-yyyy"));
                table2.AddCell(textBoxPressureValue2.Text);
                table2.AddCell(textBoxInitialTime2.Text);
                table2.AddCell(textBoxFinalTime2.Text);
                table2.AddCell(textBoxTotalTestTime2.Text);
                bool Status2 = checkBoxStatus2.Checked;
                if (Status2 == true)
                {
                    table2.AddCell("PASS");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }

            /////////////////////
            
            if (textBoxPressureValue3.Text != " ")
            {
                table2.AddCell(textBoxTestNumber3.Text);
                table2.AddCell(TestDate3.Value.ToString("dd-MM-yyyy"));
                table2.AddCell(textBoxPressureValue3.Text);
                table2.AddCell(textBoxInitialTime3.Text);
                table2.AddCell(textBoxFinalTime3.Text);
                table2.AddCell(textBoxTotalTestTime3.Text);
                bool Status3 = checkBoxStatus3.Checked;
                if (Status3 == true)
                {
                    table2.AddCell("PASS");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }

            /////////////////////

            if (textBoxPressureValue4.Text != " ")
            {
                table2.AddCell(textBoxTestNumber4.Text);
                table2.AddCell(TestDate4.Value.ToString("dd-MM-yyyy"));
                table2.AddCell(textBoxPressureValue4.Text);
                table2.AddCell(textBoxInitialTime4.Text);
                table2.AddCell(textBoxFinalTime4.Text);
                table2.AddCell(textBoxTotalTestTime4.Text);
                bool Status4 = checkBoxStatus4.Checked;
                if (Status4 == true)
                {
                    table2.AddCell("PASS");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }

            /////////////////////

            
            if (textBoxPressureValue5.Text != " ")
            {
                table2.AddCell(textBoxTestNumber5.Text);
                table2.AddCell(TestDate5.Value.ToString("dd-MM-yyyy"));
                table2.AddCell(textBoxPressureValue5.Text);
                table2.AddCell(textBoxInitialTime5.Text);
                table2.AddCell(textBoxFinalTime5.Text);
                table2.AddCell(textBoxTotalTestTime5.Text);
                bool Status5 = checkBoxStatus5.Checked;
                if (Status5 == true)
                {
                    table2.AddCell("PASS");
                }
                else
                {
                    table2.AddCell("FAIL");
                }

            }

            /////////////////////



            if (textBoxPressureValue1.Text == "")
            {
                TestDate1.Text = "";
                textBoxTestNumber1.Text = "";
                checkBoxStatus1.Text = "";
            }
            if (textBoxPressureValue2.Text == "")
            {
                TestDate2.Text = "";
                textBoxTestNumber2.Text = "";
                checkBoxStatus2.Text = "";
            }
            if (textBoxPressureValue3.Text == "")
            {
                TestDate3.Text = "";
                textBoxTestNumber3.Text = "";
                checkBoxStatus3.Text = "";
            }
            if (textBoxPressureValue4.Text == "")
            {
                TestDate4.Text = "";
                textBoxTestNumber4.Text = "";
                checkBoxStatus4.Text = "";
            }
            if (textBoxPressureValue5.Text == "")
            {
                TestDate5.Text = "";
                textBoxTestNumber5.Text = "";
                checkBoxStatus5.Text = "";
            }

            table2.WidthPercentage = 100f;


            ////////////////////// END table2   ///////////////////////////////


            ////////////////////// Page design  //////////////////////

           

            doc.Add(DownJPG);
            doc.Add(TopJPG);
            doc.Add(logoJPG);
            doc.Add(lineDown);
            doc.Add(headLine);
            doc.Add(line);
            doc.Add(rma);
            doc.Add(lineDown);
            doc.Add(table1);
            doc.Add(lineDown);
            doc.Add(table2);
            doc.Add(lineDown);
            doc.Add(Comment);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(TesterName);
            doc.Add(SignaturePNG);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(lineDown);
            doc.Add(AddressParagraph);

            doc.Close();
            MessageBox.Show("PDF file created successfully");







        }

        public void restoreItemDescription()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT itemDescription FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxItemDescription.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSerialNumber()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT serialNumber FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSerialNumber.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreTestDate1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TestDate1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                TestDate1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePressureValue1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PressureValue1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPressureValue1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInitialTime1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT InitialTime1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxInitialTime1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFinalTime1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FinalTime1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxFinalTime1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTotalTestTime1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TotalTestTime1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTotalTestTime1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreStatus1()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT status1 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd[0].ToString() == "PASS")
                {
                    checkBoxStatus1.Checked = true;
                }

                if (rd[0].ToString() == "FAIL")
                {
                    checkBox2.Checked = true;
                }
                
            }
            con.Close();
        }

        

        // // // // // // // // //

        public void restoreTestDate2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TestDate2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                TestDate2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePressureValue2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PressureValue2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPressureValue2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInitialTime2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT InitialTime2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxInitialTime2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFinalTime2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FinalTime2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxFinalTime2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTotalTestTime2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TotalTestTime2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTotalTestTime2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreStatus2()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT status2 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd[0].ToString() == "PASS")
                {
                    checkBoxStatus2.Checked = true;
                }

                if (rd[0].ToString() == "FAIL")
                {
                    checkBox4.Checked = true;
                }

            }
            con.Close();
        }

        

        // // // // // // // // //

        public void restoreTestDate3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TestDate3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                TestDate3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePressureValue3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PressureValue3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPressureValue3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInitialTime3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT InitialTime3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxInitialTime3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFinalTime3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FinalTime3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxFinalTime3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTotalTestTime3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TotalTestTime3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTotalTestTime3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreStatus3()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT status3 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd[0].ToString() == "PASS")
                {
                    checkBoxStatus3.Checked = true;
                }

                if (rd[0].ToString() == "FAIL")
                {
                    checkBox6.Checked = true;
                }

            }
            con.Close();
        }

        

        // // // // // // // // //

        public void restoreTestDate4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TestDate4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                TestDate4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePressureValue4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PressureValue4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPressureValue4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInitialTime4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT InitialTime4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxInitialTime4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFinalTime4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FinalTime4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxFinalTime4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTotalTestTime4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TotalTestTime4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTotalTestTime4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreStatus4()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT status4 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd[0].ToString() == "PASS")
                {
                    checkBoxStatus4.Checked = true;
                }

                if (rd[0].ToString() == "FAIL")
                {
                    checkBox8.Checked = true;
                }

            }
            con.Close();
        }

        

        // // // // // // // // //

        public void restoreTestDate5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TestDate5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                TestDate5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePressureValue5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PressureValue5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPressureValue5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInitialTime5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT InitialTime5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxInitialTime5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFinalTime5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FinalTime5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxFinalTime5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTotalTestTime5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TotalTestTime5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTotalTestTime5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreStatus5()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT status5 FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd[0].ToString() == "PASS")
                {
                    checkBoxStatus5.Checked = true;
                }

                else if (rd[0].ToString() == "FAIL")
                {
                    checkBox10.Checked = true;
                }

            }
            con.Close();
        }

        

        // // // // // // // // //


        public void restoreCOMMENT()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Comment FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxCOMMENT.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTesterName()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TesterName FROM [TestPressureReport_TEMPORARY_dt] WHERE serialNumber = '" + PTR_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTesterName.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void Delete_Previous_Data_From_DataBase()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string PTR = textBoxSerialNumber.Text.Trim();

            cmd.CommandText = "DELETE FROM TestPressureReport_TEMPORARY_dt WHERE serialNumber = '" + PTR + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
        }

        public void Restore_data_from_temporary_data_base()
        {

            
            restoreItemDescription();
            restoreSerialNumber();

            restorePressureValue1();
            restoreInitialTime1();
            restoreFinalTime1();
            restoreTotalTestTime1();
            restoreStatus1();

            restorePressureValue2();
            restoreInitialTime2();
            restoreFinalTime2();
            restoreTotalTestTime2();
            restoreStatus2();

            restorePressureValue3();
            restoreInitialTime3();
            restoreFinalTime3();
            restoreTotalTestTime3();
            restoreStatus3();

            restorePressureValue4();
            restoreInitialTime4();
            restoreFinalTime4();
            restoreTotalTestTime4();
            restoreStatus4();

            restorePressureValue5();
            restoreInitialTime5();
            restoreFinalTime5();
            restoreTotalTestTime5();
            restoreStatus5();

            restoreCOMMENT();
            restoreTesterName();

            restoreTestDate1();
            restoreTestDate2();
            restoreTestDate3();
            restoreTestDate4();
            restoreTestDate5();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //to do
            Restore_data_from_temporary_data_base();
        }

        public void insertToComboBox()
        {
            if (!(textBoxSerialNumber.Items.Contains(textBoxSerialNumber.Text)))
            {
            textBoxSerialNumber.Items.Add(textBoxSerialNumber.Text);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "")
            {
                MessageBox.Show("Serial number must be writed ! ! ! ");
            }
            else
            {
            Delete_Previous_Data_From_DataBase();
            insertToTemporaryDataBase();
            MessageBox.Show("Saved successfully");
            clearFieldsAfterDone();
            counterTestNumbers();
            }

        }

        public void LOAD_SERIAL_NUMBER_TO_TEXT_BOX()
        {
            string PTR_NUMBER = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SerialNumber1 FROM[RMA_dt] WHERE deviceBeenFixed = '" + 0 + "'; ";

            // "SELECT SerialNumber1 FROM[RMA_dt] WHERE isArrived = '" + 0 + "'; ";

            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBoxSerialNumber.Items.Add(rd[0].ToString());
            }
        }


        private void textBoxSerialNumber_SelectedIndexChanged(object sender, EventArgs e)
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
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\TPR.docx";
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "")
            {
                MessageBox.Show("Serial number must be writed ! ! ! ");
            }
            else
            {
                Delete_Previous_Data_From_DataBase();
                insertToTemporaryDataBase();
                MessageBox.Show("Saved successfully");
                clearFieldsAfterDone();
                counterTestNumbers();
            }
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "")
            {
                MessageBox.Show("Serial number must be writed ! ! ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    insertToTemporaryDataBase();
                    FIX_PDF();
                    insertToDataBase();
                    TPR_TO_PDF();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {
                            SendToPrinter_TPR();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }



                    }
                    clearFieldsAfterDone();
                    MessageBox.Show(" Done successfully !");
                    clearFieldsAfterDone();
                    counterTestNumbers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Test Pressure Report document will be created, but it will not be stored in the Base database");
                    TPR_TO_PDF();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_TPR();
                        }
                        catch (Exception Exe)
                        {
                            MessageBox.Show(Exe.Message);
                        }
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }
    }
}
