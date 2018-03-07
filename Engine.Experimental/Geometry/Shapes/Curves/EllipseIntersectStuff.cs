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
    /// <summary>
    /// The ellipse intersect stuff class.
    /// </summary>
    public class EllipseIntersectStuff
    {
        /// <summary>
        /// The got ellipse1.
        /// </summary>
        public bool GotEllipse1;

        /// <summary>
        /// The got ellipse2.
        /// </summary>
        public bool GotEllipse2;

        /// <summary>
        /// The ellipse1.
        /// </summary>
        public Rectangle2D Ellipse1 = new Rectangle2D();

        /// <summary>
        /// The ellipse2.
        /// </summary>
        public Rectangle2D Ellipse2 = new Rectangle2D();

        // Equations that define the ellipses.
        /// <summary>
        /// The dx1.
        /// </summary>
        public double Dx1;

        /// <summary>
        /// The dy1.
        /// </summary>
        public double Dy1;

        /// <summary>
        /// The dx2.
        /// </summary>
        public double Dx2;

        /// <summary>
        /// The dy2.
        /// </summary>
        public double Dy2;

        /// <summary>
        /// The rx1.
        /// </summary>
        public double Rx1;

        /// <summary>
        /// The ry1.
        /// </summary>
        public double Ry1;

        /// <summary>
        /// The rx2.
        /// </summary>
        public double Rx2;

        /// <summary>
        /// The ry2.
        /// </summary>
        public double Ry2;

        /// <summary>
        /// The a1.
        /// </summary>
        public double A1;

        /// <summary>
        /// The b1.
        /// </summary>
        public double B1;

        /// <summary>
        /// The c1.
        /// </summary>
        public double C1;

        /// <summary>
        /// The d1.
        /// </summary>
        public double D1;

        /// <summary>
        /// The e1.
        /// </summary>
        public double E1;

        /// <summary>
        /// The f1.
        /// </summary>
        public double F1;

        /// <summary>
        /// The a2.
        /// </summary>
        public double A2;

        /// <summary>
        /// The b2.
        /// </summary>
        public double B2;

        /// <summary>
        /// The c2.
        /// </summary>
        public double C2;

        /// <summary>
        /// The d2.
        /// </summary>
        public double D2;

        /// <summary>
        /// The e2.
        /// </summary>
        public double E2;

        /// <summary>
        /// The f2.
        /// </summary>
        public double F2;

        // The points of intersection.
        /// <summary>
        /// The roots.
        /// </summary>
        public List<Point2D> Roots = new List<Point2D>();

        /// <summary>
        /// The root sign1.
        /// </summary>
        public List<double> RootSign1 = new List<double>();

        /// <summary>
        /// The root sign2.
        /// </summary>
        public List<double> RootSign2 = new List<double>();

        /// <summary>
        /// The points of intersection.
        /// </summary>
        public List<Point2D> PointsOfIntersection = new List<Point2D>();

        // Difference function tangent lines.
        /// <summary>
        /// The tangent x.
        /// </summary>
        public double TangentX;

        /// <summary>
        /// The tangent centers.
        /// </summary>
        public List<Point2D> TangentCenters;

        /// <summary>
        /// The tangent p1.
        /// </summary>
        public List<Point2D> TangentP1;

        /// <summary>
        /// The tangent p2.
        /// </summary>
        public List<Point2D> TangentP2;
    }
}
