// <copyright file="SelectTop.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Text;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool class.
    /// </summary>
    public class SelectTop
        : Tool, ITool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectTop"/> class.
        /// </summary>
        public SelectTop()
        {
            Index = 0;
        }

        /// <summary>
        /// Provides the current index of the select tool.
        /// </summary>
        /// <returns>Returns the current index of the select tool.</returns>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool MouseDown { get; set; }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            MouseDown = true;
            Started = true;
            InUse = true;
            Index = 1;
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public override void MouseMoveUpdate(ToolStack tools)
        { }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
            MouseDown = false;
            if (Started && InUse)
            {
                MouseDown = false;
                tools.Surface.SelectedItems = new List<GraphicItem>(1) { tools.Surface.SelectItem(tools.MouseLocation) };
                RaiseFinishEvent(tools);
                Started = false;
                InUse = false;
                Index = 0;
            }
        }

        /// <summary>
        /// Reset the command if canceled mid command.
        /// </summary>
        public override void Reset()
        {
            MouseDown = false;
            Started = false;
            InUse = false;
            Index = 0;
        }

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => nameof(SelectTop);

        /// <returns></returns>
        /// <summary>
        /// The output.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Output()
        {
            var output = new StringBuilder();
            return output.ToString();
        }
    }
}
