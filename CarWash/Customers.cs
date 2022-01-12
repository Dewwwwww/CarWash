using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWash
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            showDataCustomer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dew\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            CNamaTb.Text = "";
            CAlamatTb.Text = "";
            CMobilTb.Text = "";
            CNomorTb.Text = "";
            CStatusCb.SelectedIndex = -1;
        }
        private void showDataCustomer()
        {
            con.Open();
            string query = "select * from CustomerTbl";
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            SqlCommandBuilder buil = new SqlCommandBuilder(adap);
            var ds = new DataSet();
            adap.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNamaTb.Text == "" || CAlamatTb.Text == "" || CStatusCb.SelectedIndex == -1 || CNomorTb.Text == "" || CMobilTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CustomerTbl(CNama,CNomor,CAlamat,CStatus,CMobil) values(@Cn,@Cm,@Ca,@Cs,@Cmb)", con);
                    cmd.Parameters.AddWithValue("@Cn", CNamaTb.Text);
                    cmd.Parameters.AddWithValue("@Cm", CNomorTb.Text);
                    cmd.Parameters.AddWithValue("@Ca", CAlamatTb.Text);
                    cmd.Parameters.AddWithValue("@Cs", CStatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cmb", CMobilTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data customer tersimpan");
                    con.Close();
                    showDataCustomer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Click table curtomer");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CId=@Cid", con);
                cmd.Parameters.AddWithValue("@Cid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Customer terhapus");
                con.Close();
                showDataCustomer();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CNamaTb.Text == "" || CAlamatTb.Text == "" || CStatusCb.SelectedIndex == -1 || CNomorTb.Text == "" || CMobilTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update CustomerTbl set CNama=@Cn,CNomor=@Cm,CAlamat=@Ca,CStatus=@Cs,CMobil=@Cmb where CId=@Cid", con);
                cmd.Parameters.AddWithValue("@Cn", CNamaTb.Text);
                cmd.Parameters.AddWithValue("@Cm", CNomorTb.Text);
                cmd.Parameters.AddWithValue("@Ca", CAlamatTb.Text);
                cmd.Parameters.AddWithValue("@Cs", CStatusCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Cmb", CMobilTb.Text);
                cmd.Parameters.AddWithValue("@Cid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data customer diedit");
                con.Close();
                showDataCustomer();
                Reset();
            }
        }
        
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CNamaTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CNomorTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CAlamatTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CStatusCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            CMobilTb.Text = CustomerDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CNamaTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Karyawan karyawan = new Karyawan();
            //karyawan.Show();
            //this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Washs washs = new Washs();
            washs.Show();
            this.Hide();
        }
    }
}