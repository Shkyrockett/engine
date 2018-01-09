// <copyright file="RenderFont.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
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
    /// The render font class.
    /// </summary>
    public class RenderFont
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderFont"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="size">The size.</param>
        /// <param name="textStyle">The textStyle.</param>
        public RenderFont(string name, double size, TextStyle textStyle)
        {
            Name = name;
            Size = size;
            Style = textStyle;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public TextStyle Style { get; set; }
    }
}
