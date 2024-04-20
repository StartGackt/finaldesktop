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
    public partial class RegisterUser : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public RegisterUser()
        {
            InitializeComponent();
        }

        private void btngoback_Click(object sender, EventArgs e)
        {
            new UserMain().Show();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string iduser = txtuser.Text.Trim();
            string idfamily = txtfamily.Text.Trim();
            string idciz = txtciz.Text.Trim();
            string idphone = txtphone.Text.Trim();
            string idfullname = txtfullname.Text.Trim();
            string idaddr = txtaddr.Text.Trim();
            string idthis = txtthis.Text.Trim();
            string idthisphone = txtthisphone.Text.Trim();
            string idthisphone1 = txtthisphone1.Text.Trim();
            string idthis1 = txtthis1.Text.Trim();
           

            if (iduser == "")
            {
                MessageBox.Show("Please input Username");
            }
            else if (idfamily == "")
            {
                MessageBox.Show("Please input Family");
            }
            else if (idciz == "")
            {
                MessageBox.Show("Please input Ciz");
            }
            else if (idphone == "")
            {
                MessageBox.Show("Please input Phone");
            }
            else if (idfullname == "")
            {
                MessageBox.Show("Please input Fullname");

            }
            else if (idaddr == "")
            {
                MessageBox.Show("Please input Address");
            }
            else if (idthis == "")
            {
                MessageBox.Show("Please input This");
            }
            else if (idthisphone == "")
            {
                MessageBox.Show("Please input This Phone");
            }
            else if (idthisphone1 == "")
            {
                MessageBox.Show("Please input This Phone1");
            }
            else
            {
                // register user
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();

                        string query = "INSERT INTO user (username, family, ciz, phone, fullname, address, this, this_phone, this1, this_phone1) " +
                                       "VALUES (@Username, @Family, @Ciz, @Phone, @Fullname, @Address, @This, @ThisPhone, @This1, @ThisPhone1)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Username", iduser);
                        cmd.Parameters.AddWithValue("@Family", idfamily);
                        cmd.Parameters.AddWithValue("@Ciz", idciz);
                        cmd.Parameters.AddWithValue("@Phone", idphone);
                        cmd.Parameters.AddWithValue("@Fullname", idfullname);
                        cmd.Parameters.AddWithValue("@Address", idaddr);
                        cmd.Parameters.AddWithValue("@This", idthis);
                        cmd.Parameters.AddWithValue("@ThisPhone", idthisphone);
                        cmd.Parameters.AddWithValue("@This1", idthis1);
                        cmd.Parameters.AddWithValue("@ThisPhone1", idthisphone1);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
