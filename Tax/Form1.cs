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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        //List of 50 states in USA
        public string[] states = new string[]
        {
            "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida",
            "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
            "Alabama", "Massachusetts" , "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire",
            "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania",
            "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington",
            "West Virginia", "Wisconsin", "Wyoming"
        };

        //This is the structure which collects the person info
        public struct personInfo
        {
            public string name, address, city, state, zip, SSN;
            public decimal exemp, gross, fedTaxWith, capital, realEstateTax, exciseTax, medicalExpenses;
        }

        //This is the structure which calculated the person tax
        public struct taxInfo
        {
            public decimal adjustedGross, amountTax, penalty, taxOwed, Refund;
        }

        //This is an array of type personInfo (structure) to hold the 10 records
        public personInfo[] holdInfoPerson = new personInfo[10];  // taxInfo array

        //This is an array of type taxInfo (structure) to hold the 10 records of taxes
        public taxInfo[] holdInfoTax = new taxInfo[10];  // taxInfo array

        // counter for the user's info
        int i = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            comboBox1.DataSource = states;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
            else
            {
                button2_Click(sender, e);
                this.textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    //gets the value entered for each textboxs to the stucture
                    holdInfoPerson[i].name = textBox1.Text;
                    holdInfoPerson[i].address = textBox2.Text;
                    holdInfoPerson[i].city = textBox3.Text;
                    holdInfoPerson[i].state = comboBox1.Text;
                    holdInfoPerson[i].zip = textBox4.Text;
                    holdInfoPerson[i].SSN = textBox5.Text;
                    holdInfoPerson[i].exemp = Convert.ToDecimal(textBox6.Text);
                    holdInfoPerson[i].gross = Convert.ToDecimal(textBox7.Text);
                    holdInfoPerson[i].fedTaxWith = Convert.ToDecimal(textBox8.Text);
                    holdInfoPerson[i].capital = Convert.ToDecimal(textBox9.Text);
                    holdInfoPerson[i].realEstateTax = Convert.ToDecimal(textBox10.Text);
                    holdInfoPerson[i].exciseTax = Convert.ToDecimal(textBox11.Text);
                    holdInfoPerson[i].medicalExpenses = Convert.ToDecimal(textBox12.Text);

                    //fill out the capital Type: gain or loss
                    //losses
                    if (radioButton2.Checked)
                        holdInfoPerson[i].capital = System.Math.Abs(holdInfoPerson[i].capital) * (-1);

                    //Gain
                    if (radioButton1.Checked)
                        holdInfoPerson[i].capital = System.Math.Abs(holdInfoPerson[i].capital);

                    //gets the calculted taxes to the stucture taxInfo
                    holdInfoTax[i].adjustedGross = adjustedGrossFunc(holdInfoPerson[i]);
                    holdInfoTax[i].amountTax = amountTaxFunc(holdInfoPerson[i]);
                    holdInfoTax[i].penalty = 0;
                    holdInfoTax[i].taxOwed = taxOwedFunc(holdInfoTax[i]);
                    holdInfoTax[i].Refund = 0;

                    //call method to make adjustment for penalty
                    makeJudgment(ref holdInfoTax[i], ref holdInfoPerson[i]);

                    //then show the form2
                    Form2 f2 = new Form2(holdInfoTax[i], holdInfoPerson[i]);
                    f2.ShowDialog();

                    /* 
                    DialogResult sb = f2.ShowDialog();
                    if (sb == DialogResult.OK)
                        textBox1.Text = (string)f2.Tag;
                    */

                    //clear textBoxex              
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    comboBox1.SelectedIndex = 0;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox6.Text = null;
                    textBox7.Text = null;
                    textBox8.Text = null;
                    textBox9.Text = null;
                    textBox10.Text = null;
                    textBox11.Text = null;
                    textBox12.Text = null;

                    i++; // increment number of data entered

                    //if we click back to the form1, then focus on the textBox1=name                 
                    // if (!this.Focus())
                    // textBox1.Focus();

                    //Only 10 records are entered.             
                    if (i == 10)
                    {
                        //form3 has all 10 records
                        Form3 f3 = new Form3(holdInfoPerson, holdInfoTax);
                        MessageBox.Show("We are reaching the 10 Records.");
                        f3.ShowDialog();
                        this.Close();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Invalid numeric format. Please check all entries.", "Entry Error");
            }
            catch (OverflowException)
            {
                MessageBox.Show(
                    "Overflow error. Please enter smaller values.", "Entry Error");
            }

        }//end of button

        public bool IsValidData()
        {
            return
                  IsPresent(textBox1, "Name") && IsPresent(textBox2, "Address") && IsPresent(textBox3, "City") &&
                  IsPresent(textBox4, "Zip Code") && IsPresent(textBox5, "Social Security Number") && IsPresent(textBox6, "Number of Exemptions") &&
                  IsPresent(textBox7, "Gross Earnings") && IsPresent(textBox8, "Federal Tax Withheld") &&
                  IsPresent(textBox10, "Real Estate Tax") && IsPresent(textBox11, "Excise Tax") && IsPresent(textBox12, "Medical Expenses") &&

                  IsInt32(textBox4, "Zip Code") && IsInt32(textBox5, "SSN #") && IsInt32(textBox6, "Exemptions") &&
                  IsInt32(textBox7, "Gross Earnings") && IsInt32(textBox8, "Federal Tax Withheld") && IsInt32(textBox9, "Capital Gains/Losses") &&
                  IsInt32(textBox10, "Real Estate Tax") && IsInt32(textBox11, "Excise Tax") && IsInt32(textBox12, "Medical Expenses") &&

                  IsWithinRange(textBox6, "Exemptions", 0, 10);
        }

        public bool IsPresent(TextBox textbox, string name)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textbox.Focus();
                return false;
            }
            return true;
        }

        public bool IsInt32(TextBox textbox, string name)
        {
            int number = 0;
            if (Int32.TryParse(textbox.Text, out number))
                return true;
            else
            {
                MessageBox.Show(name + " must be an integer.", "Entry Error");
                textbox.Focus();
                return false;
            }
        }

        public bool IsWithinRange(TextBox textbox, string name, decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textbox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(name + " must be between " + min + " and " + max + ".", "Entry Error");
                textbox.Focus();
                return false;
            }
            return true;
        }

        //calculte the adjusted Gross
        public decimal adjustedGrossFunc(personInfo holdInfoPerson)
        {
            decimal adjGross = 0m;
            adjGross = holdInfoPerson.gross - holdInfoPerson.exemp * 1000 - 0.08m * holdInfoPerson.medicalExpenses -
                        0.25m * (holdInfoPerson.realEstateTax + holdInfoPerson.exciseTax) + 0.15m * holdInfoPerson.capital;
            return adjGross;
        }

        //calculate the amount of tax
        public decimal amountTaxFunc(personInfo holdInfoPerson)
        {
            decimal amountTax = 0m;
            amountTax = holdInfoPerson.realEstateTax + holdInfoPerson.exciseTax;
            return amountTax;
        }

        //calculate the tax owed
        public decimal taxOwedFunc(taxInfo holdInfoTax)
        {
            decimal taxOwed = 0m;
            taxOwed = taxPercentFunc(holdInfoTax.adjustedGross) * holdInfoTax.adjustedGross;
            return taxOwed;
        }

        //percent of tax 
        public decimal taxPercentFunc(decimal earn)
        {
            if (earn >= 0 && earn <= 999.99m)
                return 0.1m;
            else if (earn >= 1000.00m && earn <= 9999.99m)
                return 0.15m;
            else if (earn >= 10000.00m && earn <= 19999.99m)
                return 0.20m;
            else if (earn >= 20000.00m && earn <= 29999.99m)
                return 0.25m;
            else
                return 0.28m;
        }

        //Penalty adjustment
        public void makeJudgment(ref taxInfo holdInfoTax, ref personInfo holdInfoPerson)
        {
            decimal ninetyPercent = 0.9m * holdInfoTax.taxOwed;
            decimal tenPercent = 0.1m * System.Math.Abs(holdInfoPerson.fedTaxWith - ninetyPercent);
            if (holdInfoPerson.fedTaxWith <= ninetyPercent)
                holdInfoTax.penalty = tenPercent;
            else
                holdInfoTax.Refund = tenPercent;
        }

    }//end of class

}//end of namespace
