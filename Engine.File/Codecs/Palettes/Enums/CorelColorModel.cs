// <copyright file="CorelColorModel.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    public enum CorelColorModel
        : ushort
    {
        /// <summary>
        /// 
        /// </summary>
        Pantone = 1,

        /// <summary>
        /// 
        /// </summary>
        CMYK = 2,

        /// <summary>
        /// 
        /// </summary>
        CMYK2 = 3,

        /// <summary>
        /// 
        /// </summary>
        CMY = 4,

        /// <summary>
        /// 
        /// </summary>
        RGB = 5,

        /// <summary>
        /// 
        /// </summary>
        HSB = 6,

        /// <summary>
        /// 
        /// </summary>
        HLS = 7,

        /// <summary>
        /// 
        /// </summary>
        Grayscale = 9,

        /// <summary>
        /// 
        /// </summary>
        BlackWhite = 10,

        /// <summary>
        /// 
        /// </summary>
        YIQ = 11,

        /// <summary>
        /// 
        /// </summary>
        Lab = 12,

        /// <summary>
        /// 
        /// </summary>
        Hexachrome = 15,

        /// <summary>
        /// 
        /// </summary>
        CMYK3 = 17,

        /// <summary>
        /// 
        /// </summary>
        Lab2 = 18,

        /// <summary>
        /// 
        /// </summary>
        Registration = 20,

        /// <summary>
        /// 
        /// </summary>
        CustomInks = 21,
    }
}
