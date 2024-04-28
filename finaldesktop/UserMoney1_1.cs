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
            txtmoney.TextChanged += txtmoney_TextChanged;
            txtmoney1.TextChanged += txtmoney1_TextChanged;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void txtmoney_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalMoney();
        }

        private void txtmoney1_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalMoney();
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
            txtmoney.Text = "0";
            txtmoney1.Text = string.Empty;
            txtmoneysucc.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void CalculateTotalMoney()
        {
            try
            {
                decimal idmoney;
                if (!decimal.TryParse(txtmoney.Text, out idmoney))
                {
                    MessageBox.Show("Please enter a valid value for Money.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmoney.Text = "0"; // Reset to default value
                    return;
                }

                decimal idmoney1;
                if (!decimal.TryParse(txtmoney1.Text, out idmoney1))
                {
                   
                    txtmoney1.Text = "0"; // Reset to default value
                    return;
                }

                decimal idmoneysucc = idmoney + idmoney1;
                txtmoneysucc.Text = idmoneysucc.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while calculating total money: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO `money1` (`username`, `idmoney`, `idmoney1`, `idmoneysucc`, `iddate`, `idstatus`) VALUES (@username, @idmoney, @idmoney1, @idmoneysucc, @iddate, 'idstatus')";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());
                    cmd.Parameters.AddWithValue("@idmoney", txtmoney.Text);
                    cmd.Parameters.AddWithValue("@idmoney1", txtmoney1.Text);
                    cmd.Parameters.AddWithValue("@idmoneysucc", txtmoneysucc.Text);
                    cmd.Parameters.AddWithValue("@iddate", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data saved successfully!");
                        // หลังจากบันทึกเสร็จสามารถอัปเดต idmoney ด้วยข้อมูลใหม่ได้
                        UpdateMoney1Data(txtuser.Text.Trim(), txtmoneysucc.Text); // อัปเดต idmoney ด้วย idmoneysucc
                        // โหลดข้อมูลใหม่เพื่ออัปเดตในฟอร์ม
                        LoadMoney1Data(txtuser.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving data: " + ex.Message);
            }
        }

        private void UpdateMoney1Data(string username, string idmoney)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE money1 SET idmoney = @idmoney WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idmoney", idmoney);
                    cmd.Parameters.AddWithValue("@username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //MessageBox.Show("idmoney updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating idmoney: " + ex.Message);
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void serach_Click(object sender, EventArgs e)
        {

            string username = txtuser.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username to search.", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadUserData();
        }

        private void del_Click(object sender, EventArgs e)
        {
            string username = txtuser.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username to delete.", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM money1 WHERE username = @username";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", username);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully!");
                            ClearFields(); // Clear all fields after deletion
                        }
                        else
                        {
                            MessageBox.Show("No record found to delete.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting record: " + ex.Message);
                }
            }
        }
    }
}
