// <copyright file="TranslateDistort.cs" company="Shkyrockett" >
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
    public class TranslateDistort
        : PreservingFilter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        public TranslateDistort(Vector2D offset)
        {
            Offset = offset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Vector2D Offset { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => point + Offset;

        #endregion
    }
}
