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

namespace CarDealer
{
    public partial class LoginForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Car Dealer ";
        public LoginForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wyjść z aplikacji?", "Potwierdzać", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _name = "", _role = "";
                cn.Open();
                cm = new SqlCommand("SELECT name, role FROM tbUser WHERE name=@name AND password=@password", cn);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.Parameters.AddWithValue("@password",txtPassword.Text);
                dr= cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    _name = dr["name"].ToString();
                    _role = dr["role"].ToString();
                    MessageBox.Show("Powitanie  " + _name + " |", "DOSTĘP PRZYZNANY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main= new MainForm();
                    main.lblUsername.Text = _name;
                    main.lblRole.Text = _role;
                    if (_role == "Administrator")
                    {
                        main.btnUser.Enabled = true;
                    }
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowa nazwa użytkownika i hasło!", "BRAK DOSTĘPU", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex) 
            {
                dr.Close();
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }
        private void btnForGet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Skontaktuj się ze swoim SZEFEM!", "ZAPOMNIAŁEŚ HASŁA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
