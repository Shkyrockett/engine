using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class ToolStripToolStripItem
        : ToolStripControlHost
    {
        /// <summary>
        /// 
        /// </summary>
        public ToolStripToolStripItem()
            : base(new ToolStrip())
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public ToolStrip ToolStrip => Control as ToolStrip;
    }
}
