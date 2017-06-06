// <copyright file="Direct2DCanvas.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Windows.Forms;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using static System.Math;
using System.Drawing;
using SharpDX.DirectWrite;

namespace Engine.Winforms.Direct2D
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Direct2DCanvas
        : UserControl
    {
        /// <summary>
        /// Window Render target.
        /// </summary>
        private WindowRenderTarget target;

        /// <summary>
        /// 
        /// </summary>
        SharpDX.Color color = SharpDX.Color.CornflowerBlue;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="m"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Direct2DCanvas_Load(object sender, EventArgs e)
        {
            // Get the changed object.
            var panel = sender as Direct2DCanvas;

            // Create DirectX target
            InitialiseTarget(panel.Width, panel.Height);

            // Initialize the world.
            UpdateWorld();

            // Set the number of ticks to refresh the world.
            timer.Interval = 50;

            // Start the world update timer if it isn't in design mode.
            if (!DesignMode) timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Direct2DCanvas_Resize(object sender, EventArgs e)
        {
            // Get the changed object.
            var panel = sender as Direct2DCanvas;

            // Update the resolution.
            InitialiseTarget(panel.Width, panel.Height);

            //Invalidate and redraw the form http://stackoverflow.com/a/9827091
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Process all changes to the world.
            UpdateWorld();

            // Now that the world has been updated it needs to be redrawn.
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render()
        {
            target?.BeginDraw();

            target?.Clear(color);

            //int bandCount = bands.Length;

            //float bandWidth = (target.Size.Width + gap) / bandCount;

            //int i = 0;
            //foreach (var bandPercentage in bands)
            //{
            //    target.FillRectangle(new RectangleF()
            //    {
            //        Top = target.Size.Height * bandPercentage / 100,
            //        Left = i * bandWidth,
            //        Width = bandWidth - gap,
            //        Bottom = target.Size.Height
            //    }, bandBrush);
            //    i++;
            //}

            //target.DrawText("", TextFormat,)

            target?.EndDraw();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateWorld()
        {
            //int phase = 0;
            //int center = 100; // 200; // 128;
            //int width = 55; // 127;

            //if (cycleCount > 0xff)
            //{
            //    cycleCount = 0;
            //}

            //cycleCount++;

            //color = new Color(
            //    (int)Floor(Sin(frequency * cycleCount + 0 + phase) * width + center),
            //    (int)Floor(Sin(frequency * cycleCount + 2 + phase) * width + center),
            //    (int)Floor(Sin(frequency * cycleCount + 4 + phase) * width + center),
            //    0xff);

            //bands = new float[bandCount];
            //for (int i = 0; i < bandCount; i++)
            //{
            //    bands[i] = random.Next(0, 100);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitialiseTarget(int width, int height)
        {
            if (target == null) CreateDxTarget(width, height);
            target.Resize(new Size2(width, height));
            CreateBandBrush();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateDxTarget(int width, int height)
        {
            var targetProperties = new RenderTargetProperties(
                RenderTargetType.Default,
                new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                0, 0,
                RenderTargetUsage.None,
                FeatureLevel.Level_10);

            var windowProperties = new HwndRenderTargetProperties()
            {
                Hwnd = Handle,
                PixelSize = new Size2(width, height),
                PresentOptions = PresentOptions.None
            };

            using (var factory = new SharpDX.Direct2D1.Factory())
            {
                target = new WindowRenderTarget(factory, targetProperties, windowProperties);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateBandBrush()
        {
            //LinearGradientBrushProperties properties = new LinearGradientBrushProperties()
            //{
            //    StartPoint = new Vector2(0, target.Size.Height),
            //    EndPoint = new Vector2(0, 0)
            //};

            //var points = new GradientStopCollection(target, new GradientStop[] {
            //    new GradientStop() {Color=Color.Green, Position=0F},
            //    new GradientStop() {Color=Color.Yellow, Position=0.8F},
            //    new GradientStop() {Color=Color.Red, Position=1F}
            //}, ExtendMode.Clamp);

            //bandBrush = new LinearGradientBrush(target, properties, points);
        }
    }
}
