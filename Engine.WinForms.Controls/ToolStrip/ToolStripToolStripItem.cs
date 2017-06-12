﻿// <copyright file="ToolStripToolStripItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public ToolStrip ToolStrip
            => Control as ToolStrip;
    }
}
