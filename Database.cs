using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comparlize
{
    class Database
    {
        Boolean isConnect
        {
            get { return isConnect; }
            set { isConnect = value; }
        }
        string conectionsString
        {
            get { return conectionsString; }
            set { conectionsString = value; }
        }
        //need to relise
        public void Add(string table, string data, string id) { }
        public void Remove(string table, string data, string id) { }
        public void Update(string table, string data, string id) { }
        public List<Bitmap> getCarD(string id) { return null; }
        public void Creates(string ConString)
        {

            string path = @"C:\Users\asafl\Documents\SQLPARTcCc.txt";
            if (!File.Exists(path))
            {
                string createText = "CREATE TABLE [dbo].[' Empty ']("
                                + "[Empty0] INT NOT NULL , "
                                + "[Empty1] NCHAR(20) NULL,"
                                + "[Empty2] INT NULL,"
                                + "CONSTRAINT ['pk_Empty'] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]";
                using (SqlCommand cmd = new SqlCommand(createText, new SqlConnection(ConString)))
                    try
                    {



                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();





                    }
                    catch (Exception)
                    {


                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
            }



            String readText = File.ReadAllText(path);
            try
            {
                using (SqlCommand cmd = new SqlCommand(readText, new SqlConnection(ConString)))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();



                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {

                return;
            }
        }

    }
}
