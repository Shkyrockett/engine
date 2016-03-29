// <copyright file="Tool.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// Base Tool class.
    /// </summary>
    public abstract class Tool
        : ITool
    {
        /// <summary>
        /// Check to determine if the tool is in use.
        /// </summary>
        protected bool inUse;

        /// <summary>
        /// Check if first action of tool has taken place.
        /// </summary>
        protected bool started;

        /// <summary>
        /// Signal the canvas to provide feedback to finish the command.
        /// </summary>
        public event ToolFinishEvent Finish;

        /// <summary>
        /// The <see cref="ToolFinishEvent"/> type delegate. 
        /// </summary>
        public delegate void ToolFinishEvent();

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
        /// Initializes a new instance of the <see cref="Tool"/> class.
        /// </summary>
        public Tool()
        {
            inUse = false;
            started = false;
        }

        /// <summary>
        /// Signal the parent to provide feedback to finish the command.
        /// </summary>
        protected virtual void RaiseFinishEvent()
        {
            if (Finish != null) Finish();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update tool on mouse down.
        /// </summary>
        /// <param name="e"></param>
        public virtual void MouseDownUpdate(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Tool on Mouse Move.
        /// </summary>
        /// <param name="e">The Mouse Move event arguments.</param>
        /// <param name="MouseDown">A bool indicating whether a mouse button has been pressed.</param>
        public virtual void MouseMoveUpdate(MouseEventArgs e, bool MouseDown)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Tool on Mouse UP signal.
        /// </summary>
        /// <param name="e"></param>
        public virtual void MouseUpUpdate(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Tool on Mouse scroll signal.
        /// </summary>
        /// <param name="e"></param>
        public void MouseScrollUpdate(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render the tool to the provided graphics object.
        /// </summary>
        public virtual void Render(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render the tool to a Graphics object.
        /// </summary>
        /// <param name="graphics">The graphics object to draw on to.</param>
        /// <param name="pen">The custom drawing pen for the tool to render.</param>
        /// <param name="brush">The drawing brush for the tool to render.</param>
        public virtual void Render(Graphics graphics, Pen pen, Brush brush)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render the tool to a Graphics object.
        /// </summary>
        /// <param name="graphics">The graphics object to draw to.</param>
        /// <param name="penBrush">The drawing pen and drawing brush combination for the line to render.</param>
        public virtual void Render(Graphics graphics, List<Tuple<Pen, Brush>> penBrush)
        {
            foreach (Tuple<Pen, Brush> item in penBrush)
            {
                Render(graphics, item.Item1, item.Item2);
            }
        }

        /// <summary>
        /// A string representing a string output of the tool.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> representing the type of the tool.</returns>
        public override string ToString()
        {
            return GetType().ToString();
        }
    }
}
