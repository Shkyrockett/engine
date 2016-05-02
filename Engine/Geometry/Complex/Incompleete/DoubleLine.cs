// <copyright file="DoubleLine.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(DoubleLine))]
    public class DoubleLine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> centerPoints = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> borderPoints = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public DoubleLine()
        {
            centerPoints = new List<Point2D>();
            borderPoints = new List<Point2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> CenterPoints
        {
            get { return centerPoints; }
            set { centerPoints = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> BorderPoints
        {
            get { return borderPoints; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DoubleLine";
        }

        /// <summary>
        /// Calculate the geometry of points offset at a specified distance. aka Double Line.
        /// </summary>
        /// <param name="pointA">First reference point.</param>
        /// <param name="pointB">First inclusive point.</param>
        /// <param name="pointC">Second inclusive point.</param>
        /// <param name="pointD">Second reference point.</param>
        /// <param name="offsetDistance">Offset Distance</param>
        /// <returns></returns>
        /// <remarks>
        /// Suppose you have 4 points; A, B C, and D. With Lines AB, BC, and CD.<BR/>
        ///<BR/>
        ///                 B1         BC1       C1<BR/>
        ///                   |\¯B¯¯¯¯¯BC¯¯¯C¯¯¯/|<BR/>
        ///                   | \--------------/ |<BR/>
        ///                   | |\____________/| |<BR/>
        ///                   | | |B2  BC2 C2| | |<BR/>
        ///                 AB| | |          | | |CD<BR/>
        ///                AB1| | |AB2    CD2| | |CD1<BR/>
        ///                   | | |          | | |<BR/>
        ///                   | | |          | | |<BR/>
        ///               A1  A  A2      D2  D  D1<BR/>
        ///</remarks>
        public static Point2D[] CenteredOffsetLinePoints(Point2D pointA, Point2D pointB, Point2D pointC, Point2D pointD, double offsetDistance)
        {
            // To get the vectors of the angles at each corner B and C, Normalize the Unit Delta Vectors along AB, BC, and CD.
            Vector2D UnitVectorAB = pointB.Subtract(pointA).Unit();
            Vector2D UnitVectorBC = pointC.Subtract(pointB).Unit();
            Vector2D UnitVectorCD = pointD.Subtract(pointC).Unit();

            //  Find the Perpendicular of the outside vectors
            Vector2D PerpendicularAB = UnitVectorAB.Perpendicular();
            Vector2D PerpendicularCD = UnitVectorCD.Perpendicular();

            //  Normalized Vectors pointing out from B and C.
            Vector2D OutUnitVectorB = (UnitVectorAB - UnitVectorBC).Unit();
            Vector2D OutUnitVectorC = (UnitVectorCD - UnitVectorBC).Unit();

            //  The distance out from B is the radius / Cos(theta) where theta is the angle
            //  from the perpendicular of BC of the UnitVector. The cosine can also be
            //  calculated by doing the dot product of  Unit(Perpendicular(AB)) and 
            //  UnitVector.
            double BPointScale = PerpendicularAB.DotProduct(OutUnitVectorB) * offsetDistance;
            double CPointScale = PerpendicularCD.DotProduct(OutUnitVectorC) * offsetDistance;

            OutUnitVectorB = OutUnitVectorB.Scale(CPointScale);
            OutUnitVectorC = OutUnitVectorC.Scale(BPointScale);

            // Corners of the parallelogram to draw
            Point2D[] Out = new Point2D[] {
                (pointC + OutUnitVectorC),
                (pointB + OutUnitVectorB),
                (pointB - OutUnitVectorB),
                (pointC - OutUnitVectorC),
                (pointC + OutUnitVectorC)
            };
            return Out;
        }
    }
}
