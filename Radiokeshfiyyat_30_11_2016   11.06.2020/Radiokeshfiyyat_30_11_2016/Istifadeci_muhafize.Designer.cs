namespace Radiokeshfiyyat_30_11_2016
{
    partial class Istifadeci_muhafize
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cixish = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_qeydiyyat = new System.Windows.Forms.Button();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "İstifadəçi";
            // 
            // btn_cixish
            // 
            this.btn_cixish.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cixish.ForeColor = System.Drawing.Color.Red;
            this.btn_cixish.Location = new System.Drawing.Point(175, 129);
            this.btn_cixish.Name = "btn_cixish";
            this.btn_cixish.Size = new System.Drawing.Size(130, 32);
            this.btn_cixish.TabIndex = 5;
            this.btn_cixish.Text = "Çıxış";
            this.btn_cixish.UseVisualStyleBackColor = true;
            this.btn_cixish.Click += new System.EventHandler(this.btn_cixish_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Parol";
            // 
            // btn_qeydiyyat
            // 
            this.btn_qeydiyyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_qeydiyyat.ForeColor = System.Drawing.Color.Green;
            this.btn_qeydiyyat.Location = new System.Drawing.Point(24, 129);
            this.btn_qeydiyyat.Name = "btn_qeydiyyat";
            this.btn_qeydiyyat.Size = new System.Drawing.Size(134, 32);
            this.btn_qeydiyyat.TabIndex = 4;
            this.btn_qeydiyyat.Text = "Qeydiyyat";
            this.btn_qeydiyyat.UseVisualStyleBackColor = true;
            this.btn_qeydiyyat.Click += new System.EventHandler(this.btn_qeydiyyat_Click);
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_username.Location = new System.Drawing.Point(175, 41);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(130, 26);
            this.txt_username.TabIndex = 2;
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(175, 83);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(130, 26);
            this.txt_password.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txt_username);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_qeydiyyat);
            this.panel1.Controls.Add(this.btn_cixish);
            this.panel1.Controls.Add(this.txt_password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(681, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 204);
            this.panel1.TabIndex = 6;
            // 
            // Istifadeci_muhafize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Radiokeshfiyyat_30_11_2016.Properties.Resources.security11;
            this.ClientSize = new System.Drawing.Size(1264, 594);
            this.Controls.Add(this.panel1);
            this.Name = "Istifadeci_muhafize";
            this.Text = "Radiokəşfiyyat Mühafizə";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_qeydiyyat;
        private System.Windows.Forms.Button btn_cixish;
        private System.Windows.Forms.Panel panel1;
    }
}