// <copyright file="CIELUV.cs" company="Shkyrockett" >
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
    public struct CIELUV
        : IColor<CIELUV>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIELUV()
        //    : this(0, 0, 0)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luminance"></param>
        /// <param name="u">red to green</param>
        /// <param name="v">blue to yellow</param>
        public CIELUV(double luminance, double u, double v)
        {
            Luminance = luminance;
            U = u;
            V = v;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Luminance { get; set; }

        /// <summary>
        /// red to green
        /// </summary>
        public double U { get; set; }

        /// <summary>
        /// blue to yellow
        /// </summary>
        public double V { get; set; }

        public bool Equals(CIELUV other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
