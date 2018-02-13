// <copyright file="Experiments.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using System.Collections.Generic;

namespace MethodSpeedTester
{
    public partial class Experiments
    {
        /// <summary>
        ///
        /// </summary>
        public class EllipseIntersectStuff
        {
            internal bool GotEllipse1;
            internal bool GotEllipse2;

            /// <summary>
            /// The ellipse1.
            /// </summary>
            private Rectangle2D Ellipse1 = new Rectangle2D();

            /// <summary>
            /// The ellipse2.
            /// </summary>
            private Rectangle2D Ellipse2 = new Rectangle2D();

            // Equations that define the ellipses.
            internal double Dx1;
            internal double Dy1;
            internal double Dx2;
            internal double Dy2;

            internal double Rx1;
            internal double Ry1;
            internal double Rx2;
            internal double Ry2;

            internal double A1;
            internal double B1;
            internal double C1;
            internal double D1;
            internal double E1;
            internal double F1;
            internal double A2;
            internal double B2;
            internal double C2;
            internal double D2;
            internal double E2;
            internal double F2;

            // The points of intersection.
            internal List<Point2D> Roots = new List<Point2D>();
            internal List<double> RootSign1 = new List<double>();
            internal List<double> RootSign2 = new List<double>();
            internal List<Point2D> PointsOfIntersection = new List<Point2D>();

            // Difference function tangent lines.
            internal double TangentX;
            internal List<Point2D> TangentCenters;
            internal List<Point2D> TangentP1;
            internal List<Point2D> TangentP2;
        }
    }
}
