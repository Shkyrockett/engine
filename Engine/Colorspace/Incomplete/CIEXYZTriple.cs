// <copyright file="CIEXYZTriple.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
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
    public struct CIEXYZTriple
        : IColor<CIEXYZTriple>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIEXYZTriple()
        //    : this(new CIEXYZ(), new CIEXYZ(), new CIEXYZ())
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public CIEXYZTriple(CIEXYZ red, CIEXYZ green, CIEXYZ blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Red { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Green { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Blue { get; set; }

        public bool Equals(CIEXYZTriple other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
