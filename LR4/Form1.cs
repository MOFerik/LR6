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
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        public class CCircle // Класс кругов
        {
            public int x;
            public int y;
            public int r = 60;
            public bool flag;
        }

        public class CircleStorage // Класс-хранилище кругов
        {
            public CCircle[] arr = new CCircle[1000];
            public int i = 0;

            public bool ctrlPress = false;

            bool select;
            public bool Check(int x, int y) // Функция проверки нажатия на объект
            {
                select = false;
                for (int j = 0; j < this.i; j++)
                {
                    if (this.arr[j] != null && x > this.arr[j].x - 30 && x < this.arr[j].x + 30 && y > this.arr[j].y - 30 && y < this.arr[j].y + 30)
                    {
                        if (ModifierKeys.HasFlag(Keys.Control) != true) // Если контрол не нажат
                            for (int k = 0; k < this.i; k++) // Снятие выделения с остальных объектов
                            {
                                if (this.arr[k] != null && !(x > this.arr[k].x - 30 && x < this.arr[k].x + 30 && y > this.arr[k].y - 30 && y < this.arr[k].y + 30))
                                    this.arr[k].flag = false;
                            }
                        this.arr[j].flag = true; // Выделение указанного объекта
                        select = true;
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

        Pen mPen = new Pen(Color.Black, 3);
        Pen aPen = new Pen(Color.Red, 3);
        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (stor.Check(e.X, e.Y)) // При нажатии на объект
            {
                for (int j = 0; j < stor.i; j++) // Отрисовка объектов с учетом выделения
                {
                    if (stor.arr[j] != null)
                    {
                        if (stor.arr[j].flag)
                        {
                            Rectangle rect = new Rectangle(stor.arr[j].x - 30, stor.arr[j].y - 30, 60, 60);
                            panel1.CreateGraphics().DrawEllipse(aPen, rect);
                        }
                        else
                        {
                            Rectangle rect = new Rectangle(stor.arr[j].x - 30, stor.arr[j].y - 30, 60, 60);
                            panel1.CreateGraphics().DrawEllipse(mPen, rect);
                        }
                    }
                }
            }
            else // При нажатии на холст
            {
                CCircle circ = new CCircle(); // Создание нового объекта
                circ.x = e.X;
                circ.y = e.Y;
                stor.AddStor(circ);
                for (int j = 0; j < stor.i; j++)
                {
                    if (stor.arr[j] != null)
                    {
                        if (j != (stor.i - 1)) // Снятие выделения с других объектов и отрисовка их
                        {
                            stor.arr[j].flag = false;
                            Rectangle rect = new Rectangle(stor.arr[j].x - 30, stor.arr[j].y - 30, 60, 60);
                            panel1.CreateGraphics().DrawEllipse(mPen, rect);
                        }
                        else // Выделение нового объекта и его отрисовка
                        {
                            stor.arr[j].flag = true;
                            Rectangle rect = new Rectangle(stor.arr[j].x - 30, stor.arr[j].y - 30, 60, 60);
                            panel1.CreateGraphics().DrawEllipse(aPen, rect);
                        }
                    }
                }
            }
        }

        void Form1_KeyDown(object sender, KeyEventArgs e) // Удаление выделенных объектов при нажатии на Delete
        {
            if (e.KeyData == Keys.Delete)
            {
                for (int j = 0; j < stor.i; j++)
                {
                    if (stor.arr[j] != null)
                    {
                        if (stor.arr[j].flag == true)
                        {
                            Pen wPen = new Pen(Color.White, 3);
                            Rectangle rect = new Rectangle(stor.arr[j].x - 30, stor.arr[j].y - 30, 60, 60);
                            this.panel1.CreateGraphics().DrawEllipse(wPen, rect);
                            stor.arr[j] = null;
                        }
                    }
                }
            }
        }
    }
}
