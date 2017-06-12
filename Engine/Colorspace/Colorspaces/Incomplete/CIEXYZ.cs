// <copyright file="CIEXYZ.cs" company="Shkyrockett" >
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
    /// CIE XYZ: The Tri-stimulus Values
    /// </summary>
    public struct CIEXYZ
        : IColor<CIEXYZ>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIEXYZ()
        //    : this(0, 0, 0)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public CIEXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Z { get; set; }

        public bool Equals(CIEXYZ other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
