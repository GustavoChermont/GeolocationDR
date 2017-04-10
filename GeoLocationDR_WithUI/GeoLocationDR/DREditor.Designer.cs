namespace GeolocationDR
{
    partial class DREditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbLatLong = new System.Windows.Forms.RadioButton();
            this.rbAddress = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbLongitude = new System.Windows.Forms.ComboBox();
            this.cbLatitude = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.rbLatLong);
            this.panel1.Controls.Add(this.rbAddress);
            this.panel1.Location = new System.Drawing.Point(6, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 76);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Output";
            // 
            // rbLatLong
            // 
            this.rbLatLong.AutoSize = true;
            this.rbLatLong.Location = new System.Drawing.Point(13, 38);
            this.rbLatLong.Name = "rbLatLong";
            this.rbLatLong.Size = new System.Drawing.Size(115, 17);
            this.rbLatLong.TabIndex = 1;
            this.rbLatLong.TabStop = true;
            this.rbLatLong.Text = "Latitude/Longitude";
            this.rbLatLong.UseVisualStyleBackColor = true;
            this.rbLatLong.CheckedChanged += new System.EventHandler(this.rbLatLong_CheckedChanged);
            // 
            // rbAddress
            // 
            this.rbAddress.AutoSize = true;
            this.rbAddress.Location = new System.Drawing.Point(13, 14);
            this.rbAddress.Name = "rbAddress";
            this.rbAddress.Size = new System.Drawing.Size(63, 17);
            this.rbAddress.TabIndex = 0;
            this.rbAddress.TabStop = true;
            this.rbAddress.Text = "Address";
            this.rbAddress.UseVisualStyleBackColor = true;
            this.rbAddress.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(314, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(314, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.cbLongitude);
            this.panel2.Controls.Add(this.cbLatitude);
            this.panel2.Location = new System.Drawing.Point(163, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 76);
            this.panel2.TabIndex = 3;
            // 
            // cbLongitude
            // 
            this.cbLongitude.FormattingEnabled = true;
            this.cbLongitude.Location = new System.Drawing.Point(10, 40);
            this.cbLongitude.Name = "cbLongitude";
            this.cbLongitude.Size = new System.Drawing.Size(121, 21);
            this.cbLongitude.TabIndex = 1;
            this.cbLongitude.Text = "Longitude Attribute";
            // 
            // cbLatitude
            // 
            this.cbLatitude.FormattingEnabled = true;
            this.cbLatitude.Location = new System.Drawing.Point(10, 13);
            this.cbLatitude.Name = "cbLatitude";
            this.cbLatitude.Size = new System.Drawing.Size(121, 21);
            this.cbLatitude.TabIndex = 0;
            this.cbLatitude.Text = "Latitude Attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(172, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source Attributes";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(6, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 37);
            this.panel3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.MaximumSize = new System.Drawing.Size(300, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Returns the formal Address from the selected Latitude and Longitude attributes.";
            // 
            // DREditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 161);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DREditor";
            this.Text = "GeoLocation Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbLatLong;
        private System.Windows.Forms.RadioButton rbAddress;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbLongitude;
        private System.Windows.Forms.ComboBox cbLatitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
    }
}