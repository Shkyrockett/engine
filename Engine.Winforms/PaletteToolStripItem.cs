using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://www.windows-tech.info/3/7435e13b97d3160f.php
    /// http://stackoverflow.com/questions/4984143/how-add-my-imagebutton-to-toolstrip
    /// </remarks>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class PaletteToolStripItem
        : ToolStripControlHost
    {
        /// <summary>
        /// 
        /// </summary>
        public PaletteToolStripItem()
            : base(new PaletteControl())
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public PaletteControl PaletteControl
        {
            get { return Control as PaletteControl; }
        }
    }
}
