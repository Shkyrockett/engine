﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/1517179/c-overriding-onpaint-on-progressbar-not-working
    /// </remarks>
    public partial class StatusProgressBar
        : ProgressBar
    {
        /// <summary>
        /// 
        /// </summary>
        public StatusProgressBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        const int WmPaint = 0x0F;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WmPaint:
                    using (var graphics = Graphics.FromHwnd(Handle))
                    {
                        var textSize = graphics.MeasureString(Text, Font);

                        using (var textBrush = new SolidBrush(ForeColor))
                            graphics.DrawString(Text, Font, textBrush, (Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));
                    }
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-3, -3);
            if (this.Value > 0)
            {
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)this.Value / this.Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // assumes this.Maximum == 100
            string text = this.Value.ToString() + '%';

            using (Font f = new Font(FontFamily.GenericMonospace, 10))
            {
                SizeF strLen = g.MeasureString(text, f);
                Point location = new Point((int)((rect.Width / 2) - (strLen.Width / 2)), (int)((rect.Height / 2) - (strLen.Height / 2)));
                g.DrawString(text, f, Brushes.Black, location);
            }
        }
    }
}