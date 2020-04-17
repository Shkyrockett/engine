// <copyright file="ParametricDestructiveDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The parametric destructive distort class.
    /// </summary>
    /// <seealso cref="Engine.DestructiveFilter" />
    public class ParametricDestructiveDistort
        : DestructiveFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricDestructiveDistort" /> class.
        /// </summary>
        /// <param name="functions">The functions.</param>
        public ParametricDestructiveDistort(params Func<Point2D, Point2D>[] functions)
        {
            Functions = functions;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        public Func<Point2D, Point2D>[] Functions { get; private set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Process(Point2D point) => Process(point, Functions);

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="functions">The functions.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">functions</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Process(Point2D point, Func<Point2D, Point2D>[] functions)
        {
            if (functions is null)
            {
                throw new ArgumentNullException(nameof(functions));
            }

            var result = point;
            foreach (var function in functions)
            {
                result = function.Invoke(result);
            }
            return result;
        }
        #endregion Methods
    }
}
