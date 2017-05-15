// <copyright file="Straightener.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        /// 
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="Straightener"/> class.
        /// </summary>
        public Straightener()
        {
            // Setup the tool properties.
            Index = 0;

            // Setup the storage properties. 
            Line = LineSegment.Empty;

            // Setup the calculation properties.
            Angle = 0;
            Theta = 0;
            Delta = 0;
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public LineSegment Line { get; set; }

        /// <summary>
        /// Provides the current index of the rubber-band line used to find the angle.
        /// </summary>
        /// <returns>Returns the current index of the rubber-band line.</returns>
        public int Index { get; set; }

        /// <summary>
        /// Absolute angle of Rubber-band line.
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// The angle to snap to.
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// The difference angle from the rotation angle to the angle to snap to.
        /// </summary>
        public double Delta { get; set; }

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
                    tools.Surface.RubberbandItems = new List<GraphicItem> { new GraphicItem(Line, null) };
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
                    if (mouseDown) Index = 1;

                    Line.B = tools.MouseLocation;

                    // angle is the absolute angle of the line.
                    Angle = Maths.AbsoluteAngle(Line[0].X, Line[0].Y, Line[1].X, Line[1].Y);

                    // theta is the angle to rotate to.
                    Theta = Maths.RoundToMultiple(Angle, Maths.Right);

                    // delta is the difference between the angle and theta which is the angle to rotate to.
                    Delta = Theta - Angle;
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
                Line[Index] = tools.MouseLocation;
                switch (Index)
                {
                    case 0:
                        Index = 1;
                        break;
                    case 1:
                        Index = 0;
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
            Index = 0;
            Angle = 0;
            Theta = 0;
            Delta = 0;
            Line = LineSegment.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => nameof(Straightener);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            var output = new StringBuilder();
            output.Append("Angle: ");
            output.Append(Round(Angle.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(", Snap to: ");
            output.Append(Round(Theta.ToDegrees(), 3).ToString("N0").PadLeft(3));
            output.Append(", Difference: ");
            output.Append(Round(Delta.ToDegrees(), 3).ToString("N3").PadLeft(8));
            output.Append(".");
            return output.ToString();
        }
    }
}
