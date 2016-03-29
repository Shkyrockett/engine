using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Engine.File.Palettes;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PalleteControl
        : PictureBox
    {
        /// <summary>
        /// The palette to load and work with.
        /// </summary>
        private Palette palette = new Palette();

        /// <summary>
        /// The current index of the palette entry the mouse is over.
        /// </summary>
        private int mousePaletteIndex = 0;

        /// <summary>
        /// The last palette entry selected using the left mouse button.
        /// </summary>
        private int selectedPaletteIndex1 = -1;

        /// <summary>
        /// The last palette entry selected using the right mouse button.
        /// </summary>
        private int selectedPaletteIndex2 = -1;

        /// <summary>
        /// The last palette entry selected using the middle mouse button.
        /// </summary>
        private int selectedPaletteIndex3 = -1;

        /// <summary>
        /// The last palette entry selected using the back mouse button.
        /// </summary>
        private int selectedPaletteIndex4 = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PalleteControl"/> class.
        /// </summary>
        public PalleteControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Palette Palette
        {
            get
            {
                return palette;
            }
            set
            {
                palette = value;
                this.Image = Palette.DrawPalette(this.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PalleteControl_MouseClick(object sender, MouseEventArgs e)
        {
            int index = Palette.PointToPaletteEntry(e.Location, this.Bounds);
            if (index >= 0)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        selectedPaletteIndex1 = index;
                        break;
                    case MouseButtons.Right:
                        selectedPaletteIndex2 = index;
                        break;
                    case MouseButtons.Middle:
                        selectedPaletteIndex3 = index;
                        break;
                    case MouseButtons.XButton1:
                        selectedPaletteIndex4 = index;
                        break;
                    case MouseButtons.XButton2:
                        break;
                    case MouseButtons.None:
                        break;
                    default:
                        break;
                }

                this.Image = Palette.DrawPalette(this.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4);
            }
        }

        private void PalleteControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Capture the index of the palette entry under the cursor.
            int index = Palette.PointToPaletteEntry(e.Location, new Rectangle(this.Bounds.Location, new Size(this.Bounds.Width - 1, this.Bounds.Height - 1)));

            // If the cursor moved over a new palette entry, update the tool-tip.
            if (mousePaletteIndex != index && index != -1)
            {
                // Update the current palette index.
                mousePaletteIndex = index;

                // Build the tool-tip string.
                StringBuilder text = new StringBuilder();
                text.Append(index.ToString());
                text.Append(" ");
                text.Append(Palette[index].ToString());

                // Set the tool-tip string.
                paletteToolTip.SetToolTip(this, text.ToString());
            }

            PalleteControl reference = (PalleteControl)sender;
            reference.Image = Palette.DrawPalette(reference.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4, index);
        }

        private void PalleteControl_ClientSizeChanged(object sender, EventArgs e)
        {
            PalleteControl reference = (PalleteControl)sender;
            reference.Image = Palette.DrawPalette(reference.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4);
        }
    }
}
