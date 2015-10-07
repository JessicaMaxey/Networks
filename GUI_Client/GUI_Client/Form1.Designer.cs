namespace GUI_Client
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
            this.main_txtbx = new System.Windows.Forms.TextBox();
            this.server_address_txtbx = new System.Windows.Forms.TextBox();
            this.message_txtbx = new System.Windows.Forms.TextBox();
            this.connect_btn = new System.Windows.Forms.Button();
            this.send_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // main_txtbx
            // 
            this.main_txtbx.BackColor = System.Drawing.Color.White;
            this.main_txtbx.Location = new System.Drawing.Point(12, 38);
            this.main_txtbx.Multiline = true;
            this.main_txtbx.Name = "main_txtbx";
            this.main_txtbx.ReadOnly = true;
            this.main_txtbx.Size = new System.Drawing.Size(694, 323);
            this.main_txtbx.TabIndex = 0;
            // 
            // server_address_txtbx
            // 
            this.server_address_txtbx.Location = new System.Drawing.Point(12, 12);
            this.server_address_txtbx.Name = "server_address_txtbx";
            this.server_address_txtbx.Size = new System.Drawing.Size(128, 20);
            this.server_address_txtbx.TabIndex = 1;
            // 
            // message_txtbx
            // 
            this.message_txtbx.Location = new System.Drawing.Point(12, 389);
            this.message_txtbx.Multiline = true;
            this.message_txtbx.Name = "message_txtbx";
            this.message_txtbx.Size = new System.Drawing.Size(613, 49);
            this.message_txtbx.TabIndex = 2;
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(146, 12);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(75, 20);
            this.connect_btn.TabIndex = 3;
            this.connect_btn.Text = "connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(631, 387);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 51);
            this.send_btn.TabIndex = 4;
            this.send_btn.Text = "send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(718, 454);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.message_txtbx);
            this.Controls.Add(this.server_address_txtbx);
            this.Controls.Add(this.main_txtbx);
            this.Name = "Form1";
            this.Text = "Chat Room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox server_address_txtbx;
        private System.Windows.Forms.TextBox message_txtbx;
        private System.Windows.Forms.Button connect_btn;
        public System.Windows.Forms.Button send_btn;
        public System.Windows.Forms.TextBox main_txtbx;
    }
}

