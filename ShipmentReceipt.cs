using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using Dapper;
using System.Configuration;

namespace Project_Product_List
{
    public partial class Waybill : Form
    {

        public Waybill()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }
        

        void InsertToDataBase()
        {


            try
            {
                using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    cnn.Execute("insert into SHIPMENT_RECEIPT(ShipmentFrom, ShipmentTo, ShipmentDate, WaybillNumber, ServiceType, PackagingType, NumberOfPieces, TotalWeight, Dimensional, Chareable, InsuredAmount, DHL_Account, RefernceInformation, Reference, DescriptionOfContents, COMMENT) values ('" + textBoxShipmentFrom.Text.Trim() + "','" + textBoxshipmentTo.Text.Trim() + "','" + dateTimePickerShipmentDate.Text.Trim() + "','" + textBoxWaybillNumber.Text.Trim() + "','" + textBoxServiceType.Text.Trim() + "','" + textBoxPackagingType.Text.Trim() + "', '" + textBoxNumberOfPieces.Text.Trim() + "','" + textBoxTotalWeight.Text.Trim() + "','" + textBoxDimensional.Text.Trim() + "','" + textBoxChareable.Text.Trim() + "','" + textBoxInsuredAmount.Text.Trim() + "','" + textBoxDHLAccount.Text.Trim() + "', ,'" + textBoxRefernceInformation.Text.Trim() + "', '" + textBoxReference.Text.Trim() + "','" + textBoxDescriptionOfContents.Text.Trim() + "','" + textBoxCOMMENT.Text.Trim() + "')");
                }
               
                MessageBox.Show("Done Successfully !");

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            } 
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                InsertToDataBase();
                MessageBox.Show(" ShipmentReceipt successfully entered into database !");
                ShipmentReceipt_to_pdf();
                MessageBox.Show("Shipment Receipt PDF Successfully !");
                clearFieldsAfterDone();
                UpdateDataGrid();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM SHIPMENT_RECEIPT", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void Waybill_Load(object sender, EventArgs e)
        {
            //test();
            UpdateDataGrid();


        }


        public void createExcelFile()
        {
            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xlsx|Excel Files(2007)|*.xlsx |Excel Files(2013)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);

                //change properties of the work Book
                ExcelApp.Columns.ColumnWidth = 30;

