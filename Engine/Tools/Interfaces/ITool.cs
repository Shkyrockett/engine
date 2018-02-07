// <copyright file="ITool.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Tools
{
    /// <summary>
    /// Interface for tool objects.
    /// </summary>
    public interface ITool
    {
        #region Callbacks
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        event Tool.ToolFinishEvent Finish;
        #endregion Callbacks

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the tool is in use.
        /// </summary>
        bool InUse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the first action of tool has taken place.
        /// </summary>
        bool Started { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mouse button is up.
        /// </summary>
        bool MouseUp { get; set; }
        #endregion Properties

        /// <summary>
        /// Reset the tool.
        /// </summary>
        void Reset();

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        void MouseDownUpdate(ToolStack tools);

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        void MouseMoveUpdate(ToolStack tools);

        /// <summary>
        /// Update Tool on Mouse UP signal.
        /// </summary>
        /// <param name="tools"></param>
        void MouseUpUpdate(ToolStack tools);

        /// <summary>
        /// Update Tool on Mouse scroll signal.
        /// </summary>
        /// <param name="tools"></param>
        void MouseScrollUpdate(ToolStack tools);

        /// <summary>
        /// 
        /// </summary>
        string ToString();
    }
}
