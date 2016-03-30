using Engine.File.Palettes;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorForm 
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, System.EventArgs e)
        {
            palleteControl1.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });
        }
    }
}
