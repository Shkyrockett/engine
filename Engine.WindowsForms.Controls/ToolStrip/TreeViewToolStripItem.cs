// <copyright file="TreeViewToolStripItem.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms.Design;

namespace Engine.WindowsForms;

/// <summary>
/// The tree view tool strip item class.
/// </summary>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public partial class TreeViewToolStripItem
    : ToolStripControlHost
{
    /// <summary>
    /// The tree.
    /// </summary>
    private static readonly TreeView tree = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TreeViewToolStripItem"/> class.
    /// </summary>
    public TreeViewToolStripItem()
        : base(tree)
    {
        InitializeComponent();
    }
}
