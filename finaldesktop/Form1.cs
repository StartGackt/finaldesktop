using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace finaldesktop
{
    public partial class Form1 : Form
    {
        private string connString = "server=localhost;user=root;database=loginform;port=3306;password=";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string iduser = txtusername.Text.Trim();
            string idpass = txtpassword.Text.Trim();
            if (iduser == "" || idpass == "")
            {
                MessageBox.Show("Please input Username and Password");
                return;
            }
            else
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM users WHERE username = @username AND password = @password LIMIT 1";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", iduser);
                        cmd.Parameters.AddWithValue("@password", idpass);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); // Added '=' to initialize DataTable
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Login Successful");
                            conn.Close();
                            new Main().Show();
                            this.Hide();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Login Failed");
                            conn.Close();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Register().Show();
            this.Hide();
        }
    }
}
