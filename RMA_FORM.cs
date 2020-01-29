using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Reflection;








namespace Project_Product_List
{
    public partial class RMA_FORM : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;


        public RMA_FORM()
        {
            InitializeComponent();
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

        void Check_If_There_Is_history_Information_In_DataBase()
        {

            if (textBoxWarrantyReview.Text == "" || comboBoxWarrantyReview.Text.Trim() == "")
            {
                MessageBox.Show("'Seriel number' AND 'Choose Product' -  must be writed !");
            }

            //////////// BOAT UNIT 14 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "UDI 14")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [Udi14_dt]";
                cmd.Connection = con;

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    if (rd[1].ToString() == textBoxWarrantyReview.Text.Trim())
                    {
                        MessageBox.Show("in the past - information was entered into the database\nPlease Do not forget to check the validity of the warranty.");
                        return;
                    }
                }
                MessageBox.Show("Product NOT found !! \nIt is possible that the product is new, and therefore No information was entered into the database.");
            }


            //////////// BOAT UNIT 28 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "UDI 28")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [Udi28_dt]";
                cmd.Connection = con;

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    if (rd[1].ToString() == textBoxWarrantyReview.Text.Trim())
                    {
                        MessageBox.Show("in the past - information was entered into the database\nPlease Do not forget to check the validity of the warranty.");
                        return;
                    }
                }
                MessageBox.Show("Product NOT found !! \nIt is possible that the product is new, and therefore No information was entered into the database.");
            }




            //////////// BOAT UNIT 14 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "BOAT UNIT 14")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [Boat14_dt]";
                cmd.Connection = con;

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    if (rd[1].ToString() == textBoxWarrantyReview.Text.Trim())
                    {
                        MessageBox.Show("in the past - information was entered into the database\nPlease Do not forget to check the validity of the warranty.");
                        return;
                    }
                }
                MessageBox.Show("Product NOT found !! \nIt is possible that the product is new, and therefore No information was entered into the database.");
            }




            //////////// BOAT UNIT 28 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "BOAT UNIT 28")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [Boat28_dt]";
                cmd.Connection = con;

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    if (rd[1].ToString() == textBoxWarrantyReview.Text.Trim())
                    {
                        MessageBox.Show("in the past - information was entered into the database\nPlease Do not forget to check the validity of the warranty.");
                        return;
                    }
                }
                MessageBox.Show("Product NOT found !! \nIt is possible that the product is new, and therefore No information was entered into the database.");
            }



            //////////// ADCS ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "ADCS")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [ADCS_dt]";
                cmd.Connection = con;

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    if (rd[1].ToString() == textBoxWarrantyReview.Text.Trim())
                    {
                        MessageBox.Show("in the past - information was entered into the database\nPlease Do not forget to check the validity of the warranty.");
                        return;
                    }
                }
                MessageBox.Show("Product NOT found !! \nIt is possible that the product is new, and therefore No information was entered into the database.");
            }
        }

        void Insert_RMA_Into_DataBase()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                

                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("RMA_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@DateCreate", dateTimePickerRMA.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ToCustomer", textBoxToCustomer.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Phone", textBoxPhone.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA_Number", textBoxRMA_NO.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_Name", comboBoxCustomerName.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@item1", textBoxItem1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ProductNumber1", textBoxProductNo1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SerialNumber1", textBoxSerialNo1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Description1", textBoxDescription1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@InvoiceNumber1", textBoxInvoiceNo1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ValueUSD1", textBoxValueUSD1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CustomerComplaint1", textBoxCustomerComplaint1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CustomerContact1", textBoxCustomerContact1.Text.Trim());



                    sqlCmd.Parameters.AddWithValue("@TotalUSD", textBoxValueUSD1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Name", textBoxName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Signature", textBoxSignature.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@isArrived", checkBoxArrived.Checked);
                    sqlCmd.Parameters.AddWithValue("@deviceBeenFixed", checkBoxFixed.Checked);



                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    MessageBox.Show(" RMA successfully entered into database !");
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.ToString());
                }

            }
        }

        void fill_Address_AND_Phone()
        {

            ///////////////////////// fill Customer Address /////////////////////////

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string CustomerName = comboBoxCustomerName.Text.Trim();

            cmd.CommandText = "SELECT Customer_address FROM Customers_dt WHERE Customer_name = '" + CustomerName + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                textBoxToCustomer.Text = Convert.ToString(reader[0]);
            }

            sqlConnection1.Close();


            ///////////////////////// fill Customer Phone /////////////////////////

            SqlConnection sqlConnection2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader Phone_reader;
            string CustomerPhone = textBoxPhone.Text.Trim();

            cmd2.CommandText = "SELECT Customer_Phone FROM Customers_dt WHERE Customer_name = '" + CustomerName + "';";
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = sqlConnection2;


            sqlConnection2.Open();

            Phone_reader = cmd2.ExecuteReader();

            if (Phone_reader.Read())
            {
                textBoxPhone.Text = Convert.ToString(Phone_reader[0]);
            }

            sqlConnection2.Close();


        }

        public void Load_RMA_Name_on_Load()
        {
            int RMAyear = dateTimePickerRMA.Value.Year;

            string SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\";

            int counterONtextBox = Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length + 1;

            SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + "RMA " + RMAyear + "_R" + counterONtextBox + ".pdf";

            while (File.Exists(SavePath))
            {
                counterONtextBox += 1;
                SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + "RMA " + RMAyear + "_R" + counterONtextBox + ".pdf";
            }
            textBoxRMA_NO.Text = "RMA " + RMAyear + "_R" + (counterONtextBox);
        }




        void CheckIfWarrantyValid()
        {
            if (textBoxWarrantyReview.Text == "" || comboBoxWarrantyReview.Text.Trim() == "" || dateTimePickerRMA.Text == "")
            {
                MessageBox.Show("Error for one of the following reasons:\n\n1. Warranty Review Seriel number -  must be Writed!\n2. Choose Product -  must be writed\n3. Date  - must be Writed!");
            }

            //////////// BOAT UNIT 14 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "UDI 14")
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string productSerialNumber = textBoxWarrantyReview.Text.Trim();

                cmd.CommandText = "SELECT Date FROM Udi14_dt WHERE Seriel_Code = '" + productSerialNumber + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DateTime parsedFromDataDate = DateTime.Parse(reader[0].ToString());
                    DateTime RMAdate = dateTimePickerRMA.Value.Date;

                    TimeSpan ts = RMAdate - parsedFromDataDate;

                    int days = ts.Days;


                    if (days <= 365)
                    {
                        MessageBox.Show("Warranty is valid - you can continue the process !");
                    }
                    else
                    {
                        MessageBox.Show("Warranty expired! ! ! !");
                    }

                }


                else
                {
                    MessageBox.Show("item Not found !");
                }

                sqlConnection1.Close();

            }

            //////////// BOAT UNIT 28 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "UDI 28")
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string productSerialNumber = textBoxWarrantyReview.Text.Trim();

                cmd.CommandText = "SELECT Date FROM Udi28_dt WHERE Seriel_Code = '" + productSerialNumber + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DateTime parsedFromDataDate = DateTime.Parse(reader[0].ToString());
                    DateTime RMAdate = dateTimePickerRMA.Value.Date;

                    TimeSpan ts = RMAdate - parsedFromDataDate;

                    int days = ts.Days;

                    if (days <= 365)
                    {
                        MessageBox.Show("Warranty is valid - you can continue the process !");
                    }
                    else
                    {
                        MessageBox.Show("Warranty expired! ! ! !");

                    }
                }


                else
                {
                    MessageBox.Show("item Not found !");
                }

                sqlConnection1.Close();
            }

            //////////// Boat14 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "BOAT UNIT 14")
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string productSerialNumber = textBoxWarrantyReview.Text.Trim();

                cmd.CommandText = "SELECT Date FROM Boat14_dt WHERE Seriel_Code = '" + productSerialNumber + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DateTime parsedFromDataDate = DateTime.Parse(reader[0].ToString());
                    DateTime RMAdate = dateTimePickerRMA.Value.Date;

                    TimeSpan ts = RMAdate - parsedFromDataDate;

                    int days = ts.Days;

                    if (days <= 365)
                    {
                        MessageBox.Show("Warranty is valid - you can continue the process !");
                    }
                    else
                    {
                        MessageBox.Show("Warranty expired! ! ! !");

                    }
                }


                else
                {
                    MessageBox.Show("item Not found !");
                }

                sqlConnection1.Close();

            }

            //////////// Boat28 ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "BOAT UNIT 28")
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string productSerialNumber = textBoxWarrantyReview.Text.Trim();

                cmd.CommandText = "SELECT Date FROM Boat28_dt WHERE Seriel_Code = '" + productSerialNumber + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DateTime parsedFromDataDate = DateTime.Parse(reader[0].ToString());
                    DateTime RMAdate = dateTimePickerRMA.Value.Date;

                    TimeSpan ts = RMAdate - parsedFromDataDate;

                    int days = ts.Days;

                    if (days <= 365)
                    {
                        MessageBox.Show("Warranty is valid - you can continue the process !");
                    }
                    else
                    {
                        MessageBox.Show("Warranty expired! ! ! !");
                    }
                }


                else
                {
                    MessageBox.Show("item Not found !");
                }

                sqlConnection1.Close();
            }

            //////////// ADCS ///////////
            else if (comboBoxWarrantyReview.Text.Trim() == "ADCS")
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string productSerialNumber = textBoxWarrantyReview.Text.Trim();

                cmd.CommandText = "SELECT Date FROM ADCS_dt WHERE Seriel_Code = '" + productSerialNumber + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DateTime parsedFromDataDate = DateTime.Parse(reader[0].ToString());
                    DateTime RMAdate = dateTimePickerRMA.Value.Date;

                    TimeSpan ts = RMAdate - parsedFromDataDate;

                    int days = ts.Days;

                    if (days <= 365)
                    {
                        MessageBox.Show("Warranty is valid - you can continue the process !");
                    }
                    else
                    {
                        MessageBox.Show("Warranty expired! ! ! !");

                    }
                }
                else
                {
                    MessageBox.Show("item Not found !");
                }
                sqlConnection1.Close();
            }
        }

        private void SendToPrinter_RMA()
        {

            int RMAyear = dateTimePickerRMA.Value.Year;
            
            string SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\";

            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry
           
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";

            
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            //info.FileName = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + textBoxRMA_NO.Text + ".pdf";


            info.FileName = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + "RMA " + RMAyear + "_R" + PDFcounter + ".pdf";
            
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


        void rmaToPdf()
        {

            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();


            int RMAyear = dateTimePickerRMA.Value.Year;

            string SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }


            int PDFcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + textBoxRMA_NO.Text + ".pdf";


            //while (File.Exists(SavePath))
            //{
            //    PDFcounter += 1;
            //    SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + "RMA " + RMAyear + "_R" + PDFcounter + ".pdf";
            //}

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
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 250f - 450f);


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
            //headLine.SetUnderline(0.5f, -1.5f);



            Paragraph CNParagraph = new Paragraph("C/N 513369199");
            Chunk AddressWithLine = new Chunk("Address:\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            AddressWithLine.SetUnderline(0.5f, -1.5f);

            Paragraph Note = new Paragraph("The Value is for customs purposes only.\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));


            Chunk AddressParagraph = new Chunk("\n\n\n\n\n\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 8496500.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            Paragraph To = new Paragraph("To: " + textBoxToCustomer.Text);
            Paragraph Phone = new Paragraph("Phone: " + textBoxPhone.Text + "                                                                               Date: " + dateTimePickerRMA.Value.ToString("dd-MM-yyyy") + "\n\n");
            Paragraph line = new Paragraph("-------------------------------------------------------------------------------------------------------------------");

            Chunk rma = new Chunk("Return Material Authorization\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));


            Paragraph lineDown = new Paragraph("\n");
            Paragraph customerComplain = new Paragraph("Customer Complaint: " + textBoxCustomerComplaint1.Text);
            Paragraph customerContact = new Paragraph("Customer Contact: " + textBoxCustomerContact1.Text);

            Chunk tracking_number_after_sending = new Chunk("Please note, in order to confirm the arrival of your package, please send us the courier tracking number.\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.RED));//Please send all documents to info

            Chunk how_to_send = new Chunk("Please send your shipment document with:\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            how_to_send.SetUnderline(0.5f, -1.5f);

            Paragraph lastStaitment = new Paragraph("   1. Company logo.\n   2. Company stamp and signature.\n   3. Correct product number and serial number.\n   4. Please send all documents to info@utc.co.il\n\n                                                                                      " + textBoxName.Text + "\n                                                                                      Underwater Technologies Center Ltd ");


            //////////////////////   table1   ///////////////////////////////

            PdfPTable table1 = new PdfPTable(1);
            PdfPCell cell1 = new PdfPCell();
            cell1.Colspan = 1;
            cell1.HorizontalAlignment = 1;
            table1.AddCell(cell1);
            table1.WidthPercentage = 100f;


            table1.AddCell("RMA - No. : " + textBoxRMA_NO.Text );

            table1.AddCell("Customer name: " + comboBoxCustomerName.Text);

            ////////////////////// END table1   ///////////////////////////////




            //////////////////////   table2   ///////////////////////////////

            PdfPTable table2 = new PdfPTable(5);
            PdfPCell cell2 = new PdfPCell();
            cell2.BackgroundColor = new BaseColor(Color.Blue);///////////
            cell2.Colspan = 5;
            cell2.HorizontalAlignment = 1;
            table2.AddCell(cell2);

            table2.SetWidths(new int[] { 3, 4, 7, 5, 3 });
            table2.WidthPercentage = 100f;


            table2.AddCell("Product No.");
            table2.AddCell("Serial No.");
            table2.AddCell("Description");
            table2.AddCell("Invoice No.");
            table2.AddCell("Value USD");



            table2.AddCell(textBoxProductNo1.Text);
            table2.AddCell(textBoxSerialNo1.Text);
            table2.AddCell(textBoxDescription1.Text);
            table2.AddCell(textBoxInvoiceNo1.Text);
            table2.AddCell(textBoxValueUSD1.Text);


            ////////////////////// END table2   ///////////////////////////////


            ////////////////////// Page design  //////////////////////

            doc.Add(lineDown);
            doc.Add(headLine);
            doc.Add(CNParagraph);
            doc.Add(line);
            doc.Add(To);
            doc.Add(Phone);
            doc.Add(rma);
            doc.Add(table1);
            doc.Add(lineDown);
            doc.Add(table2);
            doc.Add(customerComplain);
            doc.Add(customerContact);
            doc.Add(lineDown);
            doc.Add(Note);
            doc.Add(tracking_number_after_sending);//new
            doc.Add(how_to_send);
            doc.Add(lastStaitment);
            doc.Add(AddressParagraph);
            doc.Add(DownJPG);
            doc.Add(TopJPG);
            doc.Add(logoJPG);
            doc.Add(SignaturePNG);

            doc.Close();

            MessageBox.Show("RMA Create Successfully !");


        }

        void Load_Customers_To_ComboBox()
        {


            /////load the names to combobox
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string CustomerName = comboBoxCustomerName.Text.Trim();

                cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCustomerName.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();

                comboBoxCustomerName.Items.Add("*NEW CUSTOMER");
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }


        void ForTestingTheSoftware_testInputs()
        {
            ///this function will use only for test cases 
            textBoxProductNo1.Text = "B-33";
            textBoxSerialNo1.Text = "020-3344";
            textBoxDescription1.Text = "Boat Unit 14";
            textBoxInvoiceNo1.Text = "I-199";
            textBoxValueUSD1.Text = "150";
            textBoxCustomerComplaint1.Text = "NOT WORKING DEVICE - TEST.";
            textBoxCustomerContact1.Text = "NIV BEN ABAT.";
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RMA_FORM_Load(object sender, EventArgs e)
        {

            int RMAyear = dateTimePickerRMA.Value.Year;

            string SavePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }


            //ForTestingTheSoftware_testInputs(); // - Tests only ! 
            textBoxItem1.Text = "1";
            //textBoxName.Text = "Alona Moiseev.";
            Load_Customers_To_ComboBox();
            Load_RMA_Name_on_Load();
        }




        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomerName.Text == "*NEW CUSTOMER")
            {
                new Customers_List(this).Show();
                this.Hide();
            }
            else
            {
                fill_Address_AND_Phone();
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            Check_If_There_Is_history_Information_In_DataBase();
        }



        private void button5_Click(object sender, EventArgs e)
        {
            CheckIfWarrantyValid();
        }

        private void textBoxRMA_NO_TextChanged(object sender, EventArgs e)
        {

        }

        public void changeDetailsFromCustomerList(string customerName)
        {
            this.comboBoxCustomerName.Items.Add(customerName);
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxValueUSD1_TextChanged(object sender, EventArgs e)
        {

        }
        private string MyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\RMA.docx";
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
            int DescriptionLength = textBoxDescription1.Text.Length;
            int SerialNo = textBoxSerialNo1.Text.Length;
            int CustomerComplaint = textBoxCustomerComplaint1.Text.Length;
            int CustomerContact = textBoxCustomerContact1.Text.Length;
            int Signature = textBoxSignature.Text.Length;
            int Name = textBoxName.Text.Length;


            if (textBoxInvoiceNo1.Text == "")
            {
                MessageBox.Show(" Invoice Number must be writed ! ");
            }
            else if (DescriptionLength > 23)
            {
                MessageBox.Show(" Description Can not be more than 23 Characters");
            }
            else if (SerialNo > 15)
            {
                MessageBox.Show(" Serial number Can not be more than 15 Characters");
            }
            else if (CustomerComplaint > 35)
            {
                MessageBox.Show(" Customer Complaint Can not be more than 35 Characters");
            }
            else if (CustomerContact > 25)
            {
                MessageBox.Show(" Customer Contact Can not be more than 25 Characters");
            }
            else if (Signature > 12)
            {
                MessageBox.Show(" Signature Can not be more than 12 Characters");
            }
            else if (Name > 9)
            {
                MessageBox.Show(" Name Can not be more than 9 Characters");
            }

            else if (MessageBox.Show("Are you sure you Done with this RMA ?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Insert_RMA_Into_DataBase();
                    rmaToPdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {
                            SendToPrinter_RMA();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }

                    }
                    clearFieldsAfterDone();
                    textBoxItem1.Text = "1";
                    Load_RMA_Name_on_Load();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so RMA document will be created, but it will not be stored in the Base database");
                    rmaToPdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_RMA();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int DescriptionLength = textBoxDescription1.Text.Length;
            int SerialNo = textBoxSerialNo1.Text.Length;
            int CustomerComplaint = textBoxCustomerComplaint1.Text.Length;
            int CustomerContact = textBoxCustomerContact1.Text.Length;
            int Signature = textBoxSignature.Text.Length;
            int Name = textBoxName.Text.Length;


            if (textBoxInvoiceNo1.Text == "")
            { 
                MessageBox.Show(" Invoice Number must be writed ! ");
            }
            else if (DescriptionLength > 23)
            {
                MessageBox.Show(" Description Can not be more than 23 Characters");
            }
            else if (SerialNo > 15)
            {
                MessageBox.Show(" Serial number Can not be more than 15 Characters");
            }
            else if (CustomerComplaint > 35)
            {
                MessageBox.Show(" Customer Complaint Can not be more than 35 Characters");
            }
            else if (CustomerContact > 25)
            {
                MessageBox.Show(" Customer Contact Can not be more than 25 Characters");
            }
            else if (Signature > 12)
            {
                MessageBox.Show(" Signature Can not be more than 12 Characters");
            }
            else if (Name > 9)
            {
                MessageBox.Show(" Name Can not be more than 9 Characters");
            }

            else if (MessageBox.Show("Are you sure you Done with this RMA ?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Insert_RMA_Into_DataBase();
                    rmaToPdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {
                            SendToPrinter_RMA();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }

                    }
                    clearFieldsAfterDone();
                    textBoxItem1.Text = "1";
                    Load_RMA_Name_on_Load();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so RMA document will be created, but it will not be stored in the Base database");
                    rmaToPdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_RMA();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }



        public void Delete_from_RMA()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string deleteRMAnumber = textBoxRMA_NO.Text.Trim();

            cmd.CommandText = "DELETE FROM RMA_dt WHERE RMA_Number = '" + deleteRMAnumber + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
            MessageBox.Show("Done !");
        }

        private void textBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text == "Niv")
            {
                textBoxSignature.Text = "Niv Ben Abat";
            }
            else if (textBoxName.Text == "Alona")
            {
                textBoxSignature.Text = "Alona Moiseev";
            }
        }
    }
}



