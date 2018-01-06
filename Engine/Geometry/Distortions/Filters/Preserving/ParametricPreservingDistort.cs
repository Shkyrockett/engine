// <copyright file="ParametricPreservingDistort.cs" company="Shkyrockett" >
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
    /// The parametric preserving distort class.
    /// </summary>
    public class ParametricPreservingDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricPreservingDistort"/> class.
        /// </summary>
        /// <param name="functions">The functions.</param>
        public ParametricPreservingDistort(params Func<Point2D, Point2D>[] functions)
        {
            Functions = functions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the functions.
        /// </summary>
        public Func<Point2D, Point2D>[] Functions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
        {
            var result = point;
            foreach (var function in Functions)
            {
                result = function.Invoke(result);
            }
            return result;
        }

        #endregion
    }
}
