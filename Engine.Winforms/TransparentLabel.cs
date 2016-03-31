using System.Windows.Forms;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/1517179/c-overriding-onpaint-on-progressbar-not-working
    /// </remarks>
    public partial class TransparentLabel 
        : Label
    {
        /// <summary>
        /// 
        /// </summary>
        public TransparentLabel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            UpdateStyles();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
