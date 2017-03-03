// <copyright file="SphereDistort.cs" company="Shkyrockett" >
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
    public class SphereDistort
        : Filter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="strength"></param>
        public SphereDistort(Rectangle2D rect, double strength = 0.5)
        {
            Center = rect.Center;
            Radius = Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height) * 0.5;
            Strength = strength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        public SphereDistort(Point2D center, double radius, double strength = 0.5)
        {
            Center = center;
            Radius = radius;
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
        public double Radius { get; set; }

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
            => Distortions.Pinch(Center, point, Radius, -Strength);

        #endregion
    }
}
