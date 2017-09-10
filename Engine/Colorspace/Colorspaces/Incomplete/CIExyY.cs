// <copyright file="CIExyY.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Colorspace
{
    /// <summary>
    /// 
    /// </summary>
    public struct CIExyY
        : IColor
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIExyY()
        //    : this(0, 0, 0)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        public CIExyY(double x, double y1, double y2)
        {
            X = x;
            Y1 = y1;
            Y2 = y2;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y2 { get; set; }

        public bool Equals(IColor other)
        {
            var a = ToARGBTuple();
            var b = other.ToARGBTuple();
            return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;
        }

        public (byte A, byte R, byte G, byte B) ToARGBTuple() => throw new NotImplementedException();

        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
