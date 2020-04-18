// <copyright file="DoubleLine.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The double line class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    public class DoubleLine
        : Shape2D
    {
        /// <summary>
        /// The border points (readonly). Value: new List&lt;Point2D&gt;().
        /// </summary>
        private readonly List<Point2D> borderPoints = new List<Point2D>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleLine"/> class.
        /// </summary>
        public DoubleLine()
            : this(new List<Point2D>(), new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleLine"/> class.
        /// </summary>
        /// <param name="borderPoints">The borderPoints.</param>
        /// <param name="centerPoints">The centerPoints.</param>
        public DoubleLine(List<Point2D> borderPoints, List<Point2D> centerPoints)
        {
            this.borderPoints = borderPoints;
            CenterPoints = centerPoints;
        }

        /// <summary>
        /// Gets or sets the center points.
        /// </summary>
        public List<Point2D> CenterPoints { get; set; } = new List<Point2D>();

        /// <summary>
        /// Gets the border points.
        /// </summary>
        public List<Point2D> BorderPoints => borderPoints;

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => nameof(DoubleLine);
    }
}
