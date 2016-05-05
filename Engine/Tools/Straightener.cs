// <copyright file="Straightener.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Engine.Tools
{
    /// <summary>
    /// Image Straightening tool class.
    /// </summary>
    public class Straightener
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
        /// Absolute angle of Rubber-band line.
        /// </summary>
        private double angle;

        /// <summary>
        /// The angle to snap to.
        /// </summary>
        private double theta;

        /// <summary>
        /// The difference angle from the rotation angle to the angle to snap to.
        /// </summary>
        private double delta;

        /// <summary>
        /// Initializes a new instance of the <see cref="Straightener"/> class.
        /// </summary>
        public Straightener()
        {
            // Setup the tool properties.
            index = 0;

            // Setup the storage properties. 
            points = new Point[2];

            // Setup the calculation properties.
            angle = 0;
            theta = 0;
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
        /// Absolute angle of Rubber-band line.
        /// </summary>
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// The angle to snap to.
        /// </summary>
        public double Theta
        {
            get { return theta; }
            set { theta = value; }
        }

        /// <summary>
        /// The difference angle from the rotation angle to the angle to snap to.
        /// </summary>
        public double Delta
        {
            get { return delta; }
            set { delta = value; }
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
                    if (PrimitivesExtensions.Length(points[0], e.Location) > 8)
                    {
                        if (MouseDown) index = 1;
                        points[index] = e.Location;
                    }

                    if (index == 0) Points[1] = e.Location;

                    // angle is the absolute angle of the line.
                    angle = PrimitivesExtensions.AbsoluteAngle(points[0], points[1]);

                    // theta is the angle to rotate to.
                    theta = Maths.RoundToMultiple(angle, Maths.HalfPi);

                    // delta is the difference between the angle and theta which is the angle to rotate to.
                    delta = theta - angle;
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
            return nameof(Straightener);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            StringBuilder output = new StringBuilder();
            output.Append("Angle: ");
            output.Append(Math.Round(angle.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(", Snap to: ");
            output.Append(Math.Round(theta.ToDegrees(), 3).ToString("N0").PadLeft(3));
            output.Append(", Difference: ");
            output.Append(Math.Round(delta.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(".");
            return output.ToString();
        }
    }
}
