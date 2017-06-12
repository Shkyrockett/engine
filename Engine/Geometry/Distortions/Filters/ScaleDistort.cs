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
    /// 
    /// </summary>
    public class ScaleDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factors"></param>
        public ScaleDistort(Size2D factors)
        {
            Factors = factors;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Size2D Factors { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => Distortions.Scale(point, Factors);

        #endregion
    }
}
