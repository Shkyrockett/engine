// <copyright file="ToolStripToolStripItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

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
            => InitializeComponent();

        /// <summary>
        /// 
        /// </summary>
        public ToolStrip ToolStrip
            => Control as ToolStrip;
    }
}