                //Storing header part in Excel
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                }

                //Storing each row and coloumn value to excel sheet
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Excel File Create Successfully !");
            }
        }

        public void Search_ShiomentReceipt()
        {
            string waybill = textBoxFilter.Text.Trim();


            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT * FROM SHIPMENT_RECEIPT WHERE WaybillNumber LIKE '%" + waybill + "';";
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Read();
                    }

                    reader.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //excel
            createExcelFile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Search_ShiomentReceipt();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "UTC")
            {
                textBoxShipmentFrom.Text = "UTC LTD. AVIGAIL, OMARIM 8 ST., Baran building. OMER. ISRAEL 8496500. +972544499606 ";
            }
            else if (comboBox1.Text == "P&M")
            {
                textBoxShipmentFrom.Text = "PULSENMORE LTD. AVIGAIL, TAMAR 44 ST., Baran building OMER. ISRAEL 8496500. +972544499606";
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
            pi.FileName = @"P:\Archive\HELP UTC TESTS\Shipment Receipt.docx";


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

        public void test()
        {
            // i use it only for maintence the software
            textBoxShipmentFrom.Text = "UTC LTD. AVIGAIL, OMARIM 8 ST., Baran building. OMER. ISRAEL 8496500. +972544499606 ";
            textBoxshipmentTo.Text = "PULSENMORE LTD. AVIGAIL, TAMAR 44 ST., Baran building OMER. ISRAEL 8496500. +972544499606 ";
            textBoxWaybillNumber.Text = "6549541984 ";
            textBoxServiceType.Text = "PRIME ";
            textBoxPackagingType.Text = "BIG ";
            textBoxNumberOfPieces.Text = "5";
            textBoxTotalWeight.Text = "12";
            textBoxDimensional.Text = "36";
            textBoxChareable.Text = "555";
            textBoxInsuredAmount.Text = "2000";
            textBoxDHLAccount.Text = "DHL22556 ";
            textBoxRefernceInformation.Text = "NONE1";
            textBoxReference.Text = "NONE2";
            textBoxDescriptionOfContents.Text = "NONE3";
            textBoxCOMMENT.Text = "SENT ON SUNDAY - AFTER WORK FOR 15 HOURS";
        }

        public void ShipmentReceipt_to_pdf()
        {
            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();


            int ShipmentReceipt_YEAR = dateTimePickerShipmentDate.Value.Year;

            string SavePath = "U:" + @"\" + "Shipment Receipt" + @"\" + ShipmentReceipt_YEAR + @"\";


            int ShipmentReceiptcounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            SavePath = "U:" + @"\" + "Shipment Receipt" + @"\" + ShipmentReceipt_YEAR + @"\" + "Shipment Receipt #" + textBoxWaybillNumber.Text + ".pdf";

            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePath, FileMode.Create));  ///////////////// ?


            doc.Open();


            /////////////////////////////////////////////////////


            // UTC LOGO 
            iTextSharp.text.Image logoJPG = iTextSharp.text.Image.GetInstance("UTC_LOGO.png");
            logoJPG.ScalePercent(10f);
            logoJPG.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);

            /////////////////////////////////////////////////////


            // Signature logo        *************
            iTextSharp.text.Image SignaturePNG = iTextSharp.text.Image.GetInstance("SignatureutcPNG.png");
            SignaturePNG.ScalePercent(89f);
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 300f - 450f);


            /////////////////////////////////////////////////////




            // Down View        *************

            iTextSharp.text.Image DownPNG = iTextSharp.text.Image.GetInstance("Down.png");
            DownPNG.ScalePercent(55f);
            DownPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 930f - 50f);

            /////////////////////////////////////////////////////////


            //// Top View        *************

            iTextSharp.text.Image TopPNG = iTextSharp.text.Image.GetInstance("Top.png");
            TopPNG.ScalePercent(55f);
            TopPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 270f - 50f);

            ///////////////////////////////////////////////////////



            Chunk headLine = new Chunk("Underwater Technologies Center Ltd", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17, BaseColor.BLUE));

            Chunk Shipment_Details = new Chunk("Shipment Details\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED));
            Shipment_Details.SetUnderline(0.5f, -1.5f);

            Paragraph CNParagraph = new Paragraph("C/N 513369199");

            Chunk Shipment_FROM = new Chunk("Shipment From: " + textBoxShipmentFrom.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));
            Chunk Shipment_TO = new Chunk("Shipment To: " + textBoxshipmentTo.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Shipment_Date = new Chunk("Shipment Date: " + dateTimePickerShipmentDate.Value.ToString("dd-MM-yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));
            Chunk Waybill_Number = new Chunk("Waybill Number: " + textBoxWaybillNumber.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Service_Type = new Chunk("Service Type: " + textBoxServiceType.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Address_Paragraph = new Chunk("\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 84965.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            Paragraph Packaging_Type = new Paragraph("Packaging Type: " + textBoxPackagingType.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Paragraph Number_of_Pieces = new Paragraph("Number of Pieces: " + textBoxNumberOfPieces.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Paragraph line = new Paragraph("------------------------------------------------------------------------------------------------------------");

            Chunk Total_Weight = new Chunk("Total Weight: " + textBoxTotalWeight.Text + " kgs", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Dimensional = new Chunk("Dimensional: " + textBoxDimensional.Text + " kgs", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Chareable = new Chunk("Chareable: " + textBoxChareable.Text + " kgs", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Insured_Amount = new Chunk("Insured Amount: " + textBoxInsuredAmount.Text + " kgs", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Paragraph lineDown = new Paragraph("\n");

            Chunk Billing_Information = new Chunk("Billing Information\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED));
            Billing_Information.SetUnderline(0.5f, -1.5f);

            Chunk Account = new Chunk("Account: " + textBoxDHLAccount.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Refernce_Information = new Chunk("Refernce Information: " + textBoxRefernceInformation.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk Reference = new Chunk("Reference: " + textBoxReference.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));

            Chunk COMMENT = new Chunk("COMMENT: " + textBoxDescriptionOfContents.Text + "\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9));


            ////////////////////// Page design  //////////////////////


            doc.Add(TopPNG);
            doc.Add(DownPNG);

            doc.Add(SignaturePNG);
            doc.Add(logoJPG);

            doc.Add(headLine);
            doc.Add(lineDown);

            doc.Add(Shipment_Details);
            doc.Add(lineDown);

            doc.Add(Shipment_FROM);
            doc.Add(lineDown);

            doc.Add(Shipment_TO);

            doc.Add(lineDown);

            doc.Add(Shipment_Date);
            doc.Add(lineDown);

            doc.Add(Waybill_Number);
            doc.Add(lineDown);

            doc.Add(Service_Type);
            doc.Add(lineDown);

            doc.Add(Packaging_Type);
            doc.Add(lineDown);

            doc.Add(Number_of_Pieces);
            doc.Add(lineDown);

            doc.Add(Total_Weight);
            doc.Add(lineDown);

            doc.Add(Dimensional);
            doc.Add(lineDown);

            doc.Add(Chareable);
            doc.Add(lineDown);

            doc.Add(Insured_Amount);
            doc.Add(lineDown);

            doc.Add(Billing_Information);
            doc.Add(lineDown);

            doc.Add(Account);
            doc.Add(lineDown);

            doc.Add(Refernce_Information);
            doc.Add(lineDown);

            doc.Add(Reference);
            doc.Add(lineDown);

            doc.Add(COMMENT);

            doc.Add(Address_Paragraph);

            /////////////////////////////////////////////////////////

            doc.Close();

        }



        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //excel
            createExcelFile();
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                InsertToDataBase();
                MessageBox.Show(" Shipment Receipt successfully entered into database !");
                ShipmentReceipt_to_pdf();
                MessageBox.Show("Shipment Receipt PDF Successfully created!");
                clearFieldsAfterDone();
                UpdateDataGrid();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
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
            }
        }


        private void textBoxNumberOfPieces_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
