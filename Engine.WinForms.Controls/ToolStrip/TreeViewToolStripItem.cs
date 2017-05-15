// <copyright file="TreeViewToolStripItem.cs" company="Shkyrockett" >
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

namespace Engine.WindowsForms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class TreeViewToolStripItem
        : ToolStripControlHost
    {
        private TreeView tree = new TreeView();

        /// <summary>
        /// 
        /// </summary>
        public TreeViewToolStripItem()
            : base(new TreeView())
        {
            InitializeComponent();
            tree = Control as TreeView;
        }
    }
}
