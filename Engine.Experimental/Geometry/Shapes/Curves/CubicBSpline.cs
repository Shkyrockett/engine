// <copyright file="CubicBSpline.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The cubic b spline class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("Cubic B Spline")]
    public class CubicBSpline
        : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBSpline"/> class.
        /// </summary>
        public CubicBSpline()
            : this(new List<Point2D> { Point2D.Empty })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBSpline"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public CubicBSpline(List<Point2D> points)
        {
            NodePoints = points;
        }

        /// <summary>
        /// Gets or sets the node points.
        /// </summary>
        /// <value>
        /// The node points.
        /// </value>
        public List<Point2D> NodePoints { get; set; }

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.CubicBezierSpline(t, NodePoints);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(CubicBSpline);
            }

            return $"{nameof(CubicBSpline)}{{{NodePoints}}}";
        }
    }
}
