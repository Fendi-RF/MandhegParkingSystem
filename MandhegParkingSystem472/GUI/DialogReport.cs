using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MandhegParkingSystem472.GUI
{
    public partial class DialogReport : Form
    {
        Class.Koneksi konn = new Class.Koneksi();
        public DialogReport()
        {
            InitializeComponent();
        }
        void cetak()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select top 1 * from ParkingData order by id desc", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "ParkingData");

                
                Report.ReportTable report = new Report.ReportTable();
                report.SetDataSource(ds);
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void DialogReport_Load(object sender, EventArgs e)
        {
            cetak();
        }
    }
}
