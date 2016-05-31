using Engine.Geometry;
using System;
using System.Collections.Generic;

namespace Engine.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public enum ButtonState
    {
        /// <summary>
        /// 
        /// </summary>
        Up = 0,

        /// <summary>
        /// 
        /// </summary>
        Down = 1,
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum MouseButtons
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        Left = 1048576,

        /// <summary>
        /// 
        /// </summary>
        Right = 2097152,

        /// <summary>
        /// 
        /// </summary>
        Middle = 4194304,

        /// <summary>
        /// 
        /// </summary>
        XButton1 = 8388608,

        /// <summary>
        /// 
        /// </summary>
        XButton2 = 16777216
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ScrollOrientation
    {
        /// <summary>
        /// 
        /// </summary>
        HorizontalScroll = 0,

        /// <summary>
        /// 
        /// </summary>
        VerticalScroll = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ScrollEventType
    {
        /// <summary>
        /// 
        /// </summary>
        SmallDecrement = 0,

        /// <summary>
        /// 
        /// </summary>
        SmallIncrement = 1,

        /// <summary>
        /// 
        /// </summary>
        LargeDecrement = 2,

        /// <summary>
        /// 
        /// </summary>
        LargeIncrement = 3,

        /// <summary>
        /// 
        /// </summary>
        ThumbPosition = 4,

        /// <summary>
        /// 
        /// </summary>
        ThumbTrack = 5,

        /// <summary>
        /// 
        /// </summary>
        First = 6,

        /// <summary>
        /// 
        /// </summary>
        Last = 7,

        /// <summary>
        /// 
        /// </summary>
        EndScroll = 8
    }

    /// <summary>
    /// 
    /// </summary>
    public struct ToolStack
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Tool> Tools;

        /// <summary>
        /// 
        /// </summary>
        private Point2D mouseLocation;

        /// <summary>
        /// 
        /// </summary>
        private ButtonState mouseLeftStatus;

        /// <summary>
        /// 
        /// </summary>
        private ButtonState mouseMiddleStatus;

        /// <summary>
        /// 
        /// </summary>
        private ButtonState mouseRightStatus;

        /// <summary>
        /// 
        /// </summary>
        private ButtonState mouseXButton1Status;

        /// <summary>
        /// 
        /// </summary>
        private ButtonState mouseXButton2Status;

        /// <summary>
        /// 
        /// </summary>
        private double mouseHorizontalScrollDelta;

        /// <summary>
        /// 
        /// </summary>
        private double mouseVerticalScrollDelta;

        /// <summary>
        /// 
        /// </summary>
        private int clicks;

        /// <summary>
        /// 
        /// </summary>
        public Point2D MouseLocation
        {
            get { return mouseLocation; }
            set { mouseLocation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ButtonState MouseLeftStatus
        {
            get { return mouseLeftStatus; }
            set { mouseLeftStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ButtonState MouseMiddleStatus
        {
            get { return mouseMiddleStatus; }
            set { mouseMiddleStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ButtonState MouseRightStatus
        {
            get { return mouseRightStatus; }
            set { mouseRightStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ButtonState MouseXButton1Status
        {
            get { return mouseXButton1Status; }
            set { mouseXButton1Status = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ButtonState MouseXButton2Status
        {
            get { return mouseXButton2Status; }
            set { mouseXButton2Status = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MouseHorizontalScrollDelta
        {
            get { return mouseHorizontalScrollDelta; }
            set { mouseHorizontalScrollDelta = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MouseVerticalScrollDelta
        {
            get { return mouseVerticalScrollDelta; }
            set { mouseVerticalScrollDelta = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public void MouseUpdate(Point2D location)
        {
            if (mouseLocation != location)
            {
                mouseLocation = location;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public void MouseMove(Point2D location)
        {
            if (mouseLocation != location)
            {
                mouseLocation = location;
            }
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
                if (buttons == MouseButtons.Left) MouseLeftStatus = ButtonState.Up;
                if (buttons == MouseButtons.Middle) MouseMiddleStatus = ButtonState.Up;
                if (buttons == MouseButtons.Right) MouseRightStatus = ButtonState.Up;
                if (buttons == MouseButtons.XButton1) MouseXButton1Status = ButtonState.Up;
                if (buttons == MouseButtons.XButton2) MouseXButton2Status = ButtonState.Up;
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
                if (buttons == MouseButtons.Left) MouseLeftStatus = ButtonState.Down;
                if (buttons == MouseButtons.Middle) MouseMiddleStatus = ButtonState.Down;
                if (buttons == MouseButtons.Right) MouseRightStatus = ButtonState.Down;
                if (buttons == MouseButtons.XButton1) MouseXButton1Status = ButtonState.Down;
                if (buttons == MouseButtons.XButton2) MouseXButton2Status = ButtonState.Down;
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
                    mouseHorizontalScrollDelta = delta;
                    break;
                case ScrollOrientation.VerticalScroll:
                    mouseVerticalScrollDelta = delta;
                    break;
                default:
                    break;
            }
        }
    }
}
