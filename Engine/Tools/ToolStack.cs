using Engine.Geometry;
using System;
using System.Collections.Generic;

namespace Engine.Tools
{
    public enum ButtonState
    {
        Up = 0,
        Down = 1,
    }

    [Flags]
    public enum MouseButtons
    {
        None = 0,
        Left = 1048576,
        Right = 2097152,
        Middle = 4194304,
        XButton1 = 8388608,
        XButton2 = 16777216
    }

    public enum ScrollOrientation
    {
        HorizontalScroll = 0,
        VerticalScroll = 1
    }

    public enum ScrollEventType
    {
        SmallDecrement = 0,
        SmallIncrement = 1,
        LargeDecrement = 2,
        LargeIncrement = 3,
        ThumbPosition = 4,
        ThumbTrack = 5,
        First = 6,
        Last = 7,
        EndScroll = 8
    }

    public struct ToolStack
    {
        public List<Tool> Tools;

        private Point2D mouseLocation;

        private ButtonState mouseLeftStatus;

        private ButtonState mouseMiddleStatus;

        private ButtonState mouseRightStatus;

        private ButtonState mouseXButton1Status;

        private ButtonState mouseXButton2Status;

        private double mouseHorizontalScrollDelta;

        private double mouseVerticalScrollDelta;

        private int clicks;

        public Point2D MouseLocation
        {
            get { return mouseLocation; }
            set { mouseLocation = value; }
        }

        public ButtonState MouseLeftStatus
        {
            get { return mouseLeftStatus; }
            set
            {
                mouseLeftStatus = value;
            }
        }

        public ButtonState MouseMiddleStatus
        {
            get { return mouseMiddleStatus; }
            set { mouseMiddleStatus = value; }
        }

        public ButtonState MouseRightStatus
        {
            get { return mouseRightStatus; }
            set { mouseRightStatus = value; }
        }

        public ButtonState MouseXButton1Status
        {
            get { return mouseXButton1Status; }
            set { mouseXButton1Status = value; }
        }

        public ButtonState MouseXButton2Status
        {
            get { return mouseXButton2Status; }
            set { mouseXButton2Status = value; }
        }

        public double MouseHorizontalScrollDelta
        {
            get { return mouseHorizontalScrollDelta; }
            set { mouseHorizontalScrollDelta = value; }
        }

        public double MouseVerticalScrollDelta
        {
            get { return mouseVerticalScrollDelta; }
            set { mouseVerticalScrollDelta = value; }
        }

        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }

        public void MouseUpdate(Point2D location)
        {
            if (mouseLocation != location)
            {
                mouseLocation = location;
            }
        }

        public void MouseMove(Point2D location)
        {
            if (mouseLocation != location)
            {
                mouseLocation = location;
            }
        }

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

        public void MouseScroll(ScrollOrientation orientation, double delta)
        {
            switch (orientation)
            {
                case ScrollOrientation.HorizontalScroll:
                    this.mouseHorizontalScrollDelta = delta;
                    break;
                case ScrollOrientation.VerticalScroll:
                    this.mouseVerticalScrollDelta = delta;
                    break;
                default:
                    break;
            }
        }

    }
}
