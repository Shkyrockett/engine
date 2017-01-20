// <copyright file="Tool.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Tools
{
    /// <summary>
    /// Base Tool class.
    /// </summary>
    public abstract class Tool
        : ITool
    {
        #region Callbacks

        /// <summary>
        /// Signal the canvas to provide feedback to finish the command.
        /// </summary>
        public event ToolFinishEvent Finish;

        /// <summary>
        /// The <see cref="ToolFinishEvent"/> type delegate. 
        /// </summary>
        public delegate void ToolFinishEvent(object Sender, ToolStack e);

        #endregion

        #region Fields

        /// <summary>
        /// Check to determine if the tool is in use.
        /// </summary>
        protected bool inUse;

        /// <summary>
        /// Check if first action of tool has taken place.
        /// </summary>
        protected bool started;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tool"/> class.
        /// </summary>
        public Tool()
        {
            inUse = false;
            started = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Check if the tool is in use.
        /// </summary>
        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        /// <summary>
        /// Check whether the first action of tool has taken place. 
        /// </summary>
        public bool Started
        {
            get { return started; }
            set { started = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MouseUp { get; set; }

        #endregion

        /// <summary>
        /// Signal the parent to provide feedback to finish the command.
        /// </summary>
        protected virtual void RaiseFinishEvent(ToolStack tools)
            => Finish?.Invoke(this, tools);

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reset()
        { }

        /// <summary>
        /// Update the tool with the keys that have been pressed.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void KeyboardKeyDown(ToolStack obj)
        { }

        /// <summary>
        /// Update the tool with the keys that have been released.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void KeyboardKeyUp(ToolStack obj)
        { }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="tools"></param>
        public virtual void MouseDownUpdate(ToolStack tools)
        { }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="tools">The Mouse Move event arguments.</param>
        public virtual void MouseMoveUpdate(ToolStack tools)
        { }

        /// <summary>
        /// Update Tool on Mouse UP signal.
        /// </summary>
        /// <param name="tools"></param>
        public virtual void MouseUpUpdate(ToolStack tools)
        { }

        /// <summary>
        /// Update Tool on Mouse scroll signal.
        /// </summary>
        /// <param name="tools"></param>
        public virtual void MouseScrollUpdate(ToolStack tools)
        { }

        /// <summary>
        /// A string representing a string output of the tool.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> representing the type of the tool.</returns>
        public override string ToString()
            => GetType().ToString();
    }
}
