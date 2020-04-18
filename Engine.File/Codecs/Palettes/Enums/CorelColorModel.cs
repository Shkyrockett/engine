// <copyright file="CorelColorModel.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
    /// The Corel color model enum.
    /// </summary>
    public enum CorelColorModel
        : ushort
    {
        /// <summary>
        /// The Pantone = 1.
        /// </summary>
        Pantone = 1,

        /// <summary>
        /// The CMYK = 2.
        /// </summary>
        CMYK = 2,

        /// <summary>
        /// The CMYK2 = 3.
        /// </summary>
        CMYK2 = 3,

        /// <summary>
        /// The CMY = 4.
        /// </summary>
        CMY = 4,

        /// <summary>
        /// The RGB = 5.
        /// </summary>
        RGB = 5,

        /// <summary>
        /// The HSB = 6.
        /// </summary>
        HSB = 6,

        /// <summary>
        /// The HLS = 7.
        /// </summary>
        HLS = 7,

        /// <summary>
        /// The Gray scale = 9.
        /// </summary>
        Grayscale = 9,

        /// <summary>
        /// The BlackWhite = 10.
        /// </summary>
        BlackWhite = 10,

        /// <summary>
        /// The YIQ = 11.
        /// </summary>
        YIQ = 11,

        /// <summary>
        /// The Lab = 12.
        /// </summary>
        Lab = 12,

        /// <summary>
        /// The Hexa chrome = 15.
        /// </summary>
        Hexachrome = 15,

        /// <summary>
        /// The CMYK3 = 17.
        /// </summary>
        CMYK3 = 17,

        /// <summary>
        /// The Lab2 = 18.
        /// </summary>
        Lab2 = 18,

        /// <summary>
        /// The Registration = 20.
        /// </summary>
        Registration = 20,

        /// <summary>
        /// The CustomInks = 21.
        /// </summary>
        CustomInks = 21,
    }
}
