// <copyright file="CIELCh.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Alma Jenks</author>
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
        private double lightness;

        /// <summary>
        /// 
        /// </summary>
        private double chromaticity;

        /// <summary>
        /// 
        /// </summary>
        private double hue;

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
            this.lightness = lightness;
            this.chromaticity = chromaticity;
            this.hue = hue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Lightness
        {
            get { return lightness; }
            set { lightness = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Chromaticity
        {
            get { return chromaticity; }
            set { chromaticity = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Hue
        {
            get { return hue; }
            set { hue = value; }
        }
    }
}
