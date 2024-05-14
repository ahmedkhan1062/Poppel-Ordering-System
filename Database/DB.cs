using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PoppelOrder.Properties;

namespace PoppelOrder.Database
{
    public class DB
    {
        private string strConn = Settings.Default.PoppelInfoConnectionString;
        protected SqlConnection cnMain;
        public DB()
        {
            try
            {
                cnMain = new SqlConnection(strConn);
            }
            catch (SystemException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error");
                return;
            }

        }

        protected bool UpdateDataSource(SqlCommand currentCommand)
        {
            bool success;
            try
            {
                cnMain.Open();
                currentCommand.CommandType = CommandType.Text;
                currentCommand.ExecuteNonQuery();
               // currentCommand.Dispose();
                cnMain.Close();
                success = true;
            }
            catch (Exception errObj)
            {
                MessageBox.Show(errObj.Message + " " + errObj.StackTrace);
                success = false;
                cnMain.Close();
            }
           
            return success;

        }
    }
}
