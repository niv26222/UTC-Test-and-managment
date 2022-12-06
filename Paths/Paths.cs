using System;

namespace Project_Product_List.Paths
{
    public class Paths
    {
        /// <summary>
        /// New Data-Base(UTC-SERVER)
        /// </summary>
        public const string UTC_SQL_CONNECTION_NEW = @"Data Source=DC\PRI;Initial Catalog=UTCgeneral;Integrated Security=True;";


        /// <summary>
        /// Old Data-Base (LOCAL)
        /// </summary>
        public const string ORLYPC_SQL_LOCAL_CONNECTION = @"Data Source=ORLYPC\SQLEXPRESS;Initial Catalog = UTCTest; Integrated Security = True";


        /// <summary>
        /// ADMIN OPTIONS
        /// </summary>
        public static string ADMIN_OPTIONS_ROOT_PATH = "U:" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\";


        /// <summary>
        /// RMA
        /// </summary>
        public static string RMA_ROOT_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "RMA - NEW" + @"\" + DateTime.Now.Year + @"\";

        public static string RMA_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\RMA.docx";
        public static string RMA_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\RMA History.docx";



        /// <summary>
        /// SERVICE FORM
        /// </summary>
        public static string SERVICE_FORM_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Service form - NEW" + @"\" + DateTime.Now.Year + @"\";

        public static string SERVICE_FORM_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\SERVICE FORM.docx";
        public static string SERVICE_FORM_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\Help.docx";



        /// <summary>
        /// TEST PRESSURE OPACITY
        /// </summary>
        public static string TEST_PRESSURE_OPACITY_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Test Pressure Opacity" + @"\" + DateTime.Now.Year + @"\";

        public static string TEST_PRESSURE_OPACITY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TPO.docx";
        public static string TEST_PRESSURE_OPACITY_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\Help.docx";


        /// <summary>
        /// TEST PRESSURE REPORT
        /// </summary>
        public static string TEST_PRESSURE_REPORT_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Test Pressure Report" + @"\" + DateTime.Now.Year + @"\";

        public static string TEST_PRESSURE_REPORT_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TPR.docx";
        public static string TEST_PRESSURE_REPORT_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\Help.docx";

        /// <summary>
        /// UDI14
        /// </summary>
        public static string UDI_14_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\" + "UDI-14" + @"\";

        public static string UDI_14_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";
        public static string UDI_14_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TEST HISTORY.docx";


        /// <summary>
        /// UDI28
        /// </summary>
        public static string UDI_28_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\" + "UDI-28" + @"\";

        public static string UDI_28_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";
        public static string UDI_28_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TEST HISTORY.docx";


        /// <summary>
        /// BOAT UNIT 14
        /// </summary>
        public static string UDI_BOAT_14_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\" + "BOAT UNIT 14" + @"\";

        public static string UDI_BOAT_14_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";
        public static string UDI_BOAT_14_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TEST HISTORY.docx";


        /// <summary>
        /// BOAT UNIT 28
        /// </summary>
        public static string UDI_BOAT_28_PATH = "\\\\10.0.0.254" + @"\" + "Units" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\" + "BOAT UNIT 28" + @"\";

        public static string UDI_BOAT_28_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";
        public static string UDI_BOAT_28_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TEST HISTORY.docx";


        /// <summary>
        /// ADCS
        /// </summary>
        public static string ADCS_ROOT_PATH = "U:" + @"\" + "Acceptance Testing" + @"\" + DateTime.Now.Year + @"\" + "ADCS" + @"\";

        public static string ADCS_HELP_FILE = "\\\\10.0.0.254" + @"\" + "Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";
        public static string ADCS_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\TEST HISTORY.docx";



        /// <summary>
        /// MAIN MENU
        /// </summary>
        public static string MAIN_MENU_HELP_FILE = "\\\\10.0.0.254" + @"\" + "Archive\\HELP UTC TESTS\\ACCEPTING TESTS.docx";

        /// <summary>
        /// WAYBILL
        /// </summary>
        public static string WAYBILL_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\Help.docx";


        /// <summary>
        /// CHOOSE FORM
        /// </summary>
        public static string CHOOSE_FORM_HISTORY_HELP_FILE = "P:\\Archive\\HELP UTC TESTS\\Help.docx";
    }
}