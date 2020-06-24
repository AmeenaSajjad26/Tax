//Ameena
//This program creates a turbox that cauluates your taxes and display s a window with necassary demograpic finacial informaion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ameena1
{
    public partial class Form3 : Form
    {
        //constructor without arguments
        public Form3()
        {
            InitializeComponent();
        }

        //holdInfoPerson is an array of structure personInfo
        Form1.personInfo[] holdInfoPerson;

        //holdInfoTax is an array of structure taxInfo
        Form1.taxInfo[] holdInfoTax;

        //Constructor with arguments
        public Form3(Form1.personInfo[] holdInfoPerson, Form1.taxInfo[] holdInfoTax)
        {
            InitializeComponent();
            this.holdInfoPerson = holdInfoPerson;
            this.holdInfoTax = holdInfoTax;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //we have 3 columns for this dataGridView
            dataGridView2.ColumnCount = 4;

            //add 3 columns of this dataGridView
            dataGridView2.Columns[0].Name = "SSN#";
            dataGridView2.Columns[1].Name = "Name";
            dataGridView2.Columns[2].Name = "Refunded";
            dataGridView2.Columns[2].Name = "Penalty";

            //add 3 rows of this dataGridView
            for (int i = 0; i < holdInfoPerson.Length; i++)
                dataGridView2.Rows.Add(holdInfoPerson[i].SSN, holdInfoPerson[i].name, holdInfoTax[i].Refund, holdInfoTax[i].penalty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
