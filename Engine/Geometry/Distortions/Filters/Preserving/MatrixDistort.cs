// <copyright file="MatrixDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
    /// The matrix distort class.
    /// </summary>
    /// <seealso cref="Engine.PreservingFilter" />
    public class MatrixDistort
        : PreservingFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixDistort" /> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixDistort(Matrix3x2D matrix)
        {
            Matrix = matrix;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the matrix.
        /// </summary>
        /// <value>
        /// The matrix.
        /// </value>
        public Matrix3x2D Matrix { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Process(Point2D point) => Process(point, Matrix);

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Process(Point2D point, Matrix3x2D matrix) => Distortions.Matrix(point, matrix);
        #endregion Methods
    }
}
