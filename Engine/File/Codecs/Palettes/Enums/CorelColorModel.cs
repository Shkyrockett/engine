// <copyright file="Palette.cs" >
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
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
