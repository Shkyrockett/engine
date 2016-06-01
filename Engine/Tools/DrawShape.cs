// <copyright file="DrawShape.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

// ToDo: Implement shape drawing tool.

using Engine.Geometry;
using System.Collections.Generic;
using System.Text;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool class.
    /// </summary>
    public class DrawShape
        : Tool, ITool
    {
        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// Index value in the array.
        /// </summary>
        private int index;

        /// <summary>
        /// 
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawShape"/> class.
        /// </summary>
        public DrawShape()
        {
            // Setup the tool properties.
            index = 0;

            // Setup the storage properties. 
            points = new List<Point2D>(2) { Point2D.Empty, Point2D.Empty };
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public List<Point2D> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// Provides the current index of the rubber-band line used to find the angle.
        /// </summary>
        /// <returns>Returns the current index of the rubber-band line.</returns>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MouseDown
        {
            get { return mouseDown; }
            set { mouseDown = value; }
        }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            mouseDown = true;
            if (InUse)
            {
                points[index] = tools.MouseLocation;
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
            if (InUse)
            {
                if (Started)
                {
                    if (Primitives.Length(points[0], tools.MouseLocation) > 8)
                    {
                        if (mouseDown) index = 1;
                        points[index] = tools.MouseLocation;
                    }

                    if (index == 0) Points[1] = tools.MouseLocation;
                }
            }
        }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
            mouseDown = false;
            if (InUse)
            {
                points[index] = tools.MouseLocation;
                switch (index)
                {
                    case 0:
                        index = 1;
                        break;
                    case 1:
                        index = 0;
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
            index = 0;
            points = new List<Point2D>(2) { Point2D.Empty, Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(DrawShape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            StringBuilder output = new StringBuilder();
            return output.ToString();
        }
    }
}
