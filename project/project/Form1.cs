using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=COMP800\SQLEXPRESS; Initial Catalog = BookStore; Integrated Security = true";
        SqlConnection connection = null;
        DataTable table = null;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = "waitfor delay '00:00:15';select * from Author;";
                SqlCommand command = new SqlCommand(sql, connection);
                table = new DataTable();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                int line = 0;
                do
                {
                    while (await reader.ReadAsync())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                table.Columns.Add(reader.GetName(i));
                            }
                            line++;
                        }
                        DataRow dataRow = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            dataRow[i] = reader[i];
                        }
                        table.Rows.Add(dataRow);
                    }
                } while (await reader.NextResultAsync());
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://pornhub.com");
        }
    }
}
