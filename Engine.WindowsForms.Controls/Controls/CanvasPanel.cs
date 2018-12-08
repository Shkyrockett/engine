// <copyright file="CanvasPanel.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Engine.WindowsForms
{
    /// <summary>
    /// The canvas panel class.
    /// </summary>
    public partial class CanvasPanel
        : Control
    {
        /// <summary>
        /// The event mouse wheel (readonly). Value: new object().
        /// </summary>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private static readonly object EventMouseWheel = new object();

        /// <summary>
        /// The event mouse wheel tilt (readonly). Value: new object().
        /// </summary>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private static readonly object EventMouseWheelTilt = new object();

        /// <summary>
        /// Occurs when the mouse wheel tilts while the control has focus.
        /// </summary>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        [Category("Mouse")]
        [Description("Occurs when the mouse wheel tilts while the control has focus.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event MouseEventHandler MouseWheelTilt
        {
            add { Events.AddHandler(EventMouseWheelTilt, value); }
            remove { Events.RemoveHandler(EventMouseWheelTilt, value); }
        }

        /// <summary>
        /// Occurs when the mouse wheel moves while the control has focus.
        /// </summary>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        [Category("Mouse")]
        [Description("Occurs when the mouse wheel moves while the control has focus.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseWheel
        {
            add { Events.AddHandler(EventMouseWheel, value); }
            remove { Events.RemoveHandler(EventMouseWheel, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasPanel"/> class.
        /// </summary>
        public CanvasPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The wnd proc.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_MOUSEWHEEL:
                    WmMouseWheel(ref m);
                    return;
                case NativeMethods.WM_MOUSEHWHEEL:
                    WmMouseWheelTilt(ref m);
                    return;
            }

            base.WndProc(ref m);
        }

        /// <devdoc>
        ///     Handles the WM_CHAR, WM_KEYDOWN, WM_SYSKEYDOWN, WM_KEYUP, and
        ///     WM_SYSKEYUP messages.
        /// </devdoc>
        /// <internalonly/>
        /// <summary>
        /// The wm key char.
        /// </summary>
        /// <param name="m">The m.</param>
        private void WmKeyChar(ref Message m)
        {
            if (ProcessKeyMessage(ref m))
            {
                return;
            }

            DefWndProc(ref m);
        }

        /// <summary>
        /// Handles the WM_MOUSEWHEEL message
        /// </summary>
        /// <param name="m">The m.</param>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private void WmMouseWheel(ref Message m)
        {
            var p = PointToClient(new Point(m.LParam.SignedLowWord(), m.LParam.SignedHighWord()));
            var e = new HandledMouseEventArgs(MouseButtons.None, 0, p.X, p.Y, m.WParam.SignedHighWord());
            OnMouseWheel(e);
            m.Result = (IntPtr)(e.Handled ? 0 : 1);
            // Forwarding the message to the parent window
            if (!e.Handled)
            {
                DefWndProc(ref m);
            }
        }

        /// <summary>
        /// Handles the WM_MOUSEHWHEEL message
        /// </summary>
        /// <param name="m">The m.</param>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private void WmMouseWheelTilt(ref Message m)
        {
            var p = PointToClient(new Point(m.LParam.SignedLowWord(), m.LParam.SignedHighWord()));
            var e = new HandledMouseEventArgs(MouseButtons.None, 0, p.X, p.Y, m.WParam.SignedHighWord());
            OnMouseWheelTilt(e);
            m.Result = (IntPtr)(e.Handled ? 0 : 1);
            // Forwarding the message to the parent window
            if (!e.Handled)
            {
                DefWndProc(ref m);
            }
        }

        /// <summary>
        /// Raises the <see cref='MouseWheelTilt'/> event.
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseWheelTilt(MouseEventArgs e) => ((MouseEventHandler)Events[EventMouseWheelTilt])?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref='MouseWheel'/> event.
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnMouseWheel(MouseEventArgs e) => ((MouseEventHandler)Events[EventMouseWheel])?.Invoke(this, e);
    }
}
