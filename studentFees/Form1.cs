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

namespace studentFees
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Data Source= LAPTOP-5QOVRI8T\\SQLEXPRESS; Initial Catalog=studentdata; User Id=ankit; Password=abc123");

        SqlCommand cmd;

        SqlDataReader read;

        SqlDataAdapter drr;

        String id;

        bool Mode = true;

        String sql;


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void Load()
        {
            try
            {
                sql = "select * from records";

                cmd = new SqlCommand(sql, con);

                con.Open();

                read = cmd.ExecuteReader();

                drr = new SqlDataAdapter(sql, con);

                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void getID(String id)
        {
            sql = "select * from records where id = '" + id + "' ";

            cmd = new SqlCommand(sql, con);

            con.Open();

            read = cmd.ExecuteReader();

            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFees.Text = read[3].ToString();

            }
            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String course = txtCourse.Text;
            String fees = txtFees.Text;

            if (Mode == true)
            {
                sql = "insert into records(name,course,fees) values(@name,@course,@fees)";

                con.Open();

                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);

                MessageBox.Show("Record Added Succesfully..");

                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtName.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "update records set name = @name,course = @course,fees = @fees where id = @id";

                con.Open();

                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);
                cmd.Parameters.AddWithValue("@id", id);


                MessageBox.Show("Record Updated Succesfully..");

                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtName.Focus();

                button1.Text = "";

                Mode = true;
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                getID(id);

                button1.Text = "";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from records where id = @id";

                con.Open();

                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Deleted Succesfully!");

                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFees.Clear();
            txtName.Focus();

            button1.Text = "";

            Mode = true;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void Form1_Load_2(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtName.ForeColor = Color.Black;
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if(txtName.Text== "Enter Name")
            {
                txtName.Text = "";

                txtName.ForeColor = Color.Black;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if(txtName.Text=="")
            {
                txtName.Text = "Enter Name";

                txtName.ForeColor = Color.Silver;
            }
        }

        private void txtCourse_Enter(object sender, EventArgs e)
        {
            if (txtCourse.Text == "Enter Course")
            {
                txtCourse.Text = "";

                txtCourse.ForeColor = Color.Black;
            }
        }

        private void txtCourse_Leave(object sender, EventArgs e)
        {
            if (txtCourse.Text == "")
            {
                txtCourse.Text = "Enter Course";

                txtCourse.ForeColor = Color.Silver;
            }
        }

        private void txtFees_Enter(object sender, EventArgs e)
        {
            if (txtFees.Text == "Enter Fees")
            {
                txtFees.Text = "";

                txtFees.ForeColor = Color.Black;
            }
        }

        private void txtFees_Leave(object sender, EventArgs e)
        {
            if (txtFees.Text == "")
            {
                txtFees.Text = "Enter Fees";

                txtFees.ForeColor = Color.Silver;
            }
        }

        private void txtCourse_TextChanged(object sender, EventArgs e)
        {
            txtCourse.ForeColor = Color.Black;
        }

        private void txtFees_TextChanged(object sender, EventArgs e)
        {
            txtFees.ForeColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
    
}