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
    public partial class bitCount : Form
    {
        bool b_Ok = false;
        string bitText;
        public bitCount()
        {
            InitializeComponent();
        }
        public string numberBit()
        {
            return разрядностьcomboBox1.Text;
        }
        public bool allOk()
        {
            return b_Ok;
        }
        public void setBit(string newBit)
        {
            bitText = newBit;
        }
        public string getBit()
        {
            return bitText;
        }
        public bool correctBit()
        {
            int num;
            if (Int32.TryParse(разрядностьcomboBox1.Text, out num) && (Int32.Parse(разрядностьcomboBox1.Text) <= 4 || Int32.Parse(разрядностьcomboBox1.Text) > 1))
                return true;
            else
                return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (correctBit())
            {
                bitText = разрядностьcomboBox1.Text;
                b_Ok = true;
                numberBit();
                Close();
            }
            else
                MessageBox.Show("Разрядность указана неправильно!");
        }
    }
}
