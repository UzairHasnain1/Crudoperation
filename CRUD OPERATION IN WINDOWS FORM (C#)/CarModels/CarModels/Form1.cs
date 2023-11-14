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

namespace CarModels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=CarModelsDb;User ID=sa;Password=aptech");
        int carId = 0;

        private void PopulateData()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tblCarDetails", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ClearControls()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlCommand cmd = new SqlCommand("insert into tblCarDetails(Names,Model,Years) values(@name,@model,@year)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@model", textBox3.Text);
                cmd.Parameters.AddWithValue("@year", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Car Details Inserted successfully");
                PopulateData();
                ClearControls();
            }
            else
            {
                MessageBox.Show("Please enter car details");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlCommand cmd = new SqlCommand("update tblCarDetails set Names=@name,Model=@model,Years=@year where id=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", carId);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@model", textBox3.Text);
                cmd.Parameters.AddWithValue("@year", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Car Details updated successfully");
                PopulateData();
                ClearControls();
            }
            else
            {
                MessageBox.Show("Please enter car details");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(carId != 0)
            {
                SqlCommand cmd = new SqlCommand("delete tblCarDetails where id=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", carId);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Car Deleted Successfully");
                PopulateData();
                ClearControls();
            }
            else
            {
                MessageBox.Show("Please select record to delete");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            carId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            PopulateData();
            ClearControls();
        }
    }

}
