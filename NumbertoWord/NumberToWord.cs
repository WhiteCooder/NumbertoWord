using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumbertoWord
{
    public partial class NumberToWord : Form
    {
        public NumberToWord()
        {
            InitializeComponent();
        }

        private void txtDigit_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtDigit.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only number...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDigit.Text = txtDigit.Text.Remove(txtDigit.Text.Length - 1);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDigit.Clear();
            txtDigit.Focus();
            txtWord.Clear();
        }

        public static string NumberToWords(int number)
        {
            if (number == 0) return "Zero";
            if (number < 0) return "minus" + NumberToWords(Math.Abs(number));
            string words = "";

            if((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }
            if((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if((number / 100)> 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if(number > 0)
            {
                var unitmap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensmap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                {
                    words += unitmap[number];
                }
                else
                {
                    words += tensmap[number / 10];
                    if((number % 10) > 0)
                    {
                        words += "-" + unitmap[number % 10];
                    }
                }
            }

            return words;

            
        }

        private void txtDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtDigit.Text.Trim() != string.Empty)
            {
                if(e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    try
                    {
                        txtWord.Text = NumberToWords(Convert.ToInt32(txtDigit.Text));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please Check your Amount.", "Error");
                    }
                }
                else if(txtDigit.Text.Trim() == string.Empty)
                {
                    txtWord.Clear();
                }
            }
        }
    }
}
