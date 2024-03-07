using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calculator calculator;
        int cursorIndex;
        string newText;

        public  Form1()
        {
            InitializeComponent();
            ActiveControl = tbInput;
            calculator = new Calculator();
        }

        private void on_click_number(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + b.Text + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex);
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 1;
            tbInput.SelectionLength = 0;
        }

        private void on_click_operand(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + b.Text + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex);
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 1;
            tbInput.SelectionLength = 0;
        }

        private void on_click_function(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + b.Text + '(' + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex);
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 4;
            tbInput.SelectionLength = 0;
        }

        private void on_click_inverse_function(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string func = b.Text.Substring(3, 3);
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + "arc" + func + "(" + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex)+")";
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 7;
            tbInput.SelectionLength = 0;
        }

        private void on_click_backspace(object sender, EventArgs e)
        {
                if (tbInput.Text.Length > 0)
                {
                    tbInput.Text = tbInput.Text.Remove(tbInput.Text.Length - 1);
                }
        }


        private void on_click_log(object sender, EventArgs e)
        {
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + "log[10](" + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex)+ ")";
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 7;
            tbInput.SelectionLength = 0;
        }
        private void on_click_ln(object sender, EventArgs e)
        {
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + "ln(" + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex) + ")";
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + 3;
            Console.WriteLine(cursorIndex);
            tbInput.SelectionLength = 0;
        }

        private void on_click_root(object sender, EventArgs e)
        {
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + "√(" + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex)+")";
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex+2;
            tbInput.SelectionLength = 0;
        }

        private void on_click_factorial(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + "!" + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex);
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex;
            tbInput.SelectionLength = 0;
        }

        private void on_click_equal(object sender, EventArgs e)
        {
            getResult();
        }

        private void on_click_ans(object sender, EventArgs e)
        {
            cursorIndex = tbInput.SelectionStart;
            newText = tbInput.Text.Substring(0, cursorIndex) + tbResult.Text + tbInput.Text.Substring(cursorIndex, tbInput.Text.Length - cursorIndex);
            tbInput.Text = newText;
            ActiveControl = tbInput;
            tbInput.SelectionStart = cursorIndex + tbResult.Text.Length;
            tbInput.SelectionLength = 0;
        }

        private void on_click_clear(object sender, EventArgs e)
        {
            tbInput.Clear();
            ActiveControl = tbInput;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                getResult();
            }
        }

        private void tbResult_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != 'c' - 96) // except: CTRL + C (copy result)
            {
                e.Handled = true;
            }
        }
       
        private void deleteLastOperand()
        {
            if (!string.IsNullOrEmpty(tbInput.Text))
            {
                int index = tbInput.Text.Length - 1;
                while (index >= 0 && !char.IsDigit(tbInput.Text[index]))
                {
                    index--;
                }
                while (index >= 0 && char.IsDigit(tbInput.Text[index]))
                {
                    index--;
                }
                index++;
                if (index < tbInput.Text.Length)
                {
                    tbInput.Text = tbInput.Text.Remove(index);
                }
            }
        }

        private void on_click_ce(object sender, EventArgs e)
        {
            deleteLastOperand();
        }


        private void getResult()
        {
            string result = calculator.calculate(tbInput.Text, out Color color);
            tbResult.ForeColor = color;
            tbResult.Text = result;
        }

       
    }
}