namespace IPPacketAnalysis
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
            this.start_btn = new System.Windows.Forms.Button();
            this.ip_lb = new System.Windows.Forms.ListBox();
            this.trans_lb = new System.Windows.Forms.ListBox();
            this.app_lb = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(12, 542);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(1047, 39);
            this.start_btn.TabIndex = 0;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // ip_lb
            // 
            this.ip_lb.FormattingEnabled = true;
            this.ip_lb.Location = new System.Drawing.Point(12, 12);
            this.ip_lb.Name = "ip_lb";
            this.ip_lb.Size = new System.Drawing.Size(345, 524);
            this.ip_lb.TabIndex = 1;
            // 
            // trans_lb
            // 
            this.trans_lb.FormattingEnabled = true;
            this.trans_lb.Location = new System.Drawing.Point(363, 12);
            this.trans_lb.Name = "trans_lb";
            this.trans_lb.Size = new System.Drawing.Size(345, 524);
            this.trans_lb.TabIndex = 2;
            // 
            // app_lb
            // 
            this.app_lb.FormattingEnabled = true;
            this.app_lb.Location = new System.Drawing.Point(714, 12);
            this.app_lb.Name = "app_lb";
            this.app_lb.Size = new System.Drawing.Size(345, 524);
            this.app_lb.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 586);
            this.Controls.Add(this.app_lb);
            this.Controls.Add(this.trans_lb);
            this.Controls.Add(this.ip_lb);
            this.Controls.Add(this.start_btn);
            this.Name = "Form1";
            this.Text = "Packet Sniffer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.ListBox ip_lb;
        private System.Windows.Forms.ListBox trans_lb;
        private System.Windows.Forms.ListBox app_lb;
    }
}

