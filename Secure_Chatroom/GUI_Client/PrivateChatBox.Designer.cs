namespace GUI_Client
{
    partial class PrivateChatBox
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
            this.send_btn = new System.Windows.Forms.Button();
            this.message_txtbx = new System.Windows.Forms.TextBox();
            this.main_txtbx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // send_btn
            // 
            this.send_btn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.send_btn.Location = new System.Drawing.Point(631, 350);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 51);
            this.send_btn.TabIndex = 7;
            this.send_btn.Text = "send";
            this.send_btn.UseVisualStyleBackColor = false;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // message_txtbx
            // 
            this.message_txtbx.Location = new System.Drawing.Point(12, 352);
            this.message_txtbx.Multiline = true;
            this.message_txtbx.Name = "message_txtbx";
            this.message_txtbx.Size = new System.Drawing.Size(613, 49);
            this.message_txtbx.TabIndex = 6;
            // 
            // main_txtbx
            // 
            this.main_txtbx.BackColor = System.Drawing.Color.White;
            this.main_txtbx.Location = new System.Drawing.Point(12, 12);
            this.main_txtbx.Multiline = true;
            this.main_txtbx.Name = "main_txtbx";
            this.main_txtbx.ReadOnly = true;
            this.main_txtbx.Size = new System.Drawing.Size(694, 323);
            this.main_txtbx.TabIndex = 5;
            // 
            // PrivateChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 413);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.message_txtbx);
            this.Controls.Add(this.main_txtbx);
            this.Name = "PrivateChatBox";
            this.Text = "PrivateChatBox";
            this.Load += new System.EventHandler(this.PrivateChatBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.TextBox message_txtbx;
        public System.Windows.Forms.TextBox main_txtbx;
    }
}