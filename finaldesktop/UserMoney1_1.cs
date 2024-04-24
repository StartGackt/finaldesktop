using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finaldesktop
{
    public partial class Usermoney1_1 : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public Usermoney1_1()
        {
            InitializeComponent();
            txtuser.TextChanged += txtuser_TextChanged;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
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
                            string idphone = reader["phone"].ToString();
                            string idfullname = reader["fullname"].ToString();

                            txtfamily.Text = idfamily;
                            txtphone.Text = idphone;
                            txtfullname.Text = idfullname;

                            LoadMoney1Data(txtuser.Text.Trim());
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
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message);
            }
        }

        private void LoadMoney1Data(string username)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM money1 WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string idmoney = reader["idmoney"].ToString();
                            string idmoney1 = reader["idmoney1"].ToString();
                            string idmoneysucc = reader["idmoneysucc"].ToString();
                            DateTime iddate = reader.GetDateTime("iddate");

                            txtmoney.Text = idmoney;
                            txtmoney1.Text = idmoney1;
                            txtmoneysucc.Text = idmoneysucc;
                            dateTimePicker1.Value = iddate;
                        }
                        else
                        {
                            ClearMoneyFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving money1 data: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtfamily.Text = string.Empty;
            txtphone.Text = string.Empty;
            txtfullname.Text = string.Empty;
            ClearMoneyFields();
        }

        private void ClearMoneyFields()
        {
            txtmoney.Text = string.Empty;
            txtmoney1.Text = string.Empty;
            txtmoneysucc.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
