// <copyright file="ParametricPreservingDistort.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public class ParametricPreservingDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functions"></param>
        public ParametricPreservingDistort(params Func<Point2D, Point2D>[] functions)
        {
            Functions = functions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Func<Point2D, Point2D>[] Functions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
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
