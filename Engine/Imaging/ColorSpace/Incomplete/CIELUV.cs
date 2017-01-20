// <copyright file="CIELUV.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// 
    /// </summary>
    public class CIELUV
    {
        /// <summary>
        /// 
        /// </summary>
        public CIELUV()
            : this(0, 0, 0)
        { }

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
    }
}
