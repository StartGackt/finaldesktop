using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finaldesktop
{
    public partial class UserMoney1_1 : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public UserMoney1_1()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
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
                            string iduser = reader.GetString("username");
                            string idfamily = reader.GetString("family");
                            string idphone = reader.GetString("phone");
                            string idfullname = reader.GetString("fullname");

                            // Set the retrieved values to your textboxes or variables
                            txtfamily.Text = idfamily;
                            txtphone.Text = idphone;
                            txtfullname.Text = idfullname;
                        }
                        else
                        {
                            MessageBox.Show("User not found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message);
            }
        }
    }
}
