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
    public partial class FormVehicle : Form
    {
        Class.Koneksi konn = new Class.Koneksi();

        int doCommand;
        string VehID;
        string MemID;
        public FormVehicle()
        {
            InitializeComponent();
            refresh();
            konn.SetComboBox("name", "VehicleType", txtVehicle);
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Member", conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txtOwner.Items.Add(sdr[0].ToString() +" - " + sdr[2].ToString());
                    txtOwner.AutoCompleteCustomSource.Add(sdr[0].ToString() + " - " + sdr[2].ToString());
                }
            }
            finally
            {
                conn.Close();
            }
            txtVehicle.SelectedIndex = 0;
        }
        void refresh()
        {
            txtLicense.Text = "";
            txtNote.Text = "";
            txtOwner.Text = "";
            txtSearch.Text = "";
            cmbSearchBy.SelectedIndex = 0;

            btnSubCan(false);
            btnCommand(false);
            txtEnable(false);

            btnInsert.Enabled = true;
            konn.SetDataGrid("*", "vw_Vehicle", dataGridView1);
        }
        void btnSubCan(bool status)
        {
            btnSubmit.Enabled = status;
            btnCancel.Enabled = status;
        }
        void btnCommand(bool status)
        {
            btnUpdate.Enabled = status;
            btnDel.Enabled = status;
            btnInsert.Enabled = status;

        }
        void txtEnable(bool status)
        {
            txtVehicle.Enabled = status;
            txtOwner.Enabled = status;
            txtNote.Enabled = status;
            txtLicense.Enabled = status;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            refresh();
            btnCommand(false);
            btnSubCan(true);
            txtEnable(true);
            btnInsert.Enabled = true;

            doCommand = 1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnCommand(false);
            btnSubCan(true);
            btnUpdate.Enabled = true;

            doCommand = 2;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            btnCommand(false);
            btnSubCan(true);
            btnDel.Enabled = true;

            doCommand = 3;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if ((txtLicense.Text.Length < txtLicense.MaxLength) || (txtOwner.Text == ""))
            {
                MessageBox.Show("Data belum lengkap");
            }
            else
            {
                switch (doCommand)
                {
                    case 1:
                        konn.SqlInsert("Vehicle", "([vehicle_type_id],[member_id],[license_plate],[notes],[created_at]) values("+(txtVehicle.SelectedIndex + 1)+", "+txtOwner.Text.Substring(0, 2)+", '"+txtLicense.Text+"','"+txtNote.Text+"', '"+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+"')");
                        refresh();
                        MessageBox.Show("Data berhasil ditambahkan");
                        break;
                    case 2:
                        konn.SqlUpdate("Vehicle", "vehicle_type_id = " + (txtVehicle.SelectedIndex + 1) + ", member_id=" + txtOwner.Text.Substring(0, 2) + ", license_plate = '" + txtLicense.Text + "', notes='" + txtNote.Text + "', last_updated_at ='" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "'", VehID);
                        MessageBox.Show("Data telah diperbarui");
                        refresh();
                        break;
                    case 3:
                        
                        DialogResult dialog = MessageBox.Show("Hapus", "Yakin?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.Yes)
                        {
                            konn.SqlDelete("Vehicle", VehID);
                            refresh();
                            MessageBox.Show("Data berhasil dihapus");
                        }
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCommand(true);
            btnSubCan(false);
            if (doCommand == 1)
            {
                btnCommand(false);
                btnInsert.Enabled = true;
                txtEnable(false);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEnable(true);
            btnCommand(true);
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                VehID = row.Cells[0].Value.ToString();
                txtVehicle.SelectedIndex = int.Parse(row.Cells[8].Value.ToString()) - 1;
                MemID = row.Cells[9].Value.ToString();
                txtLicense.Text = row.Cells[3].Value.ToString();
                txtNote.Text = row.Cells[4].Value.ToString();

               

                txtOwner.Text = MemID +" - " + konn.GetValueByID("name", "Member", MemID);
            }
        }

        private void cmbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            search();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        void search()
        {
            string searchBy;
            if (cmbSearchBy.SelectedIndex == 0)
            {
                searchBy = "license_plate";
            }
            else
            {
                searchBy = "[Owner Name]";
            }

            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from vw_Vehicle where "+searchBy+" like '%"+txtSearch.Text+"%'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "vw_Vehicle");
                dataGridView1.DataSource = ds;
                dataGridView1.Refresh();

            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void txtLicense_TextChanged(object sender, EventArgs e)
        {
            if(txtLicense.Text.Length == 2)
            {
                txtLicense.Text += " ";
                txtLicense.Focus();
                txtLicense.SelectionStart = txtLicense.Text.Length;
            }
            else if (txtLicense.Text.Length == 7){
                txtLicense.Text += " ";
                txtLicense.Focus();
                txtLicense.SelectionStart = txtLicense.Text.Length;
            }


        }
        
    }
}
