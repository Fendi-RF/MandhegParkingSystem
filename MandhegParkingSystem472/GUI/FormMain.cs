using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MandhegParkingSystem472.GUI
{
    public partial class FormMain : Form
    {
        string EmployeeID;

        Class.Koneksi konn = new Class.Koneksi();
        public FormMain(string EmpID)
        {
            InitializeComponent();
            EmployeeID = EmpID;
            lblNama.Text = "Hello, " +konn.GetValueByID("name", "Employee", EmployeeID);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTinme.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            var nxtForm = new GUI.FormMember();
            this.Hide();
            nxtForm.StartPosition = this.StartPosition;
            nxtForm.FormClosed += (s, args) => this.Show();
            nxtForm.Show();
        }

        private void btnVehicle_Click(object sender, EventArgs e)
        {
            var nxtForm = new GUI.FormVehicle();
            this.Hide();
            nxtForm.StartPosition = this.StartPosition;
            nxtForm.FormClosed += (s, args) => this.Show();
            nxtForm.Show();
        }

        private void btnParking_Click(object sender, EventArgs e)
        {
            var nxtForm = new GUI.FormParking(EmployeeID);
            this.Hide();
            nxtForm.StartPosition = this.StartPosition;
            nxtForm.FormClosed += (s, args) => this.Show();
            nxtForm.Show();
        }
    }
}
