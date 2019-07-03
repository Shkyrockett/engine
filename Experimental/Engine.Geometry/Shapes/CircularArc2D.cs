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
    public class CircularArc2D
    {
        private object center;
        private double radius;
        private double v;
        private double tau;

        public CircularArc2D(object center, double radius, double v, double tau)
        {
            this.center = center;
            this.radius = radius;
            this.v = v;
            this.tau = tau;
        }

        public object Center { get; internal set; }
        public double Radius { get; internal set; }
        public double StartAngle { get; internal set; }
        public double SweepAngle { get; internal set; }
        public double X { get; internal set; }
        public double Y { get; internal set; }
    }
}