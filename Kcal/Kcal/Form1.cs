using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kcal
{
    public partial class Kcal : Form
    {
        OleDbConnection myconnection = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "\\Kcal.accdb");
        public Kcal()
        {
            InitializeComponent();
        }
        int newcal = 0;
        int sumKcal = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            showlist();
            myconnection.Open();
            OleDbCommand datecheck = new OleDbCommand("select Kcal from Kcall where tarih=@tarih", myconnection);
            datecheck.Parameters.Add("tarih", OleDbType.DBDate).Value = dateTimePicker1.Text;
            OleDbDataReader readd = datecheck.ExecuteReader();
            if (readd.Read() == true)
            {
                newcal = Convert.ToInt32(textBox2.Text) + Convert.ToInt32(readd["Kcal"].ToString());
                try
                {
                    OleDbCommand Addline = new OleDbCommand("UPDATE Kcall set Kcal='" + newcal + "' where tarih=@tarih", myconnection);
                    Addline.Parameters.Add("tarih", OleDbType.DBDate).Value = dateTimePicker1.Text; //***
                     Addline.ExecuteNonQuery();
                    myconnection.Close();
                    showlist();
                    calcsum();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR");
                    myconnection.Close();
                    throw;
                }
            }
            else
            {
                try
                {
                    OleDbCommand Addline = new OleDbCommand("insert into Kcall(tarih,Kcal) values('" + dateTimePicker1.Text + "','" + textBox2.Text + "')", myconnection);
                    Addline.ExecuteNonQuery();
                    myconnection.Close();
                    showlist();
                    calcsum();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR");
                    myconnection.Close();
                    throw;
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            showlist();
            try
            {
                myconnection.Open();
                OleDbCommand updateline = new OleDbCommand("UPDATE Kcall set Kcal='" + textBox2.Text + "' where tarih=@tarih",myconnection);
                updateline.Parameters.Add("tarih", OleDbType.DBDate).Value = dateTimePicker1.Text; //***
                updateline.ExecuteNonQuery();
                myconnection.Close();
                showlist();
                calcsum();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message, "ERROR");
                myconnection.Close();
                throw;
            }
        }

        private void showlist()
        {
            try
            {
                myconnection.Open();
                OleDbDataAdapter dgvlist = new OleDbDataAdapter("select * from Kcall", myconnection);
                DataSet dgvmemory = new DataSet();
                dgvlist.Fill(dgvmemory);
                dataGridView1.DataSource = dgvmemory.Tables[0];
                myconnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR");
                myconnection.Close();
                throw;
            }
        }
        private void calcsum()
        {
            sumKcal = 0;
            myconnection.Open();
            OleDbCommand calcsum = new OleDbCommand("select Kcal from Kcall", myconnection);
            OleDbDataReader readdd = calcsum.ExecuteReader();
            while(readdd.Read())
            {
                sumKcal = sumKcal + Convert.ToInt32(readdd["Kcal"].ToString());
                
            }
            myconnection.Close();
            textBox1.Text = sumKcal.ToString();
            prgsbar();

        }
        private void prgsbar()
        {
            if (sumKcal <= 7500)
            {
                progressBar1.Value = sumKcal;
                progressBar2.Value = 0;
                progressBar3.Value = 0;
                progressBar4.Value = 0;
                progressBar5.Value = 0;
                label2.Text = progressBar1.Value.ToString();
                label4.Text = "0";
                label6.Text = "0";
                label8.Text = "0";
                label10.Text = "0";
            }
            else if (sumKcal > 7500 && sumKcal <= 15000)
            {
                progressBar1.Value = 7500;
                progressBar2.Value = sumKcal - 7500;
                progressBar3.Value = 0;
                progressBar4.Value = 0;
                progressBar5.Value = 0;
                label2.Text = "7500";
                label4.Text = progressBar2.Value.ToString();
                label6.Text = "0";
                label8.Text = "0";
                label10.Text = "0";
            }
            else if (sumKcal >15000 && sumKcal <= 22500)
            {
                progressBar1.Value = 7500;
                progressBar2.Value = 7500;
                progressBar3.Value = sumKcal - 15000;
                progressBar4.Value = 0;
                progressBar5.Value = 0;
                label2.Text = "7500";
                label4.Text = "7500";
                label6.Text = progressBar3.Value.ToString();
                label8.Text = "0";
                label10.Text = "0";
            }
            else if (sumKcal > 22500 && sumKcal <= 30000)
            {
                progressBar1.Value = 7500;
                progressBar2.Value = 7500;
                progressBar3.Value = 7500;
                progressBar4.Value = sumKcal - 22500;
                progressBar5.Value = 0;
                label2.Text = "7500";
                label4.Text = "7500";
                label6.Text = "7500";
                label8.Text = progressBar3.Value.ToString();
                label10.Text = "0";

            }
            else if (sumKcal > 30000 && sumKcal <= 37500)
            {
                progressBar1.Value = 7500;
                progressBar2.Value = 7500;
                progressBar3.Value = 7500;
                progressBar4.Value = 7500;
                progressBar5.Value = sumKcal - 30000;
                label2.Text = "7500";
                label4.Text = "7500";
                label6.Text = "7500";
                label8.Text = "7500";
                label10.Text = progressBar3.Value.ToString();
            }
            else if(sumKcal > 37500)
            {
                MessageBox.Show("Value is too high!");
            }
        }

        private void Kcal_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            calcsum();
            showlist();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            showlist();
            try
            {
                myconnection.Open();
                OleDbCommand deleteline = new OleDbCommand("delete from Kcall where tarih=@tarih", myconnection);
                deleteline.Parameters.Add("tarih", OleDbType.DBDate).Value = dateTimePicker1.Text;
                deleteline.ExecuteNonQuery();
                myconnection.Close();
                showlist();
                calcsum();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR");
                myconnection.Close();
                throw;
            }
        }
    }
}
