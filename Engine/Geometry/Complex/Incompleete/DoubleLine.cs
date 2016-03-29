// <copyright file="DoubleLine.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DisplayName("Double Line")]
    public class DoubleLine
        : Shape
    {

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
        public static PointF[] CenteredOffsetLinePoints(PointF pointA, PointF pointB, PointF pointC, PointF pointD, float offsetDistance)
        {
            // To get the vectors of the angles at each corner B and C, Normalize the Unit Delta Vectors along AB, BC, and CD.
            VectorF UnitVectorAB = pointB.Subtract(pointA).Unit();
            VectorF UnitVectorBC = pointC.Subtract(pointB).Unit();
            VectorF UnitVectorCD = pointD.Subtract(pointC).Unit();

            //  Find the Perpendicular of the outside vectors
            VectorF PerpendicularAB = UnitVectorAB.Perpendicular();
            VectorF PerpendicularCD = UnitVectorCD.Perpendicular();

            //  Normalized Vectors pointing out from B and C.
            VectorF OutUnitVectorB = (UnitVectorAB - UnitVectorBC).Unit();
            VectorF OutUnitVectorC = (UnitVectorCD - UnitVectorBC).Unit();

            //  The distance out from B is the radius / Cos(theta) where theta is the angle
            //  from the perpendicular of BC of the UnitVector. The cosine can also be
            //  calculated by doing the dot product of  Unit(Perpendicular(AB)) and 
            //  UnitVector.
            double BPointScale = PerpendicularAB.DotProduct(OutUnitVectorB) * offsetDistance;
            double CPointScale = PerpendicularCD.DotProduct(OutUnitVectorC) * offsetDistance;

            OutUnitVectorB = OutUnitVectorB.Scale(CPointScale);
            OutUnitVectorC = OutUnitVectorC.Scale(BPointScale);

            // Corners of the parallelogram to draw
            PointF[] Out = new PointF[] {
                (pointC + OutUnitVectorC),
                (pointB + OutUnitVectorB),
                (pointB - OutUnitVectorB),
                (pointC - OutUnitVectorC),
                (pointC + OutUnitVectorC)
            };
            return Out;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<PointF> CenterPoints = new List<PointF>();

        /// <summary>
        /// 
        /// </summary>
        private List<PointF> BorderPoints = new List<PointF>();

        /// <summary>
        /// 
        /// </summary>
        public DoubleLine()
        {
            CenterPoints = new List<PointF>();
            BorderPoints = new List<PointF>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DoubleLine";
        }
    }
}
