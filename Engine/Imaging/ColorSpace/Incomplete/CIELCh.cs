// <copyright file="CIELCh.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// Lightness Chromaticity and Hue color space structure.
    /// </summary>
    public class CIELCh
    {
        /// <summary>
        /// 
        /// </summary>
        public CIELCh()
            : this(0, 0, 0)
        {
        }

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
    }
}
