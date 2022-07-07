using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;

namespace Radiokeshfiyyat_30_11_2016
{
    public partial class Cedvel : Form
    {
        public Cedvel()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        public static string comPortNo = "";
        SerialPort com_port = new SerialPort();
        int btnClicked = 0;

        private void Cedvel_Load(object sender, EventArgs e)
        {
            DataGridViewColumn dataGridViewColumn = GridView1.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.Red;
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.Yellow;

            dtpBegin.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        }

        private void lbl_load_siqnallar_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = "Siqnalların təhlili";
            btnClicked = 1;
            Cedvel_sechimi();

        }

        private void lbl_yeni_siqnallar_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = "Yeni siqnalların təhlili";
            btnClicked = 2;
            Cedvel_sechimi();
        }

        private void lbl_shubheli_Click_1(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = "Şübhəli siqnalların təhlili";
            btnClicked = 3;
            Cedvel_sechimi();
        }

        private void lbl_qorunan_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = "Qorunan siqnalların təhlili";
            btnClicked = 4;
            Cedvel_sechimi();
        }

        private void Cedvel_sechimi()
        {            
            //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Keshfiyyat;Integrated Security=True");
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            SqlCommand com = new SqlCommand("DATEOFWEEK", con);
            com.Parameters.AddWithValue("@BDATE", dtpBegin.Value.ToString("yyyy-MM-dd"));
            com.Parameters.AddWithValue("@EDATE", dtpEnd.Value.ToString("yyyy-MM-dd"));
            com.Parameters.AddWithValue("@TABLETYPE", btnClicked);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                da.Fill(ds);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            GridView1.DataSource = ds.Tables[0];
        }

        private void txt_tezlik_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Keshfiyyat;Integrated Security=True");
            SqlCommand com = new SqlCommand("DATEOFWEEK", con);
            com.Parameters.AddWithValue("@BDATE", dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            com.Parameters.AddWithValue("@EDATE", dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            com.Parameters.AddWithValue("@TABLETYPE", btnClicked);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                da.Fill(ds);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = string.Format("MTezlikler LIKE '{0}%'", txt_tezlik.Text.ToString());
            GridView1.DataSource = dv;
        }
    }
}