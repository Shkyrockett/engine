// <copyright file="IPlatformTextMetrics.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
    /// The IPlatformTextMetrics interface.
    /// https://stackoverflow.com/a/6708492/7004229
    /// </summary>
    public interface IPlatformTextMetrics
    {
        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="format">The format.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        Size2D MeasureString(string text, RenderFont font, TextFormat format, int width);

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        Size2D MeasureString(string text, RenderFont font, int width);

        /// <summary>
        /// The measure string close.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="width">The width.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        Size2D MeasureStringClose(string text, RenderFont font, int width);
    }
}
