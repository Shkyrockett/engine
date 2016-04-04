// <copyright file="CIELUV.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Alma Jenks</author>
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
        private double luminance;

        /// <summary>
        /// red to green
        /// </summary>
        private double u;

        /// <summary>
        /// blue to yellow
        /// </summary>
        private double v;

        /// <summary>
        /// 
        /// </summary>
        public CIELUV()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luminance"></param>
        /// <param name="u">red to green</param>
        /// <param name="v">blue to yellow</param>
        public CIELUV(double luminance, double u, double v)
        {
            this.luminance = luminance;
            this.u = u;
            this.v = v;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Luminance
        {
            get { return luminance; }
            set { luminance = value; }
        }

        /// <summary>
        /// red to green
        /// </summary>
        public double U
        {
            get { return u; }
            set { u = value; }
        }

        /// <summary>
        /// blue to yellow
        /// </summary>
        public double V
        {
            get { return v; }
            set { v = value; }
        }
    }
}
