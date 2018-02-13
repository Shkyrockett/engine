﻿// <copyright file="FlipDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The flip distort class.
    /// </summary>
    public class FlipDistort
        : PreservingFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FlipDistort"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="flipHorizontal">The flipHorizontal.</param>
        /// <param name="flipVertical">The flipVertical.</param>
        public FlipDistort(Point2D center, bool flipHorizontal, bool flipVertical)
        {
            Center = center;
            FlipHorizontal = flipHorizontal;
            FlipVertical = flipVertical;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public Point2D Center { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool FlipHorizontal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool FlipVertical { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Process(Point2D point)
            => Process(point, Center, FlipHorizontal, FlipVertical);

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="center"></param>
        /// <param name="flipHorizontal"></param>
        /// <param name="flipVertical"></param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Process(Point2D point, Point2D center, bool flipHorizontal, bool flipVertical)
            => Distortions.Flip(point, center, flipHorizontal, flipVertical);
        #endregion Methods
    }
}
