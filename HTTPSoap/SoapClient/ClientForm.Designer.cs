namespace SoapClient
{
    partial class ClientForm
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
            this.SendButton = new System.Windows.Forms.Button();
            this.client_txtbx = new System.Windows.Forms.TextBox();
            this.file_location_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ip_address_txtbx = new System.Windows.Forms.TextBox();
            this.port_txtbx = new System.Windows.Forms.TextBox();
            this.decompress_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(12, 330);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 0;
            this.SendButton.Text = "Compress";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // client_txtbx
            // 
            this.client_txtbx.Location = new System.Drawing.Point(12, 116);
            this.client_txtbx.Multiline = true;
            this.client_txtbx.Name = "client_txtbx";
            this.client_txtbx.Size = new System.Drawing.Size(341, 208);
            this.client_txtbx.TabIndex = 1;
            // 
            // file_location_txtbx
            // 
            this.file_location_txtbx.Location = new System.Drawing.Point(12, 24);
            this.file_location_txtbx.Name = "file_location_txtbx";
            this.file_location_txtbx.Size = new System.Drawing.Size(341, 20);
            this.file_location_txtbx.TabIndex = 2;
            this.file_location_txtbx.Text = "C:\\Users\\Jess\\Documents\\download.jpg";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Data";
            // 
            // ip_address_txtbx
            // 
            this.ip_address_txtbx.Location = new System.Drawing.Point(12, 63);
            this.ip_address_txtbx.Name = "ip_address_txtbx";
            this.ip_address_txtbx.Size = new System.Drawing.Size(100, 20);
            this.ip_address_txtbx.TabIndex = 7;
            this.ip_address_txtbx.Text = "localhost";
            // 
            // port_txtbx
            // 
            this.port_txtbx.Location = new System.Drawing.Point(127, 63);
            this.port_txtbx.Name = "port_txtbx";
            this.port_txtbx.Size = new System.Drawing.Size(46, 20);
            this.port_txtbx.TabIndex = 8;
            this.port_txtbx.Text = "8080";
            // 
            // decompress_btn
            // 
            this.decompress_btn.Location = new System.Drawing.Point(98, 330);
            this.decompress_btn.Name = "decompress_btn";
            this.decompress_btn.Size = new System.Drawing.Size(75, 23);
            this.decompress_btn.TabIndex = 9;
            this.decompress_btn.Text = "Decompress";
            this.decompress_btn.UseVisualStyleBackColor = true;
            this.decompress_btn.Click += new System.EventHandler(this.decompress_btn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 363);
            this.Controls.Add(this.decompress_btn);
            this.Controls.Add(this.port_txtbx);
            this.Controls.Add(this.ip_address_txtbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file_location_txtbx);
            this.Controls.Add(this.client_txtbx);
            this.Controls.Add(this.SendButton);
            this.Name = "ClientForm";
            this.Text = "Soap Compression Client";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox client_txtbx;
        private System.Windows.Forms.TextBox file_location_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ip_address_txtbx;
        private System.Windows.Forms.TextBox port_txtbx;
        private System.Windows.Forms.Button decompress_btn;
    }
}

