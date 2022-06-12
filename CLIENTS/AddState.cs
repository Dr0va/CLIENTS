using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENTS
{
    public partial class AddState : Form
    {
        private bool m_OK = false;
        public AddState()
        {
            InitializeComponent();

        }
        public bool allOk()
        {
            return m_OK;
        }
        private bool[] m_arr = new bool[8];

        private void button1_Click(object sender, EventArgs e)
        {
            m_arr[0] = checkBox1.Checked;
            m_arr[1] = checkBox2.Checked;
            m_arr[2] = checkBox3.Checked;
            m_arr[3] = checkBox4.Checked;
            m_arr[4] = checkBox5.Checked;
            m_arr[5] = checkBox6.Checked;
            m_arr[6] = checkBox7.Checked;
            m_arr[7] = checkBox8.Checked;
            m_OK = true;
            Close();
        }
        public bool[] getStates()
        {
            return m_arr;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_OK = false;
            Close();

        }

        private void AddState_Load(object sender, EventArgs e)
        {

            m_OK = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
        }
    }
}
