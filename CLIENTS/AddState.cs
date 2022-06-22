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
        private int bitcountTemp;
        public AddState()
        {
            InitializeComponent();
        }
        public bool allOk()
        {
            return m_OK;
        }
        private List<bool> listStates = new List<bool>();
        public void setOffBtn()
        {
            a3btn.Enabled = false;
            a7btn.Enabled = false;
            a15btn.Enabled = false;
            a3btn.Visible = false;
            a7btn.Visible = false;
            a15btn.Visible = false;
        }
        public void chooseButton(string bitcount)
        {
            bitcountTemp = (int)Math.Pow(2, Int32.Parse(bitcount));
            switch (Int32.Parse(bitcount))
            {
                case 2:
                    a3btn.Enabled = true;
                    a3btn.Visible = true;
                    break;
                case 3:
                    a7btn.Enabled = true;
                    a7btn.Visible = true;
                    
                    break;
                case 4:
                    a15btn.Enabled = true;
                    a15btn.Visible = true;
                    break;
                default:
                    MessageBox.Show("Не выбрана разрядность!");
                    break;
            }
        }
        public List<bool> getStates()
        {
            return listStates;
        }

        private void AddState_Load(object sender, EventArgs e)
        {
            listStates.Clear();
            m_OK = false;
        }
        private void a0btn_Click(object sender, EventArgs e)
        {
            listStates.Add(true);
            for (int i = 1; i <= bitcountTemp; i++)
                listStates.Add(false);
            m_OK = true;
            Close();
        }
        private void a3btn_Click(object sender, EventArgs e)
        {
            listStates.Add(false);
            for (int i = 1; i <= 3; i++)
                listStates.Add(true);
            m_OK = true;
            Close();
        }

        private void a7btn_Click(object sender, EventArgs e)
        {
            listStates.Add(false);
            for (int i = 1; i <= 7; i++)
                listStates.Add(true);
            m_OK = true;
            Close();
        }

        private void a15btn_Click(object sender, EventArgs e)
        {
            listStates.Add(false);
            for (int i = 1; i <= 15; i++)
                listStates.Add(true);
            m_OK = true;
            Close();
        }
    }
}
