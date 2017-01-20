// <copyright file="CIEXYZTriple.cs" company="Shkyrockett" >
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
    public class CIEXYZTriple
    {
        /// <summary>
        /// 
        /// </summary>
        public CIEXYZTriple()
            : this(new CIEXYZ(), new CIEXYZ(), new CIEXYZ())
        { }

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
    }
}
