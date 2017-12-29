// <copyright file="SelectStack.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Text;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool SelectContaing class.
    /// </summary>
    public class SelectStack
        : Tool, ITool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectStack"/> class.
        /// </summary>
        public SelectStack()
        {
            Index = 0;
        }

        /// <summary>
        /// Provides the current index of the select tool.
        /// </summary>
        /// <returns>Returns the current index of the select tool.</returns>
        public int Index { get; set; }

        /// <summary>
        /// 
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
                tools.Surface.SelectedItems = tools.Surface.SelectItems(tools.MouseLocation);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => nameof(SelectTop);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Output()
        {
            var output = new StringBuilder();
            return output.ToString();
        }
    }
}
