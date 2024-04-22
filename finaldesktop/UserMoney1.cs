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
    public partial class UserMoney1 : Form
    {
        public UserMoney1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Usermoney1_1().ShowDialog();
        }

        private void UserMoney1_Load(object sender, EventArgs e)
        {

        }
    }
}
