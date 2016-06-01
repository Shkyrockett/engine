// <copyright file="SelectTop.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

// ToDo: Implement shape drawing tool.

using Engine.Objects;
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
        /// Index value in the array.
        /// </summary>
        private int index;

        /// <summary>
        /// 
        /// </summary>
        bool mouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectTop"/> class.
        /// </summary>
        public SelectTop()
        {
            index = 0;
        }

        /// <summary>
        /// Provides the current index of the select tool.
        /// </summary>
        /// <returns>Returns the current index of the select tool.</returns>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MouseDown
        {
            get { return mouseDown; }
            set { mouseDown = value; }
        }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            mouseDown = true;
            Started = true;
            InUse = true;
            index = 1;
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public override void MouseMoveUpdate(ToolStack tools)
        {
        }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
            mouseDown = false;
            if (Started && InUse)
            {
                mouseDown = false;
                tools.Surface.SelectedItems = new List<GraphicItem>(1) { tools.Surface.SelectItem(tools.MouseLocation) };
                RaiseFinishEvent(tools);
                Started = false;
                InUse = false;
                index = 0;
            }
        }

        /// <summary>
        /// Reset the command if canceled mid command.
        /// </summary>
        public override void Reset()
        {
            mouseDown = false;
            Started = false;
            InUse = false;
            index = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(SelectTop);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            StringBuilder output = new StringBuilder();
            return output.ToString();
        }
    }
}
