// <copyright file="MouseMoveUpdateTool.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// ToDo: Implement shape drawing tool.

using System;
using System.Text;

namespace Engine.Tools
{
    /// <summary>
    /// Image drawing tool class.
    /// </summary>
    public class MouseMoveUpdateTool
        : Tool, ITool
    {
        /// <summary>
        /// 
        /// </summary>
        private Action<Point2D> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawShape"/> class.
        /// </summary>
        public MouseMoveUpdateTool(Action<Point2D> action)
        {
            this.action = action;
            Point =  Point2D.Empty ;
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MouseDown { get; set; }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools) => MouseDown = true;

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public override void MouseMoveUpdate(ToolStack tools) => Point = tools.MouseLocation;

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools) => MouseDown = false;

        /// <summary>
        /// Reset the command if canceled mid command.
        /// </summary>
        public override void Reset()
        {
            InUse = false;
            Started = false;
            Point = Point2D.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => nameof(MouseMoveUpdateTool);

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
