using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finaldesktop
{
    public partial class Register : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public Register()
        {
            InitializeComponent();
        }

        private void btngoback_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string iduser = txtusername.Text.Trim();
            string idpass = txtpassword.Text.Trim();
            string idciz = txtciz.Text.Trim();
            string idfullname = txtfullname.Text.Trim();
            string idphone = txtphone.Text.Trim();
            string idaddr = txtaddr.Text.Trim();
            string idthis = txtsel.Text.Trim();

            if (iduser == "")
            {
                MessageBox.Show("Please input Username");
            }
            else if (idpass == "")
            {
                MessageBox.Show("Please input Password");
            }
            else if (idciz == "")
            {
                MessageBox.Show("Please input Ciz");
            }
            else if (idfullname == "")
            {
                MessageBox.Show("Please input Fullname");
            }
            else if (idphone == "")
            {
                MessageBox.Show("Please input Phone");
            }
            else if (idaddr == "")
            {
                MessageBox.Show("Please input Address");
            }
            else if (idthis == "")
            {
                MessageBox.Show("Please input This");
            }
            else
            {
                // register logic
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();
                        string query = "INSERT INTO users (username, password, id_card_number, full_name, phone_number, address, position) VALUES (@iduser, @idpass, @idciz, @idfullname, @idphone, @idaddr, @idthis)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@iduser", iduser);
                        cmd.Parameters.AddWithValue("@idpass", idpass);
                        cmd.Parameters.AddWithValue("@idciz", idciz);
                        cmd.Parameters.AddWithValue("@idfullname", idfullname);
                        cmd.Parameters.AddWithValue("@idphone", idphone);
                        cmd.Parameters.AddWithValue("@idaddr", idaddr);
                        cmd.Parameters.AddWithValue("@idthis", idthis);
                        int status = cmd.ExecuteNonQuery();
                        if (status > 0)
                        {
                            MessageBox.Show("User registered successfully!");
                            conn.Close();
                            new Form1().Show();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
