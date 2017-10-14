// <copyright file="ToolStack.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class ToolStack
    {
        #region Callbacks

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> keyboardKeyUp;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> keyboardKeyDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseMove;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseScroll;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseScrollTilt;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseLeftButtonDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseMiddleButtonDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseRightButtonDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseBackButtonDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseForwardButtonDown;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseLeftButtonUp;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseMiddleButtonUp;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseRightButtonUp;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseBackButtonUp;

        /// <summary>
        /// 
        /// </summary>
        internal Action<ToolStack> mouseForwardButtonUp;

        #endregion

        #region Fields

        private Dictionary<MouseButtons, Tool> tools;

        /// <summary>
        /// 
        /// </summary>
        private Point2D mouseLocation;

        /// <summary>
        /// 
        /// </summary>
        private double mouseScrollTiltDelta;

        /// <summary>
        /// 
        /// </summary>
        private double mouseVerticalScrollDelta;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ToolStack(VectorMap surface)
        {
            Surface = surface;
            tools = new Dictionary<MouseButtons, Tool>();
            KeyboardKeyStates = Keys.None;
            mouseLocation = Point2D.Empty;
            MouseButtonStates = MouseButtons.None;
            mouseScrollTiltDelta = 0;
            Clicks = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the press state of the keyboard keys.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The press state of the keyboard keys.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Keys KeyboardKeyStates { get; set; }

        /// <summary>
        /// Gets or sets the location of the mouse cursor.
        /// </summary>
        //[Category("Location")]
        //[Description("The location of the mouse cursor.")]
        [TypeConverter(typeof(Point2DConverter))]
        //[RefreshProperties(RefreshProperties.All)]
        public Point2D MouseLocation
        {
            get { return mouseLocation; }
            set
            {
                mouseLocation = value;
                mouseMove?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the number of times a mouse button has been clicked.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The number of times a mouse button has been clicked.")]
        //[RefreshProperties(RefreshProperties.All)]
        public int Clicks { get; set; }

        /// <summary>
        /// Gets or sets the click state of the mouse buttons.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the mouse buttons.")]
        //[RefreshProperties(RefreshProperties.All)]
        public MouseButtons MouseButtonStates { get; set; }

        /// <summary>
        /// Gets or sets the click state of the <see cref="MouseButtons.Left"/> mouse button.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the " + nameof(MouseButtons.Left) + " mouse button.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Verticality MouseLeftButtonStatus
        {
            get { return ((MouseButtonStates & MouseButtons.Left) != 0) ? Verticality.Down : Verticality.Up; }
            set
            {
                MouseButtonStates = (value == Verticality.Down) ? MouseButtonStates |= MouseButtons.Left : MouseButtonStates &= ~MouseButtons.Left;
                switch (value)
                {
                    case Verticality.Up:
                        //mouseButtonUp?.Invoke(this);
                        mouseLeftButtonUp?.Invoke(this);
                        break;
                    case Verticality.Down:
                        //mouseButtonDown?.Invoke(this);
                        mouseLeftButtonDown?.Invoke(this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the click state of the <see cref="MouseButtons.Middle"/> mouse button.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the " + nameof(MouseButtons.Middle) + " mouse button.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Verticality MouseMiddleButtonStatus
        {
            get { return ((MouseButtonStates & MouseButtons.Middle) != 0) ? Verticality.Down : Verticality.Up; }
            set
            {
                MouseButtonStates = (value == Verticality.Down) ? MouseButtonStates |= MouseButtons.Middle : MouseButtonStates &= ~MouseButtons.Middle;
                switch (value)
                {
                    case Verticality.Up:
                        mouseMiddleButtonUp?.Invoke(this);
                        break;
                    case Verticality.Down:
                        mouseMiddleButtonDown?.Invoke(this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the click state of the <see cref="MouseButtons.Right"/> mouse button.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the " + nameof(MouseButtons.Right) + " mouse button.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Verticality MouseRightButtonStatus
        {
            get { return ((MouseButtonStates & MouseButtons.Right) != 0) ? Verticality.Down : Verticality.Up; }
            set
            {
                MouseButtonStates = (value == Verticality.Down) ? MouseButtonStates |= MouseButtons.Right : MouseButtonStates &= ~MouseButtons.Right;
                switch (value)
                {
                    case Verticality.Up:
                        mouseRightButtonUp?.Invoke(this);
                        break;
                    case Verticality.Down:
                        mouseRightButtonDown?.Invoke(this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the  click state of the <see cref="MouseButtons.Back"/> mouse button.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the " + nameof(MouseButtons.Back) + " mouse button.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Verticality MouseBackButtonStatus
        {
            get { return ((MouseButtonStates & MouseButtons.Back) != 0) ? Verticality.Down : Verticality.Up; }
            set
            {
                MouseButtonStates = (value == Verticality.Down) ? MouseButtonStates |= MouseButtons.Back : MouseButtonStates &= ~MouseButtons.Back;
                switch (value)
                {
                    case Verticality.Up:
                        mouseBackButtonUp?.Invoke(this);
                        break;
                    case Verticality.Down:
                        mouseBackButtonDown?.Invoke(this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the click state of the <see cref="MouseButtons.Forward"/> mouse button.
        /// </summary>
        //[Category("Buttons")]
        //[Description("The click state of the " + nameof(MouseButtons.Forward) + " mouse button.")]
        //[RefreshProperties(RefreshProperties.All)]
        public Verticality MouseForwardButtonStatus
        {
            get { return ((MouseButtonStates & MouseButtons.Forward) != 0) ? Verticality.Down : Verticality.Up; }
            set
            {
                MouseButtonStates = (value == Verticality.Down) ? MouseButtonStates |= MouseButtons.Forward : MouseButtonStates &= ~MouseButtons.Forward;
                switch (value)
                {
                    case Verticality.Up:
                        mouseForwardButtonUp?.Invoke(this);
                        break;
                    case Verticality.Down:
                        mouseForwardButtonDown?.Invoke(this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the last scroll delta of the mouse scroll wheel.
        /// </summary>
        //[Category("Scrolling")]
        //[Description("The last scroll delta of the mouse scroll wheel.")]
        //[RefreshProperties(RefreshProperties.All)]
        public double MouseScrollDelta
        {
            get { return mouseVerticalScrollDelta; }
            set
            {
                mouseVerticalScrollDelta = value;
                mouseScroll?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the last tilt delta of the mouse scroll wheel.
        /// </summary>
        //[Category("Scrolling")]
        //[Description("The last tilt delta of the mouse scroll wheel.")]
        //[RefreshProperties(RefreshProperties.All)]
        public double MouseScrollTiltDelta
        {
            get { return mouseScrollTiltDelta; }
            set
            {
                mouseScrollTiltDelta = value;
                mouseScrollTilt?.Invoke(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public VectorMap Surface { get; set; }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        public void KeyUp(Keys keys)
            => KeyboardKeyStates |= ~keys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        public void KeyDown(Keys keys)
            => KeyboardKeyStates |= keys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public void MouseMove(Point2D location)
        {
            if (mouseLocation != location) MouseLocation = location;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="clicks"></param>
        public void MouseUp(MouseButtons buttons, int clicks)
        {
            if (buttons != MouseButtons.None)
            {
                if (buttons == MouseButtons.Left && MouseLeftButtonStatus != Verticality.Up) MouseLeftButtonStatus = Verticality.Up;
                if (buttons == MouseButtons.Middle && MouseMiddleButtonStatus != Verticality.Up) MouseMiddleButtonStatus = Verticality.Up;
                if (buttons == MouseButtons.Right && MouseRightButtonStatus != Verticality.Up) MouseRightButtonStatus = Verticality.Up;
                if (buttons == MouseButtons.Back && MouseBackButtonStatus != Verticality.Up) MouseBackButtonStatus = Verticality.Up;
                if (buttons == MouseButtons.Forward && MouseForwardButtonStatus != Verticality.Up) MouseForwardButtonStatus = Verticality.Up;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="clicks"></param>
        public void MouseDown(MouseButtons buttons, int clicks)
        {
            if (buttons != MouseButtons.None)
            {
                if (buttons == MouseButtons.Left && MouseLeftButtonStatus != Verticality.Down) MouseLeftButtonStatus = Verticality.Down;
                if (buttons == MouseButtons.Middle && MouseMiddleButtonStatus != Verticality.Down) MouseMiddleButtonStatus = Verticality.Down;
                if (buttons == MouseButtons.Right && MouseRightButtonStatus != Verticality.Down) MouseRightButtonStatus = Verticality.Down;
                if (buttons == MouseButtons.Back && MouseBackButtonStatus != Verticality.Down) MouseBackButtonStatus = Verticality.Down;
                if (buttons == MouseButtons.Forward && MouseForwardButtonStatus != Verticality.Down) MouseForwardButtonStatus = Verticality.Down;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="delta"></param>
        public void MouseScroll(ScrollOrientation orientation, double delta)
        {
            switch (orientation)
            {
                case ScrollOrientation.HorizontalScroll:
                    MouseScrollTiltDelta = delta;
                    break;
                case ScrollOrientation.VerticalScroll:
                    MouseScrollDelta = delta;
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterObserver(Tool tool)
        {
            keyboardKeyDown += tool.KeyboardKeyDown;
            keyboardKeyUp += tool.KeyboardKeyUp;
            mouseMove += tool.MouseMoveUpdate;
            mouseLeftButtonDown += tool.MouseDownUpdate;
            mouseLeftButtonUp += tool.MouseUpUpdate;
            mouseMiddleButtonDown += tool.MouseDownUpdate;
            mouseMiddleButtonUp += tool.MouseUpUpdate;
            mouseRightButtonDown += tool.MouseDownUpdate;
            mouseRightButtonUp += tool.MouseUpUpdate;
            mouseBackButtonDown += tool.MouseDownUpdate;
            mouseBackButtonUp += tool.MouseUpUpdate;
            mouseForwardButtonDown += tool.MouseDownUpdate;
            mouseForwardButtonUp += tool.MouseUpUpdate;
            mouseScroll += tool.MouseScrollUpdate;
            mouseScrollTilt += tool.MouseScrollUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseMove(Tool tool)
            => mouseMove += tool.MouseMoveUpdate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseLeftButton(Tool tool)
        {
            if (!tools.ContainsKey(MouseButtons.Left))
            {
                tools.Add(MouseButtons.Left, tool);
                mouseMove += tool.MouseMoveUpdate;
                mouseLeftButtonDown += tool.MouseDownUpdate;
                mouseLeftButtonUp += tool.MouseUpUpdate;
            }
            else
            {
                Tool t = tools[MouseButtons.Left];
                mouseMove -= t.MouseMoveUpdate;
                mouseLeftButtonDown -= t.MouseDownUpdate;
                mouseLeftButtonUp -= t.MouseUpUpdate;

                tools.Remove(MouseButtons.Left);
                tools.Add(MouseButtons.Left, tool);

                mouseMove += tool.MouseMoveUpdate;
                mouseLeftButtonDown += tool.MouseDownUpdate;
                mouseLeftButtonUp += tool.MouseUpUpdate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseMiddleButton(Tool tool)
        {
            if (!tools.ContainsKey(MouseButtons.Middle))
            {
                tools.Add(MouseButtons.Middle, tool);
                mouseMove += tool.MouseMoveUpdate;
                mouseMiddleButtonDown += tool.MouseDownUpdate;
                mouseMiddleButtonUp += tool.MouseUpUpdate;
            }
            else
            {
                Tool t = tools[MouseButtons.Middle];
                tools.Add(MouseButtons.Middle, tool);
                mouseMove += t.MouseMoveUpdate;
                mouseMiddleButtonDown += t.MouseDownUpdate;
                mouseMiddleButtonUp += t.MouseUpUpdate;

                tools.Remove(MouseButtons.Middle);
                tools.Add(MouseButtons.Middle, tool);

                mouseMove += tool.MouseMoveUpdate;
                mouseMiddleButtonDown += tool.MouseDownUpdate;
                mouseMiddleButtonUp += tool.MouseUpUpdate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseRightButton(Tool tool)
        {
            if (!tools.ContainsKey(MouseButtons.Right))
            {
                tools.Add(MouseButtons.Right, tool);
                mouseMove += tool.MouseMoveUpdate;
                mouseRightButtonDown += tool.MouseDownUpdate;
                mouseRightButtonUp += tool.MouseUpUpdate;
            }
            else
            {
                Tool t = tools[MouseButtons.Right];
                mouseMove += t.MouseMoveUpdate;
                mouseRightButtonDown += t.MouseDownUpdate;
                mouseRightButtonUp += t.MouseUpUpdate;

                tools.Remove(MouseButtons.Right);
                tools.Add(MouseButtons.Right, tool);

                mouseMove += tool.MouseMoveUpdate;
                mouseRightButtonDown += tool.MouseDownUpdate;
                mouseRightButtonUp += tool.MouseUpUpdate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseBackButton(Tool tool)
        {
            if (!tools.ContainsKey(MouseButtons.Back))
            {
                tools.Add(MouseButtons.Back, tool);
                mouseMove += tool.MouseMoveUpdate;
                mouseBackButtonDown += tool.MouseDownUpdate;
                mouseBackButtonUp += tool.MouseUpUpdate;
            }
            else
            {
                Tool t = tools[MouseButtons.Back];
                mouseMove += t.MouseMoveUpdate;
                mouseBackButtonDown += t.MouseDownUpdate;
                mouseBackButtonUp += t.MouseUpUpdate;

                tools.Remove(MouseButtons.Back);
                tools.Add(MouseButtons.Back, tool);

                mouseMove += tool.MouseMoveUpdate;
                mouseBackButtonDown += tool.MouseDownUpdate;
                mouseBackButtonUp += tool.MouseUpUpdate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseForwardButton(Tool tool)
        {
            if (!tools.ContainsKey(MouseButtons.Forward))
            {
                tools.Add(MouseButtons.Forward, tool);
                mouseMove += tool.MouseMoveUpdate;
                mouseForwardButtonDown += tool.MouseDownUpdate;
                mouseForwardButtonUp += tool.MouseUpUpdate;
            }
            else
            {
                Tool t = tools[MouseButtons.Forward];
                mouseMove += t.MouseMoveUpdate;
                mouseForwardButtonDown += t.MouseDownUpdate;
                mouseForwardButtonUp += t.MouseUpUpdate;

                tools.Remove(MouseButtons.Forward);
                tools.Add(MouseButtons.Forward, tool);

                mouseMove += tool.MouseMoveUpdate;
                mouseForwardButtonDown += tool.MouseDownUpdate;
                mouseForwardButtonUp += tool.MouseUpUpdate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseScroll(Tool tool)
        {
            mouseMove += tool.MouseMoveUpdate;
            mouseScroll += tool.MouseScrollUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        public void RegisterMouseScrollTilt(Tool tool)
        {
            mouseMove += tool.MouseMoveUpdate;
            mouseScrollTilt += tool.MouseScrollUpdate;
        }
    }
}
