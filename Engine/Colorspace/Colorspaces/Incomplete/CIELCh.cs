// <copyright file="CIELCh.cs" company="Shkyrockett" >
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
    /// Lightness Chromatically and Hue color space structure.
    /// </summary>
    public struct CIELCh
        : IColor<CIELCh>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public CIELCh()
        //    : this(0, 0, 0)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        public CIELCh(double lightness, double chromaticity, double hue)
        {
            Lightness = lightness;
            Chromaticity = chromaticity;
            Hue = hue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Lightness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Chromaticity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Hue { get; set; }

        public bool Equals(CIELCh other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
