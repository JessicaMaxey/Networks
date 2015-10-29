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
            this.ip_txtbx = new System.Windows.Forms.TextBox();
            this.transport_txtbx = new System.Windows.Forms.TextBox();
            this.app_layer_txtbx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(12, 546);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(75, 23);
            this.start_btn.TabIndex = 0;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // ip_txtbx
            // 
            this.ip_txtbx.Location = new System.Drawing.Point(12, 12);
            this.ip_txtbx.Multiline = true;
            this.ip_txtbx.Name = "ip_txtbx";
            this.ip_txtbx.Size = new System.Drawing.Size(570, 172);
            this.ip_txtbx.TabIndex = 1;
            // 
            // transport_txtbx
            // 
            this.transport_txtbx.Location = new System.Drawing.Point(12, 190);
            this.transport_txtbx.Multiline = true;
            this.transport_txtbx.Name = "transport_txtbx";
            this.transport_txtbx.Size = new System.Drawing.Size(570, 172);
            this.transport_txtbx.TabIndex = 2;
            // 
            // app_layer_txtbx
            // 
            this.app_layer_txtbx.Location = new System.Drawing.Point(12, 368);
            this.app_layer_txtbx.Multiline = true;
            this.app_layer_txtbx.Name = "app_layer_txtbx";
            this.app_layer_txtbx.Size = new System.Drawing.Size(570, 172);
            this.app_layer_txtbx.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 576);
            this.Controls.Add(this.app_layer_txtbx);
            this.Controls.Add(this.transport_txtbx);
            this.Controls.Add(this.ip_txtbx);
            this.Controls.Add(this.start_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.TextBox ip_txtbx;
        private System.Windows.Forms.TextBox transport_txtbx;
        private System.Windows.Forms.TextBox app_layer_txtbx;
    }
}

