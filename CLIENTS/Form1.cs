using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CLIENTS
{
    public partial class Form1 : Form
    {
        public enum rectState
        {
            none,
            redact,     //Рисуется
            draw,       //Отображается
            mouseArea   //Мышь 
        }
        public enum machineType
        {
            Moora=0,
            Mealy
        }
        public enum state 
        {
            create,
            translate,
            scale
        }
        public MyRect _copyRect;
        public static Pen _myDashPen    = new Pen(new SolidBrush(Color.Red));
        public static Pen _myDrawPen    = new Pen(new SolidBrush(Color.Blue));
        public static Pen _myMousePen   = new Pen(new SolidBrush(Color.ForestGreen));
        public string ImageSaved = "";
        public string FullImage = "";
        OpenFileDialog OFD = new OpenFileDialog();
        SaveFileDialog SFD = new SaveFileDialog();
        inputBitCount bitCount = new inputBitCount();
        Image image;
        Bitmap bmp;
        bool mousePressed;
        MyRectContainer myContainer = new MyRectContainer();
        MyGraphics myGrpyphics = new MyGraphics();
        AddState ad = new AddState();
        int FPS = 0;
        public state _eState=state.create;
        public MyRect selectedRect = null;
        public string imageName;
        int offX, offY,mX,mY;
        //public static int machineType = 0;
        class MyRectContainer
        {
            machineType m_eMType;
            bool m_bNeedToRedraw = false;
            private ArrayList[] m_arrRects = new ArrayList[2];
            private bool m_MouseAreaState = false;
            MyRect m_SelectedRect = null;
            public override string ToString() // для отладки, функция возвращает строку содержащую информацию об объекте
            {
                return "Мура: " + m_arrRects[0].Count.ToString() + " Мили: " + m_arrRects[1].Count.ToString();
            }
            public void changeType(int type) // функция изменяет тип используемого автомата (Мили / Мура)
            {
                m_eMType = (machineType)type;
            }
            public void deleteSelectedRect() // функция удаления выделенной вершины
            {
                if (m_SelectedRect != null)
                    m_arrRects[(int)m_eMType].Remove(m_SelectedRect);
                m_SelectedRect = null;
            }
            public void deleteRect() // функция удаления выделенной вершины
            {
                if (m_arrRects != null)
                    m_arrRects[(int)m_eMType].Clear();
                m_SelectedRect = null;
            }
            public MyRectContainer() //конструктор
            {
                m_eMType = machineType.Moora;
                m_arrRects[0] = new ArrayList();
                m_arrRects[1] = new ArrayList();
            }
            public void redraw() // функция устанавливает флаг, оповещающий о необходимости перерисовки экрана 
            {
                m_bNeedToRedraw = true;
            }
            public void addRect(int _X, int _Y) // функция добавляет вершину с координатами Х,У
            {
                m_arrRects[(int)m_eMType].Add(new MyRect(_X, _Y, m_arrRects[(int)m_eMType].Count)); 
            }
            public void addRect(MyRect mr)  // функция добавляет вершину переданую по ссылке
            {
               
                m_arrRects[(int)m_eMType].Add(mr);
            }
            public bool needToRedraw() // функция возвращает значение, оповещающее о необходимости перерисовать
            {
                return m_bNeedToRedraw;
            }
            public bool someOneInMouse(int _X, int _Y) // функция проверяет, находится ли курсор внутри одной из вершин
            {
                foreach (MyRect r in m_arrRects[(int)m_eMType])
                    if (r.mouseInRect(_X, _Y)) return true;
                return false;
            }
            public MyRect getRectByMouse(int _X, int _Y) // функция возвращает ссылку на вершину, на которую наведен курсор, если курсор не наведен на вершину, возвращается значение null
            {
                foreach (MyRect r in m_arrRects[(int)m_eMType])
                    if (r.mouseInRect(_X, _Y)) return r;
                return null;
            }
            public bool mouseArea()
            {
                return m_MouseAreaState;
            }
            public MyRect getSelectedRect() // функция возвращает ссылку на выделенную вершину
            {
                return m_SelectedRect;
            }
            public void changeState(int _X, int _Y) // функция получает координаты курсора и перебирает все вершины, если среди вершин найдется та, на которую наведен курсор, будет изменено состояние
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
            public void saveToFile(System.IO.StreamWriter _SW) // функция сохраняет контейнер в файл
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
            public string getCount() // функция возвращает количество вершин
            {
                return m_arrRects[(int)m_eMType].Count.ToString();
            }
            public void draw(Graphics _gr) // функция рисует все вершины
            {
                    for (int i = 0; i < m_arrRects[(int)m_eMType].Count; ++i)
                        ((MyRect)m_arrRects[(int)m_eMType][i]).draw(_gr);

            }
            public void resizeContainer(Point _p) // функция изменяет размеры последней добавленной вершины
            {
                ((MyRect)m_arrRects[(int)m_eMType][m_arrRects[(int)m_eMType].Count - 1]).resize(_p);
            }
            public void stopResize()
            {
            }
        }
        
        public class MyRect:ICloneable
        {
            private int m_Number;
            private int m_leftX,m_topY,m_width,m_height;
            rectState m_eState;
            public int getX() { return m_leftX;}
            public int getY() { return m_topY; }
            public void setX(int _X) { m_leftX=_X;  }
            public void setY(int _Y) { m_topY=_Y;   }
            List<bool> m_arrStates = new List<bool>();
            public bool mouseCanScale(int _X, int _Y, int _Eps) // проверяет, находится ли курсор не более чем на 5 пикселей дальше от левого нижнего угла
            {
                if ((Math.Abs(_X - (m_leftX + m_width)) <= _Eps) && (Math.Abs(_Y - (m_topY + m_height)) <= _Eps))
                    return true;
                else
                    return false;
            }
            public object Clone() //создает копию объекта
            {
                MyRect mr = new MyRect(m_leftX,m_topY,0);
                mr.m_width = m_width;
                mr.m_height = m_height;
                mr.m_arrStates = m_arrStates.GetRange(0, m_arrStates.Count);
                return mr;
            }
            public void translate(int _X, int _y) // функция изменяет положение вершины
            {
                m_leftX = _X;
                m_topY = _y;
            }
            public rectState getState()
            {
                return m_eState;
            }
            public void saveToFile(System.IO.StreamWriter _SW) // функция записывает вершину в файл
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
            public void changeStates(List<bool> _arrStates) // функция изменяет состояния
            {
                m_arrStates = _arrStates.GetRange(0, _arrStates.Count);
            }
            public bool mouseInRect(int _X, int _Y) // проверяет, находится ли курсор мыши внутри состояния
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
            public void draw(Graphics _gr) // рисует вершину
            {
                _gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                string states = "";
                Font font = new System.Drawing.Font("Arial", 12);
                if (m_arrStates.Count > 0)
                    if (m_arrStates[0] == true)
                        states = "a0";
                    else
                        states = "a1 - " + "a" + (m_arrStates.Count - 1).ToString();
                int t = (int)_gr.MeasureString(states, font).Width;
                int t2 = (int)_gr.MeasureString(states, font).Height;
                switch (m_eState)
                {
                    case rectState.redact:
                        _gr.DrawRectangle(_myDashPen, m_leftX, m_topY, m_width, m_height);
                        _gr.DrawString(states, font, _myDashPen.Brush, m_leftX + m_width + 5, m_topY + m_height / 2 - t2 / 2);

                        break;
                    case rectState.draw:
                        _gr.DrawRectangle(_myDrawPen, m_leftX, m_topY, m_width, m_height);
                        _gr.DrawString(states, font, _myDrawPen.Brush, m_leftX + m_width + 5, m_topY + m_height / 2 - t2 / 2);
                        break;
                    case rectState.mouseArea:
                        _gr.DrawString(states, font, _myMousePen.Brush, m_leftX + m_width + 5, m_topY + m_height / 2 - t2 / 2);
                        _gr.DrawRectangle(_myMousePen, m_leftX, m_topY, m_width, m_height);
                        break;
                }
            }
            public void resize(int _X, int _Y) // функция  изменяет размеры
            {
                m_width = _X - m_leftX;
                m_height = _Y - m_topY;
                if (m_width < 10) m_width = 10;
                if (m_height < 10) m_height = 10;
            }
            public void resize(Point _p) // функция аналогична предыдущей, но имеет иные входные параметры
            {
                m_width = _p.X - m_leftX;
                m_height = _p.Y - m_topY;
                if (m_width < 10) m_width = 10;
                if (m_height < 10) m_height = 10;
            }
            public MyRect(int _X, int _Y, int _number) // конструктор
            {
                m_leftX = _X;
                m_topY = _Y;
                m_width = m_height = 10;
                m_eState = rectState.redact;
                m_Number = _number;
                for (int i = 0; i  < m_arrStates.Count;++i)
                     m_arrStates[i] = false;
            }
        }
        class MyGraphics
        {
            private Graphics m_gGraph;
            private Image m_iImage, m_reservImage;

            public MyGraphics()
            {
            }
            public void setImage(Image _img) // функция  устанавливает картинку
            {
                m_iImage = _img;
                m_reservImage = _img;
               
            }
            public void Draw(MyRectContainer _MRC) // рисует картинку и состояния
            {
                m_iImage = new Bitmap(m_reservImage);
                m_gGraph = Graphics.FromImage(m_iImage);
                _MRC.draw(m_gGraph);
            }
            public Bitmap getImage() // функция возвращает картинку
            {
                
                return (Bitmap)m_iImage;
            }
            public Bitmap getReservImage()
            {
                return new Bitmap(m_reservImage);
            }
        }

        public Form1() // конструктор
        {
            InitializeComponent();
            mousePressed = false;

            _myDashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _myDashPen.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;

            _myDashPen.Width = 5;
            _myDrawPen.Width = 5;
            _myMousePen.Width = 5;

            pictureBox1.Enabled = false;
            pictureBox1.ContextMenuStrip.Items[0].Click += ChangeRect;
            pictureBox1.ContextMenuStrip.Items[1].Click += DeleteRect;
            pictureBox1.ContextMenuStrip.Items[2].Click += copyRect;
            pictureBox1.ContextMenuStrip.Items[3].Click += pasteRect;
        }
        public void copyRect(object sender, System.EventArgs e)  // функция копирования состояния
        {
            _copyRect = (MyRect)myContainer.getSelectedRect().Clone();
        
        }
        public void pasteRect(object sender, System.EventArgs e) // функция вставки состояния
        {
            if (_copyRect==null) return;
            MyRect r = (MyRect)_copyRect.Clone();
            r.setX(mX);
            r.setY(mY);
            myContainer.addRect(r);
            redrawALL();
        }
        public void ChangeRect(object sender, System.EventArgs e) // функция изменения состояния
        {
            mousePressed = false;
            ad.chooseButton(bitCount.getBit());
            ad.ShowDialog();
            if (ad.allOk())
                myContainer.getSelectedRect().changeStates(ad.getStates());
        }
        public void DeleteRect(object sender, System.EventArgs e) // функция удаления состояния
        {
            myContainer.deleteSelectedRect();
            redrawALL();
        }
        private void открытьКартинкуToolStripMenuItem_Click(object sender, EventArgs e) // функция открытия картинки
        {
            OFD.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(OFD.FileName);
                bmp = new Bitmap(Image.FromFile(OFD.FileName));
                pictureBox1.Image = bmp;
                pictureBox1.Height = bmp.Height;
                pictureBox1.Width = bmp.Width;

                myGrpyphics.setImage(bmp);
                //image = null;
                bmp = null;
                GC.Collect();
                pictureBox1.Enabled = true;
                //pictureBox1.Location = new Point(300, 330);
                string t = "";
                ImageSaved = OFD.FileName.Substring(0, OFD.FileName.LastIndexOf('.'));
                t = OFD.FileName.Substring(OFD.FileName.LastIndexOf('\\') + 1, OFD.FileName.Length- OFD.FileName.LastIndexOf('\\')-1);
                imageName = "Vars\\imgs\\" + t;
                FullImage = OFD.FileName;
                bitCount.ShowDialog();
                bitCount.clearText();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) // функция вызывается при движении курсора по картинке
        {
            mX = e.X;
            mY = e.Y;
            if (mousePressed == true)
            {
                switch (_eState)
                {
                    case state.create:
                        myContainer.resizeContainer(new Point(e.X, e.Y));
                        break;
                    case state.scale:
                        selectedRect.resize(e.X, e.Y);
                        break;
                    case state.translate:
                        this.Cursor = Cursors.Hand;
                        selectedRect.translate(e.X + offX, e.Y + offY);
                        break;
                }

                ++FPS;
                if (FPS == 5)
                {
                    myGrpyphics.Draw(myContainer);
                    pictureBox1.Image = myGrpyphics.getImage();
                    pictureBox1.Invalidate();
                    FPS = 0;
                }
            }
            else
            {
                myContainer.changeState(e.X, e.Y);
                MyRect r = myContainer.getRectByMouse(e.X, e.Y);
                if (r != null)
                    this.Cursor = Cursors.Default;
                if (myContainer.needToRedraw())
                {
                    myGrpyphics.Draw(myContainer);
                    pictureBox1.Image = myGrpyphics.getImage();
                    pictureBox1.Invalidate();
                    FPS = 0;
                }
                else
                    GC.Collect();
                pictureBox1.ContextMenuStrip.Items[0].Visible = myContainer.mouseArea();
                pictureBox1.ContextMenuStrip.Items[1].Visible = myContainer.mouseArea();
                pictureBox1.ContextMenuStrip.Items[2].Visible = myContainer.mouseArea();
                pictureBox1.ContextMenuStrip.Items[3].Visible = !myContainer.mouseArea();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // функция вызывается при нажатии курсора на картинке
        {
            if ((e.X > (image.Width - 15)) || (e.Y > (image.Height - 15))) { mousePressed = false; return; }
            if (e.Button == MouseButtons.Left)
            {
                mousePressed = true;
                MyRect r = myContainer.getRectByMouse(e.X, e.Y);
                if (r == null)
                {
                    _eState = state.create;
                    myContainer.addRect(e.X, e.Y);
                }
                else 
                {
                    if (r.mouseCanScale(e.X,e.Y,5) == false)
                    {
                        offX = r.getX() - e.X;
                        offY = r.getY() - e.Y;
                        selectedRect = r;
                        _eState = state.translate;
                    }
                    else
                    {
                        selectedRect = r;
                        _eState = state.scale;
                        this.Cursor = Cursors.SizeAll;
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // функция вызывается при отжатии курсора на картинке
        {
            mousePressed = false;
            this.Cursor = Cursors.Default;
            GC.Collect();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void сохрнитьЭталонToolStripMenuItem_Click(object sender, EventArgs e) // функция сохранения в файл
        {
           // FileStream FS = System.IO.File.OpenWrite(ImageSaved + ".GSM");
            FileStream FS = System.IO.File.Open(ImageSaved + ".GSM",FileMode.Create);
            System.IO.StreamWriter st = new System.IO.StreamWriter(FS);
            //st.WriteLine(FullImage);
            st.WriteLine(imageName);
            myContainer.saveToFile(st);
            st.Close();
            FS.Close();
            MessageBox.Show("Вариант удачно сохранен");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void contextMenuStrip1_MouseLeave(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
        }
        private void очиститьbutton1_Click(object sender, EventArgs e)
        {
            myContainer.deleteRect();
            redrawALL();
        }
        public void redrawALL()
        {
            if (pictureBox1.Enabled == false) return;
            myContainer.redraw(); myGrpyphics.Draw(myContainer);
            pictureBox1.Image = myGrpyphics.getImage();
            pictureBox1.Invalidate();
            FPS = 0;
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myContainer.changeType(toolStripComboBox1.SelectedIndex);
            redrawALL();
        }
    }
}
