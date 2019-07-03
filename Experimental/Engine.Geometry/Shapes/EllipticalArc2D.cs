// <copyright file="Splittings.cs" >
//    Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    public class EllipticalArc2D
    {
        public EllipticalArc2D(double center, double rX, double rY, double angle, double startAngle, double sweepAngle)
        {
            this.Center = center;
            this.RX = rX;
            this.RY = rY;
            this.Angle = angle;
            this.StartAngle = startAngle;
            this.SweepAngle = sweepAngle;
        }

        public double Center { get; internal set; }
        public double RX { get; internal set; }
        public double RY { get; internal set; }
        public double Angle { get; internal set; }
        public double CosAngle { get; internal set; }
        public double SinAngle { get; internal set; }
        public double StartAngle { get; internal set; }
        public double SweepAngle { get; internal set; }
        public double X { get; internal set; }
        public double Y { get; internal set; }
    }
}