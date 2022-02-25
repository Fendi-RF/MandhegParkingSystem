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
    public partial class FormParking : Form
    {
        Class.Koneksi konn = new Class.Koneksi();

        string EmployeeID;
        string VehID;
        string MemID;
        string HourlyID;
        decimal HourlyRates;

        int sqlrow;

        DateTime dtpIn;
        DateTime dtpOut;
        public FormParking(string EmpID)
        {
            InitializeComponent();
            EmployeeID = EmpID;;
            refresh();
            konn.SetComboBox("name", "Membership", cmbMember);
            konn.SetComboBox("name", "VehicleType", cmbVehicle);

            cmbVehicle.SelectedIndex = 0;
            cmbMember.SelectedIndex = 0;
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select license_plate from Vehicle", conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txtLicense.AutoCompleteCustomSource.Add(sdr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }
        void refresh()
        {
            txtLicense.Text = "";
            txtAmount.Text = "0";
            txtHourly.Text = "0";
            txtParkdur.Text = "0";

            txtAmount.Enabled = false;
            txtHourly.Enabled = false;
            txtOwner.Enabled = false;
            txtParkdur.Enabled = false;
            cmbMember.Enabled = false;
            cmbVehicle.Enabled = false;


            dtpEnable(false);
        }
        void dtpEnable(bool status)
        {
            dtpInDate.Enabled = status;
            dtpInTime.Enabled = status;
            dtpOutDate.Enabled = status;
            dtpOutTime.Enabled = status;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if ((txtLicense.Text.Length < txtLicense.MaxLength) || (txtParkdur.Text == "0") || (dtpIn == dtpOut))
            {
                MessageBox.Show("Data belum lengkap");
            }
            else
            {
                if (VehID == "")
                {
                    konn.SqlInsert("ParkingData", "([license_plate],[employee_id],[hourly_rates_id],[datetime_in],[datetime_out],[amount_to_pay] ,[created_at]) values('" + txtLicense.Text + "', " + EmployeeID + ", " + HourlyID + ", '" + dtpIn.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + dtpOut.ToString("yyyy/MM/dd HH:mm:ss") + "', " + txtAmount.Text + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' ) ");
                }
                else
                {
                    konn.SqlInsert("ParkingData", "([license_plate],[vehicle_id],[employee_id],[hourly_rates_id],[datetime_in],[datetime_out],[amount_to_pay] ,[created_at]) values('" + txtLicense.Text + "', " + VehID + ", " + EmployeeID + ", " + HourlyID + ", '" + dtpIn.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + dtpOut.ToString("yyyy/MM/dd HH:mm:ss") + "', " + txtAmount.Text + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' ) ");
                }

                refresh();
                var dialog = new GUI.DialogReport();
                dialog.ShowDialog();
            }
        }

        private void txtLicense_TextChanged(object sender, EventArgs e)
        {
            if (txtLicense.Text.Length == txtLicense.MaxLength)
            {
                if(detectrow() > 0)
                {
                    getIDbyValue();

                    MemID =  konn.GetValueByID("member_id", "Vehicle", VehID);
                    cmbVehicle.SelectedIndex = int.Parse(konn.GetValueByID("vehicle_type_id", "Vehicle", VehID)) - 1;
                    txtOwner.Text = MemID + " - " + konn.GetValueByID("name", "Member", MemID);
                    cmbMember.SelectedIndex = int.Parse(konn.GetValueByID("membership_id", "Member", MemID)) - 1;
                    cmbVehicle.Enabled = false;
                }
                else
                {
                    cmbVehicle.Enabled = true;
                }
                dtpEnable(true);
                btnSubmit.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = false;
                VehID = "";
                cmbVehicle.Enabled = false;
                dtpEnable(false);
                txtOwner.Text = "";
                cmbMember.SelectedIndex = 0;
            }
            if (txtLicense.Text.Length == 2)
            {
                txtLicense.Text += " ";
                txtLicense.Focus();
                txtLicense.SelectionStart = txtLicense.Text.Length;
            }
            else if (txtLicense.Text.Length == 7)
            {
                txtLicense.Text += " ";
                txtLicense.Focus();
                txtLicense.SelectionStart = txtLicense.Text.Length;
            }

        }
        int detectrow()
        {
            
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from Vehicle where license_plate like '%"+txtLicense.Text+"%'", conn);
                sqlrow = (int)cmd.ExecuteScalar();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return sqlrow;
        }
        void getIDbyValue()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select id from Vehicle where license_plate ='"+txtLicense.Text+"'", conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    VehID = sdr[0].ToString();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        void Hourly()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from HourlyRates where membership_id="+(cmbMember.SelectedIndex +1)+" and vehicle_type_id="+(cmbVehicle.SelectedIndex+1), conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    HourlyID = sdr[0].ToString();
                    HourlyRates = decimal.Parse(sdr[3].ToString());
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            txtHourly.Text = Math.Round(HourlyRates).ToString();
            
        }

        private void cmbVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hourly();
        }

        private void cmbMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hourly();
        }
        void Parkdur()
        {
            dtpIn = dtpInDate.Value.Date.Add(dtpInTime.Value.TimeOfDay);
            dtpOut = dtpOutDate.Value.Date.Add(dtpOutTime.Value.TimeOfDay);

            dtpInDate.MaxDate = dtpOutDate.Value;
            dtpInTime.MaxDate = dtpOutTime.Value;

            txtParkdur.Text = Math.Round(dtpOut.Subtract(dtpIn).TotalHours).ToString();
            Amount();
        }
        void Amount()
        {
            txtAmount.Text = (int.Parse(txtHourly.Text) * int.Parse(txtParkdur.Text)).ToString();
        }

        private void dtpInDate_ValueChanged(object sender, EventArgs e)
        {
            Parkdur();

        }

        private void dtpInTime_ValueChanged(object sender, EventArgs e)
        {
            Parkdur();

        }

        private void dtpOutDate_ValueChanged(object sender, EventArgs e)
        {
            Parkdur();
        }

        private void dtpOutTime_ValueChanged(object sender, EventArgs e)
        {
            Parkdur();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
