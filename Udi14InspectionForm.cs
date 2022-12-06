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
    public partial class Udi14InspectionForm : Form
    {

        string SavePath = Paths.Paths.UDI_14_PATH;
        //string SavePath = "T:\\Users\\NivBE\\source\\repos";
        

        void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Name FROM CUSTOMER";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBoxCustomerName.Items.Add(dr["Name"]);
                    }

                    dr.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }



        }

        public Udi14InspectionForm()
        {
            InitializeComponent();
        }
        
        private void Udi14InspectionForm_Load(object sender, EventArgs e)
        {
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

            Load_Customers_To_ComboBox();
        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxMessageToAllUsersOK_CheckedChanged(object sender, EventArgs e)
        {

        }
        

        public void InsertToDataBase()
        {
            int Number = 1;

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    cnn.Execute("insert into UDI14 (Number, Seriel_Code, Screen, Antena, Plastic, Screw, Button_function, Cover, Message_to_all_users, Message_between_2_udi_14_wrist, SOS_message, Compass, PC_Connection_Dive_sim, Load_Messages, Load_Names, Update_udi_ver, Date_Time, Charging, Quick_connector, USBcable, Power_Supply, Case_, Date, Tested_by, Signature, Customer_Name, Pressure_sensor, WaybillNumber, Table_Tag, Serial_Correct) values ('" + Number + "','" + textBoxUdiSerielNumber.Text.Trim() + "', '" + Convert.ToInt32(checkBoxScreenOK.Checked) + "', '" + Convert.ToInt32(checkBoxAntenaOK.Checked) + "' , '" + Convert.ToInt32(checkBoxPlasticOK.Checked) + "', '" + Convert.ToInt32(checkBoxScrewOK.Checked) + "', '" + Convert.ToInt32(checkBoxButtonFunctionOK.Checked) + "','" + Convert.ToInt32(checkBoxCoverOK.Checked) + "' , '" + Convert.ToInt32(checkBoxMessageToAllUsersOK.Checked) + "', '" + Convert.ToInt32(checkBoxMessageBetween2UDI14WristOK.Checked) + "', '" + Convert.ToInt32(checkBoxSOSMessageOK.Checked) + "', '" + Convert.ToInt32(checkBoxCompassOK.Checked) + "', '" + Convert.ToInt32(checkBoxPCConnectionToDiveSimOK.Checked) + "', '" + Convert.ToInt32(checkBoxLoadMessageOK.Checked) + "', '" + Convert.ToInt32(checkBoxLoadNamesOK.Checked) + "', '" + textBoxUdiVer.Text.Trim() + "', '" + Convert.ToInt32(checkBoxDateTimeOK.Checked) + "', '" + Convert.ToInt32(checkBoxChargingLightOK.Checked) + "',  '" + Convert.ToInt32(checkBoxQuickConnectorOK.Checked) + "', '" + Convert.ToInt32(checkBoxUSBCableOK.Checked) + "', '" + Convert.ToInt32(checkBoxPowerSupplyOK.Checked) + "', '" + Convert.ToInt32(checkBoxCaseOK.Checked) + "', '" + dateTimePicker1.Text.Trim() + "', '" + textBoxTestedBy.Text.Trim() + "', '" + textBoxSignature.Text.Trim() + "', '" + comboBoxCustomerName.Text.Trim() + "', '" + textBoxPressureSensor.Text.Trim() + "', '" + textBoxWaybillNumber.Text.Trim() + "', '" + comboBoxTableTag.Text.Trim() + "', '" + checkBoxUdiSerialCorrectOK.Checked + "')");

                }

                MessageBox.Show("Done Successfully !");

                
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }
        
        private void SendToPrinter_udi14()
        {
            int DOCyear = dateTimePicker1.Value.Year;

            ProcessStartInfo info = new ProcessStartInfo();

            info.Verb = "print";

            
            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry

            info.FileName = SavePath + textBoxUdiSerielNumber.Text + "_AT" + PDFcounter + ".pdf";


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
                    InsertToDataBase();
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_udi14();
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
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {
                            SendToPrinter_udi14();
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



        void PDFCreator()
        {


            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();

            int DOCyear = dateTimePicker1.Value.Year;




            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }


            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry

            string SavePathNew = SavePath + textBoxUdiSerielNumber.Text + "_AT" + PDFcounter + ".pdf";//niv

            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePathNew, FileMode.Create));


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
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 250f - 580f);

            ///////////////////////////////////////////////////////



            Chunk headLine = new Chunk("Underwater Technologies Center Ltd.\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE));
            Chunk productName = new Chunk("UDI-14 Inspection.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE));

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

            if(NOT_CHECKED_Screen)
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
            cell3.Colspan = 11;
            cell3.HorizontalAlignment = 11;
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
            table3.AddCell("Message between 2 UDI-14 Wrist");
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
            table3.AddCell(textBoxUdiVer.Text);


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
            ///////////////////////////////////
            table3.AddCell("17");
            table3.AddCell("Pressure sensor");
            table3.AddCell(textBoxPressureSensor.Text);


            table3.AddCell("18");
            table3.AddCell("Table tag");
            table3.AddCell(comboBoxTableTag.Text);


            table3.AddCell("19");
            table3.AddCell("Serial Number Correct");
            bool SerialCorrectFail = (checkBoxUdiSerialCorrectOK.Checked);
            bool NOT_CHECKED_SerialCorrect = (!checkBoxUdiSerialCorrectOK.Checked) && (!checkBoxUdiSerialCorrectFail.Checked);

            if (NOT_CHECKED_SerialCorrect)
            {
                table3.AddCell("NOT CHECKED");
            }
            else
            {
                if (SerialCorrectFail)
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


            table4.AddCell("20");
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

            table4.AddCell("21");
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

            table4.AddCell("22");
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

            table4.AddCell("23");
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

            table4.AddCell("24");
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

            Paragraph Finish = new Paragraph("              Tested by: " + textBoxTestedBy.Text + "             Signature: " + textBoxSignature.Text + "                  Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

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


        private void button3_Click(object sender, EventArgs e)
        {
            new ChooseTestForm().Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click_1(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click_1(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxCustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }


        private void button2_Click(object sender, EventArgs e)
        {
            PDFCreator();
            clearFieldsAfterDone();

        }

        private void textBoxPressureSensor_TextChanged(object sender, EventArgs e)
        {

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
                    //Update_WayBill();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
            pi.FileName = Paths.Paths.UDI_14_HELP_FILE;

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
                    InsertToDataBase();
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_udi14();
                    }
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so AT document will be created, but it will not be stored in the Base database");
                    PDFCreator();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SendToPrinter_udi14();
                    }
                    MessageBox.Show("Successfully Done !");
                    clearFieldsAfterDone();
                }
            }
        }

        private void moreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UDI14_H_Form().Show();
            this.Hide();
        }

        private void textBoxUdiVer_SelectedIndexChanged(object sender, EventArgs e)
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
