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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(12, 551);
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
            this.ip_lb.Location = new System.Drawing.Point(12, 21);
            this.ip_lb.Name = "ip_lb";
            this.ip_lb.Size = new System.Drawing.Size(345, 524);
            this.ip_lb.TabIndex = 1;
            // 
            // trans_lb
            // 
            this.trans_lb.FormattingEnabled = true;
            this.trans_lb.Location = new System.Drawing.Point(363, 21);
            this.trans_lb.Name = "trans_lb";
            this.trans_lb.Size = new System.Drawing.Size(345, 524);
            this.trans_lb.TabIndex = 2;
            // 
            // app_lb
            // 
            this.app_lb.FormattingEnabled = true;
            this.app_lb.Location = new System.Drawing.Point(714, 21);
            this.app_lb.Name = "app_lb";
            this.app_lb.Size = new System.Drawing.Size(345, 524);
            this.app_lb.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Transport Protocol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(711, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Application Layer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 599);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.app_lb);
            this.Controls.Add(this.trans_lb);
            this.Controls.Add(this.ip_lb);
            this.Controls.Add(this.start_btn);
            this.Name = "Form1";
            this.Text = "Packet Sniffer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.ListBox ip_lb;
        private System.Windows.Forms.ListBox trans_lb;
        private System.Windows.Forms.ListBox app_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

