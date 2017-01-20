// <copyright file="Transform.cs" company="Shkyrockett" >
//    Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct Transform
    {
        /// <summary>
        /// 
        /// </summary>
        public static Transform Identity = new Transform(0, 0, 0, 0, 1, 1, 0);

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double skewX;

        /// <summary>
        /// 
        /// </summary>
        private double skewY;

        /// <summary>
        /// 
        /// </summary>
        private double scaleX;

        /// <summary>
        /// 
        /// </summary>
        private double scaleY;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public Transform((double x, double y, double skewX, double skewY, double scaleX, double scaleY, double angle) tuple)
            => (x, y, skewX, skewY, scaleX, scaleY, angle) = tuple;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="skewX"></param>
        /// <param name="skewY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="angle"></param>
        public Transform(double x, double y, double skewX, double skewY, double scaleX, double scaleY, double angle)
        {
            this.x = x;
            this.y = y;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.angle = angle;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get { return x; } set { x = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get { return y; } set { y = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double SkewX { get { return skewX; } set { skewX = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double SkewY { get { return skewY; } set { skewY = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double ScaleX { get { return scaleX; } set { scaleX = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double ScaleY { get { return scaleY; } set { scaleY = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double Angle { get { return angle; } set { angle = value; } }
    }
}
