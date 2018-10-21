// <copyright file="ToolStripToolStripItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// <summary>
    /// The tool strip tool strip item class.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class ToolStripToolStripItem
        : ToolStripControlHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStripToolStripItem"/> class.
        /// </summary>
        public ToolStripToolStripItem()
            : base(new ToolStrip())
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the tool strip.
        /// </summary>
        public ToolStrip ToolStrip
            => Control as ToolStrip;
    }
}
