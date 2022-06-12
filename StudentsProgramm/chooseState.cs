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
    public partial class chooseState : Form
    {
        public chooseState()
        {
            InitializeComponent();
        }
        private int m_chooseState = 0;
        private void chooseState_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        public int getSelectedState()
        {
            return m_chooseState;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_chooseState = comboBox1.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
