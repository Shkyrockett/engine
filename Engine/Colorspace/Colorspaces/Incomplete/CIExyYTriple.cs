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
        : IColor<CIExyYTriple>
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

        public bool Equals(CIExyYTriple other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
