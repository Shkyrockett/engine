// <copyright file="IPlatformTextMetrics.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// https://stackoverflow.com/a/6708492/7004229
    /// </summary>
    public interface IPlatformTextMetrics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="format"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Size2D MeasureString(string text, RenderFont font, TextFormat format, int width);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Size2D MeasureString(string text, RenderFont font, int width);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Size2D MeasureStringClose(string text, RenderFont font, int width);
    }
}
