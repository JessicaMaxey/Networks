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
            this.screenname_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.private_chat_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // main_txtbx
            // 
            this.main_txtbx.BackColor = System.Drawing.Color.White;
            this.main_txtbx.Location = new System.Drawing.Point(12, 49);
            this.main_txtbx.Multiline = true;
            this.main_txtbx.Name = "main_txtbx";
            this.main_txtbx.ReadOnly = true;
            this.main_txtbx.Size = new System.Drawing.Size(694, 323);
            this.main_txtbx.TabIndex = 0;
            // 
            // server_address_txtbx
            // 
            this.server_address_txtbx.Location = new System.Drawing.Point(12, 23);
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
            this.connect_btn.Location = new System.Drawing.Point(317, 23);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(75, 20);
            this.connect_btn.TabIndex = 3;
            this.connect_btn.Text = "connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // send_btn
            // 
            this.send_btn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.send_btn.Location = new System.Drawing.Point(631, 387);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 51);
            this.send_btn.TabIndex = 4;
            this.send_btn.Text = "send";
            this.send_btn.UseVisualStyleBackColor = false;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // screenname_txtbx
            // 
            this.screenname_txtbx.Location = new System.Drawing.Point(146, 23);
            this.screenname_txtbx.Name = "screenname_txtbx";
            this.screenname_txtbx.Size = new System.Drawing.Size(165, 20);
            this.screenname_txtbx.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(143, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Screen Name";
            // 
            // private_chat_btn
            // 
            this.private_chat_btn.Location = new System.Drawing.Point(398, 23);
            this.private_chat_btn.Name = "private_chat_btn";
            this.private_chat_btn.Size = new System.Drawing.Size(126, 21);
            this.private_chat_btn.TabIndex = 8;
            this.private_chat_btn.Text = "Private Chat";
            this.private_chat_btn.UseVisualStyleBackColor = true;
            this.private_chat_btn.Click += new System.EventHandler(this.private_chat_btn_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.send_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(718, 454);
            this.Controls.Add(this.private_chat_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screenname_txtbx);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.message_txtbx);
            this.Controls.Add(this.server_address_txtbx);
            this.Controls.Add(this.main_txtbx);
            this.Name = "Form1";
            this.Text = "Chat Room";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox server_address_txtbx;
        private System.Windows.Forms.TextBox message_txtbx;
        private System.Windows.Forms.Button connect_btn;
        public System.Windows.Forms.Button send_btn;
        public System.Windows.Forms.TextBox main_txtbx;
        private System.Windows.Forms.TextBox screenname_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button private_chat_btn;
    }
}

