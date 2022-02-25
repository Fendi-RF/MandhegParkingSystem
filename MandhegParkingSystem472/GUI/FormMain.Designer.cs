
namespace MandhegParkingSystem472.GUI
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.lblNama = new System.Windows.Forms.Label();
            this.btnMember = new System.Windows.Forms.Button();
            this.lblDateTinme = new System.Windows.Forms.Label();
            this.btnVehicle = new System.Windows.Forms.Button();
            this.btnParking = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.Location = new System.Drawing.Point(12, 9);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(35, 13);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "label1";
            // 
            // btnMember
            // 
            this.btnMember.BackColor = System.Drawing.Color.Teal;
            this.btnMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMember.Font = new System.Drawing.Font("Impact", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMember.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnMember.Location = new System.Drawing.Point(128, 130);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(151, 74);
            this.btnMember.TabIndex = 0;
            this.btnMember.Text = "MASTER MEMBER";
            this.btnMember.UseVisualStyleBackColor = false;
            this.btnMember.Click += new System.EventHandler(this.btnMember_Click);
            // 
            // lblDateTinme
            // 
            this.lblDateTinme.AutoSize = true;
            this.lblDateTinme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTinme.Location = new System.Drawing.Point(12, 38);
            this.lblDateTinme.Name = "lblDateTinme";
            this.lblDateTinme.Size = new System.Drawing.Size(35, 13);
            this.lblDateTinme.TabIndex = 0;
            this.lblDateTinme.Text = "label1";
            // 
            // btnVehicle
            // 
            this.btnVehicle.BackColor = System.Drawing.Color.Teal;
            this.btnVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehicle.Font = new System.Drawing.Font("Impact", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVehicle.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnVehicle.Location = new System.Drawing.Point(128, 235);
            this.btnVehicle.Name = "btnVehicle";
            this.btnVehicle.Size = new System.Drawing.Size(151, 74);
            this.btnVehicle.TabIndex = 1;
            this.btnVehicle.Text = "MASTER VEHICLE";
            this.btnVehicle.UseVisualStyleBackColor = false;
            this.btnVehicle.Click += new System.EventHandler(this.btnVehicle_Click);
            // 
            // btnParking
            // 
            this.btnParking.BackColor = System.Drawing.Color.Teal;
            this.btnParking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParking.Font = new System.Drawing.Font("Impact", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParking.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnParking.Location = new System.Drawing.Point(128, 340);
            this.btnParking.Name = "btnParking";
            this.btnParking.Size = new System.Drawing.Size(151, 74);
            this.btnParking.TabIndex = 2;
            this.btnParking.Text = "PARKING PAYMENT";
            this.btnParking.UseVisualStyleBackColor = false;
            this.btnParking.Click += new System.EventHandler(this.btnParking_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "MANDHEG PARKING SYSTEM";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(417, 429);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnParking);
            this.Controls.Add(this.btnVehicle);
            this.Controls.Add(this.btnMember);
            this.Controls.Add(this.lblDateTinme);
            this.Controls.Add(this.lblNama);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Button btnMember;
        private System.Windows.Forms.Label lblDateTinme;
        private System.Windows.Forms.Button btnVehicle;
        private System.Windows.Forms.Button btnParking;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}