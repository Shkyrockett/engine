// <copyright file="SwirlDistort.cs" company="Shkyrockett" >
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
    public class SwirlDistort
        : DestructiveFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        public SwirlDistort(Point2D center, double strength = 0.008)
        {
            Center = center;
            Strength = strength;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Point2D Center { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Strength { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => Distortions.Swirl(point, Center, -Strength);

        #endregion
    }
}
