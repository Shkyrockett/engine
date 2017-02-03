using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Direct2DForm
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Direct2DForm()
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}
    }
}
