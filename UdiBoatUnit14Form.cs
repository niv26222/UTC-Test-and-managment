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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Project_Product_List
{
    public partial class UdiBoatUnit14Form : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;


        void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox

            SqlConnection sqlConnection1 = new SqlConnection(@"Data Source=ORLYPC\SQLEXPRESS;Initial Catalog = UTCTest; Integrated Security = True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string CustomerName = comboBox1.Text.Trim();

            cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();

        }


        public UdiBoatUnit14Form()
        {
            InitializeComponent();
        }


        public void UpdateDataGrid()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Boat14_dt] ; ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
            }
        }

        public void InsertIntoData()
        {
            int counter = 1;



            try
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("Boat14_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@Number", counter);
                    sqlCmd.Parameters.AddWithValue("@Seriel_Code", textBoxUdiSerielNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Screen", checkBoxScreenOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Antena", checkBoxAntenaOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Plastic", checkBoxPlasticOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Screw", checkBoxScrewOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Button_function", checkBoxButtonFunctionOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Cover", checkBoxCoverOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Message_to_all_users", checkBoxMessageToAllUsersOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Message_between_2_udi_14_wrist", checkBoxMessageBetween2UDI14WristOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@SOS_message", checkBoxSOSMessageOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Compass", checkBoxCompassOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@PC_Connection", checkBoxPCConnectionToDiveSimOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Load_Messages", checkBoxLoadMessageOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Load_Names", checkBoxLoadNamesOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Update_udi_ver", textBoxUpdateUDI.Text.Trim());////
                    sqlCmd.Parameters.AddWithValue("@Date_Time", checkBoxDateTimeOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Charging", checkBoxChargingLightOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Quick_connector", checkBoxQuickConnectorOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@USBcable", checkBoxUSBCableOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Power_Supply", checkBoxPowerSupplyOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Case_", checkBoxCaseOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Cover_color", checkBoxCoverColorOK.Checked);
                    sqlCmd.Parameters.AddWithValue("@Date", dateTimePicker1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Tested_by", textBoxTestedBy.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Signature", textBoxSignature.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_Name", comboBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@WaybillNumber", textBoxWaybillNumber.Text.Trim());


                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    MessageBox.Show("Done Successfully !");
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }



        }


        private void SendToPrinter_Boat14()
        {
            int DOCyear = dateTimePicker1.Value.Year;

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";

            string SavePath = "U:" + @"\" + "Acceptance Testing" + @"\" + DOCyear + @"\" + "BOAT UNIT 14" + @"\";

            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry

            info.FileName = "U:" + @"\" + "Acceptance Testing" + @"\" + DOCyear + @"\" + "BOAT UNIT 14" + @"\" + textBoxUdiSerielNumber.Text + "_AT" + PDFcounter + ".pdf";

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


            int TestedBy = textBoxTestedBy.Text.Length;
            int Signature = textBoxSignature.Text.Length;

            if (textBoxUdiSerielNumber.Text == "")
            {
                MessageBox.Show(" UDI S/N must be writed ! ");

            }

            else if (TestedBy > 15)
            {
                MessageBox.Show(" Tested By Can not be more than 15 Characters");
            }

            else if (Signature > 15)
            {
                MessageBox.Show(" Signature Can not be more than 15 Characters");
            }
            else
            {
                try
                {
                    InsertIntoData();
                    CreateInspectionTestDocument();
                    UpdateDataGrid();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        try
                        {
                            SendToPrinter_Boat14();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                    }
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so AT document will be created, but it will not be stored in the Base database");
                    CreateInspectionTestDocument();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_Boat14();
                        }
                        catch (Exception Exe)
                        {
                            MessageBox.Show(Exe.Message);
                        }
                    }
                    MessageBox.Show("Successfully Done !");
                    clearFieldsAfterDone();
                }
            }

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
        private void UdiBoatUnit14Form_Load(object sender, EventArgs e)
        {

            int DOCyear = dateTimePicker1.Value.Year;

            string SavePath = "U:" + @"\" + "Acceptance Testing" + @"\" + DOCyear + @"\" + "BOAT UNIT 14" + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            Load_Customers_To_ComboBox();
            UpdateDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChooseTestForm().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void CreateInspectionTestDocument()
        {

            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();

            int DOCyear = dateTimePicker1.Value.Year;

            string SavePath = "U:" + @"\" + "Acceptance Testing" + @"\" + DOCyear + @"\" + "BOAT UNIT 14" + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            SavePath = "U:" + @"\" + "Acceptance Testing" + @"\" + DOCyear + @"\" + "BOAT UNIT 14" + @"\" + textBoxUdiSerielNumber.Text + "_AT" + PDFcounter + ".pdf";



            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePath, FileMode.Create));


            doc.Open();


            ///////////////////////////////////////////////////////


            // UTC LOGO 
            iTextSharp.text.Image logoPNG = iTextSharp.text.Image.GetInstance("UTC_LOGO.png");
            logoPNG.ScalePercent(10f);
            logoPNG.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);

            ///////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////

            // Signature logo
            iTextSharp.text.Image SignaturePNG = iTextSharp.text.Image.GetInstance("SignatureutcPNG.png");
            SignaturePNG.ScalePercent(75f);
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 250f - 550f);

            ///////////////////////////////////////////////////////




            Chunk headLine = new Chunk("Underwater Technologies Center Ltd.\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE));
            Chunk productName = new Chunk("BOAT UNIT 14 Inspection.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE));

            Paragraph lineDown = new Paragraph("\n");
            Chunk SerialNumberAndDate = new Chunk("              UDI S/N: " + textBoxUdiSerielNumber.Text + "           " + "Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18));
            //SerialNumberAndDate.SetUnderline(0.5f, -1.5f);



            ////////////////////////   table1 - Visual test   ///////////////////////////////

            Paragraph VZ = new Paragraph("                                                                  Visual test", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));


            PdfPTable table1 = new PdfPTable(3);
            PdfPCell cell1 = new PdfPCell();
            cell1.Colspan = 4;
            cell1.HorizontalAlignment = 4;
            table1.AddCell(cell1);



            table1.AddCell("Step");
            table1.AddCell("Procedure");
            table1.AddCell("Result");

            table1.AddCell("1");
            table1.AddCell("Screen");
            bool Screen = (checkBoxScreenOK.Checked);
            bool NOT_CHECKED_Screen = (!checkBoxScreenOK.Checked) && (!checkBoxScreenFAIL.Checked);

            if (NOT_CHECKED_Screen)
            {
                table1.AddCell("NOT CHECKED");
            }
            else
            {
                if (Screen)
                {
                    table1.AddCell("OK");
                }
                else
                {
                    table1.AddCell("FAIL");
                }
            }

            table1.AddCell("2");
            table1.AddCell("Antena");
            bool Antena = checkBoxAntenaOK.Checked;
            bool NOT_CHECKED_Antena = (!checkBoxAntenaOK.Checked) && (!checkBoxAntenaFAIL.Checked);

            if (NOT_CHECKED_Antena)
            {
                table1.AddCell("NOT CHECKED");
            }
            else
            {
                if (Antena)
                {
                    table1.AddCell("OK");
                }
                else
                {
                    table1.AddCell("FAIL");
                }
            }



            table1.AddCell("3");
            table1.AddCell("Plastic");
            bool Plastic = checkBoxPlasticOK.Checked;
            bool NOT_CHECKED_Plastic = (!checkBoxPlasticOK.Checked) && (!checkBoxPlasticFAIL.Checked);

            if (NOT_CHECKED_Plastic)
            {
                table1.AddCell("NOT CHECKED");
            }
            else
            {
                if (Plastic)
                {
                    table1.AddCell("OK");
                }
                else
                {
                    table1.AddCell("FAIL");
                }
            }





            table1.AddCell("4");
            table1.AddCell("Screw");
            bool Screw = checkBoxScrewOK.Checked;
            bool NOT_CHECKED_Screw = (!checkBoxScrewOK.Checked) && (!checkBoxScrewFAIL.Checked);
            if (NOT_CHECKED_Screw)
            {
                table1.AddCell("NOT CHECKED");
            }
            else
            {
                if (Screw)
                {
                    table1.AddCell("OK");
                }
                else
                {
                    table1.AddCell("FAIL");
                }
            }
            //////////////////////// END table1  - Visual test   ///////////////////////////////



            ////////////////////////   table2 - Mechanical test   ///////////////////////////////

            Paragraph MT = new Paragraph("                                                                  Mechanical Tests", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));


            PdfPTable table2 = new PdfPTable(3);
            PdfPCell cell2 = new PdfPCell();
            cell2.Colspan = 3;
            cell2.HorizontalAlignment = 3;
            table2.AddCell(cell2);

            table2.AddCell("Step");
            table2.AddCell("Procedure");
            table2.AddCell("Result");

            table2.AddCell("5");
            table2.AddCell("Button function");
            bool ButtonFunction = (checkBoxButtonFunctionOK.Checked);
            bool NOT_CHECKED_ButtonFunction = (!checkBoxButtonFunctionOK.Checked) && (!checkBoxButtonFunctionFAIL.Checked);

            if (NOT_CHECKED_ButtonFunction)
            {
                table2.AddCell("NOT CHECKED");
            }
            else
            {
                if (ButtonFunction)
                {
                    table2.AddCell("OK");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }


            table2.AddCell("6");
            table2.AddCell("Covers");
            bool Covers = (checkBoxCoverOK.Checked);
            bool NOT_CHECKED_Covers = (!checkBoxCoverOK.Checked) && (!checkBoxCoverFAIL.Checked);

            if (NOT_CHECKED_Covers)
            {
                table2.AddCell("NOT CHECKED");
            }
            else
            {
                if (Covers)
                {
                    table2.AddCell("OK");
                }
                else
                {
                    table2.AddCell("FAIL");
                }
            }

            ////////////////////////   END table2 - Mechanical test   ///////////////////////////////



            ////////////////////////   table3 - Tests  ///////////////////////////////

            Paragraph Tests = new Paragraph("                                                                  Tests", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            PdfPTable table3 = new PdfPTable(3);
            PdfPCell cell3 = new PdfPCell();
            cell3.Colspan = 10;
            cell3.HorizontalAlignment = 10;
            table3.AddCell(cell3);



            table3.AddCell("Step");
            table3.AddCell("Procedure");
            table3.AddCell("Result");



            table3.AddCell("7");
            table3.AddCell("Message to all users");
            bool MTAU = (checkBoxMessageToAllUsersOK.Checked);
            bool NOT_CHECKED_MTAU = (!checkBoxMessageToAllUsersOK.Checked) && (!checkBoxMessageToAllUsersFAIL.Checked);

            if (NOT_CHECKED_MTAU)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (MTAU)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("8");
            table3.AddCell("Message between 2 UDI-28 Wrist");
            bool MB2UW = (checkBoxMessageBetween2UDI14WristOK.Checked);
            bool NOT_CHECKED_MB2UW = (!checkBoxMessageBetween2UDI14WristOK.Checked) && (!checkBoxMessageBetween2UDI14WristFAIL.Checked);

            if (NOT_CHECKED_MB2UW)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (MB2UW)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("9");
            table3.AddCell("SOS message");
            bool SOSM = (checkBoxSOSMessageOK.Checked);
            bool NOT_CHECKED_SOSM = (!checkBoxSOSMessageOK.Checked) && (!checkBoxSOSMessageFAIL.Checked);

            if (NOT_CHECKED_SOSM)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (SOSM)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("10");
            table3.AddCell("Compass");
            bool Compass = (checkBoxCompassOK.Checked);
            bool NOT_CHECKED_Compass = (!checkBoxCompassOK.Checked) && (!checkBoxCompassFAIL.Checked);

            if (NOT_CHECKED_Compass)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (Compass)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("11");
            table3.AddCell("PC Connection");
            bool PCConnection = (checkBoxPCConnectionToDiveSimOK.Checked);
            bool NOT_CHECKED_PCConnection = (!checkBoxPCConnectionToDiveSimOK.Checked) && (!checkBoxPCConnectionToDiveSimFAIL.Checked);

            if (NOT_CHECKED_PCConnection)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (PCConnection)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("12");
            table3.AddCell("Load Messages");
            bool LoadMessages = (checkBoxLoadMessageOK.Checked);
            bool NOT_CHECKED_LoadMessages = (!checkBoxLoadMessageOK.Checked) && (!checkBoxLoadMessageFAIL.Checked);

            if (NOT_CHECKED_LoadMessages)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (LoadMessages)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("13");
            table3.AddCell("Load Names");
            bool LoadNames = (checkBoxLoadNamesOK.Checked);
            bool NOT_CHECKED_LoadNames = (!checkBoxLoadNamesOK.Checked) && (!checkBoxLoadNamesFAIL.Checked);

            if (NOT_CHECKED_LoadNames)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (LoadNames)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("14");
            table3.AddCell("Update Udi Ver");
            table3.AddCell(textBoxUpdateUDI.Text);


            table3.AddCell("15");
            table3.AddCell("Time & Date");
            bool TimeDate = (checkBoxDateTimeOK.Checked);
            bool NOT_CHECKED_TimeDate = (!checkBoxDateTimeOK.Checked) && (!checkBoxDateTimeFAIL.Checked);

            if (NOT_CHECKED_TimeDate)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (TimeDate)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }

            table3.AddCell("16");
            table3.AddCell("Charging");
            bool ChargingLight = (checkBoxChargingLightOK.Checked);
            bool NOT_CHECKED_ChargingLight = (!checkBoxChargingLightOK.Checked) && (!checkBoxChagingLightFAIL.Checked);

            if (NOT_CHECKED_ChargingLight)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (ChargingLight)
                {
                    table3.AddCell("OK");
                }
                else
                {
                    table3.AddCell("FAIL");
                }
            }


            ////////////////////////  END table3 - Tests  ///////////////////////////////




            ////////////////////////  table4 - Package contents  ///////////////////////////////

            Paragraph PackageContents = new Paragraph("                                                                  Package contents", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));





            PdfPTable table4 = new PdfPTable(3);
            PdfPCell cell4 = new PdfPCell();
            cell4.Colspan = 5;
            cell4.HorizontalAlignment = 5;
            table4.AddCell(cell4);



            table4.AddCell("Step");
            table4.AddCell("Procedure");
            table4.AddCell("Result");



            table4.AddCell("17");
            table4.AddCell("Quick connector");
            bool QuickConnector = (checkBoxQuickConnectorOK.Checked);
            bool NOT_CHECKED_QuickConnector = (!checkBoxQuickConnectorOK.Checked) && (!checkBoxQuickConnectorFAIL.Checked);

            if (NOT_CHECKED_QuickConnector)
            {
                table4.AddCell("NOT CHECKED");
            }
            else
            {
                if (QuickConnector)
                {
                    table4.AddCell("OK");
                }
                else
                {
                    table4.AddCell("FAIL");
                }
            }

            table4.AddCell("18");
            table4.AddCell("USB Cable");
            bool USBCable = (checkBoxUSBCableOK.Checked);
            bool NOT_CHECKED_USBCable = (!checkBoxUSBCableOK.Checked) && (!checkBoxUSBCableFAIL.Checked);

            if (NOT_CHECKED_USBCable)
            {
                table4.AddCell("NOT CHECKED");
            }
            else
            {
                if (USBCable)
                {
                    table4.AddCell("OK");
                }
                else
                {
                    table4.AddCell("FAIL");
                }
            }

            table4.AddCell("19");
            table4.AddCell("Power supply");
            bool PowerSupply = (checkBoxPowerSupplyOK.Checked);
            bool NOT_CHECKED_PowerSupply = (!checkBoxPowerSupplyOK.Checked) && (!checkBoxPowerSupplyFAIL.Checked);

            if (NOT_CHECKED_PowerSupply)
            {
                table4.AddCell("NOT CHECKED");
            }
            else
            {
                if (PowerSupply)
                {
                    table4.AddCell("OK");
                }
                else
                {
                    table4.AddCell("FAIL");
                }
            }//

            table4.AddCell("20");
            table4.AddCell("Case");
            bool Case = (checkBoxCaseOK.Checked);
            bool NOT_CHECKED_Case = (!checkBoxCaseOK.Checked) && (!checkBoxCaseFAIL.Checked);

            if (NOT_CHECKED_Case)
            {
                table4.AddCell("NOT CHECKED");
            }
            else
            {
                if (Case)
                {
                    table4.AddCell("OK");
                }
                else
                {
                    table4.AddCell("FAIL");
                }
            }

            table4.AddCell("21");
            table4.AddCell("Cover");
            bool Cover = (checkBoxCoverColorOK.Checked);
            bool NOT_CHECKED_Cover = (!checkBoxCoverColorOK.Checked) && (!checkBoxCoverColorFAIL.Checked);

            if (NOT_CHECKED_Cover)
            {
                table4.AddCell("NOT CHECKED");
            }
            else
            {
                if (Cover)
                {
                    table4.AddCell("OK");
                }
                else
                {
                    table4.AddCell("FAIL");
                }
            }
            //////////////////////// END table4 - Package contents  ///////////////////////////////



            //////////////////////// Finish Document  ///////////////////////////////
            Chunk AddressParagraph = new Chunk("\n\n\n\n\n\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 84965.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            Paragraph Finish = new Paragraph("              Tested by: " + textBoxTestedBy.Text + "                  Signature: " + textBoxSignature.Text + "                  Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            //////////////////////// END Finish Document  ///////////////////////////////




            //////////////////////// Page design  //////////////////////

            doc.Add(logoPNG);
            doc.Add(headLine);
            doc.Add(lineDown);
            doc.Add(productName);
            doc.Add(lineDown);
            doc.Add(SerialNumberAndDate);
            doc.Add(lineDown);
            doc.Add(VZ);
            doc.Add(lineDown);
            doc.Add(table1);
            //doc.Add(lineDown);
            doc.Add(MT);
            doc.Add(lineDown);
            doc.Add(table2);
            //doc.Add(lineDown);
            doc.Add(Tests);
            doc.Add(lineDown);
            doc.Add(table3);
            //doc.Add(lineDown);
            doc.Add(PackageContents);
            doc.Add(lineDown);
            doc.Add(table4);
            //doc.Add(lineDown);
            doc.Add(Finish);
            doc.Add(lineDown);
            //doc.Add(AddressParagraph);
            doc.Add(SignaturePNG);

            doc.Close();

            MessageBox.Show("PDF Create Successfully !");


        }

        public void Update_WayBill()
        {

            SqlConnection sqlConnection1 = new SqlConnection(@"Data Source=ORLYPC\SQLEXPRESS;Initial Catalog = UTCTest; Integrated Security = True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string SerielNumber = textBoxUdiSerielNumber.Text.Trim();
            string WaybillNumber = textBoxWaybillNumber.Text.Trim();
            cmd.CommandText = "UPDATE  Boat14_dt SET WaybillNumber= '" + WaybillNumber + "' WHERE Seriel_Code = '" + SerielNumber + "';";



            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
            MessageBox.Show("Updated successfully");


        }


        private void button2_Click(object sender, EventArgs e)
        {
            CreateInspectionTestDocument();
            clearFieldsAfterDone();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBoxUdiSerielNumber.Text == "")
            {
                MessageBox.Show("Seriel Number Text Box is Empty");
            }
            else
            {
                try
                {
                    Update_WayBill();
                    UpdateDataGrid();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
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
            pi.FileName = @"P:\Niv\HELP UTC TESTS\ACCEPTING TESTS.docx";
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


            int TestedBy = textBoxTestedBy.Text.Length;
            int Signature = textBoxSignature.Text.Length;

            if (textBoxUdiSerielNumber.Text == "")
            {
                MessageBox.Show(" UDI S/N must be writed ! ");

            }

            else if (TestedBy > 15)
            {
                MessageBox.Show(" Tested By Can not be more than 15 Characters");
            }

            else if (Signature > 15)
            {
                MessageBox.Show(" Signature Can not be more than 15 Characters");
            }
            else
            {
                try
                {
                    InsertIntoData();
                    CreateInspectionTestDocument();
                    UpdateDataGrid();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_Boat14();
                    }
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so AT document will be created, but it will not be stored in the Base database");
                    CreateInspectionTestDocument();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_Boat14();
                    }
                    MessageBox.Show("Successfully Done !");
                    clearFieldsAfterDone();
                }
            }

        }

        private void moreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BoatUnit14_H_Form().Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBoxTestedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBoxTestedBy.Text == "Niv")
            {
                textBoxSignature.Text = "Niv Ben Abat";
            }
            else if (textBoxTestedBy.Text == "Alona")
            {
                textBoxSignature.Text = "Alona Moiseev";
            }
        }
    }
}
