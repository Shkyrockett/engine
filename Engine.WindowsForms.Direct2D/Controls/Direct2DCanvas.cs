// <copyright file="Direct2DCanvas.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System;
using System.Windows.Forms;
using static System.Math;

namespace Engine.Winforms.Direct2D
{
    /// <summary>
    /// The direct2d canvas class.
    /// </summary>
    public partial class Direct2DCanvas
        : UserControl
    {
        /// <summary>
        /// Window Render target.
        /// </summary>
        private WindowRenderTarget target;

        /// <summary>
        /// The color
        /// </summary>
        Color color = Color.CornflowerBlue;

        /// <summary>
        /// The bands
        /// </summary>
        private float[] bands;

        /// <summary>
        /// The gap
        /// </summary>
        private readonly float gap;

        /// <summary>
        /// The frequency
        /// </summary>
        private static readonly int frequency;

        /// <summary>
        /// The band brush.
        /// </summary>
        private LinearGradientBrush bandBrush;

        /// <summary>
        /// The random.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// The cycle count.
        /// </summary>
        private int cycleCount = 0;

        /// <summary>
        /// The band count
        /// </summary>
        private readonly int bandCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Direct2DCanvas" /> class.
        /// </summary>
        public Direct2DCanvas()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Selectable
                , true);
            SetStyle(
                ControlStyles.UserPaint // We will be doing all of our own painting.
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw // We are doing our own repainting when the control is resized.
                | ControlStyles.Opaque  // Let's let the parent paint, so we can have transparencies if needed.
                , false);
            InitializeComponent();
        }

        /// <summary>
        /// The wnd proc.
        /// </summary>
        /// <param name="m">The m.</param>
        protected override void WndProc(ref Message m)
        {
            // Process other controls first.
            base.WndProc(ref m);

            // Handle messages we want to customize.
            switch ((WindowsMessages)m.Msg)
            {
                case WindowsMessages.WM_PAINT:
                    // We are doing our own painting.
                    Render();
                    m.Result = (IntPtr)1;
                    break;
                case WindowsMessages.WM_ERASEBKGND:
                    // We are ignoring EraseBackground since we are drawing it all ourselves.
                    m.Result = (IntPtr)1;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The direct2d canvas load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Direct2DCanvas_Load(object sender, EventArgs e)
        {
            // Get the changed object.
            var panel = sender as Direct2DCanvas;

            // Create DirectX target
            InitializeTarget(panel.Width, panel.Height);

            // Initialize the world.
            UpdateWorld();

            // Set the number of ticks to refresh the world.
            timer.Interval = 50;

            // Start the world update timer if it isn't in design mode.
            if (!DesignMode) timer.Start();
        }

        /// <summary>
        /// The direct2d canvas resize.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Direct2DCanvas_Resize(object sender, EventArgs e)
        {
            // Get the changed object.
            var panel = sender as Direct2DCanvas;

            // Update the resolution.
            InitializeTarget(panel.Width, panel.Height);

            //Invalidate and redraw the form http://stackoverflow.com/a/9827091
            Update();
        }

        /// <summary>
        /// The timer tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Process all changes to the world.
            UpdateWorld();

            // Now that the world has been updated it needs to be redrawn.
            Invalidate();
        }

        /// <summary>
        /// The render.
        /// </summary>
        public void Render()
        {
            target?.BeginDraw();

            target?.Clear(color);

            var bandCount = bands.Length;

            var bandWidth = (target.Size.Width + gap) / bandCount;

            var i = 0;
            foreach (var bandPercentage in bands)
            {
                target.FillRectangle(new RectangleF(i * bandWidth, target.Size.Height * bandPercentage / 100, bandWidth - gap, target.Size.Height - (target.Size.Height * bandPercentage / 100)), bandBrush);
                i++;
            }

            //target.DrawText("Testing",  TextFormat,);

            target?.EndDraw();
        }

        /// <summary>
        /// Update the world.
        /// </summary>
        private void UpdateWorld()
        {
            var phase = 0;
            var center = 100; // 200; // 128;
            var width = 55; // 127;

            if (cycleCount > 0xff)
            {
                cycleCount = 0;
            }

            cycleCount++;

            color = new Color(
                (int)Floor((Sin((frequency * cycleCount) + 0 + phase) * width) + center),
                (int)Floor((Sin((frequency * cycleCount) + 2 + phase) * width) + center),
                (int)Floor((Sin((frequency * cycleCount) + 4 + phase) * width) + center),
                0xff);

            bands = new float[bandCount];
            for (var i = 0; i < bandCount; i++)
            {
                bands[i] = random.Next(0, 100);
            }
        }

        /// <summary>
        /// The initialize target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void InitializeTarget(int width, int height)
        {
            if (target is null) CreateDxTarget(width, height);
            target.Resize(new Size2(width, height));
            CreateBandBrush();
        }

        /// <summary>
        /// Create the dx target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void CreateDxTarget(int width, int height)
        {
            var targetProperties = new RenderTargetProperties(
                RenderTargetType.Default,
                new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                0, 0,
                RenderTargetUsage.None,
                FeatureLevel.Level_10);

            var windowProperties = new HwndRenderTargetProperties
            {
                Hwnd = Handle,
                PixelSize = new Size2(width, height),
                PresentOptions = PresentOptions.None
            };

            using var factory = new SharpDX.Direct2D1.Factory();
            target = new WindowRenderTarget(factory, targetProperties, windowProperties);
        }

        /// <summary>
        /// Create the band brush.
        /// </summary>
        private void CreateBandBrush()
        {
            var properties = new LinearGradientBrushProperties()
            {
                StartPoint = new Vector2(0, target.Size.Height),
                EndPoint = new Vector2(0, 0)
            };

            using var points = new GradientStopCollection(target, new GradientStop[] {
                new GradientStop() {Color=Color.Green, Position=0F},
                new GradientStop() {Color=Color.Yellow, Position=0.8F},
                new GradientStop() {Color=Color.Red, Position=1F}
            }, ExtendMode.Clamp);
            bandBrush = new LinearGradientBrush(target, properties, points);
        }
    }
}
