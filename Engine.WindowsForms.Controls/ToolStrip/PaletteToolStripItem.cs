// <copyright file="PaletteToolStripItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Engine.WindowsForms
{
    /// <remarks>
    /// <para>http://www.windows-tech.info/3/7435e13b97d3160f.php
    /// http://stackoverflow.com/questions/4984143/how-add-my-imagebutton-to-toolstrip</para>
    /// </remarks>
    /// <summary>
    /// The palette tool strip item class.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class PaletteToolStripItem
        : ToolStripControlHost
    {
        private static readonly PaletteControl palette = new PaletteControl();

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteToolStripItem"/> class.
        /// </summary>
        public PaletteToolStripItem()
            : base(palette)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the palette control.
        /// </summary>
        public PaletteControl PaletteControl => Control as PaletteControl;
    }
}
