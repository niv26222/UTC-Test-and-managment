using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Product_List
{
    public class General
    {

        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public static void ActOnDb(string actionType, string DBValue, string DBTableName, string DBreferenceToRestore, string referenceToRestore, Control controlToRestore)
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                if (actionType == "SELECT")
                {
                    try
                    {
                        cmd = new SQLiteCommand();
                        cmd.CommandText = "" + actionType + " " + DBValue + " FROM " + DBTableName + " WHERE " + DBreferenceToRestore + " = '" + referenceToRestore + "';";
                        cmd.Connection = conn;
                        conn.Open();
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            controlToRestore.Text = Convert.ToString(dr[0]);
                        }

                        dr.Close();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (actionType == "DELETE")//UPDATE
                {
                    try
                    {
                        cmd = new SQLiteCommand();
                        cmd.CommandText = "" + actionType + " " + DBValue + " FROM " + DBTableName + " WHERE " + DBreferenceToRestore + " = '" + referenceToRestore + "';";
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
                else if (actionType == "UPDATE")
                {

                    
                }
            }
        }



        public static string GetPath()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                }
            }
            return filePath;
        }
    }
}
