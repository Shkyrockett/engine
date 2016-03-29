// <copyright file="ITool.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// 
        /// </summary>
        event Tool.ToolFinishEvent Finish;

        /// <summary>
        /// Check to determine if the tool is in use.
        /// </summary>
        bool InUse
        {
            get;
            set;
        }

        /// <summary>
        /// Check if first action of tool has taken place.
        /// </summary>
        bool Started
        {
            get;
            set;
        }

        /// <summary>
        /// Reset the tool.
        /// </summary>
        void Reset();

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="e"></param>
        void MouseDownUpdate(MouseEventArgs e);

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="e">The Mouse Move event arguments.</param>
        /// <param name="MouseDown">A bool indicating whether a mouse button has been pressed.</param>
        void MouseMoveUpdate(MouseEventArgs e, bool MouseDown);

        /// <summary>
        /// Update Tool on Mouse UP signal.
        /// </summary>
        /// <param name="e"></param>
        void MouseUpUpdate(MouseEventArgs e);

        /// <summary>
        /// Update Tool on Mouse scroll signal.
        /// </summary>
        /// <param name="e"></param>
        void MouseScrollUpdate(MouseEventArgs e);

        /// <summary>
        /// Provide rendering support to the canvas.
        /// </summary>
        void Render(Graphics graphics);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        void Render(Graphics graphics, Pen pen, Brush brush);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="penBrush"></param>
        void Render(Graphics graphics, List<Tuple<Pen, Brush>> penBrush);

        /// <summary>
        /// 
        /// </summary>
        string ToString();
    }
}
