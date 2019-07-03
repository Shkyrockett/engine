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
    public class QuadraticBezier2D
    {
        public Point2D[] Points { get; internal set; }
        public Polynomial CurveX { get; internal set; }
        public Polynomial CurveY { get; internal set; }
    }
}