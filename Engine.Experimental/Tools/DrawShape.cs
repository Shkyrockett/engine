// <copyright file="DrawShape.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// ToDo: Implement shape drawing tool.

using System.Text;

namespace Engine.Tools;

/// <summary>
/// Image drawing tool class.
/// </summary>
public class DrawShape
    : Tool, ITool
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawShape"/> class.
    /// </summary>
    public DrawShape()
    {
        // Setup the tool properties.
        Index = 0;

        // Setup the storage properties. 
        Points = [Point2D.Empty, Point2D.Empty];
    }

    /// <summary>
    /// Array of points for the Rubber-band line.
    /// </summary>
    public List<Point2D> Points { get; set; }

    /// <summary>
    /// Provides the current index of the rubber-band line used to find the angle.
    /// </summary>
    /// <returns>Returns the current index of the rubber-band line.</returns>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool MouseDown { get; set; }

    /// <summary>
    /// Update tool on mouse down.
    /// </summary>
    /// <param name="tools"></param>
    public override void MouseDownUpdate(ToolStack tools)
    {
        if (tools is null) return;
        MouseDown = true;
        if (InUse)
        {
            Points[Index] = tools.MouseLocation;
            if (!Started)
            {
                Points[1] = tools.MouseLocation;
            }

            Started = true;
        }
    }

    /// <summary>
    /// Update Tool on Mouse Move.
    /// </summary>
    /// <param name="tools">The Mouse Move event arguments.</param>
    public override void MouseMoveUpdate(ToolStack tools)
    {
        if (tools is null) return;
        if (InUse)
        {
            if (Started)
            {
                if (Measurements.Distance(Points[0], tools.MouseLocation) > 8)
                {
                    if (MouseDown)
                    {
                        Index = 1;
                    }

                    Points[Index] = tools.MouseLocation;
                }

                if (Index == 0)
                {
                    Points[1] = tools.MouseLocation;
                }
            }
        }
    }

    /// <summary>
    /// Update Tool on Mouse UP.
    /// </summary>
    /// <param name="tools"></param>
    public override void MouseUpUpdate(ToolStack tools)
    {
        if (tools is null) return;
        MouseDown = false;
        if (InUse)
        {
            Points[Index] = tools.MouseLocation;
            switch (Index)
            {
                case 0:
                    Index = 1;
                    break;
                case 1:
                    Index = 0;
                    Started = false;
                    RaiseFinishEvent(tools);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Reset the command if canceled mid command.
    /// </summary>
    public override void Reset()
    {
        InUse = false;
        Started = false;
        Index = 0;
        Points = [Point2D.Empty, Point2D.Empty];
    }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
        => nameof(DrawShape);

    /// <summary>
    /// The output.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public static string Output()
    {
        var output = new StringBuilder();
        return output.ToString();
    }
}
