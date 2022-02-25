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
    public partial class FormMember : Form
    {
        Class.Koneksi konn = new Class.Koneksi();

        int doCommand;
        string MemID;
        public FormMember()
        {
            InitializeComponent();
            refresh();

            konn.SetComboBox("name", "Membership", cmbMember);
            cmbMember.SelectedIndex = 0;
        }
        void refresh()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            dateTimePicker1.Value = Convert.ToDateTime("12/12/2003");
            rdFem.Checked = false;
            rdMale.Checked = false;

            btnSubCan(false);
            btnCommand(false);
            txtEnable(false);

            btnInsert.Enabled = true;
            konn.SetDataGrid("*", "Member", dataGridView1);
        }
        void btnSubCan(bool status)
        {
            btnSubmit.Enabled = status;
            btnCancel.Enabled = status;
        }
        void btnCommand(bool status) {
            btnUpdate.Enabled = status;
            btnDel.Enabled = status;
            btnInsert.Enabled = status;
                
        }
        void txtEnable(bool status)
        {
            txtName.Enabled = status;
            txtEmail.Enabled = status;
            txtAddress.Enabled = status;
            txtPhone.Enabled = status;
            dateTimePicker1.Enabled = status;
            cmbMember.Enabled = status;
            rdFem.Enabled = status;
            rdMale.Enabled = status;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            refresh();
            txtEnable(true);
            btnCommand(false);
            btnSubCan(true);
            
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
            if ((txtName.Text == "") || (txtEmail.Text == "") || (txtPhone.Text == "") || (txtAddress.Text == "") || ((rdFem.Checked = false) &&(rdMale.Checked = false)))
            {
                MessageBox.Show("Data belum lengkap");
            }
            else
            {
                switch (doCommand)
                {
                    case 1:
                        konn.SqlInsert("Member", "([membership_id],[name],[email] ,[phone_number],[address] ,[date_of_birth] ,[gender] ,[created_at]) values(" + (cmbMember.SelectedIndex + 1) + ", '" + txtName.Text + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "', '" + txtAddress.Text + "', '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "', '"+radio()+"', '"+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+"' )");
                        MessageBox.Show("Data telah ditambahkan");
                        refresh();
                        break;
                    case 2:
                        konn.SqlUpdate("Member", " membership_id=" + (cmbMember.SelectedIndex + 1) + ", name='" + txtName.Text + "', email='" + txtEmail.Text + "', phone_number='" + txtPhone.Text + "', address='" + txtAddress.Text + "', date_of_birth='" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "', gender = '" + radio() + "', last_updated_at='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'", MemID);
                        MessageBox.Show("Data berhasil diperbarui");
                        refresh();
                        break;
                    case 3:
                        
                        DialogResult dialog = MessageBox.Show("Hapus", "Yakin?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if(dialog == DialogResult.Yes)
                        {
                            konn.SqlDelete("Member", MemID);
                            MessageBox.Show("Data Berhasil dihapus");
                            refresh();
                        }
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCommand(true);
            btnSubCan(false);
            if(doCommand == 1)
            {
                btnCommand(false);
                btnInsert.Enabled = true;
                txtEnable(false);
            }
        }
        string radio()
        {
            if (rdFem.Checked)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }
        void getRadio(string gender)
        {
            if (gender == "Male")
            {
                rdMale.Checked = true;
            }
            else
            {
                rdFem.Checked = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEnable(true);
            btnCommand(true);
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                MemID = row.Cells[0].Value.ToString();
                cmbMember.SelectedIndex = (int.Parse(row.Cells[1].Value.ToString()) - 1);
                txtName.Text = row.Cells[2].Value.ToString();
                txtEmail.Text = row.Cells[3].Value.ToString();
                txtPhone.Text = row.Cells[4].Value.ToString();
                txtAddress.Text = row.Cells[5].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[6].Value);
                getRadio(row.Cells[7].Value.ToString());
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar) && (e.KeyChar != '-'))){
                e.Handled = true;
            }
        }
    }
}
