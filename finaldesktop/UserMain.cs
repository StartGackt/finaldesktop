using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finaldesktop
{
    public partial class UserMain : Form
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new UserSearch().Show();
            this.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            new RegisterUser().Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new UserSearch().Show();
            this.Hide();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            new Main().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new UserMoney1().Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Usermoney1_2().Show();
        }
    }
}
