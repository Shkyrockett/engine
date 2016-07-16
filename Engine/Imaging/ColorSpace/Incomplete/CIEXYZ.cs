// <copyright file="CIEXYZ.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2015 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// CIE XYZ: The Tri-stimulus Values
    /// </summary>
    public class CIEXYZ
    {
        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public CIEXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Z { get; set; }
    }
}
