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
    public partial class Washs : Form
    {
        public Washs()
        {
            InitializeComponent();
            GetNamaCust();
            GetNamaService();
            NamaKaryawan.Text = LoginUser.Username;
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dew\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetNamaCust()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CNama from CustomerTbl",con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNama", typeof(string));
            dt.Load(dr);
            NamaCustomerCb.ValueMember = "CNama";
            NamaCustomerCb.DataSource = dt;
            con.Close();
        }
        private void GetNamaService()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select SNama from ServiceTbl", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SNama", typeof(string));
            dt.Load(dr);
            ServiceCb.ValueMember = "SNama";
            ServiceCb.DataSource = dt;
            con.Close();
        }
        private void GetNomerCust()
        {
            con.Open();
            string query = "select * from CustomerTbl where CNama='" + NamaCustomerCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query,con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                WNomorTb.Text = dr["CNomor"].ToString();
            }
            con.Close();
        }
        private void GetHargaService()
        {
            con.Open();
            string query = "select * from ServiceTbl where SNama='" + ServiceCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                HargaTb.Text = dr["SHarga"].ToString();
            }
            con.Close();
        }
        int i = 0, total = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (HargaTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                DataGridViewRow dgv = new DataGridViewRow();
                dgv.CreateCells(ServiceDGV);
                dgv.Cells[0].Value = i + 1;
                dgv.Cells[1].Value = ServiceCb.SelectedValue.ToString();
                dgv.Cells[2].Value = HargaTb.Text;
                ServiceDGV.Rows.Add(dgv);
                i++;
                total = total + Convert.ToInt32(HargaTb.Text);
                Totall.Text = "Rp " + total;
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NamaCustomerCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetNomerCust();
        }
        private void Reset()
        {
            NamaCustomerCb.SelectedIndex = -1;
            ServiceCb.SelectedIndex = -1;
            WNomorTb.Text = "";
            HargaTb.Text = "";
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NamaCustomerCb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into InvoiceTbl(CustNama,CustNomor,KNama,Amt,IDate) values(@Cn,@Cm,@Kn,@Am,@Id)", con);
                    cmd.Parameters.AddWithValue("@Cn", NamaCustomerCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Cm", WNomorTb.Text);
                    cmd.Parameters.AddWithValue("@Kn", NamaKaryawan.Text);
                    cmd.Parameters.AddWithValue("@Am", total);
                    cmd.Parameters.AddWithValue("@Id", Tanggal.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Invoice tersimpan");
                    con.Close();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Karyawan karyawan = new Karyawan();
            //karyawan.Show();
            //this.Hide();
        }

        private void ServiceCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetHargaService();
        }
    }
}
