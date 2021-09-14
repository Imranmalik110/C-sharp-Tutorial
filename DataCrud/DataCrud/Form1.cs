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
namespace DataCrud
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SMG7RGT\MYSQLSERVER;Initial Catalog=TPDL;Integrated Security=True");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("insert into crud(enroll,name,password,course) values('"+enroll.Text+ "','" +name.Text + "','" +pass.Text + "','" +comboBox1.Text +"')",con);
            command.ExecuteNonQuery();
            MessageBox.Show("Inserted Successfully");
            clear();
            BindData();
            con.Close();
        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from crud", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void clear()
        {
            enroll.Text = "";
            name.Text = "";
            pass.Text = "";
            comboBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "update crud set name='" + name.Text + "', password='" + pass.Text + "', course='" + comboBox1.Text + "' where enroll='" + enroll.Text + "' ";
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Updated Successfully");
            clear();
            BindData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                enroll.Text = dataGridView1.Rows[e.RowIndex].Cells["enroll"].FormattedValue.ToString();
                name.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                pass.Text = dataGridView1.Rows[e.RowIndex].Cells["password"].FormattedValue.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["course"].FormattedValue.ToString();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (enroll.Text == "")
            {
                MessageBox.Show("Enroll Id Required and please select data from list");
            }
            else
            {
                con.Open();
                string sql = "delete from crud where enroll='" + enroll.Text + "'  ";
                SqlCommand command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record deleted Successfully");
                clear();
                BindData();
            }
        }

        public void searchData(string valueToSearch)
        {
            string query = "select * from crud where concat(enroll,name,password,course) like '%" + valueToSearch +"%'";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string valueToSearch = search.Text.ToString();
            searchData(valueToSearch);
        }
    }
}
