using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MandhegParkingSystem472.Class
{
    class Koneksi
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataSet ds;

        string value;
        public SqlConnection GetConn()
        {
            string cs = "Data Source = .\\SQLEXPRESS;Initial Catalog=MandhegParkingSystem;Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            return con;
        }
        public string GetValueByID(string column, string table, string id)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select " + column + " from " + table + " where id =" + id, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                   value = sdr[0].ToString();
                }
            }
            finally
            {
                conn.Close();
            }
            return value;
        }
        public void SetDataGrid(string column, string table, DataGridView dgv)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select " + column + " from " + table, conn);
                ds = new DataSet();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, table);
                dgv.DataSource = ds;
                dgv.DataMember = table;
                dgv.ReadOnly = true;
                dgv.AllowUserToResizeRows = false;
                dgv.Refresh();
            }
            finally
            {
                conn.Close();
            }
        }
        public void SetComboBox(string column, string table, ComboBox cbx)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select " + column + " from " + table, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cbx.Items.Add(sdr[0].ToString());
                }
            }
            finally
            {
                conn.Close();
            }
        }
        public void SqlInsert( string table, string query)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("insert into "+table +" " + query, conn);
                cmd.ExecuteNonQuery();
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
        public void SqlUpdate(string table, string query, string id)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("update "+table+" set "+query+" where id = " + id, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public void SqlDelete(string table, string id)
        {
            SqlConnection conn = GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("delete from "+table+" where id = " + id, conn);
                cmd.ExecuteNonQuery();
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
    }
}
