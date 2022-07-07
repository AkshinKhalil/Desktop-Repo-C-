using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Radiokeshfiyyat_30_11_2016
{
    public partial class Istifadeci_muhafize : Form
    {
        public Istifadeci_muhafize()
        {
            InitializeComponent();
        }

        private void btn_cixish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_qeydiyyat_Click(object sender, EventArgs e)
        {
           /* SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFileName=C:\USERS\HP\DOCUMENTS\DATA.MDF;Integrated Security=True;Connect Timeout=30;User Instance=True;");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From LOGIN Where UserName='"+txt_username.Text+"'AND Password='"+txt_password.Text+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count ==1)
            {
                this.Hide();
                Cedvel ss = new Cedvel();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Istifadeci adi ve ya parolu duzgun daxil edin!");
            }*/
            if(txt_username.Text=="Istifadeci" || txt_password.Text=="parol")
            {
                this.Hide();
                RadioKeshfiyyat ss = new RadioKeshfiyyat();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Istifadeci adi ve ya parolu duzgun daxil edin!");
            }
            
        }
    }
}
