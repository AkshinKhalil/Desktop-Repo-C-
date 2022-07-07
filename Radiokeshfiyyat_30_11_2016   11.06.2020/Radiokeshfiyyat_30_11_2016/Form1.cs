using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;
using System.Globalization;

namespace Radiokeshfiyyat_30_11_2016
{
    public partial class RadioKeshfiyyat : Form
    {
        BindingSource bs = new BindingSource();

        public static string comPortNo = "";
        SerialPort com_port = new SerialPort();

        int btnClicked = 0;

        //modulyasiya sechimi
        string melum_modulyasiya = "";
        string modulyasiya = "";

        //addim sechimi
        string melum_addim = "";
        double addim;

        //period sechimi
        int period;

        //Ashagi/ Yuxari diapazonlar
        double min_Hz;
        double max_Hz;

        //Gelen melumatlarin deyiwenleri
        string result;
        int m = 0;
        int i;
        List<String> signal_seviyye = new List<String>();
        List<String> tezlik = new List<String>();

        private BackgroundWorker bw = new BackgroundWorker();

        public RadioKeshfiyyat()
        {
            InitializeComponent();
        }

        private void RadioKeshfiyyat_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            button1.Visible = false;
            richTextBox1.Visible = false;
            btn_delete.Visible = false;
            btn_sech_qorunan.Visible = false;
            btn_sech_shubheli.Visible = false;

            dtpBegin.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpBegin.Value.AddDays(-10);
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbPorts.Items.Add(port);
            }

            //string constring = (@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            string constring = (@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");// kompyuter ferqi

