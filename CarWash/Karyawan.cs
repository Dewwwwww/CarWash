using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarWash
{
    public partial class Karyawan : Form
    {
        public Karyawan()
        {
            InitializeComponent();
            showDataKaryawan();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dew\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            KNamaTb.Text = "";
            KAlamatTb.Text = "";
            KNomorTb.Text = "";
            KJenisKelaminCb.SelectedIndex = -1;
        }
        private void showDataKaryawan()
        {
            con.Open();
            string query = "select * from KaryawanTbl";
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            SqlCommandBuilder buil  = new SqlCommandBuilder(adap);
            var ds = new DataSet();
            adap.Fill(ds);
            KaryawanDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (KNamaTb.Text == "" || KAlamatTb.Text == "" || KJenisKelaminCb.SelectedIndex == -1 || KNomorTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into KaryawanTbl(KNama,KNomor,KJenisKelamin,KAlamat,KPass) values(@Kn,@Km,@Kj,@Ka,@Kp)", con);
                    cmd.Parameters.AddWithValue("@Kn", KNamaTb.Text);
                    cmd.Parameters.AddWithValue("@Km", KNomorTb.Text);
                    cmd.Parameters.AddWithValue("@Kj", KJenisKelaminCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ka", KAlamatTb.Text);
                    cmd.Parameters.AddWithValue("@Kp", KPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data karyawan tersimpan");
                    con.Close();
                    showDataKaryawan();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void KaryawanDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KNamaTb.Text = KaryawanDGV.SelectedRows[0].Cells[1].Value.ToString();
            KNomorTb.Text = KaryawanDGV.SelectedRows[0].Cells[2].Value.ToString();
            KJenisKelaminCb.SelectedItem = KaryawanDGV.SelectedRows[0].Cells[3].Value.ToString();           
            KAlamatTb.Text = KaryawanDGV.SelectedRows[0].Cells[4].Value.ToString();
            KPassTb.Text = KaryawanDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (KNamaTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(KaryawanDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Click table karyawan");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from KaryawanTbl where KId=@Kid",con);
                cmd.Parameters.AddWithValue("@Kid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Karyawan terhapus");
                con.Close();
                showDataKaryawan();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (KNamaTb.Text == "" || KAlamatTb.Text == "" || KJenisKelaminCb.SelectedIndex == -1 || KNomorTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update KaryawanTbl set KNama=@Kn,KNomor=@Km,KJenisKelamin=@Kj,KPass=@Kp,KAlamat=@Ka where KId=@Kid", con);
                cmd.Parameters.AddWithValue("@Kn", KNamaTb.Text);
                cmd.Parameters.AddWithValue("@Km", KNomorTb.Text);
                cmd.Parameters.AddWithValue("@Kj", KJenisKelaminCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Kp", KPassTb.Text);
                cmd.Parameters.AddWithValue("@Ka", KAlamatTb.Text);
                cmd.Parameters.AddWithValue("@Kid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data karyawan diedit");
                con.Close();
                showDataKaryawan();
                Reset();
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Washs washs = new Washs();
            washs.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}