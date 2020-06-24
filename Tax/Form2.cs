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
    public partial class Form2 : Form
    {
        //holdInfoTax is a variable of structure taxInfo
        Form1.taxInfo holdInfoTax;

        //holdInfoPerson is a variable of structure personInfo
        Form1.personInfo holdInfoPerson;

        //Constructor without arguments
        public Form2()
        {
            InitializeComponent();
        }

        //Constructor with arguments
        public Form2(Form1.taxInfo holdInfoTax, Form1.personInfo holdInfoPerson)
        {
            InitializeComponent();
            this.holdInfoTax = holdInfoTax;
            this.holdInfoPerson = holdInfoPerson;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = holdInfoTax.adjustedGross.ToString("c"); ;
            textBox2.Text = holdInfoTax.amountTax.ToString("c");
            textBox3.Text = holdInfoPerson.fedTaxWith.ToString("c");
            textBox4.Text = holdInfoTax.penalty.ToString("c"); ;
            textBox5.Text = holdInfoTax.taxOwed.ToString("c");
            textBox6.Text = holdInfoTax.Refund.ToString("c");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }
    }
}
