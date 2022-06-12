using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentsProgramm
{
    
    public partial class Form1 : Form
    {
        bool fl = false;
        #region GLOBALS
        OpenFileDialog OFD = new OpenFileDialog();
        bool mousePressed = false;
        private chooseState m_fCS = new chooseState();
        Image img;
        Bitmap bmp;
        MyRectContainer myContainer = new MyRectContainer();
        chooseVar variant = new chooseVar();
        bitCount bit = new bitCount();
        iputBit inputbit = new iputBit();
        bool[] m_arrStates = new bool[8];
        public static Pen _myMousePen = new Pen(new SolidBrush(Color.ForestGreen));
        public int curRow = 0;
        public bool variantEnabled;
        #endregion
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < m_arrStates.Length; ++i)
                m_arrStates[i] = false;
            dataGridView1.ReadOnly = true;
        }
        public string GSMImage = "";
        private void открытьВариантToolStripMenuItem_Click(object sender, EventArgs e)
        {
            variant.ShowDialog();
            if (variant.allOk())
            {
                if (variant.variantLength() == 1)
                    variant.setText('0' + variant.numberVar());
                readFile();
            }
        }
        public void readFile()
        {
            fl = true;
            FileStream FS = File.Open("Vars\\imgs\\" + variant.getText() + ".GSM", FileMode.Open);
            StreamReader SR = new StreamReader(FS);
            readFile(SR);
            FS.Close();
            SR.Close();
            variantEnabled = true;
            variantEnabled = false;
        }
        public void readFile(StreamReader SR)
        {
            string t = SR.ReadLine();
            img = Image.FromFile(t);
            bmp = new Bitmap(Image.FromFile(t));
            pictureBox1.Image = bmp;
            pictureBox1.Width += 300;
            myContainer.readFromFile(SR);
            bit.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bit.allOk())
            {
                MyRect mr = myContainer.getRectByMouse(e.X, e.Y);
                if (mr == null)
                {
                    //MessageBox.Show("Ошибка!", "Ошибка при заполнении");
                }
                else
                {
                    if (!mr.isFree())
                    {
                        return;
                    }
                    if (m_fCS.ShowDialog() == DialogResult.OK)
                    {
                        if (mr.isCorrectState(m_fCS.getSelectedState()))
                        {
                            if (m_arrStates[m_fCS.getSelectedState()] == true && (m_fCS.getSelectedState() != 0))
                            {
                                MessageBox.Show("Такое состояние уже было заполнено", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                inputbit.setMaxLenght(Int32.Parse(bit.getBit()));
                                inputbit.ShowDialog();
                                mr.setState(m_fCS.getSelectedState());
                                m_arrStates[m_fCS.getSelectedState()] = true;
                                Graphics _gr = Graphics.FromImage(pictureBox1.Image);
                                mr.draw(_gr);
                                pictureBox1.Invalidate();
                                dataGridView1.Rows.Add("a" + m_fCS.getSelectedState().ToString(), inputbit.getString());
                                //dataGridView1.Rows.Add("a" + m_fCS.getSelectedState().ToString(), intToBin(m_fCS.getSelectedState(), Int32.Parse(bit.getBit())));

                            }
                        }
                        else
                            MessageBox.Show("Недопустимое состояние", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else MessageBox.Show("Введите разрядность кода состояний!");
        }
        public string intToBin(int _a,int _bitCount)
        {

            string t = "";
            int mask = 1;
            for (int i = 0; i < _bitCount - 1; ++i)
                mask <<= 1;
            for (int i = 0; i < _bitCount; ++i)
            {
                if ((_a & mask) != 0)
                    t += "1";
                else
                    t += "0";
                mask >>= 1;
            }
            return t;
        }
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            myContainer.changeType((int)(toolStripComboBox1.SelectedIndex));
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 2;
            for (int i = 0; i < m_arrStates.Length; ++i)
                m_arrStates[i] = false;
            if (fl == false) return;
            FileStream FS = File.Open(OFD.FileName, FileMode.Open);
            StreamReader SR = new StreamReader(FS);
            readFile(SR);
            FS.Close();
            SR.Close();
        }

        private void проверитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void проверитьbutton_Click(object sender, EventArgs e)
        {
            if (myContainer.check() == false)
                MessageBox.Show("Не все состояния указаны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("ГСМ размечена правильно!", "Верно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public enum rectState
    {
        none,
        redact,     //Рисуется
        draw,       //Отображается
        mouseArea   //Мышь 
    }
    public enum machineType
    {
        Moora = 0, Mealy
    }
    public class MyRect
    {
        private int m_Number;
        private int m_leftX, m_topY, m_width, m_height;
        rectState m_eState;
        bool m_bFree;

        bool[] m_arrStates = new bool[8];
        public rectState getState()
        {
            return m_eState;
        }
        public bool isFree()
        {
            return m_bFree;
        }
        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();

            SB.AppendFormat(m_leftX.ToString() + " " + m_topY.ToString() + " " + m_width.ToString() + " " + m_height.ToString());
            
            return SB.ToString();
        }
        public void saveToFile(System.IO.StreamWriter _SW)
        {
            _SW.WriteLine(m_leftX.ToString());
            _SW.WriteLine(m_topY.ToString());
            _SW.WriteLine(m_width.ToString());
            _SW.WriteLine(m_height.ToString());
            for (int i = 0; i < m_arrStates.Length; ++i)
                if (m_arrStates[i] == true)
                    _SW.WriteLine("1");
                else
                    _SW.WriteLine("0");
        }
        public void readFromFile(System.IO.StreamReader _SR)
        {
            m_leftX = int.Parse(_SR.ReadLine());
            m_topY = int.Parse(_SR.ReadLine());
            m_width = int.Parse(_SR.ReadLine());
            m_height = int.Parse(_SR.ReadLine());
            for (int i = 0; i < m_arrStates.Length; ++i)
                if ((int.Parse(_SR.ReadLine())) == 1)
                    m_arrStates[i] = true;
                else
                    m_arrStates[i] = false;
        }
        public bool isCorrectState(int a)
        {
            try
            {
                if (m_arrStates[a] == true)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void changeStates(bool[] _arrStates)
        {
            for (int i = 0; (i < m_arrStates.Length) && (i < _arrStates.Length); ++i)
                m_arrStates[i] = _arrStates[i];
        }
        public void setState(int _stateNumber)
        {
            for (int i = 0; (i < m_arrStates.Length); ++i)
                m_arrStates[i] = false;
            m_arrStates[_stateNumber] = true;
            m_bFree = false;
        }
        public bool mouseInRect(int _X, int _Y)
        {
            if
                (
                    (
                        (_X < (m_leftX + m_width)) && (_X > m_leftX)
                    )
                    &&
                    (
                        (_Y < (m_topY + m_height)) && (_Y > m_topY)
                    )
                )
                return true;
            else
                return false;
        }
        public void changeStatus(rectState _newState)
        {
            m_eState = _newState;
        }
        
        public void draw(Graphics _gr)
        {
            _gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            string states = "";
            int t, t2;
            Font font = new System.Drawing.Font("Times New Roman", 12);
            for (int i = 0; i < m_arrStates.Length; ++i)
            {
                if (m_arrStates[i] == true)
                {
                    if (states != "")
                        states += " ";
                    states += "a" + i.ToString();
                }
            }

            t = (int)_gr.MeasureString(states, font).Width;
            t2 = (int)_gr.MeasureString(states, font).Height;

            Pen _myMousePen = new Pen(new SolidBrush(Color.ForestGreen));
            _myMousePen.Width = 5;
            _gr.DrawString(states, font, _myMousePen.Brush, m_leftX + m_width  +5, m_topY + m_height / 2 - t2/2);
            _gr.DrawRectangle(_myMousePen, m_leftX, m_topY, m_width, m_height);
        }
        
        public void resize(Point _p)
        {
            m_width = _p.X - m_leftX;
            m_height = _p.Y - m_topY;
            if (m_width < 10) m_width = 10;
            if (m_height < 10) m_height = 10;
        }
        public MyRect(int _X, int _Y, int _number)
        {
            m_bFree = true;
            m_leftX = _X;
            m_topY = _Y;
            m_width = m_height = 0;
            m_eState = rectState.redact;
            m_Number = _number;
            for (int i = 0; i < m_arrStates.Length; ++i)
                m_arrStates[i] = false;
        }
    }
    public class MyRectContainer
    {
        machineType m_eMType;
        bool m_bNeedToRedraw = false;
        private ArrayList[] m_arrRects = new ArrayList[2];
        private bool m_MouseAreaState = false;
        MyRect m_SelectedRect = null;
        public void changeType(int type)
        {
            m_eMType = (machineType)type;
        }
        public bool check()
        {
            for (int i = 0; i < m_arrRects[(int)m_eMType].Count; ++i)
                if (((MyRect)(m_arrRects[(int)m_eMType][i])).isFree())
                {
                    return false;
                }
            return true;
        }
        public int getMaxState()
        {
            return m_arrRects[(int)m_eMType].Count;
        }
        public MyRect getRectByMouse(int _X,int _Y)
        {
            foreach (MyRect mr in m_arrRects[(int)m_eMType])
            {
                if (mr.mouseInRect(_X, _Y))
                    return mr;
            }
            return null;
        }
        public void deleteSelectedRect()
        {
            if (m_SelectedRect != null)
                m_arrRects[(int)m_eMType].Remove(m_SelectedRect);
            m_SelectedRect = null;
        }
        public MyRectContainer()
        {
            m_eMType = machineType.Moora;
            m_arrRects[0] = new ArrayList();
            m_arrRects[1] = new ArrayList();
        }
        public void redraw()
        {
            m_bNeedToRedraw = true;
        }
        public void addRect(int _X, int _Y)
        {
            m_arrRects[(int)m_eMType].Add(new MyRect(_X, _Y, m_arrRects[(int)m_eMType].Count));
        }
        public bool needToRedraw()
        {
            return m_bNeedToRedraw;
        }
        public bool mouseArea()
        {
            return m_MouseAreaState;
        }
        public MyRect getSelectedRect()
        {
            return m_SelectedRect;
        }
        public void changeState(int _X, int _Y)
        {
            m_bNeedToRedraw = false;
            m_MouseAreaState = false;
            rectState _rS;
            foreach (MyRect r in m_arrRects[(int)m_eMType])
            {
                _rS = r.getState();
                if (r.mouseInRect(_X, _Y))
                    r.changeStatus(rectState.mouseArea);
                else
                    r.changeStatus(rectState.draw);
                if (r.getState() != _rS)
                    m_bNeedToRedraw = true;
                if (r.getState() == rectState.mouseArea)
                {
                    m_MouseAreaState = true;
                    m_SelectedRect = r;
                }
            }
        }
        public void saveToFile(System.IO.StreamWriter _SW)
        {
            _SW.WriteLine(m_arrRects[0].Count);
            foreach (MyRect r in m_arrRects[0])
            {
                r.saveToFile(_SW);
            }
            _SW.WriteLine(m_arrRects[1].Count);
            foreach (MyRect r in m_arrRects[1])
            {
                r.saveToFile(_SW);
            }
        }
        public void readFromFile(StreamReader _SR)
        {
            int n = int.Parse(_SR.ReadLine());
            MyRect mr;
            m_arrRects[0].Clear();
            m_arrRects[1].Clear();
            for (int i = 0; i < n; ++i)
            {
                mr = new MyRect(0,0,i);
                mr.readFromFile(_SR);
                m_arrRects[0].Add(mr);
            }
            n = int.Parse(_SR.ReadLine());
            for (int i = 0; i < n; ++i)
            {
                mr = new MyRect(0, 0, i);
                mr.readFromFile(_SR);
                m_arrRects[1].Add(mr);
            }
        }
        public string getCount()
        {
            return m_arrRects[(int)m_eMType].Count.ToString();
        }
        public void draw(Graphics _gr)
        {
            //for (int i = 0; i < m_arrRects[(int)m_eMType].Count; ++i)
            //    ((MyRect)m_arrRects[(int)m_eMType][i]).draw(_gr);

        }
        public void resizeContainer(Point _p)
        {
            ((MyRect)m_arrRects[(int)m_eMType][m_arrRects[(int)m_eMType].Count - 1]).resize(_p);
        }
        public void stopResize()
        {
        }
    }
}
