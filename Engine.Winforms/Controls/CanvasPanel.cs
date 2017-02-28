// <copyright file="CanvasPanel.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CanvasPanel
        : Control
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private static readonly object EventMouseWheel = new object();

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public CanvasPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
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
        private void WmKeyChar(ref Message m)
        {
            if (ProcessKeyMessage(ref m)) return;
            DefWndProc(ref m);
        }

        /// <devdoc>
        /// Handles the WM_MOUSEWHEEL message
        /// </devdoc>
        /// <internalonly/>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private void WmMouseWheel(ref Message m)
        {
            Point p = PointToClient(new Point(m.LParam.SignedLOWORD(), m.LParam.SignedHIWORD()));
            var e = new HandledMouseEventArgs(MouseButtons.None, 0, p.X, p.Y, m.WParam.SignedHIWORD());
            OnMouseWheel(e);
            m.Result = (IntPtr)(e.Handled ? 0 : 1);
            // Forwarding the message to the parent window
            if (!e.Handled) DefWndProc(ref m);
        }

        /// <devdoc>
        /// Handles the WM_MOUSEHWHEEL message
        /// </devdoc>
        /// <internalonly/>
        /// <remarks>
        /// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Control.cs,4325aceddf2ad61a
        /// </remarks>
        private void WmMouseWheelTilt(ref Message m)
        {
            Point p = PointToClient(new Point(m.LParam.SignedLOWORD(), m.LParam.SignedHIWORD()));
            var e = new HandledMouseEventArgs(MouseButtons.None, 0, p.X, p.Y, m.WParam.SignedHIWORD());
            OnMouseWheelTilt(e);
            m.Result = (IntPtr)(e.Handled ? 0 : 1);
            // Forwarding the message to the parent window
            if (!e.Handled) DefWndProc(ref m);
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
