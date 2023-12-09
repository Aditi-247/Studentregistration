using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// First we have to import the sql namespace. (using System.Data.SqlClient);
using System.Data.SqlClient;

namespace Studentregistration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Loading();
        }

    //After that we have to Establish the Database Connection
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-C6BO8UK6\\SQLEXPRESS; Initial Catalog=student; User Id=adi; Password = aditi123");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        //i have created bool variable Mode.
        //if the Mode is true means allow to add the new records. Mode false means update the existing records.
        bool Mode = true;
        string sql;

        //View Records
        public void Loading()
        {
            try
            {
                sql = "select * from student";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
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

        //if you wish to edit or delete the records first have to click the edit or delete button of the particular row.
        //First Create the function getID .
        //when the data load into the datagridview data loaded along with the edit and delete buttons.i attached the screen shot image below.

        public void getID(String id)
        {
            sql = "select * from student where id = '" + id + "'  ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFees.Text = read[3].ToString();
            }
            con.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFees.Clear();
            txtName.Focus();
            button1.Text = "Save";
            Mode = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //Save and Update Records done by same button.
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fees = txtFees.Text;

            // if the mode is true add the record otherwise update the record
            if (Mode == true)
            {
                sql = "insert into student(stname,course,fees) values(@stname,@course,@fees)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);
                MessageBox.Show("Record Added Successfully!!");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtName.Focus();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set stname = @stname, course= @course,fees = @fees where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated SuccesFully!!");
                cmd.ExecuteNonQuery();
                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtName.Focus();
                button1.Text = "Save";
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
                button1.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id  = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id ", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Succesfully!!");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loading();
        }
    }
}
