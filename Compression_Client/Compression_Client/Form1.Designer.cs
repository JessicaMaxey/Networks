namespace Compression_Client
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.file_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.open_btn = new System.Windows.Forms.Button();
            this.send_btn = new System.Windows.Forms.Button();
            this.ip_address_txtbx = new System.Windows.Forms.TextBox();
            this.port_txtbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 111);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(498, 323);
            this.textBox1.TabIndex = 0;
            // 
            // file_txtbx
            // 
            this.file_txtbx.Location = new System.Drawing.Point(12, 21);
            this.file_txtbx.Name = "file_txtbx";
            this.file_txtbx.Size = new System.Drawing.Size(417, 20);
            this.file_txtbx.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Path";
            // 
            // open_btn
            // 
            this.open_btn.Location = new System.Drawing.Point(435, 19);
            this.open_btn.Name = "open_btn";
            this.open_btn.Size = new System.Drawing.Size(75, 23);
            this.open_btn.TabIndex = 3;
            this.open_btn.Text = "Open";
            this.open_btn.UseVisualStyleBackColor = true;
            this.open_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(435, 440);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 4;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            // 
            // ip_address_txtbx
            // 
            this.ip_address_txtbx.Location = new System.Drawing.Point(12, 64);
            this.ip_address_txtbx.Name = "ip_address_txtbx";
            this.ip_address_txtbx.Size = new System.Drawing.Size(122, 20);
            this.ip_address_txtbx.TabIndex = 5;
            // 
            // port_txtbx
            // 
            this.port_txtbx.Location = new System.Drawing.Point(164, 64);
            this.port_txtbx.Name = "port_txtbx";
            this.port_txtbx.Size = new System.Drawing.Size(71, 20);
            this.port_txtbx.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Data:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 475);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port_txtbx);
            this.Controls.Add(this.ip_address_txtbx);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.open_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file_txtbx);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox file_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button open_btn;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.TextBox ip_address_txtbx;
        private System.Windows.Forms.TextBox port_txtbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

