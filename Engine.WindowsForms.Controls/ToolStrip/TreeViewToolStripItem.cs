// <copyright file="TreeViewToolStripItem.cs" company="Shkyrockett" >
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
    /// 
    /// </summary>
    /// <remarks></remarks>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class TreeViewToolStripItem
        : ToolStripControlHost
    {
        /// <summary>
        /// The tree.
        /// </summary>
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
