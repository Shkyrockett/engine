// <copyright file="MathBox.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>


namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class MathBox
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Expression"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The equation to render.
        /// </summary>
        public Equation Equation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void BuildTable()
        { }
    }
}
