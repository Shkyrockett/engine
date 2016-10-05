// <copyright file="CIExyY.cs" >
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
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
    public class CIExyY
    {
        /// <summary>
        /// 
        /// </summary>
        public CIExyY()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        public CIExyY(double x,double y1,double y2)
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
    }
}
