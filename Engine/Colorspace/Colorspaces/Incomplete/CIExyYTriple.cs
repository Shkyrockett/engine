// <copyright file="CIExyYTriple.cs" company="Shkyrockett" >
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
    public struct CIExyYTriple
        : IColor
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIExyYTriple()
        //    : this(new CIExyY(), new CIExyY(), new CIExyY())
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public CIExyYTriple(CIExyY red, CIExyY green, CIExyY blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Red { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Green { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Blue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IColor other)
        {
            var (A0, R0, G0, B0) = ToARGBTuple();
            var (A1, R1, G1, B1) = other.ToARGBTuple();
            return A0 == A1 && R0 == R1 && G0 == G1 && B0 == B1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple() => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
