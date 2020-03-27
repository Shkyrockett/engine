using Engine;
using System;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Form1
        : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            var point1 = Point2D.Empty;
            var point2 = new Point2D(0d, 0d);
            label1.Text += $"\n\r{point1} {(point1 == point2 ? "==" : "!=")} {point2}";
        }

        /// <summary>
        /// Handles the Click event of the ButtonExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonExit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
