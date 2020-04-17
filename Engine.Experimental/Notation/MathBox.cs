// <copyright file="MathBox.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.MathNotation
{
    /// <summary>
    /// The math box class.
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
        /// Build the table.
        /// </summary>
        public static void BuildTable()
        { }
    }
}
