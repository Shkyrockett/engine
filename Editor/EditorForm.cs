using Engine.File.Palettes;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    public partial class EditorForm 
        : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_Load(object sender, System.EventArgs e)
        {
            palleteControl1.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });
        }
    }
}
