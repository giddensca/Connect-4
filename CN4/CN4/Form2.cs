using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CN4
{
    public partial class Form2 : Form
    {
        public Form2(string[,] passedData)
        {
            InitializeComponent();
            for (int row = 0; row < 6; row++)
            {
                writeLine(passedData, row);
            }

            dataTextBox.Select(0, 0);
        }
        private void writeLine(string[,] data, int rowToPrint)
        {
            string lineToPrint = "";
            string temp;

            for (int i = 0; i < 7; i++)
            {
                temp = data[rowToPrint, i];
                lineToPrint += temp.PadLeft(10 - temp.Length);
            }

            dataTextBox.Text += lineToPrint + "\r\n";
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
