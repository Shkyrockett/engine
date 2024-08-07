﻿// <copyright file="PaletteExtensions.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.File.Palettes;
using Engine.Imaging;

namespace Engine.WindowsForms;

/// <summary>
/// The palette extensions class.
/// </summary>
public static class PaletteExtensions
{
    /// <summary>
    /// Draw the palette to a bitmap.
    /// </summary>
    /// <param name="palette"></param>
    /// <param name="bounds">The limiting bounds of the container control.</param>
    /// <param name="selection1">The index of the first palette entry that should be selected.</param>
    /// <param name="selection2">The index of the second palette entry that should be selected.</param>
    /// <param name="selection3">The index of the third palette entry that should be selected.</param>
    /// <param name="selection4">The index of the fourth palette entry that should be selected.</param>
    /// <param name="selection5">The index of the fifth palette entry that should be selected.</param>
    /// <param name="highlight">The highlight queue index.</param>
    /// <returns>A <see cref="Bitmap"/> representing the palette as a grid of color entries.</returns>
    public static Bitmap DrawPalette(this Palette palette, Rectangle bounds, int selection1, int selection2, int selection3, int selection4, int selection5, int highlight = -1)
    {
        // Exit if data is not properly formated.
        if (palette?.Colors is null)
        {
            return null;
        }

        if (palette.Colors.Count == 0)
        {
            return null;
        }

        var grid = new RectangleCellGrid(new Rectangle2D(bounds.Location.ToPoint2D(), new Size2D(bounds.Size.Width - 1, bounds.Size.Height - 1)), palette.Colors.Count);
        //RectangleCellGrid grid = new RectangleCellGrid(bounds, this.colors.Count);

        // Create the Bitmap and graphics object to draw on.
        var image = new Bitmap((int)(grid.InnerBounds.Width + 1), (int)(grid.InnerBounds.Height + 1));
        var canvas = Graphics.FromImage(image);

        // Iterate through each color in the list and draw it on the canvas
        var index = 0;
        foreach (var item in palette.Colors)
        {
            // Calculate the location of the cell to draw.
            var cell = grid[index].ToRectangleF();

            // Draw the color cell to the canvas
            using var brush = new SolidBrush(item.ToColor());
            canvas.FillRectangle(brush, cell);
            using var pen = new Pen(Color.White);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));

            // Iterate to the next index in the palette entry list.
            index++;
        }

        // Add any borders for any selected colors.
        if (selection1 >= 0 && selection1 <= palette.Colors.Count)
        {
            var cell = grid[selection1].ToRectangleF();
            using var pen = new Pen(Color.Yellow);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        if (selection2 >= 0 && selection2 <= palette.Colors.Count)
        {
            var cell = grid[selection2].ToRectangleF();
            using var pen = new Pen(Color.Red);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        if (selection3 >= 0 && selection3 <= palette.Colors.Count)
        {
            var cell = grid[selection3].ToRectangleF();
            using var pen = new Pen(Color.Blue);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        if (selection4 >= 0 && selection4 <= palette.Colors.Count)
        {
            var cell = grid[selection4].ToRectangleF();
            using var pen = new Pen(Color.Lime);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        if (selection5 >= 0 && selection5 <= palette.Colors.Count)
        {
            var cell = grid[selection5].ToRectangleF();
            using var pen = new Pen(Color.Cyan);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        if (highlight >= 0 && highlight <= palette.Colors.Count + 1)
        {
            var cell = grid[highlight].ToRectangleF();
            var highlightColor = Color.FromArgb(128, Color.CornflowerBlue.R, Color.CornflowerBlue.G, Color.CornflowerBlue.B);
            using var pen = new Pen(highlightColor, 3);
            canvas.DrawRectangle(pen, Rectangle.Round(cell));
        }

        return image;
    }

    /// <summary>
    /// Looks up the palette entry index for the specified point in the palette grid.
    /// </summary>
    /// <param name="palette"></param>
    /// <param name="location">The location to look in the palette grid.</param>
    /// <param name="bounds">The limiting bounds of the container control.</param>
    /// <returns>The index of the palette entry at the specified coordinates of the grid.</returns>
    public static int PointToPaletteEntry(this Palette palette, Point location, Rectangle bounds)
    {
        var value = -1;

        // Exit if data is not properly formated.
        if (palette?.Colors?.Count != 0)
        {
            var grid = new RectangleDCellGrid(bounds.ToRectangle2D(), palette.Colors.Count);

            // Calculate the index of the item under the point location.
            value = grid[location.ToPoint2D()];
            value = (value < palette.Colors.Count) ? value : -1;
        }

        return value;
    }
}
