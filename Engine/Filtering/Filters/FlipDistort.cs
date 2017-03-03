// <copyright file="FlipDistort.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
    public class FlipDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="flipHorizontal"></param>
        /// <param name="flipVertical"></param>
        public FlipDistort(Point2D center, bool flipHorizontal, bool flipVertical)
        {
            Center = center;
            FlipHorizontal = flipHorizontal;
            FlipVertical = flipVertical;
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
        public bool FlipHorizontal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FlipVertical { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => Distortions.Flip(Center, point, FlipHorizontal, FlipVertical);

        #endregion
    }
}
