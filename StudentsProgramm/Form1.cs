﻿using System;
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
        List<bool> m_arrStates = new List<bool>();
        public string ImageSaved;
        public string imageName;
        public static Pen _myMousePen = new Pen(new SolidBrush(Color.ForestGreen));
        public int curRow = 0;
        public bool variantEnabled;
        int offX, offY, mX, mY;
        #endregion
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < m_arrStates.Count; ++i)
                m_arrStates[i] = false;
            dataGridView1.ReadOnly = true;
            //
            dataGridView1.Rows.Clear();
            ToolStripMenuItem ChangeBit = new ToolStripMenuItem("Изменить код состояния");
            contextMenuStrip1.Items.Add(ChangeBit);
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            ChangeBit.Click += ChangeBit_;

            //pictureBox1.ContextMenuStrip.Items[0].Click += ChangeBit;
            //
        }
        public string GSMImage = "";

        private void открытьВариантToolStripMenuItem_Click(object sender, EventArgs e)
        {
            variant.countFiles();
            variant.ShowDialog();
            if (variant.allOk())
            {
                if (variant.variantLength() == 1)
                    variant.setText('0' + variant.numberVar());
                variant.clearText();
                readFile();
            }
        }
        public void saveWork()
        {

        }
        public void readFile()
        {
            fl = true;
            ImageSaved = "Vars\\imgs\\" + variant.getText() + ".BIT";
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
            dataGridView1.Rows.Clear();
            bit.ShowDialog();
            for (int i = 0; i < Math.Pow(2, int.Parse(bit.getBit())); i++)
                m_arrStates.Add(false);
            bit.clearText();
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
            var value = "a0";
            m_fCS.SetNewComboBoxText(Int32.Parse(bit.getBit()));
            if (bit.allOk())
            {
                MyRect mr = myContainer.getRectByMouse(e.X, e.Y);
                //myContainer.getSelectedRect();
                //m_arrStates = myContainer.getSelectedRect().getStates();
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
                            //m_arrStates = mr. GetRange(0, _arrStates.Count);
                            if (m_arrStates[m_fCS.getSelectedState()] == true && (m_fCS.getSelectedState() != 0))
                            {
                                MessageBox.Show("Такое состояние уже было заполнено", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                inputbit.setMaxLenght(Int32.Parse(bit.getBit()));
                                inputbit.eriseString();
                                inputbit.setStateText("a" + m_fCS.getSelectedState().ToString() + ':');
                                inputbit.ShowDialog();
                                mr.setState(m_fCS.getSelectedState());
                                myContainer.getSelectedRect().setStateRect(m_fCS.getSelectedState());
                                m_arrStates[m_fCS.getSelectedState()] = true;
                                myContainer.getSelectedRect().setStateRectBool(m_arrStates[m_fCS.getSelectedState()]);
                                Graphics _gr = Graphics.FromImage(pictureBox1.Image);
                                mr.draw(_gr);
                                pictureBox1.Invalidate();
                                dataGridView1.Rows.Add("a" + m_fCS.getSelectedState().ToString(), inputbit.getString());
                                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                                if (dataGridView1.RowCount - 2 >= 1)
                                {
                                    if (dataGridView1.Rows[1].Cells[0].Value.ToString().Contains(value))
                                    {
                                        dataGridView1.Rows.Add("a0", dataGridView1.Rows[1].Cells[1].Value);
                                        dataGridView1.Rows.RemoveAt(1);
                                    }
                                }
                            }
                        }
                        else
                            MessageBox.Show("Недопустимое состояние", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else MessageBox.Show("Введите разрядность кода состояний!");
        }
        public void ChangeBit_(object sender, EventArgs e)
        {
            myContainer.getSelectedRect().getStateRect();
            //if (m_arrStates[m_fCS.getSelectedState()] == false)
            //{
            //    MessageBox.Show("Состояние ещё не указано!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            if (myContainer.getSelectedRect().getStateRectBool() != true)
            {
                MessageBox.Show("Состояние ещё не указано!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                inputbit.eriseString();
                var value = "a0";
                dataGridView1.ClearSelection();
                inputbit.setStateText("a" + myContainer.getSelectedRect().getStateRect() + ':');
                inputbit.ShowDialog();
                if (dataGridView1.RowCount - 2 >= 1)
                {
                    if (dataGridView1.Rows[1].Cells[0].Value.ToString().Contains(value))
                    {
                        dataGridView1.Rows.Add("a0", dataGridView1.Rows[1].Cells[1].Value);
                        dataGridView1.Rows.RemoveAt(1);
                    }
                }
                dataGridView1.Rows.RemoveAt(myContainer.getSelectedRect().getStateRect());
                dataGridView1.Rows.Add("a" + myContainer.getSelectedRect().getStateRect(), inputbit.getString());
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                if (dataGridView1.RowCount - 2 >= 1)
                {
                    if (dataGridView1.Rows[1].Cells[0].Value.ToString().Contains(value))
                    {
                        dataGridView1.Rows.Add("a0", dataGridView1.Rows[1].Cells[1].Value);
                        dataGridView1.Rows.RemoveAt(1);
                    }
                }
            }
        }
        public string intToBin(int _a, int _bitCount)
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
            for (int i = 0; i < m_arrStates.Count; ++i)
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
        private void contextMenuStrip1_MouseLeave(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mX = e.X;
            mY = e.Y;
            myContainer.changeState(e.X, e.Y);
            pictureBox1.ContextMenuStrip.Items[0].Visible = myContainer.mouseArea();
        }

        public enum rectState
        {
            none,
            redact,     //Рисуется
            draw,       //Отображается
            mouseArea   //Мышь 
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(ImageSaved, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);
            try
            {
                streamWriter.Write(variant.getText() + '\n');
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                    {
                        streamWriter.Write(dataGridView1.Rows[j].Cells[i].Value + "\t");
                    }
                    streamWriter.WriteLine();
                }
                streamWriter.Close();
                fs.Close();
                MessageBox.Show("Вариант удачно сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        public enum machineType
        {
            Moora = 0, Mealy
        }
        public class MyRect
        {
            private int m_Number;
            public int m_leftX, m_topY, m_width, m_height;
            private int stateRect;
            string temp = "";
            string temp2 = "";
            rectState m_eState;
            bool m_bFree, stateRectBool;
            List<bool> m_arrStates = new List<bool>();
            public rectState getState()
            {
                return m_eState;
            }
            public bool isFree()
            {
                return m_bFree;
            }
            public List<bool> getStates()
            {
                return m_arrStates;
            }
            public void SetCountList(int count)
            {
                for (int i = 0; i < count; i++)
                    m_arrStates.Add(false);
            }
            public void changeStates(List<bool> _arrStates) // функция изменяет состояния
            {
                m_arrStates = _arrStates.GetRange(0, _arrStates.Count);
            }
            public int getStateRect()
            {
                return stateRect;
            }
            public bool getStateRectBool()
            {
                return stateRectBool;
            }
            public void setStateRectBool(bool sSRB)
            {
                stateRectBool = sSRB;
            }
            public void setStateRect(int stRc)
            {
                stateRect = stRc;
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
                for (int i = 0; i < m_arrStates.Count; ++i)
                    if (m_arrStates[i] == true)
                        _SW.WriteLine("1");
                    else
                        _SW.WriteLine("0");
            }
            public string readFromFile(System.IO.StreamReader _SR, string precol)
            {

                try
                { //string temp;
                    switch (int.Parse(precol))
                    {
                        case 0:
                            m_leftX = int.Parse(_SR.ReadLine());
                            break;
                        case 1:
                            m_leftX = int.Parse(_SR.ReadLine());
                            break;
                        default:
                            m_leftX = int.Parse(precol);
                            break;
                    }
                }
                catch { return null; }
                //if (int.Parse(precol) == 0 || int.Parse(precol) == 1)
                //    m_leftX = int.Parse(_SR.ReadLine());
                //else
                //    m_leftX = int.Parse(precol);
                m_topY = int.Parse(_SR.ReadLine());
                m_width = int.Parse(_SR.ReadLine());
                m_height = int.Parse(_SR.ReadLine());
                temp = _SR.ReadLine();
                for (; temp == "0" || temp == "1"; temp = _SR.ReadLine())
                {
                    if (temp == "1")
                    {
                        //_SR.ReadLine();
                        m_arrStates.Add(true);
                        //precol = temp;
                    }

                    else
                    {
                        //_SR.ReadLine();
                        m_arrStates.Add(false);
                        //precol = temp;
                    }

                }
                //for (int i = 0; i < m_arrStates.Count; ++i)
                //    if ((int.Parse(_SR.ReadLine())) == 1)
                //        m_arrStates[i] = true;
                //    else
                //        m_arrStates[i] = false;
                return temp;
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
                for (int i = 0; (i < m_arrStates.Count) && (i < _arrStates.Length); ++i)
                    m_arrStates[i] = _arrStates[i];
            }
            public void setState(int _stateNumber)
            {
                for (int i = 0; (i < m_arrStates.Count); ++i)
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
                for (int i = 0; i < m_arrStates.Count; ++i)
                {
                    if (m_arrStates[i] == true)
                    {
                        if (states != "")
                            states += " ";
                        states += "a" + i.ToString();
                    }
                }
                //if (m_arrStates.Count > 0)
                //    if (m_arrStates[0] == true)
                //        states = "a0";
                //    else
                //        states = "a1 - " + "a" + (m_arrStates.Count - 1).ToString();
                t = (int)_gr.MeasureString(states, font).Width;
                t2 = (int)_gr.MeasureString(states, font).Height;

                Pen _myMousePen = new Pen(new SolidBrush(Color.ForestGreen));
                _myMousePen.Width = 5;
                _gr.DrawString(states, font, _myMousePen.Brush, m_leftX + m_width + 5, m_topY + m_height / 2 - t2 / 2);
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
            }
        }
        public class vremyanka
        {
            string temp2 = "0";
            public vremyanka() 
            { 
            }
            public void WriteTemp(string temp_)
            {
                temp2 = temp_;
            }
            public string ReadTemp()
            {
                return temp2;
            }
        }
        public class MyRectContainer
        {
            machineType m_eMType;
            bool m_bNeedToRedraw = false;
            private ArrayList[] m_arrRects = new ArrayList[2];
            private bool m_MouseAreaState = false;
            MyRect m_SelectedRect = null;
            vremyanka textik = new vremyanka();
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
            public void deleteRect() // функция удаления выделенной вершины
            {
                if (m_arrRects != null)
                    m_arrRects[(int)m_eMType].Clear();
                m_SelectedRect = null;
            }
            public MyRect getRectByMouse(int _X, int _Y)
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
            //public void saveToFile(System.IO.StreamWriter _SW)
            //{
            //    _SW.WriteLine(m_arrRects[0].Count);
            //    foreach (MyRect r in m_arrRects[0])
            //    {
            //        r.saveToFile(_SW);
            //    }
            //    _SW.WriteLine(m_arrRects[1].Count);
            //    foreach (MyRect r in m_arrRects[1])
            //    {
            //        r.saveToFile(_SW);
            //    }
            //}
            public void readFromFile(StreamReader _SR)
            {
                int n = int.Parse(_SR.ReadLine());
                //string circle = textik.ReadTemp();
                MyRect mr;
                
                m_arrRects[0].Clear();
                m_arrRects[1].Clear();
                for (int i = 0; i < n; ++i)
                {
                    
                    mr = new MyRect(0, 0, i);
                    //mr.SetCountList(n);
                    textik.WriteTemp(mr.readFromFile(_SR, textik.ReadTemp()));
                    m_arrRects[0].Add(mr);
                }
                n = int.Parse(_SR.ReadLine());
                for (int i = 0; i < n; ++i)
                {
                    mr = new MyRect(0, 0, i);
                    textik.WriteTemp(mr.readFromFile(_SR, textik.ReadTemp()));
                    m_arrRects[1].Add(mr);
                }
            }
            public string getCount()
            {
                return m_arrRects[(int)m_eMType].Count.ToString();
            }
            public ArrayList getThisRect()
            {
                return m_arrRects[(int)m_eMType];
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
}
