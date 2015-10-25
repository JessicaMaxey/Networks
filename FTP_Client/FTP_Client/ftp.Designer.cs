namespace FTP_Client
{
    partial class ftp
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
            this.label2 = new System.Windows.Forms.Label();
            this.username_txtbx = new System.Windows.Forms.TextBox();
            this.password_txtbx = new System.Windows.Forms.TextBox();
            this.host_txtbx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.port_txtbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.connect_btn = new System.Windows.Forms.Button();
            this.local_tv = new System.Windows.Forms.TreeView();
            this.remote_tv = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.local_site_txtbx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.remote_site_txtbx = new System.Windows.Forms.TextBox();
            this.upload_server_btn = new System.Windows.Forms.Button();
            this.delete_server_btn = new System.Windows.Forms.Button();
            this.rename_server_btn = new System.Windows.Forms.Button();
            this.upload_client_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // username_txtbx
            // 
            this.username_txtbx.Location = new System.Drawing.Point(224, 6);
            this.username_txtbx.Name = "username_txtbx";
            this.username_txtbx.Size = new System.Drawing.Size(100, 20);
            this.username_txtbx.TabIndex = 2;
            this.username_txtbx.Text = "user";
            // 
            // password_txtbx
            // 
            this.password_txtbx.Location = new System.Drawing.Point(398, 6);
            this.password_txtbx.Name = "password_txtbx";
            this.password_txtbx.Size = new System.Drawing.Size(100, 20);
            this.password_txtbx.TabIndex = 3;
            this.password_txtbx.Text = "password";
            // 
            // host_txtbx
            // 
            this.host_txtbx.Location = new System.Drawing.Point(47, 6);
            this.host_txtbx.Name = "host_txtbx";
            this.host_txtbx.Size = new System.Drawing.Size(100, 20);
            this.host_txtbx.TabIndex = 1;
            this.host_txtbx.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Host";
            // 
            // port_txtbx
            // 
            this.port_txtbx.Location = new System.Drawing.Point(547, 6);
            this.port_txtbx.Name = "port_txtbx";
            this.port_txtbx.Size = new System.Drawing.Size(59, 20);
            this.port_txtbx.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(515, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port";
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(626, 6);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(95, 20);
            this.connect_btn.TabIndex = 5;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // local_tv
            // 
            this.local_tv.Location = new System.Drawing.Point(15, 71);
            this.local_tv.Name = "local_tv";
            this.local_tv.Size = new System.Drawing.Size(377, 216);
            this.local_tv.TabIndex = 10;
            this.local_tv.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.local_tv_NodeMouseDoubleClick);
            // 
            // remote_tv
            // 
            this.remote_tv.Location = new System.Drawing.Point(398, 71);
            this.remote_tv.Name = "remote_tv";
            this.remote_tv.Size = new System.Drawing.Size(389, 216);
            this.remote_tv.TabIndex = 11;
            this.remote_tv.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.remote_tv_NodeMouseDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Local site:";
            // 
            // local_site_txtbx
            // 
            this.local_site_txtbx.Location = new System.Drawing.Point(73, 45);
            this.local_site_txtbx.Name = "local_site_txtbx";
            this.local_site_txtbx.Size = new System.Drawing.Size(319, 20);
            this.local_site_txtbx.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Remote site:";
            // 
            // remote_site_txtbx
            // 
            this.remote_site_txtbx.Location = new System.Drawing.Point(471, 45);
            this.remote_site_txtbx.Name = "remote_site_txtbx";
            this.remote_site_txtbx.Size = new System.Drawing.Size(316, 20);
            this.remote_site_txtbx.TabIndex = 15;
            // 
            // upload_server_btn
            // 
            this.upload_server_btn.Location = new System.Drawing.Point(15, 293);
            this.upload_server_btn.Name = "upload_server_btn";
            this.upload_server_btn.Size = new System.Drawing.Size(75, 36);
            this.upload_server_btn.TabIndex = 6;
            this.upload_server_btn.Text = "Upload To Server";
            this.upload_server_btn.UseVisualStyleBackColor = true;
            this.upload_server_btn.Click += new System.EventHandler(this.upload_server_btn_Click);
            // 
            // delete_server_btn
            // 
            this.delete_server_btn.Location = new System.Drawing.Point(96, 293);
            this.delete_server_btn.Name = "delete_server_btn";
            this.delete_server_btn.Size = new System.Drawing.Size(75, 36);
            this.delete_server_btn.TabIndex = 7;
            this.delete_server_btn.Text = "Delete From Server";
            this.delete_server_btn.UseVisualStyleBackColor = true;
            this.delete_server_btn.Click += new System.EventHandler(this.delete_server_btn_Click);
            // 
            // rename_server_btn
            // 
            this.rename_server_btn.Location = new System.Drawing.Point(177, 293);
            this.rename_server_btn.Name = "rename_server_btn";
            this.rename_server_btn.Size = new System.Drawing.Size(75, 36);
            this.rename_server_btn.TabIndex = 8;
            this.rename_server_btn.Text = "Rename File On Server";
            this.rename_server_btn.UseVisualStyleBackColor = true;
            this.rename_server_btn.Click += new System.EventHandler(this.rename_server_btn_Click);
            // 
            // upload_client_btn
            // 
            this.upload_client_btn.Location = new System.Drawing.Point(398, 296);
            this.upload_client_btn.Name = "upload_client_btn";
            this.upload_client_btn.Size = new System.Drawing.Size(75, 36);
            this.upload_client_btn.TabIndex = 16;
            this.upload_client_btn.Text = "Download To Client";
            this.upload_client_btn.UseVisualStyleBackColor = true;
            this.upload_client_btn.Click += new System.EventHandler(this.upload_client_btn_Click);
            // 
            // ftp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 346);
            this.Controls.Add(this.upload_client_btn);
            this.Controls.Add(this.rename_server_btn);
            this.Controls.Add(this.delete_server_btn);
            this.Controls.Add(this.upload_server_btn);
            this.Controls.Add(this.remote_site_txtbx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.local_site_txtbx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.remote_tv);
            this.Controls.Add(this.local_tv);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.port_txtbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.host_txtbx);
            this.Controls.Add(this.password_txtbx);
            this.Controls.Add(this.username_txtbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ftp";
            this.Text = "FTP Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox username_txtbx;
        private System.Windows.Forms.TextBox password_txtbx;
        private System.Windows.Forms.TextBox host_txtbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox port_txtbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.TreeView local_tv;
        private System.Windows.Forms.TreeView remote_tv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox local_site_txtbx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox remote_site_txtbx;
        private System.Windows.Forms.Button upload_server_btn;
        private System.Windows.Forms.Button delete_server_btn;
        private System.Windows.Forms.Button rename_server_btn;
        private System.Windows.Forms.Button upload_client_btn;
    }
}

