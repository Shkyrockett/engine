using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class TreeViewToolStripItem
        : ToolStripControlHost
    {
        private TreeView tree = new TreeView();

        /// <summary>
        /// 
        /// </summary>
        public TreeViewToolStripItem()
            : base(new TreeView())
        {
            InitializeComponent();
            tree = Control as TreeView;
        }
    }
}
