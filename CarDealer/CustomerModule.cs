﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Xml.Linq;

namespace CarDealer
{
    public partial class CustomerModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Car Dealer ";
        bool check = false;
        CustomerForm customer;
        public CustomerModule(CustomerForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            this.customer = form;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Czy na pewno chcesz zarejestrować tego klienta?", "Rejestracja klienta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(name,address,phone)VALUES(@name,@address,@phone)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Klient został pomyślnie zarejestrowany!", title);
                        Clear();
                        customer.LoadCustomer();
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
                    if (MessageBox.Show("Czy na pewno chcesz edytować ten rekord?", "Edycja rekordu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCustomer SET name=@name, address=@address, phone=@phone WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lblcid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Dane klienta zostały pomyślnie zaktualizowane!", title);
                        Clear();
                        customer.LoadCustomer();
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
        #region Method
        public void CheckField()
        {
            if (txtName.Text == "" || txtAddress.Text == ""|| txtPhone.Text=="")
            {
            MessageBox.Show("Pole danych wymagane!", "Ostrzeżenie");
            return;
            }
           
            check = true;
        }
       
        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion Method
    }

}