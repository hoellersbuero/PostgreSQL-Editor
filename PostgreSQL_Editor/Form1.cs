using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_Editor
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection npgsql = null;

        public Form1()
        {
            InitializeComponent();
            string connectionstring = "Host=localhost:5434;Username=admin;Password=test123;Database=postgres";
            npgsql = new NpgsqlConnection(connectionstring);
            try
            {
                npgsql.Open();
            }
            catch (Exception ex)
            {
                tsslInfo.Text = "Connection failed";
            }
            if (npgsql.State == ConnectionState.Open)
            {
                tsslInfo.Text = "CONNECTED";
                NpgsqlCommand cmd = npgsql.CreateCommand();
                cmd.Connection = npgsql;
                cmd.CommandText = "SET schema 'public'";
                cmd.ExecuteNonQuery();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (npgsql.State == ConnectionState.Open)
                npgsql.Close();
            tsslInfo.Text = "Connection closed";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (npgsql.State == ConnectionState.Open)
            {
                NpgsqlCommand cmd = npgsql.CreateCommand();
                cmd.Connection = npgsql;
                //cmd.CommandText = "CREATE TABLE Articles (PID UUID PRIMARY KEY, ArticleNumber VARCHAR, Name VARCHAR, IsAvailable BOOLEAN, Weight REAL)";
                //cmd.ExecuteNonQuery();


                cmd.CommandText = "SELECT * FROM pg_catalog.pg_tables WHERE schemaname = 'public';";
                //cmd.CommandText = "SELECT * FROM information_schema.tables WHERE table_schema NOT IN ('pg_catalog', 'information_schema')";
                NpgsqlDataReader rd = cmd.ExecuteReader();
                if (rd != null)
                {
                    var dataTable = new DataTable();
                    dataTable.Load(rd);
                    dgv.DataSource = dataTable;
                    tsslSuccess.Text = "SUCCESS (" + dataTable.Rows.Count.ToString() + ")";
                }
                else
                {
                    tsslSuccess.Text = "FAILED";
                }
            }
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            NpgsqlCommand cmd = npgsql.CreateCommand();
            cmd.Connection = npgsql;
            cmd.CommandText = tbQuery.Text;
            if (cmd.CommandText.ToUpper().Contains("SELECT"))
            {
                NpgsqlDataReader rd = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(rd);
                dgv.DataSource = dataTable;
                tsslSuccess.Text = "SUCCESS (" + dataTable.Rows.Count.ToString() + ")";
            }
            else
            {
                cmd.ExecuteNonQuery();
                tsslSuccess.Text = "SUCCESS";
            }
        }
    }
}
