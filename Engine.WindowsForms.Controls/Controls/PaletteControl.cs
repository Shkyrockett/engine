// <copyright file="PaletteControl.cs" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Drawing;
using System.Windows.Forms;
using Engine.File.Palettes;

namespace Engine.WindowsForms
{
    /// <summary>
    /// Editor Palette control.
    /// </summary>
    public partial class PaletteControl
        : PictureBox
    {
        /// <summary>
        /// The palette to load and work with.
        /// </summary>
        private Palette palette = new Palette();

        /// <summary>
        /// The current index of the palette entry the mouse is over.
        /// </summary>
        private int mousePaletteIndex = -1;

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
        /// The last palette entry selected using the x button 2 mouse button.
        /// </summary>
        private int selectedPaletteIndex5 = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteControl"/> class.
        /// </summary>
        public PaletteControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The palette list of colors.
        /// </summary>
        public Palette Palette
        {
            get { return palette; }
            set
            {
                palette = value;
                Image = Palette.DrawPalette(ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4, selectedPaletteIndex5);
            }
        }

        /// <summary>
        /// Interpret click events on the palette.
        /// </summary>
        /// <param name="sender">The object sending the click events.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void PaleteControl_MouseClick(object sender, MouseEventArgs e)
        {
            var index = Palette.PointToPaletteEntry(e.Location, ClientRectangle);
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
                        selectedPaletteIndex5 = index;
                        break;
                    case MouseButtons.None:
                        break;
                }

                Image = Palette.DrawPalette(ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4, selectedPaletteIndex5);
            }
        }

        /// <summary>
        /// Locate cells when the mouse moves across the control. 
        /// </summary>
        /// <param name="sender">The object sending the move events.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void PalleteControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Capture the index of the palette entry under the cursor.
            var index = Palette.PointToPaletteEntry(e.Location, new Rectangle(ClientRectangle.Location, new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)));

            // If the cursor moved over a new palette entry, update the tool-tip.
            if (mousePaletteIndex != index && index != -1)
            {
                // Update the current palette index.
                mousePaletteIndex = index;

                // Set the tool-tip string.
                paletteToolTip.SetToolTip(this, $"{index.ToString()} {Palette[index].ToString()}");
            }

            var reference = (PaletteControl)sender;
            reference.Image = Palette.DrawPalette(reference.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4, selectedPaletteIndex5, index);
        }

        /// <summary>
        /// Update the control when the control resizes.
        /// </summary>
        /// <param name="sender">The object sending the control.</param>
        /// <param name="e">The event arguments.</param>
        private void PalleteControl_ClientSizeChanged(object sender, EventArgs e)
        {
            var reference = (PaletteControl)sender;
            reference.Image = Palette.DrawPalette(reference.ClientRectangle, selectedPaletteIndex1, selectedPaletteIndex2, selectedPaletteIndex3, selectedPaletteIndex4, selectedPaletteIndex5);
        }
    }
}
