namespace Radiokeshfiyyat_30_11_2016
{
    partial class Tek_tezliyin_axtarishi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_search = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_tek_modulyasiya = new System.Windows.Forms.ComboBox();
            this.txt_tek_tezlik = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Green;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(39, 159);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(207, 40);
            this.btn_search.TabIndex = 60;
            this.btn_search.Text = "Axtar";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(59, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 25);
            this.label3.TabIndex = 63;
            this.label3.Text = "Tezliyin Axtarışı";
            // 
            // cmb_tek_modulyasiya
            // 
            this.cmb_tek_modulyasiya.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_tek_modulyasiya.FormattingEnabled = true;
            this.cmb_tek_modulyasiya.Items.AddRange(new object[] {
            "WFM",
            "NFM",
            "AM",
            "USB",
            "LSB",
            "CW",
            "SFM",
            "WAM",
            "NAM"});
            this.cmb_tek_modulyasiya.Location = new System.Drawing.Point(146, 78);
            this.cmb_tek_modulyasiya.Name = "cmb_tek_modulyasiya";
            this.cmb_tek_modulyasiya.Size = new System.Drawing.Size(100, 24);
            this.cmb_tek_modulyasiya.TabIndex = 62;
            // 
            // txt_tek_tezlik
            // 
            this.txt_tek_tezlik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tek_tezlik.Location = new System.Drawing.Point(146, 113);
            this.txt_tek_tezlik.Name = "txt_tek_tezlik";
            this.txt_tek_tezlik.Size = new System.Drawing.Size(100, 23);
            this.txt_tek_tezlik.TabIndex = 58;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btn_search);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.cmb_tek_modulyasiya);
            this.panel4.Controls.Add(this.txt_tek_tezlik);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(50, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(275, 236);
            this.panel4.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 61;
            this.label2.Text = "Modulyasiya";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 59;
            this.label1.Text = "Tezlik(MHz)";
            // 
            // Tek_tezliyin_axtarishi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = global::Radiokeshfiyyat_30_11_2016.Properties.Resources._0_5a3b4_5cf782ab_M;
            this.ClientSize = new System.Drawing.Size(379, 317);
            this.Controls.Add(this.panel4);
            this.Name = "Tek_tezliyin_axtarishi";
            this.Text = "Tek_tezliyin_axtarishi";
            this.Load += new System.EventHandler(this.Tek_tezliyin_axtarishi_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_tek_modulyasiya;
        private System.Windows.Forms.TextBox txt_tek_tezlik;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}