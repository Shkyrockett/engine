// <copyright file="Envelope.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The envelope distort test class.
    /// </summary>
    public class Envelope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Envelope"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Envelope(double x, double y, double width, double height)
        {
            var w3 = width / 3;
            var h3 = height / 3;

            //  TOP LEFT
            ControlPointTopLeft = new CubicControlPoint
            {
                Point = new Point2D(x, y),
                AnchorA = new Point2D(w3, 0),
                AnchorB = new Point2D(0, h3)
            };

            //  TOP RIGHT
            ControlPointTopRight = new CubicControlPoint
            {
                Point = new Point2D(x + width, y),
                AnchorA = new Point2D(-w3, 0),
                AnchorB = new Point2D(0, h3)
            };

            //  BOTTOM LEFT
            ControlPointBottomLeft = new CubicControlPoint
            {
                Point = new Point2D(x, y + height),
                AnchorA = new Point2D(w3, 0),
                AnchorB = new Point2D(0, -h3)
            };

            //  BOTTOM RIGHT
            ControlPointBottomRight = new CubicControlPoint
            {
                Point = new Point2D(x + width, y + height),
                AnchorA = new Point2D(-w3, 0),
                AnchorB = new Point2D(0, -h3)
            };

            //Update();
        }

        /// <summary>
        /// Gets or sets the control point top left.
        /// </summary>
        public CubicControlPoint ControlPointTopLeft { get; set; }

        /// <summary>
        /// Gets or sets the control point top right.
        /// </summary>
        public CubicControlPoint ControlPointTopRight { get; set; }

        /// <summary>
        /// Gets or sets the control point bottom left.
        /// </summary>
        public CubicControlPoint ControlPointBottomLeft { get; set; }

        /// <summary>
        /// Gets or sets the control point bottom right.
        /// </summary>
        public CubicControlPoint ControlPointBottomRight { get; set; }

        /// <summary>
        /// The to polycurve.
        /// </summary>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour ToPolycurve()
        {
            var curve = new PolycurveContour(ControlPointTopLeft.Point);
            curve.AddCubicBezier(ControlPointTopLeft.AnchorAGlobal, ControlPointTopRight.AnchorAGlobal, ControlPointTopRight.Point);
            curve.AddCubicBezier(ControlPointTopRight.AnchorBGlobal, ControlPointBottomRight.AnchorBGlobal, ControlPointBottomRight.Point);
            curve.AddCubicBezier(ControlPointBottomRight.AnchorAGlobal, ControlPointBottomLeft.AnchorAGlobal, ControlPointBottomLeft.Point);
            curve.AddCubicBezier(ControlPointBottomLeft.AnchorBGlobal, ControlPointTopLeft.AnchorBGlobal, ControlPointTopLeft.Point);
            return curve;
        }
    }
}
