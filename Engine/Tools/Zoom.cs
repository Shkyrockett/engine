// <copyright file="Select.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

// ToDo: Implement shape drawing tool.

using Engine.Geometry;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool class.
    /// </summary>
    public class Zoom
        : Tool, ITool
    {
        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        private Point[] points;

        /// <summary>
        /// Index value in the array.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        public Zoom()
        {
            // Setup the tool properties.
            index = 0;

            // Setup the storage properties. 
            points = new Point[2];
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public Point[] Points
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
        /// Render the tool to a Graphics object.
        /// </summary>
        /// <param name="graphics">The graphics object to draw to.</param>
        /// <param name="pen">The drawing pen for the line to render.</param>
        /// <param name="brush">The drawing brush for the line to render. Null.</param>
        public override void Render(Graphics graphics, Pen pen, Brush brush)
        {
            if (graphics != null)
            {
                if (brush != null)
                {

                }

                if (pen != null)
                {
                    graphics.DrawLines(pen, points);
                }
            }
        }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="e"></param>
        public override void MouseDownUpdate(MouseEventArgs e)
        {
            if (InUse)
            {
                points[index] = e.Location;
                if (!Started)
                {
                    Points[1] = e.Location;
                }

                Started = true;
            }
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="e">The Mouse Move event arguments.</param>
        /// <param name="MouseDown">A bool indicating whether a mouse button has been pressed.</param>
        public override void MouseMoveUpdate(MouseEventArgs e, bool MouseDown)
        {
            if (InUse)
            {
                if (Started)
                {
                    if (Primitives.Length(points[0], e.Location) > 8)
                    {
                        if (MouseDown) index = 1;
                        points[index] = e.Location;
                    }

                    if (index == 0) Points[1] = e.Location;
                }
            }
        }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="e"></param>
        public override void MouseUpUpdate(MouseEventArgs e)
        {
            if (InUse)
            {
                points[index] = e.Location;
                switch (index)
                {
                    case 0:
                        index = 1;
                        break;
                    case 1:
                        index = 0;
                        Started = false;
                        RaiseFinishEvent();
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
            points = new Point[2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(Select);
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
