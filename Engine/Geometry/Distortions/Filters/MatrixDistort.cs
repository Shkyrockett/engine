// <copyright file="MatrixDistort.cs" company="Shkyrockett" >
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
    /// The matrix distort class.
    /// </summary>
    public class MatrixDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixDistort"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixDistort(Matrix3x2D matrix)
        {
            Matrix = matrix;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the matrix.
        /// </summary>
        public Matrix3x2D Matrix { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => Distortions.Matrix(point, Matrix);

        #endregion
    }
}
