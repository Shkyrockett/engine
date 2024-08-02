// <copyright file="PathLabel.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine.WindowsForms;

/// <summary>
/// The path label class.
/// </summary>
/// <remarks>
/// <para>http://stackoverflow.com/questions/2397860/c-sharp-winforms-smart-textbox-control-to-auto-format-path-length-based-on-tex
/// https://social.msdn.microsoft.com/Forums/vstudio/en-US/f452fd0d-a4f1-49fa-ac3a-da614540cbf1/creating-a-label-that-displays-a-path-with-middleellipsis?forum=csharpgeneral</para>
/// </remarks>
public partial class PathLabel
    : TransparentLabel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathLabel"/> class.
    /// </summary>
    public PathLabel()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    [Browsable(false)]
    public override bool AutoSize
    {
        get { return base.AutoSize; }
        set { base.AutoSize = false; }
    }

    /// <summary>
    /// Raises the paint event.
    /// </summary>
    /// <param name="e">The paint event arguments.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (e is null) return;
        const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.PathEllipsis;
        TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, flags);
    }
}
