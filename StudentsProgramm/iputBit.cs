using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsProgramm
{
    public partial class iputBit : Form
    {
        public iputBit()
        {
            InitializeComponent();
        }
        public string getString()
        {
            return inputBit.Text;
        }
        public void eriseString()
        {
            inputBit.Text = "";
            label1.Text = "";
        }
        public void setStateText(string state)
        {
            label1.Text = state;
        }
        public int setMaxLenght(int maxLength)
        {
            return inputBit.MaxLength = maxLength;
        }
        private void inputBin_button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
