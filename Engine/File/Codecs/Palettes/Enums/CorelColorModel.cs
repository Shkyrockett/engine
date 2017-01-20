// <copyright file="CorelColorModel.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    internal enum CorelColorModel
        : ushort
    {
        Pantone = 1,
        CMYK = 2,
        CMYK2 = 3,
        CMY = 4,
        RGB = 5,
        HSB = 6,
        HLS = 7,
        Grayscale = 9,
        BlackWhite = 10,
        YIQ = 11,
        Lab = 12,
        Hexachrome = 15,
        CMYK3 = 17,
        Lab2 = 18,
        Registration = 20,
        CustomInks = 21,
    }
}
