using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDealer
{
    internal class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;
        
        public string connection()
        {
            //con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Konzo\source\repos\CarDealer\CarDealer\dbCarDealer.mdf;Integrated Security=True;";
            con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\CarDealer\CarDealer\dbCarDealer.mdf;Integrated Security=True";
            return con;
        }
        public void executeQuery(string sql)
        {
            try
            {
                cn.ConnectionString = connection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

        }
    }
}
