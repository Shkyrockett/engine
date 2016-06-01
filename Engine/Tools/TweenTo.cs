// <copyright file="TweenTo.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Objects;
using Engine.Tweening;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace Engine.Tools
{
    /// <summary>
    /// Tween to tool class.
    /// </summary>
    public class TweenTo
        : Tool, ITool
    {
        /// <summary>
        /// Rubber-band line.
        /// </summary>
        private LineSegment line;

        /// <summary>
        /// Index value in the array.
        /// </summary>
        private int index;

        /// <summary>
        /// 
        /// </summary>
        bool mouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="Straightener"/> class.
        /// </summary>
        public TweenTo()
        {
            // Setup the tool properties.
            index = 0;

            // Setup the storage properties. 
            line = LineSegment.Empty;
        }

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public LineSegment Line
        {
            get { return line; }
            set { line = value; }
        }

        /// <summary>
        /// Provides the current index of the rubber-band line used to find the angle.
        /// </summary>
        /// <returns>Returns the current index of the rubber-band line.</returns>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseDownUpdate(ToolStack tools)
        {
            mouseDown = true;
            InUse = true;
            if (InUse)
            {
                Line.B = tools.MouseLocation;
                if (!Started)
                {
                    Line.A = tools.MouseLocation;
                    tools.Surface.RubberbandItems = new List<GraphicItem>() { new GraphicItem(Line, null) };
                    tools.Surface.SelectedItems = new List<GraphicItem>(1) { tools.Surface.SelectItem(tools.MouseLocation) };
                    if (tools.Surface.SelectedItems == null) return;
                    Started = true;
                }
            }
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public override void MouseMoveUpdate(ToolStack tools)
        {
            if (InUse)
            {
                if (Started)
                {
                    if (mouseDown) index = 1;

                    line.B = tools.MouseLocation;
                }
            }
        }

        /// <summary>
        /// Update Tool on Mouse UP.
        /// </summary>
        /// <param name="tools"></param>
        public override void MouseUpUpdate(ToolStack tools)
        {
            mouseDown = false;
            if (InUse)
            {
                line[index] = tools.MouseLocation;
                switch (index)
                {
                    case 0:
                        index = 1;
                        break;
                    case 1:
                        index = 0;
                        Started = false;
                        tools.Surface.RubberbandItems.Clear();
                        if (tools.Surface?.SelectedItems?.Count > 0)
                        {
                            Tween tt = tools.Surface.Tweener.Tween(tools.Surface.SelectedItems[0].Item, new { Location = tools.MouseLocation }, 100, 0);
                        }
                        RaiseFinishEvent(tools);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Reset the command if canceled mid command.
        /// </summary>
        public override void Reset()
        {
            InUse = false;
            Started = false;
            index = 0;
            line = LineSegment.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(Straightener);
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
