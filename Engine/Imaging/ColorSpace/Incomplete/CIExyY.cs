// <copyright file="CIExyY.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Alma Jenks</author>
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
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y1;

        /// <summary>
        /// 
        /// </summary>
        private double y2;

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
            this.x = x;
            this.y1 = y1;
            this.y2 = y2;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Y1
        {
            get { return y1; }
            set { y1 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Y2
        {
            get { return y2; }
            set { y2 = value; }
        }
    }
}
