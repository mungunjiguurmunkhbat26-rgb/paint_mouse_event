using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace paint_mouse_event
{

    public partial class Form1 : Form
    {
        Graphics g;     // зурах цаас
        Pen p;          // үзэг
        bool draw;      // хулганы товч дарагдсан эсэх
        int x, y;       // өмнөх цэгийн координат
        int startX, startY;
        string tool = "Үзэг";

        List<Bitmap> history = new List<Bitmap>();
        Bitmap canvasBitmap;
        public Form1()
        {
            InitializeComponent();
            // Зурах хэрэгсэл сонгох
            ComboBox cmbTool = new ComboBox();
            cmbTool.Items.AddRange(new object[]
            {
    "Үзэг", "Баллуур", "Шулуун",
    "Тэгш өнцөгт", "Эллипс"
            });
            cmbTool.SelectedIndex = 0;
            cmbTool.Location = new Point(10, 10);
            cmbTool.SelectedIndexChanged += (s, e) =>
            {
                tool = cmbTool.SelectedItem.ToString();
            };

            this.Controls.Add(cmbTool);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            p = new Pen(Color.Black, 2);
            draw = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            x = e.X;
            y = e.Y;
            startX = e.X;
            startY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                if (tool == "Баллуур")
                {
                    using (Pen eraser = new Pen(Color.White, 20))
                        g.DrawLine(eraser, x, y, e.X, e.Y);
                }
                else
                {
                    g.DrawLine(p, x, y, e.X, e.Y);
                }
                x = e.X;
                y = e.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tool == "Шулуун")
                g.DrawLine(p, startX, startY, e.X, e.Y);

            else if (tool == "Тэгш өнцөгт")
                g.DrawRectangle(p,
                    Math.Min(startX, e.X),
                    Math.Min(startY, e.Y),
                    Math.Abs(e.X - startX),
                    Math.Abs(e.Y - startY));

            else if (tool == "Эллипс")
                g.DrawEllipse(p,
                    Math.Min(startX, e.X),
                    Math.Min(startY, e.Y),
                    Math.Abs(e.X - startX),
                    Math.Abs(e.Y - startY));
            draw = false;
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p.Color = colorDialog1.Color;

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p.Width = (float)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}