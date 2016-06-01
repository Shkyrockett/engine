// <copyright file="Straightener.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Objects;
using System.Collections.Generic;
using System.Text;
using static System.Math;

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
        private LineSegment line;

        /// <summary>
        /// Index value in the array.
        /// </summary>
        private int index;

        /// <summary>
        /// 
        /// </summary>
        bool mouseDown;

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
            line = LineSegment.Empty;

            // Setup the calculation properties.
            angle = 0;
            theta = 0;
            delta = 0;
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public LineSegment Line
        {
            get { return line; }
            set { line = value; }
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
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            mouseDown = true;
            InUse = true;
            if (InUse)
            {
                Line.B = tools.MouseLocation;
                if (!Started)
                {
                    Line.A = tools.MouseLocation;
                    tools.Surface.RubberbandItems = new List<GraphicItem>() { new GraphicItem(Line, null) };
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
                    if (mouseDown) index = 1;

                    line.B = tools.MouseLocation;

                    // angle is the absolute angle of the line.
                    angle = Maths.AbsoluteAngle(line[0].X, line[0].Y, line[1].X, line[1].Y);

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
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
            mouseDown = false;
            if (InUse)
            {
                line[index] = tools.MouseLocation;
                switch (index)
                {
                    case 0:
                        index = 1;
                        break;
                    case 1:
                        index = 0;
                        Started = false;
                        tools.Surface.RubberbandItems.Clear();
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
            angle = 0;
            theta = 0;
            delta = 0;
            line = LineSegment.Empty;
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
            output.Append(Round(angle.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(", Snap to: ");
            output.Append(Round(theta.ToDegrees(), 3).ToString("N0").PadLeft(3));
            output.Append(", Difference: ");
            output.Append(Round(delta.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(".");
            return output.ToString();
        }
    }
}
