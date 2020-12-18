using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.panel1.BackColor = System.Drawing.Color.White;
        }

        public class CCircle // Класс фигур
        {
            public int x;
            public int y;
            public int r = 60;
            public bool flag;
            public int figure;
            public int lineX1;
            public int lineX2;
            public int lineY1;
            public int lineY2;
            public Color clr = Color.Black;
        }

        public class CircleStorage // Класс-хранилище фигур
        {
            int div(int a, int b)
            {
                if (b != 0)
                    return a / b;
                else
                    return 0;
            }
            public CCircle[] arr = new CCircle[1000];
            public int i = 0;

            public int readyLine = -1;

            public bool ctrlPress = false;

            bool select;
            public bool Check(int x, int y) // Функция проверки нажатия на объект
            {
                select = false;
                for (int j = 0; j < this.i; j++)
                {
                    if (this.arr[j] != null && this.arr[j].figure != 3)
                    {
                        if (this.arr[j] != null && x > this.arr[j].x - this.arr[j].r / 2 && x < this.arr[j].x + this.arr[j].r / 2 && y > this.arr[j].y - this.arr[j].r / 2 && y < this.arr[j].y + this.arr[j].r / 2 )
                        {
                            if (ModifierKeys.HasFlag(Keys.Control) != true) // Если контрол не нажат
                                for (int k = 0; k < this.i; k++) // Снятие выделения с остальных объектов
                                {
                                    if (this.arr[k] != null && this.arr[k].figure != 3)
                                    {
                                        if (this.arr[k] != null && !(x > this.arr[k].x - this.arr[k].r / 2 && x < this.arr[k].x + this.arr[k].r / 2 && y > this.arr[k].y - this.arr[k].r / 2 && y < this.arr[k].y + this.arr[k].r / 2))
                                            this.arr[k].flag = false;
                                    }
                                    else
                                    {
                                        if (this.arr[k] != null && !(!(x > this.arr[k].lineX1 && x > this.arr[k].lineX2) && !(y > this.arr[k].lineY1 && y > this.arr[k].lineY2) && !(x < this.arr[k].lineX1 && x < this.arr[k].lineX2) && !(y < this.arr[k].lineY1 && y < this.arr[k].lineY2) && div((x - this.arr[k].lineX1), (y - this.arr[k].lineY1)) == div((this.arr[k].lineX2 - this.arr[k].lineX1), (this.arr[k].lineY2 - this.arr[k].lineY1))))
                                        {
                                            this.arr[k].flag = false;
                                        }
                                    }
                                }
                            this.arr[j].flag = true; // Выделение указанного объекта
                            select = true;
                        }
                    }
                    else
                    {
                        if (this.arr[j] != null && !(x > this.arr[j].lineX1 && x > this.arr[j].lineX2) && !(y > this.arr[j].lineY1 && y > this.arr[j].lineY2) && !(x < this.arr[j].lineX1 && x < this.arr[j].lineX2) && !(y < this.arr[j].lineY1 && y < this.arr[j].lineY2) && div((x - this.arr[j].lineX1), (y - this.arr[j].lineY1)) == div((this.arr[j].lineX2 - this.arr[j].lineX1), (this.arr[j].lineY2 - this.arr[j].lineY1)))
                        {
                            if (ModifierKeys.HasFlag(Keys.Control) != true) // Если контрол не нажат
                                for (int k = 0; k < this.i; k++) // Снятие выделения с остальных объектов
                                {
                                    if (this.arr[k] != null && this.arr[k].figure != 3)
                                    {
                                        if (this.arr[k] != null && !(x > this.arr[k].x - this.arr[k].r / 2 && x < this.arr[k].x + this.arr[k].r / 2 && y > this.arr[k].y - this.arr[k].r / 2 && y < this.arr[k].y + this.arr[k].r / 2))
                                            this.arr[k].flag = false;
                                    }
                                    else
                                    {
                                        if (this.arr[k] != null && !(!(x > this.arr[k].lineX1 && x > this.arr[k].lineX2) && !(y > this.arr[k].lineY1 && y > this.arr[k].lineY2) && !(x < this.arr[k].lineX1 && x < this.arr[k].lineX2) && !(y < this.arr[k].lineY1 && y < this.arr[k].lineY2) && div((x - this.arr[k].lineX1), (y - this.arr[k].lineY1)) == div((this.arr[k].lineX2 - this.arr[k].lineX1), (this.arr[k].lineY2 - this.arr[k].lineY1))))
                                        {
                                            this.arr[k].flag = false;
                                        }
                                    }
                                }
                            this.arr[j].flag = true; // Выделение указанного объекта
                            select = true;
                        }
                    }
                }
                return select;
            }

            public void AddStor(CCircle circ) // Добавление созданного объекта в хранилище
            {
                if (i < 1000)
                {
                    arr[i] = circ;
                    i++;
                }
                else
                    return;
            }
        }

        CircleStorage stor = new CircleStorage();

        Pen aPen = new Pen(Color.Red, 3);
        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (stor.readyLine >= 0 && stor.arr[stor.readyLine] == null)
            {
                stor.readyLine = -1;
            }
            if (stor.Check(e.X, e.Y)) // При нажатии на объект
            {
                for (int j = 0; j < stor.i; j++) // Отрисовка объектов с учетом выделения
                {
                    if (stor.arr[j] != null)
                    {
                        if (stor.arr[j].flag)
                        {
                            Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                            Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                            switch (stor.arr[j].figure)
                            {
                                case 0:
                                    this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                    break;
                                case 1:
                                    this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                    break;
                                case 2:
                                    this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                    break;
                                case 3:
                                    if (stor.readyLine >= 0)
                                        panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                    else
                                        this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                    break;
                                case 4:
                                    panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                    break;
                            }
                        }
                        else
                        {
                            Pen mPen = new Pen(stor.arr[j].clr, 3);
                            SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                            Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                            Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                            switch (stor.arr[j].figure)
                            {
                                case 0:
                                    this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                                    break;
                                case 1:
                                    this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                                    break;
                                case 2:
                                    this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                    break;
                                case 3:
                                    if (stor.readyLine >= 0)
                                        panel1.CreateGraphics().FillEllipse(brush, dot);
                                    else
                                        this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                    break;
                                case 4:
                                    panel1.CreateGraphics().FillEllipse(brush, dot);
                                    break;
                            }
                        }
                    }
                }
            }
            else // При нажатии на холст
            {
                CCircle circ = new CCircle(); // Создание нового объекта
                circ.x = e.X;
                circ.y = e.Y;
                circ.r = Convert.ToInt32(numericUpDown1.Value);
                circ.clr = colorDialog1.Color;
                stor.AddStor(circ);
                if (listBox1.SelectedIndex == 3 && stor.readyLine >= 0)
                {
                    stor.arr[stor.i - 1].figure = 3;
                    Pen wPen = new Pen(Color.White, 3);
                    Rectangle dot = new Rectangle(stor.arr[stor.readyLine].x, stor.arr[stor.readyLine].y, 2, 2);
                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                    this.panel1.CreateGraphics().DrawLine(aPen, circ.x, circ.y, stor.arr[stor.readyLine].x, stor.arr[stor.readyLine].y);
                    stor.arr[stor.i - 1].lineX1 = stor.arr[stor.i - 1].x;
                    stor.arr[stor.i - 1].lineY1 = stor.arr[stor.i - 1].y;
                    stor.arr[stor.i - 1].lineX2 = stor.arr[stor.readyLine].x;
                    stor.arr[stor.i - 1].lineY2 = stor.arr[stor.readyLine].y;

                    stor.readyLine = -1;
                }
                else
                {
                    for (int j = 0; j < stor.i; j++)
                    {
                        if (stor.arr[j] != null)
                        {
                            if (j != (stor.i - 1)) // Снятие выделения с других объектов и отрисовка их
                            {
                                Pen mPen = new Pen(stor.arr[j].clr, 3);
                                SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                                stor.arr[j].flag = false;
                                Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                                switch (stor.arr[j].figure)
                                {
                                    case 0:
                                        this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                                        break;
                                    case 1:
                                        this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                                        break;
                                    case 2:
                                        this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                        break;
                                    case 3:
                                        if (stor.readyLine >= 0)
                                            panel1.CreateGraphics().FillEllipse(brush, dot);
                                        else
                                            this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                        break;
                                    case 4:
                                        panel1.CreateGraphics().FillEllipse(brush, dot);
                                        break;
                                }
                            }
                            else // Выделение нового объекта и его отрисовка
                            {
                                stor.arr[j].flag = true;
                                Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                                switch (listBox1.SelectedIndex)
                                {
                                    case 0:
                                        this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                        circ.figure = 0;
                                        break;
                                    case 1:
                                        this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                        circ.figure = 1;
                                        break;
                                    case 2:
                                        this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                        circ.figure = 2;
                                        break;
                                    case 3:
                                        panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                        stor.readyLine = j;
                                        stor.arr[j].r = 6;
                                        circ.figure = 3;
                                        break;
                                    case 4:
                                        panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                        circ.figure = 4;
                                        stor.arr[j].r = 6;
                                        break;
                                    default:
                                        this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                        circ.figure = 0;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null)
                {
                    if (stor.arr[j].flag == true)
                    {
                        Pen wPen = new Pen(Color.White, 3);
                        Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                break;
                        }
                        stor.arr[j] = null;
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r/2, stor.arr[j].y - stor.arr[j].r/2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null)
                {
                    if (stor.arr[j].flag == true)
                    {
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        Pen wPen = new Pen(Color.White, 3);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                stor.arr[j].r = Convert.ToInt32(numericUpDown1.Value);
                                this.panel1.CreateGraphics().DrawEllipse(aPen, stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                stor.arr[j].r = Convert.ToInt32(numericUpDown1.Value);
                                this.panel1.CreateGraphics().DrawRectangle(aPen, stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                stor.arr[j].r = Convert.ToInt32(numericUpDown1.Value);
                                this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                        }
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == false)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel2.BackColor = colorDialog1.Color;
                for (int j = 0; j < stor.i; j++)
                {
                    if (stor.arr[j] != null)
                    {
                        if (stor.arr[j].flag == true)
                        {
                            stor.arr[j].clr = colorDialog1.Color;
                            Pen wPen = new Pen(colorDialog1.Color, 3);
                            SolidBrush brush = new SolidBrush(colorDialog1.Color);
                            Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                            Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                            switch (stor.arr[j].figure)
                            {
                                case 0:
                                    this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                    break;
                                case 1:
                                    this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                    break;
                                case 2:
                                    this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                    break;
                                case 3:
                                    if (stor.readyLine >= 0)
                                        panel1.CreateGraphics().FillEllipse(brush, dot);
                                    else
                                        this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                    break;
                                case 4:
                                    panel1.CreateGraphics().FillEllipse(brush, dot);
                                    break;
                            }
                        }
                    }
                }
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == true)
                {
                    if (stor.arr[j].figure == 3)
                    {
                        if ((stor.readyLine == -1) && (stor.arr[j].lineY1 > 0) && (stor.arr[j].lineY2 > 0))
                        {
                            Pen wPen = new Pen(Color.White, 3);
                            this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            stor.arr[j].lineY1--;
                            stor.arr[j].lineY2--;
                            this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                        }
                    }
                    else if (stor.arr[j].y - (stor.arr[j].r / 2) > 0)
                    {
                        Pen wPen = new Pen(Color.White, 3);
                        Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                break;
                        }
                        stor.arr[j].y--;
                        rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                        }
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == false)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == true)
                {
                    if (stor.arr[j].figure == 3)
                    {
                        if ((stor.readyLine == -1) && (stor.arr[j].lineY1 < panel1.Height - 4) && (stor.arr[j].lineY2 < panel1.Height - 4))
                        {
                            Pen wPen = new Pen(Color.White, 3);
                            this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            stor.arr[j].lineY1++;
                            stor.arr[j].lineY2++;
                            this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                        }
                    }
                    else if (stor.arr[j].y + (stor.arr[j].r / 2) < panel1.Height - 4)
                    {
                        Pen wPen = new Pen(Color.White, 3);
                        Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                break;
                        }
                        stor.arr[j].y++;
                        rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                        }
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == false)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == true)
                {
                    if (stor.arr[j].figure == 3)
                    {
                        if ((stor.readyLine == -1) && (stor.arr[j].lineX1 > 0) && (stor.arr[j].lineX2 > 0))
                        {
                            Pen wPen = new Pen(Color.White, 3);
                            this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            stor.arr[j].lineX1--;
                            stor.arr[j].lineX2--;
                            this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                        }
                    }
                    else if (stor.arr[j].x - (stor.arr[j].r / 2) > 0)
                    {
                        Pen wPen = new Pen(Color.White, 3);
                        Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                break;
                        }
                        stor.arr[j].x--;
                        rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                        }
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == false)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == true)
                {
                    if (stor.arr[j].figure == 3)
                    {
                        if ((stor.readyLine == -1) && (stor.arr[j].lineX1 < panel1.Height - 44) && (stor.arr[j].lineX2 < panel1.Height - 44))
                        {
                            Pen wPen = new Pen(Color.White, 3);
                            this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            stor.arr[j].lineX1++;
                            stor.arr[j].lineX2++;
                            this.panel1.CreateGraphics().DrawLine(aPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                        }
                    }
                    else if (stor.arr[j].x + (stor.arr[j].r / 2) < panel1.Height - 44)
                    {
                        Pen wPen = new Pen(Color.White, 3);
                        Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(wPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(wPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                if (stor.readyLine >= 0)
                                    panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                else
                                    this.panel1.CreateGraphics().DrawLine(wPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.White, dot);
                                break;
                        }
                        stor.arr[j].x++;
                        rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                        dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                        switch (stor.arr[j].figure)
                        {
                            case 0:
                                this.panel1.CreateGraphics().DrawEllipse(aPen, rect);
                                break;
                            case 1:
                                this.panel1.CreateGraphics().DrawRectangle(aPen, rect);
                                break;
                            case 2:
                                this.panel1.CreateGraphics().DrawPolygon(aPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                                break;
                            case 3:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                            case 4:
                                panel1.CreateGraphics().FillEllipse(Brushes.Red, dot);
                                break;
                        }
                    }
                }
            }
            for (int j = 0; j < stor.i; j++)
            {
                if (stor.arr[j] != null && stor.arr[j].flag == false)
                {
                    Pen mPen = new Pen(stor.arr[j].clr, 3);
                    SolidBrush brush = new SolidBrush(stor.arr[j].clr);
                    Rectangle rect = new Rectangle(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y - stor.arr[j].r / 2, stor.arr[j].r, stor.arr[j].r);
                    Rectangle dot = new Rectangle(stor.arr[j].x, stor.arr[j].y, 2, 2);
                    switch (stor.arr[j].figure)
                    {
                        case 0:
                            this.panel1.CreateGraphics().DrawEllipse(mPen, rect);
                            break;
                        case 1:
                            this.panel1.CreateGraphics().DrawRectangle(mPen, rect);
                            break;
                        case 2:
                            this.panel1.CreateGraphics().DrawPolygon(mPen, new PointF[] { new PointF(stor.arr[j].x - stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x + stor.arr[j].r / 2, stor.arr[j].y + stor.arr[j].r / 2), new PointF(stor.arr[j].x, stor.arr[j].y - stor.arr[j].r / 2) });
                            break;
                        case 3:
                            if (stor.readyLine >= 0)
                                panel1.CreateGraphics().FillEllipse(brush, dot);
                            else
                                this.panel1.CreateGraphics().DrawLine(mPen, stor.arr[j].lineX1, stor.arr[j].lineY1, stor.arr[j].lineX2, stor.arr[j].lineY2);
                            break;
                        case 4:
                            panel1.CreateGraphics().FillEllipse(brush, dot);
                            break;
                    }
                }
            }
        }
    }
}
