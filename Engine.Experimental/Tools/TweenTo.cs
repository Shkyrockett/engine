// <copyright file="TweenTo.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// Tween to tool class.
    /// </summary>
    public class TweenTo
        : Tool, ITool
    {
        #region Fields

        /// <summary>
        /// The mouse down.
        /// </summary>
        private bool mouseDown;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Straightener"/> class.
        /// </summary>
        public TweenTo()
        {
            // Setup the tool properties.
            Index = 0;

            // Setup the storage properties. 
            Line = LineSegment.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Array of points for the Rubber-band line.
        /// </summary>
        public LineSegment Line { get; set; }

        /// <summary>
        /// Provides the current index of the rubber-band line used to find the angle.
        /// </summary>
        /// <returns>Returns the current index of the rubber-band line.</returns>
        public int Index { get; set; }

        #endregion

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
                    tools.Surface.SelectedItems = new List<GraphicItem>(1) { tools.Surface.SelectItem(tools.MouseLocation) };
                    if (tools.Surface.SelectedItems == null) return;
                    Line.A = tools.Surface.SelectedItems[0].Shape.Bounds.Location;
                    tools.Surface.RubberbandItems = new List<GraphicItem> { new GraphicItem(Line, null) };
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
                    if (mouseDown) Index = 1;

                    Line.B = tools.MouseLocation;
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
                Line[Index] = tools.MouseLocation;
                switch (Index)
                {
                    case 0:
                        Index = 1;
                        break;
                    case 1:
                        Index = 0;
                        Started = false;
                        tools.Surface.RubberbandItems.Clear();
                        if (tools.Surface?.SelectedItems?.Count > 0)
                        {
                            //Tween tt = tools.Surface.Tweener.Tween(tools.Surface.SelectedItems[0].Item, new { Location = tools.MouseLocation }, 100, 0);
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
            Index = 0;
            Line = LineSegment.Empty;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => nameof(Straightener);

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
