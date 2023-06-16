using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CarDealer
{
    public partial class Dashboard : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Car Dealer ";
        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }
        #region Method
        public int extractData(string str)
        {
            int data = 0;
            try
            {
                cn.Open();
                cm= new SqlCommand("SELECT ISNULL(SUM(pqty), 0)AS qty FROM tbProduct WHERE pcategory = '"+str+"'",cn);
                data =int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message,title);
            }
            return data;
        }
        #endregion Method

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblCoupe.Text = extractData("Coupe").ToString();
            lblSedan.Text = extractData("Sedan").ToString();
            lblKombi.Text = extractData("Kombi").ToString();
            lblSuv.Text = extractData("Suv").ToString();


        }
    }
}
