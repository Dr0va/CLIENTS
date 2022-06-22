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
            comboBox1.Items.Clear();
        }
        public void SetNewComboBoxText(int countState)
        {
            comboBox1.Items.Clear();
            switch (Math.Pow(2,countState))
            {
                case 4:
                    for (int i = 0; i < 4; i++)
                        comboBox1.Items.Add("a" + i.ToString());
                    break;
                case 8:
                    for (int i = 0; i < 8; i++)
                        comboBox1.Items.Add("a" + i.ToString());
                    break;
                case 16:
                    for (int i = 0; i < 16; i++)
                        comboBox1.Items.Add("a" + i.ToString());
                    break;
                default:
                    MessageBox.Show("Не выбрана разрядность!");
                    break;
            }
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
