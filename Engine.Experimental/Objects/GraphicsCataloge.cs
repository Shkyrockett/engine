// <copyright file="GraphicsCataloge.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The graphics cataloge class.
    /// </summary>
    public class GraphicsCataloge
    {
        /// <summary>
        /// The items.
        /// </summary>
        private List<GraphicsObject> items;

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<GraphicsObject> Items { get { return items; } set { items = value; } }
    }
}
