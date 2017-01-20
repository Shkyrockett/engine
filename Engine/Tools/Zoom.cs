// <copyright file="Select.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Text;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool class.
    /// </summary>
    public class Zoom
        : Tool, ITool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectTop"/> class.
        /// </summary>
        public Zoom()
        {
            // Setup the tool properties.
            Index = 0;

            // Setup the storage properties. 
            Points = new List<Point2D>(2) { Point2D.Empty, Point2D.Empty };
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
        /// 
        /// </summary>
        public bool MouseDown { get; set; }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            MouseDown = true;
            if (InUse)
            {
                Points[Index] = tools.MouseLocation;
                if (!Started)
                    Points[1] = tools.MouseLocation;

                Started = true;
            }
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public override void MouseMoveUpdate(ToolStack tools)
        {
            if (InUse)
            {
                if (Started)
                {
                    if (Distances.Length(Points[0], tools.MouseLocation) > 8)
                    {
                        if (MouseDown) Index = 1;
                        Points[Index] = tools.MouseLocation;
                    }

                    if (Index == 0) Points[1] = tools.MouseLocation;
                }
            }
        }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
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
        /// Update Tool on Mouse scroll signal.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseScrollUpdate(ToolStack tools)
            => tools.Surface.Zoom += 120d / tools.MouseScrollDelta;

        /// <summary>
        /// Reset the command if canceled mid command.
        /// </summary>
        public override void Reset()
        {
            InUse = false;
            Started = false;
            Index = 0;
            Points = new List<Point2D>(2) { Point2D.Empty, Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(SelectTop)}{{{Index}}}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            var output = new StringBuilder();
            return output.ToString();
        }
    }
}
