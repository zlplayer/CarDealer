using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarDealer.Images
{
    public partial class UserModule : Form
    {
        SqlConnection cn= new SqlConnection();
        SqlCommand cm= new SqlCommand();
        DbConnect dbcon= new DbConnect();
        string title = "Car Dealer ";
        bool check = false;
        UserForm userForm;

        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            this.userForm = user;
            cbRole.SelectedIndex = 1;
        }

        private void UserModule_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Czy na pewno chcesz zarejestrować tego użytkownika?", "Rejestracja Użytkownika", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbUser(name,address,phone,role,dob,password)VALUES(@name,@address,@phone,@role,@dob,@password)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPass.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Użytkownik został pomyślnie zarejestrowany!", title);
                        Clear();
                        userForm.LoadUser();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Czy na pewno chcesz zaktualizować ten rekord?", "Edytuj rekord", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbUser SET name=@name, address=@address, phone=@phone, role=@role, dob=@dob, password=@password WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lbluid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPass.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Dane użytkownika zostały pomyślnie zaktualizowane!", title);
                        Clear();
                        userForm.LoadUser();
                        this.Dispose();
                    }

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Employee")
            {
                this.Height = 453 - 26;
                lblPass.Visible = false;
                txtPass.Visible = false;

            }
            else
            {
                lblPass.Visible = true;
                txtPass.Visible = true;
                this.Height = 453;

            }
        }
        #region Method
        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtPass.Clear();
            cbRole.SelectedIndex = 0;
            dtDob.Value=DateTime.Now;

            btnUpdate.Enabled = false;
        }
        //to check field and date of birth
        public void CheckField()
        {
            if(txtName.Text==""|| txtAddress.Text == "")
            {
                MessageBox.Show("Wymagane pole danych!", "Ostrzeżenie");
                return;
            }
            if (checkAge(dtDob.Value) < 18)
            {
                MessageBox.Show("Użytkownik jest pracownikiem dziecięcym!. Poniżej 18 roku życia", "Ostrzeżenie");
                return;
            }
            check = true;
        }
        // to calculate Age for under 18
        private static int checkAge(DateTime dateofBirth)
        {
            int age=DateTime.Now.Year-dateofBirth.Year;
            if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        #endregion Method

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
