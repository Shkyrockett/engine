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
    /// 
    /// </summary>
    public class MatrixDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        public MatrixDistort(Matrix3x2D matrix)
        {
            Matrix = matrix;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Matrix3x2D Matrix { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => Distortions.Matrix(point, Matrix);

        #endregion
    }
}
