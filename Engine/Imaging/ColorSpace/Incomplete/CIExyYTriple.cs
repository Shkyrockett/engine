// <copyright file="CIExyYTriple.cs" company="Shkyrockett" >
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
    public class CIExyYTriple
    {
        /// <summary>
        /// 
        /// </summary>
        public CIExyYTriple()
            : this(new CIExyY(), new CIExyY(), new CIExyY())
        { }

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
    }
}
