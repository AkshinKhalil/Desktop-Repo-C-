using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Radiokeshfiyyat_30_11_2016
{
    public partial class Tek_tezliyin_axtarishi : Form
    {
        SerialPort com_port = new SerialPort();

        string tek_tezlik = "";
        string tek_modulyasiya = "";
        string modulyasiya = "";
        public Tek_tezliyin_axtarishi()
        {
            InitializeComponent();
        }

        private void Tek_tezliyin_axtarishi_Load(object sender, EventArgs e)
        {
            if (!com_port.IsOpen)
            {
                com_port.PortName = RadioKeshfiyyat.comPortNo;
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
        public void modulyasiya_commands()
        {
            modulyasiya = cmb_tek_modulyasiya.Text;

            if (modulyasiya == "WFM")
            {
                tek_modulyasiya = "0";
            }
            else if (modulyasiya == "NFM")
            {
                tek_modulyasiya = "1";
            }
            else if (modulyasiya == "AM")
            {
                tek_modulyasiya = "2";
            }
            else if (modulyasiya == "USB")
            {
                tek_modulyasiya = "3";
            }
            else if (modulyasiya == "LSB")
            {
                tek_modulyasiya = "4";
            }
            else if (modulyasiya == "CW")
            {
                tek_modulyasiya = "5";
            }
            else if (modulyasiya == "SFM")
            {
                tek_modulyasiya = "6";
            }
            else if (modulyasiya == "WAM")
            {
                tek_modulyasiya = "7";
            }
            else if (modulyasiya == "NAM")
            {
                tek_modulyasiya = "8";
            }
        }

        private void btn_search_Click_1(object sender, EventArgs e)
        {
            tek_tezlik = txt_tek_tezlik.Text;
            modulyasiya_commands();
            com_port.Close();
            com_port.Open();
            com_port.WriteLine("MD" + tek_modulyasiya + "\r");
            com_port.Write("RF" + tek_tezlik + "\r");
            com_port.Close(); ;
        }
    }
}