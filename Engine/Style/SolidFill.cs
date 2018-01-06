// <copyright file="SolidFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public struct SolidFill
        : IFill
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fillMode"></param>
        public SolidFill(IColor color, FillMode fillMode = FillMode.Alternate)
        {
            Color = color;
            FillMode = fillMode;
        }

        /// <summary>
        /// 
        /// </summary>
        public IColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode FillMode { get; set; }
    }
}