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
    public partial class Services : Form
    {
        public Services()
        {
            InitializeComponent();
            showDataService();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dew\Documents\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            ServiceTb.Text = "";
            HargaTb.Text = "";
        }
        private void showDataService()
        {
            con.Open();
            string query = "select * from ServiceTbl";
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            SqlCommandBuilder buil = new SqlCommandBuilder(adap);
            var ds = new DataSet();
            adap.Fill(ds);
            ServiceDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ServiceTb.Text == "" || HargaTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ServiceTbl(SNama,SHarga) values(@Sn,@Sh)", con);
                    cmd.Parameters.AddWithValue("@Sn", ServiceTb.Text);
                    cmd.Parameters.AddWithValue("@Sh", HargaTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data service tersimpan");
                    con.Close();
                    showDataService();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Click table service");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from ServiceTbl where SId=@Sid", con);
                cmd.Parameters.AddWithValue("@Sid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data service terhapus");
                con.Close();
                showDataService();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ServiceTb.Text == "" || HargaTb.Text == "")
            {
                MessageBox.Show("Tidak ada data");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ServiceTbl set SNama=@Sn,SHarga=@Sh where SId=@Sid", con);
                cmd.Parameters.AddWithValue("@Sn", ServiceTb.Text);
                cmd.Parameters.AddWithValue("@Sh", HargaTb.Text);
                cmd.Parameters.AddWithValue("@Sid", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data customer diedit");
                con.Close();
                showDataService();
                Reset();
            }
        }

        private void ServiceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ServiceTb.Text = ServiceDGV.SelectedRows[0].Cells[1].Value.ToString();
            HargaTb.Text = ServiceDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (ServiceTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ServiceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}