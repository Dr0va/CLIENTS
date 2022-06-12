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
    public partial class chooseVar : Form
    {
        bool b_Ok = false;
        string variantText;
        public chooseVar()
        {
            InitializeComponent();
        }
        public int variantLength()
        {
            return вариантcomboBox1.Text.Length;
        }
        public bool allOk()
        {
            return b_Ok;
        }
        public string numberVar()
        {
            return вариантcomboBox1.Text;
        }
        public void setText(string newText)
        {
            variantText = newText;
        }
        public string getText()
        {
            return variantText;
        }
        public bool correctVariant()
        {
            int num;
            if (Int32.TryParse(вариантcomboBox1.Text, out num) && variantLength() < 3 && Int32.Parse(вариантcomboBox1.Text) <= 60)
                return true; 
            else
                return false;
        }
        private void okbutton_Click(object sender, EventArgs e)
        {
            if (correctVariant())
            {
                variantText = вариантcomboBox1.Text;
                b_Ok = true;
                numberVar();
                Close();
            }
            else
                MessageBox.Show("Вариант указан неверно!");
        }
    }
}
