using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace MandhegParkingSystem472.GUI
{
    public partial class FormLogin : Form
    {
        Class.Koneksi konn = new Class.Koneksi();

        string decryptedPW;
        string EmpID;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            matchPW(txtID.Text, txtPW.Text);
            
        }
        string ComputeHash256(string rawText)
        {
            using(SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawText));

                StringBuilder builder = new StringBuilder();
                for (int i=0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void txtPW_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtPW.PasswordChar == '*')
            {
                txtPW.PasswordChar = '\0';
            }
            else
            {
                txtPW.PasswordChar = '*';
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar))){
                e.Handled = true;
            }
        }

        private void txtPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
        void matchPW(string id, string password)
        {
            if ((txtID.Text == "") || (txtPW.Text == ""))
            {
                MessageBox.Show("Data belum Lengkap");
            }
            else
            {
                SqlConnection conn = konn.GetConn();
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Employee where id =" + id, conn);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        EmpID = sdr[0].ToString();
                        decryptedPW = sdr[3].ToString();
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
                if (ComputeHash256(password) == decryptedPW &&(id == EmpID))
                {
                    var nxtForm = new GUI.FormMain(txtID.Text);
                    this.Hide();
                    nxtForm.StartPosition = this.StartPosition;
                    nxtForm.FormClosed += (s, args) => this.Show();
                    nxtForm.Show();
                }
                else
                {
                    MessageBox.Show("ID atau Kata Sandi yang anda masukkan salah");
                }

            }
        }
    }
}
