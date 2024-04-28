using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finaldesktop
{
    public partial class Usermoney1_2 : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public Usermoney1_2()
        {
            InitializeComponent();
            txtuser.TextChanged += txtuser_TextChanged;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM user WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string idfamily = reader["family"].ToString();
                            string idfullname = reader["fullname"].ToString();

                            txtfamily.Text = idfamily;
                            txtfullname.Text = idfullname;

                            LoadMoney2Data(txtuser.Text.Trim());
                        }
                        else
                        {
                            ClearFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadMoney2Data(string username)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM money2 WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string idnumberpage = reader["numberpage"].ToString();
                            string idpay = reader["pay"].ToString();
                            string idmoneyvat = reader["moneyvat"].ToString();
                            string idmoneypay = reader["moneypay"].ToString();
                            string moneytotal = reader["moneytotal"].ToString();

                            txtnumberpage.Text = idnumberpage;
                            txtpay.Text = idpay;
                            txtmoneyvat.Text = idmoneyvat;
                            txtmoneypay.Text = idmoneypay;
                            txtmoneytotal.Text = moneytotal;
                        }
                        else
                        {
                            // Handle case where no data found in money2 table
                            ClearMoneyFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            // Clear user information fields
            txtfamily.Text = "";
            txtfullname.Text = "";
        }

        private void ClearMoneyFields()
        {
            // Clear money fields
            txtnumberpage.Text = "";
            txtpay.Text = "";
            txtmoneyvat.Text = "";
            txtmoneypay.Text = "";
            txtmoneytotal.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Implement this method for button click event
        }
    }
}
