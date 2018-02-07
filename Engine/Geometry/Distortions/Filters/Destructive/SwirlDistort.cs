// <copyright file="SwirlDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The swirl distort class.
    /// </summary>
    public class SwirlDistort
        : DestructiveFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SwirlDistort"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="strength">The strength.</param>
        public SwirlDistort(Point2D center, double strength = 0.008)
        {
            Center = center;
            Strength = strength;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public Point2D Center { get; set; }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public double Strength { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => Distortions.Swirl(point, Center, -Strength);
        #endregion Methods
    }
}
