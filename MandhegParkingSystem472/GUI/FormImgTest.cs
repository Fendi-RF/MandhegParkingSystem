using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace MandhegParkingSystem472.GUI
{
    public partial class FormImgTest : Form
    {
        Class.Koneksi konn = new Class.Koneksi();
        string ImgID;
        byte[] img;

        public FormImgTest()
        {
            InitializeComponent();
            konn.SetDataGrid("*", "Image", dataGridView1);
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    img = File.ReadAllBytes(file);
                    try
                    {
                        Bitmap bm = new Bitmap(file);

                        pictureBox1.Image = bm;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                
                
            }
            if (openFileDialog1.FileName != "")
            {
                button2.Enabled = true;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                ImgID = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Image (img) values (@img)", conn);
                cmd.Parameters.AddWithValue("@img", imgTOBinary(pictureBox1.Image));
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            dataGridView1.Refresh();
        }
        byte[] imgTOBinary(Image img)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
