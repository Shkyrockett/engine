// <copyright file="PaletteToolStripItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
            => Control as PaletteControl;
    }
}
