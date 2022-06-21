using System;
using System.IO;
using System.Collections;
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
        int lengthFiles;
        public chooseVar()
        {
            InitializeComponent();
        }
        public void countFiles()
        {
            try
            {
                вариантcomboBox1.Items.Clear();
                string[] dirs = Directory.GetFiles(@"Vars\\imgs\\", "*.GSM");
                setListLength(dirs.Length);
                for (int i = 1; i <= dirs.Length; i++)
                    вариантcomboBox1.Items.Add(i);
            }
            catch (Exception e)
            {
                MessageBox.Show("Варианты не обнаружены! : ", e.ToString());
            }
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
            if (Int32.TryParse(вариантcomboBox1.Text, out num) && Int32.Parse(вариантcomboBox1.Text) <= GetListLength())
                return true; 
            else
                return false;
        }
        public int GetListLength()
        {
            return lengthFiles;
        }
        public void setListLength(int lf)
        {
            lengthFiles = lf;
        }
        public void clearText()
        {
            вариантcomboBox1.Text = "Вариант";
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
