// <copyright file="ColorTrackBarControl.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms.VisualStyles;

namespace Engine.WindowsForms;

/// <summary>
/// The color track bar control class.
/// </summary>
/// <remarks>
/// <para>http://stackoverflow.com/questions/1551889/how-to-make-an-ownerdraw-trackbar-in-winforms</para>
/// </remarks>
public partial class ColorTrackBarControl
    : TrackBar
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorTrackBarControl"/> class.
    /// </summary>
    public ColorTrackBarControl()
    {
        InitializeComponent();
        SetStyle(
            ControlStyles.DoubleBuffer
            | ControlStyles.UserPaint
            | ControlStyles.AllPaintingInWmPaint
            , true);
        UpdateStyles();
    }

    /// <summary>
    /// The wnd proc.
    /// </summary>
    /// <param name="m">The m.</param>
    protected override void WndProc(ref Message m)
        => base.WndProc(ref m);

    /// <summary>
    /// Raises the paint background event.
    /// </summary>
    /// <param name="pevent">The paint event arguments.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        if (pevent is null) return;
        base.OnPaintBackground(pevent);
        //var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis;
        //TextRenderer.DrawText(e.Graphics, "Hello", Font, Bounds, Color.Black, flags);

        ProgressBarRenderer.DrawHorizontalBar(pevent.Graphics, ClientRectangle);

        TrackBarRenderer.DrawHorizontalTicks(pevent.Graphics, ClientRectangle, TickFrequency, EdgeStyle.Etched);
    }

    /// <summary>
    /// Raises the paint event.
    /// </summary>
    /// <param name="e">The paint event arguments.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (e is null) return;
        base.OnPaint(e);
        var ThumbRect = new Rectangle(Point.Empty, TrackBarRenderer.GetBottomPointingThumbSize(e.Graphics, TrackBarThumbState.Normal));
        TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, ThumbRect, TrackBarThumbState.Normal);
        //var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis;
        //TextRenderer.DrawText(e.Graphics, "Hello", Font, Bounds, Color.Black, flags);
    }
}
