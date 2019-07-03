using Engine;
using System;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var point1 = Point2D.Empty;
            var point2 = new Point2D(0d, 0d);
            label1.Text += $"\n\r{point1} {(point1 == point2 ? "==" : "!=")} {point2}";
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
