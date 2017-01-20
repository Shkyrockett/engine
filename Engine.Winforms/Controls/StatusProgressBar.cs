// <copyright file="StatusProgressBar.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
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

        private const int WmPaint = 0x0F;

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
                    using (Graphics graphics = Graphics.FromHwnd(Handle))
                    {
                        SizeF textSize = graphics.MeasureString(Text, Font);

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
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-3, -3);
            if (Value > 0)
            {
                var clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // assumes this.Maximum == 100
            string text = Value.ToString() + '%';

            using (var f = new Font(FontFamily.GenericMonospace, 10))
            {
                SizeF strLen = g.MeasureString(text, f);
                var location = new Point((int)((rect.Width / 2) - (strLen.Width / 2)), (int)((rect.Height / 2) - (strLen.Height / 2)));
                g.DrawString(text, f, Brushes.Black, location);
            }
        }
    }
}
