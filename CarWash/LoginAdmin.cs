using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWash
{
    public partial class LoginAdmin : Form
    {
        public LoginAdmin()
        {
            InitializeComponent();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PasswordAdminTb.Text == "")
            {
                MessageBox.Show("Masukan Passward");
            } else if (PasswordAdminTb.Text == "admin")
            {
                Karyawan karyawan = new Karyawan();
                karyawan.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Passward salah");
            }
        }
    }
}
