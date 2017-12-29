// <copyright file="ScaleDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// The scale distort class.
    /// </summary>
    public class ScaleDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleDistort"/> class.
        /// </summary>
        /// <param name="factors">The factors.</param>
        public ScaleDistort(Size2D factors)
        {
            Factors = factors;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the factors.
        /// </summary>
        public Size2D Factors { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => Distortions.Scale(point, Factors);

        #endregion
    }
}