            using (SqlConnection con = new SqlConnection(constring))
            {
                string cedvel = "";
                cedvel = "DELETE FROM Melum_Tezlikler WHERE MT_TARIX <= @MT_TARIX";
                using (SqlCommand cmd = new SqlCommand(cedvel, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@MT_TARIX", DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss"));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        private void cbPorts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!com_port.IsOpen)
            {
                com_port.PortName = cbPorts.SelectedItem.ToString();
                comPortNo = cbPorts.SelectedItem.ToString();
                com_port.BaudRate = 9600;
                com_port.DataBits = 8;
                com_port.Parity = Parity.None;
                com_port.StopBits = StopBits.Two;
                com_port.RtsEnable = true;
                com_port.DtrEnable = true;
                com_port.WriteTimeout = 500;
                com_port.ReadTimeout = 500;
                com_port.Handshake = Handshake.RequestToSend;
                com_port.Open();
            }
        }

        //Portdan gelen melumatlar
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            result = com_port.ReadExisting();
            if (result != String.Empty)
            {
                switch (btnClicked)
                {
                    case 1:
                        Thread.Sleep(300);
                        if (result.Length > 22)
                        {
                            string[] test = result.Split('L');
                            for (i = 1; i < test.Length - 1; i++)
                            {
                                signal_seviyye.Add(test[i].Substring(1, 3));
                                tezlik.Add(test[i].Substring(10, 4) + "." + test[i].Substring(14, 4));

                                if (signal_seviyye[i - 1] != "000")
                                {
                                    Invoke(new Action(() => dataGridView1.Rows.Add()));
                                    Invoke(new Action(() => dataGridView1.Rows[m].Cells[1].Value = tezlik[i - 1]));
                                    Invoke(new Action(() => dataGridView1.Rows[m].Cells[2].Value = signal_seviyye[i - 1]));
                                    Invoke(new Action(() => dataGridView1.Rows[m].Cells[3].Value = modulyasiya));
                                    Invoke(new Action(() => dataGridView1.Rows[m].Cells[4].Value = DateTime.Now));
                                    MelumTezliklerin_Yazilmasi();
                                    m++;
                                }
                            }
                            Invoke(new Action(() => richTextBox1.AppendText(result)));
                            result = "";
                        }
                        break;
                    case 2:
                        Thread.Sleep(100);
                        if (result.Length > 22)
                        {
                            bool t = false;
                            string[] test = result.Split('L');
                            for (i = 1; i < test.Length - 1; i++)
                            {
                                signal_seviyye.Add(test[i].Substring(1, 3));
                                tezlik.Add(test[i].Substring(10, 4) + "." + test[i].Substring(14, 4));

                                if (signal_seviyye[i - 1] != "000")
                                {
                                    for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                                    {
                                        if (tezlik[i - 1] == dataGridView1.Rows[j].Cells[1].Value.ToString())
                                        {
                                            t = true;
                                        }
                                    }
                                    if (!t)
                                    {
                                        Invoke(new Action(() => dataGridView_New_HZ.Rows.Add()));
                                        Invoke(new Action(() => dataGridView_New_HZ.Rows[m].Cells[1].Value = tezlik[i - 1]));
                                        Invoke(new Action(() => dataGridView_New_HZ.Rows[m].Cells[2].Value = signal_seviyye[i - 1]));
                                        Invoke(new Action(() => dataGridView_New_HZ.Rows[m].Cells[3].Value = modulyasiya));
                                        Invoke(new Action(() => dataGridView_New_HZ.Rows[m].Cells[4].Value = DateTime.Now));
                                        MelumTezliklerin_Yazilmasi();
                                        m++;
                                    }
                                    t = false;
                                }
                            }
                            Invoke(new Action(() => richTextBox1.AppendText(result)));
                            result = "";
                        }
                        break;
                }
            }
        }
        //Tezliklerin axtarishi ve axtarishin dayandirilmasi
        private void btn_search_Hz_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_min_Hz.Text) || string.IsNullOrWhiteSpace(txt_max_Hz.Text) || string.IsNullOrWhiteSpace(cmb_melum_modulyasiya.Text)
                    || string.IsNullOrWhiteSpace(cmb_melum_addim.Text) || string.IsNullOrWhiteSpace(numericUpDown1.Text))
                {
                    MessageBox.Show("Axtarış məlumatlarını doldurun!");
                }
                else
                {
                    tezlik.Clear();
                    signal_seviyye.Clear();
                    //  result = "";
                    btn_delete.Visible = false;
                    btn_sech_qorunan.Visible = false;
                    btn_sech_shubheli.Visible = false;
                    m = 0;
                    com_port.Close();
                    dataGridView1.Rows.Clear();
                    com_port.Open();

                    modulyasiya = cmb_melum_modulyasiya.Text;
                    melum_addim = cmb_melum_addim.Text;
                    period = Convert.ToInt32(numericUpDown1.Value.ToString());
                    min_Hz = Double.Parse(txt_min_Hz.Text, CultureInfo.InvariantCulture);
                    max_Hz = Double.Parse(txt_max_Hz.Text, CultureInfo.InvariantCulture);
                    lbl_tezliyBashliq.Text = (min_Hz + " -- " + max_Hz + " (MHz) tezliklər arası axtarış");

                    modulyasiya_commands();
                    addim_commands();
                    com_port.WriteLine("ST" + addim + "\r");
                    //com_port.WriteLine("ST" + 0.5 + "\r");
                    com_port.WriteLine("MD" + melum_modulyasiya + "\r");
                    com_port.WriteLine("RF" + min_Hz + "\r");
                    com_port.WriteLine("LC1\r");
                    btnClicked = 1;
                    com_port.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                    while (min_Hz < max_Hz)
                    {
                        min_Hz = min_Hz + addim;
                        //min_Hz = min_Hz + 0.5;
                        NumberFormatInfo nfi = new NumberFormatInfo();
                        nfi.NumberGroupSeparator = ".";
                        com_port.WriteLine("RF" + min_Hz.ToString(nfi) + "\r");
                        com_port.WriteLine("LC1\r");
                        btnClicked = 1;
                        com_port.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                        Thread.Sleep(300);
                    }
                }
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_min_Hz.Text) || string.IsNullOrWhiteSpace(txt_max_Hz.Text) || string.IsNullOrWhiteSpace(cmb_melum_modulyasiya.Text)
                    || string.IsNullOrWhiteSpace(cmb_melum_addim.Text) || string.IsNullOrWhiteSpace(numericUpDown1.Text))
                {
                    MessageBox.Show("Axtarış məlumatlarını doldurun!");
                }
                else
                {
                    btn_delete.Visible = false;
                    btn_sech_qorunan.Visible = false;
                    btn_sech_shubheli.Visible = false;
                    btn_search_Hz_Click(sender, e);
                    timer1.Interval = Convert.ToInt32(numericUpDown1.Value.ToString()) * 60000;
                    timer1.Enabled = !timer1.Enabled;
                    if (timer1.Enabled)
                    {
                        btnStop.Text = "Axtarış dayansın";
                        btnStop.BackColor = Color.Red;
                    }
                    else
                    {
                        btnStop.Text = "Avtomatik axtarış";
                        btnStop.BackColor = Color.LimeGreen;
                    }
                }
            }
        }
        //Yeni tezliklerin axtarishi ve axtarishin dayandirilmasi
        private void btn_new_search_Hz_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_min_Hz.Text) || string.IsNullOrWhiteSpace(txt_max_Hz.Text) || string.IsNullOrWhiteSpace(cmb_melum_modulyasiya.Text)
                    || string.IsNullOrWhiteSpace(cmb_melum_addim.Text) || string.IsNullOrWhiteSpace(numericUpDown1.Text))
                {
                    MessageBox.Show("Axtarış məlumatlarını doldurun!");
                }
                else
                {
                    tezlik.Clear();
                    signal_seviyye.Clear();
                    m = 0;
                    com_port.Close();
                    dataGridView_New_HZ.Rows.Clear();
                    com_port.Open();

                    modulyasiya = cmb_melum_modulyasiya.Text;
                    melum_addim = cmb_melum_addim.Text;
                    period = Convert.ToInt32(numericUpDown1.Value.ToString());
                    min_Hz = Double.Parse(txt_min_Hz.Text, CultureInfo.InvariantCulture);
                    max_Hz = Double.Parse(txt_max_Hz.Text, CultureInfo.InvariantCulture);
                    lbl_Yeni_tezliyBashliq.Text = (min_Hz + " -- " + max_Hz + " (MHz) tezliklər arası yeni axtarış");

                    modulyasiya_commands();
                    addim_commands();
                    com_port.WriteLine("ST" + addim + "\r");
                    com_port.WriteLine("MD" + melum_modulyasiya + "\r");
                    com_port.WriteLine("RF" + min_Hz + "\r");
                    com_port.WriteLine("LC1\r");
                    btnClicked = 2;
                    com_port.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                    while (min_Hz < max_Hz)
                    {
                        min_Hz = min_Hz + addim;
                        NumberFormatInfo nfi = new NumberFormatInfo();
                        nfi.NumberGroupSeparator = ".";
                        com_port.WriteLine("RF" + min_Hz.ToString(nfi) + "\r");
                        com_port.WriteLine("LC1\r");
                        btnClicked = 2;
                        com_port.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                        Thread.Sleep(300);
                    }
                }
            }
        }
        private void btnStop_new_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_min_Hz.Text) || string.IsNullOrWhiteSpace(txt_max_Hz.Text) || string.IsNullOrWhiteSpace(cmb_melum_modulyasiya.Text)
                    || string.IsNullOrWhiteSpace(cmb_melum_addim.Text) || string.IsNullOrWhiteSpace(numericUpDown1.Text))
                {
                    MessageBox.Show("Axtarış məlumatlarını doldurun!");
                }
                else
                {
                    btn_new_search_Hz_Click(sender, e);
                    timer2.Interval = Convert.ToInt32(numericUpDown1.Value.ToString()) * 60000;
                    timer2.Enabled = !timer2.Enabled;
                    if (timer2.Enabled)
                    {
                        btnStop_new.Text = "Axtarış dayansın";
                        btnStop_new.BackColor = Color.Red;
                    }
                    else
                    {
                        btnStop_new.Text = "Avtomatik axtarış";
                        btnStop_new.BackColor = Color.LimeGreen;
                    }
                }
            }
        }

        //Cedvellerin yuklenmesi
        private void Cedveli_Yukle()
        {
            //string constring = @"Data Source=.;Initial Catalog=Keshfiyyat; User id=sa; Password=123456; Integrated Security=True";
            //SqlConnection con = new SqlConnection(@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            string cedvel = "";
            if (btnClicked == 3)
            {
                cedvel = "SELECT * FROM MELUM_TEZLIKLER WHERE MT_TARIX >= '" + (dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")) +
                         "' AND  MT_TARIX <= '" + dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND MT_TIP='" + 1 + "'";  //umumi siqnallar=1
            }
            else if (btnClicked == 4)
            {
                cedvel = "SELECT * FROM Melum_Tezlikler  WHERE MT_TARIX >= '" + (dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")) +
                         "' AND  MT_TARIX <= '" + dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND MT_TIP='" + 2 + "'"; // yeni siqnallar=2
            }
            else if (btnClicked == 5)
            {
                cedvel = "SELECT * FROM Melum_Tezlikler  WHERE MT_TARIX >= '" + (dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")) +
                         "' AND  MT_TARIX <= '" + dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND MT_TIP='" + 3 + "'";   //shubheli siqnallar=3
            }
            else if (btnClicked == 6)
            {
                cedvel = "SELECT * FROM Melum_Tezlikler  WHERE MT_TARIX >= '" + (dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")) +
                         "' AND  MT_TARIX <= '" + dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND MT_TIP='" + 4 + "'";  // Qorunan siqnallar 4
            }
            SqlDataAdapter sda = new SqlDataAdapter(cedvel, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            // dataGridView1.Rows.Clear();

            //Code Block
            if (this.dataGridView1.DataSource != null)
            {
                // this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
            else
            {
                this.dataGridView1.Rows.Clear();
            }
            // Clear(dataGridView1);
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[1].Value = item["MTezlikler"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["MT_signal"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["MT_modulyasiya"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["MT_tarix"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["MT_period"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["MT_tip"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["MT_ID"].ToString();
            }
        }
        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView1.Rows.Count > 1)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
        }

        //Cedvellerden silinme
        private void btn_delete_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in dataGridView1.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["MSech"].Value) == true
                                                  select row).ToList();
           // string constring = (@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            string constring = (@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            if (MessageBox.Show(string.Format("Seçilən {0} sətiri pozmağa əminsiniz?", selectedRows.Count), "Diqqət", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        string cedvel = "DELETE FROM Melum_Tezlikler WHERE MT_ID = @MT_ID";
                        using (SqlCommand cmd = new SqlCommand(cedvel, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@MT_ID", row.Cells["MT_ID"].Value);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                this.Cedveli_Yukle();
            }
        }

        //Cedvellere yazilma
        public void MelumTezliklerin_Yazilmasi()
        {
            //string constring = (@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            string constring = (@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            SqlConnection myConnection = new SqlConnection(constring);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            string comText = "";
            if (btnClicked == 1)
            {
                comText = "INSERT INTO Melum_Tezlikler (MTezlikler,MT_signal, MT_modulyasiya, MT_Tarix, MT_period,MT_tip) VALUES ('" + tezlik[i - 1] + "','" + signal_seviyye[i - 1] + "','" + modulyasiya + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")) + "','" + period + "','" + btnClicked + "')"; //btnclicked=1 Umumi siqnallar
                                                                                                                                                                                                                                              // comText = "INSERT INTO Melum_Tezlikler (MTezlikler,MT_signal, MT_modulyasiya,MT_period) VALUES ('" + tezlik[i - 1] + "','" + signal_seviyye[i - 1] + "','" + modulyasiya + "','" + period + "')";
            }
            else if (btnClicked == 2)
            {
                comText = "INSERT INTO Melum_Tezlikler(MTezlikler,MT_signal, MT_modulyasiya, MT_Tarix, MT_period,MT_tip) VALUES ('" + tezlik[i - 1] + "','" + signal_seviyye[i - 1] + "','" + modulyasiya + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")) + "','" + period + "','" + btnClicked + "')";  //btnclicked=2  Yeni siqnallar
                                                                                                                                                                                                                                              // comText = "INSERT INTO Melum_Tezlikler_Yeni(MTezlikler,MT_signal, MT_modulyasiya,MT_period) VALUES ('" + tezlik[i - 1] + "','" + signal_seviyye[i - 1] + "','" + modulyasiya + "','" + period + "')";
            }
            myCommand.CommandText = comText;
            myCommand.ExecuteNonQuery();
        }

        //Tek tezliklerin axtarishi
        private void Tek_TezliklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                com_port.Close();
                Tek_tezliyin_axtarishi f2 = new Tek_tezliyin_axtarishi();
                f2.Show();
            }
        }
        //Sechilmish tezliyi seslendirme
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    string sechilen_HZ = row.Cells["MTezlikler"].Value.ToString();
                    modulyasiya = row.Cells["MT_modulyasiya"].Value.ToString();
                    modulyasiya_commands();

                    com_port.Close();
                    com_port.Open();
                    com_port.WriteLine("MD" + melum_modulyasiya + "\r");
                    com_port.Write("RF" + sechilen_HZ + "\r");
                }
            }
        }
        private void dataGridView_New_HZ_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView_New_HZ.Rows[e.RowIndex];
                    string sechilen_HZ = row.Cells["MTezlikler2"].Value.ToString();
                    modulyasiya = row.Cells["MT_modulyasiya"].Value.ToString();
                    modulyasiya_commands();
                    com_port.Close();
                    com_port.Open();
                    com_port.WriteLine("MD" + melum_modulyasiya + "\r");
                    com_port.Write("RF" + sechilen_HZ + "\r");
                }
            }
        }
        //Cihazi yandir/sondur
        private void btn_on_off_Click(object sender, EventArgs e)
        {
            if (cbPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Cihazla əlaqəni yaradın!");
            }
            else
            {
                com_port.Close();
                com_port.Open();
                com_port.Write("QP\r");
            }
        }
        //Proqramdan cixish
        private void cixishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Modulyasiya novunu secmek
        public void modulyasiya_commands()
        {
            //modulyasiya = cmb_melum_modulyasiya.Text;
            if (modulyasiya == "WFM")
            {
                melum_modulyasiya = "0";
            }
            else if (modulyasiya == "NFM")
            {
                melum_modulyasiya = "1";
            }
            else if (modulyasiya == "AM")
            {
                melum_modulyasiya = "2";
            }
            else if (modulyasiya == "USB")
            {
                melum_modulyasiya = "3";
            }
            else if (modulyasiya == "LSB")
            {
                melum_modulyasiya = "4";
            }
            else if (modulyasiya == "CW")
            {
                melum_modulyasiya = "5";
            }
            else if (modulyasiya == "SFM")
            {
                melum_modulyasiya = "6";
            }
            else if (modulyasiya == "WAM")
            {
                melum_modulyasiya = "7";
            }
            else if (modulyasiya == "NAM")
            {
                melum_modulyasiya = "8";
            }
        }
        //Axtarish addimini secmek
        public void addim_commands()
        {
            // melum_addim = cmb_melum_addim.Text;
            if (melum_addim == "0.05")
            {
                addim = 0.05;
            }
            else if (melum_addim == "0.10")
            {
                addim = 0.10;
            }
            else if (melum_addim == "0.20")
            {
                addim = 0.20;
            }
            else if (melum_addim == "0.50")
            {
                addim = 0.50;
            }
            else if (melum_addim == "1.00")
            {
                addim = 1.00;
            }
            else if (melum_addim == "2.00")
            {
                addim = 2.00;
            }
            else if (melum_addim == "5.00")
            {
                addim = 5.00;
            }
            else if (melum_addim == "10.00")
            {
                addim = 10.00;
            }
            else if (melum_addim == "12.50")
            {
                addim = 12.50;
            }
            else if (melum_addim == "15.00")
            {
                addim = 15.00;
            }
            else if (melum_addim == "20.00")
            {
                addim = 20.00;
            }
            else if (melum_addim == "25.00")
            {
                addim = 25.00;
            }
            else if (melum_addim == "30.00")
            {
                addim = 30.00;
            }
            else if (melum_addim == "50.00")
            {
                addim = 50.00;
            }
            else if (melum_addim == "100.00")
            {
                addim = 100.00;
            }
            else
            {
                addim = Convert.ToDouble(cmb_melum_addim.Text);  // buna fikir ver sonda
            }
        }
        //Timerlerin qurashdirilmasi
        private void timer1_Tick(object sender, EventArgs e)
        {
            btn_search_Hz_Click(sender, e);
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            btn_new_search_Hz_Click(sender, e);
        }
        //Sechilenleri cedvellere atmaq
        private void btn_sech_shubheli_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in dataGridView1.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["MSech"].Value) == true
                                                  select row).ToList();
           // string constring = (@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            string constring = (@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            if (MessageBox.Show(string.Format("Seçilən {0} sətiri Şübhəli Siqnallar cədvəlinə keçsin?", selectedRows.Count), "Diqqət", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    string cedvel = "";
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        cedvel = "UPDATE Melum_Tezlikler SET MT_tip=@MT_TIP WHERE MT_ID=@MT_ID";
                        using (SqlCommand cmd = new SqlCommand(cedvel, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@MT_ID", row.Cells["MT_ID"].Value);
                            cmd.Parameters.AddWithValue("@MT_TIP", 3);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    dataGridView1.Rows.Remove(row);
                }
                this.Cedveli_Yukle();
            }
        }
        private void btn_sech_qorunan_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in dataGridView1.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["MSech"].Value) == true
                                                  select row).ToList();
            string constring = (@"Data Source=(local);Initial Catalog=Keshfiyyat;User id=Hp-PC\Hp; Password=; Integrated Security=True");
            // string constring = (@"Data Source=AKSHIN;Initial Catalog=Keshfiyyat;User id=sa; Password=123456; Integrated Security=True");
            if (MessageBox.Show(string.Format("Seçilən {0} sətiri Qorunan Siqnallar cədvəlinə keçsin?", selectedRows.Count), "Diqqət", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    string cedvel = "";
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        cedvel = "UPDATE Melum_Tezlikler SET MT_tip=@MT_TIP WHERE MT_ID=@MT_ID";
                        using (SqlCommand cmd = new SqlCommand(cedvel, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@MT_ID", row.Cells["MT_ID"].Value);
                            cmd.Parameters.AddWithValue("@MT_TIP", 4);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    dataGridView1.Rows.Remove(row);
                }
                this.Cedveli_Yukle();
            }
        }
        //Cedveller
        private void lbl_load_siqnallar_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = ("Məlum tezliklərin cədvəli");
            btn_delete.Visible = true;
            btn_sech_qorunan.Visible = true;
            btn_sech_shubheli.Visible = true;
            btnClicked = 3;
            Cedveli_Yukle();
        }
        private void lbl_yeni_siqnallar_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = ("Yeni tezliklərin cədvəli");
            btn_delete.Visible = true;
            btn_sech_qorunan.Visible = true;
            btn_sech_shubheli.Visible = true;
            btnClicked = 4;
            Cedveli_Yukle();
        }
        private void lbl_shubheli_Click_1(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = ("Şübhəli tezliklərin cədvəli");
            btn_delete.Visible = true;
            btn_sech_qorunan.Visible = true;
            btn_sech_shubheli.Visible = true;
            btnClicked = 5;
            Cedveli_Yukle();
        }
        private void lbl_qorunan_Click(object sender, EventArgs e)
        {
            lbl_tezliyBashliq.Text = ("Qorunan tezliklərin cədvəli");
            btn_delete.Visible = true;
            btn_sech_qorunan.Visible = true;
            btn_sech_shubheli.Visible = true;
            btnClicked = 6;
            Cedveli_Yukle();
        }
        //Cedveller panelini acir
        private void btn_cedveller_Click(object sender, EventArgs e)
        {
            pTables.Visible = !pTables.Visible;
        }
        private void təhlilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            com_port.Close();
            Cedvel f2 = new Cedvel();
            f2.Show();
        }

        private void cbPorts_Click(object sender, EventArgs e)
        {

        }
    }
}