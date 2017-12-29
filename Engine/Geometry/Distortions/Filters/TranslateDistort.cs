// <copyright file="TranslateDistort.cs" company="Shkyrockett" >
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
    /// The translate distort class.
    /// </summary>
    public class TranslateDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateDistort"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public TranslateDistort(Vector2D offset)
        {
            Offset = offset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2D Offset { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => point + Offset;

        #endregion
    }
}
