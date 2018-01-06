// <copyright file="SphereDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// The sphere distort class.
    /// </summary>
    public class SphereDistort
        : DestructiveFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SphereDistort"/> class.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="strength">The strength.</param>
        public SphereDistort(Rectangle2D rect, double strength = 0.5)
        {
            Center = rect.Center;
            Radius = Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height) * 0.5;
            Strength = strength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SphereDistort"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        public SphereDistort(Point2D center, double radius, double strength = 0.5)
        {
            Center = center;
            Radius = radius;
            Strength = strength;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public Point2D Center { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public double Strength { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => Distortions.Pinch(point, Center, Radius, -Strength);

        #endregion
    }
}
