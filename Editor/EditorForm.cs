using Engine;
using Engine.File.Palettes;
using Engine.Geometry;
using Engine.Imaging;
using Engine.Objects;
using Engine.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorForm
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        VectorMap vectorMap;

        /// <summary>
        /// 
        /// </summary>
        ToolStack toolStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForm"/> class.
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();

            SetDoubleBuffered(CanvasPanel);

            List<Type> tools = EngineReflection.ListTools();
            toolStripComboBoxTools.ComboBox.DataSource = tools;
            toolStripComboBoxTools.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxTools.ComboBox.Items.Count > 0)
                toolStripComboBoxTools.ComboBox.SelectedItem = toolStripComboBoxTools.ComboBox.Items[0];

            List<Type> fileTypes = EngineReflection.ListFileObjects();
            toolStripComboBoxFiles.ComboBox.DataSource = fileTypes;
            toolStripComboBoxFiles.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFiles.ComboBox.Items.Count > 0)
                toolStripComboBoxFiles.ComboBox.SelectedItem = toolStripComboBoxFiles.ComboBox.Items[0];

            List<Type> graphicsTypes = EngineReflection.ListGraphicsObjects();
            toolStripComboBoxObjects.ComboBox.DataSource = graphicsTypes;
            toolStripComboBoxObjects.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxObjects.ComboBox.Items.Count > 0)
                toolStripComboBoxObjects.ComboBox.SelectedItem = toolStripComboBoxObjects.ComboBox.Items[0];

            List<Type> brushTypes = EngineReflection.ListBrushes();
            comboBox1.DataSource = brushTypes;
            comboBox1.ValueMember = "Name";
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            paletteToolStripItem1.PaletteControl.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });

            vectorMap = new VectorMap();



            List<ShapeStyle> styles = new List<ShapeStyle>()
            {
                new ShapeStyle(new Pen(Brushes.Red), new Pen(Brushes.Plum)),
                new ShapeStyle(new Pen(Brushes.DarkGreen), new Pen(Brushes.ForestGreen)),
                new ShapeStyle(new Pen(Brushes.BlueViolet), new Pen(Brushes.AliceBlue)),
                new ShapeStyle(new Pen(Brushes.Bisque), new Pen(Brushes.Beige)),
                new ShapeStyle(new Pen(Brushes.Azure), new Pen(Brushes.BlanchedAlmond)),
                new ShapeStyle(new Pen(Brushes.DarkCyan), new Pen(Brushes.Cyan)),
                new ShapeStyle(new Pen(Brushes.Maroon), new Pen(Brushes.MediumPurple)),
                new ShapeStyle(new Pen(Brushes.DarkGoldenrod), new Pen(Brushes.Honeydew)),
                new ShapeStyle(new Pen(Brushes.AntiqueWhite), new Pen(Brushes.CadetBlue)),
                new ShapeStyle(new Pen(Brushes.Azure), new Pen(Brushes.Transparent)),
                new ShapeStyle(new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard,Color.Pink,Color.Transparent)), new Pen(Brushes.Transparent)),
            };

            //Shape triangle = new Triangle(new Point2D(10, 10), new Point2D(50, 50), new Point2D(10, 100))
            //{ Style = styles[0] };
            //vectorMap.Add(triangle);

            //Shape circle = new Circle(new Point2D(200, 200), 100)
            //{ Style = styles[1] };
            //vectorMap.Add(circle);

            //Rectangle2D rect1 = new Rectangle2D(new Point2D(100, 100), new Size2D(100, 100))
            //{ Style = styles[2] };
            //vectorMap.Add(rect1);

            //double cent = 1d;

            //Shape rect2 = Experimental.RotatedRectangle(rect1, rect1.Center() * cent, 20d.ToRadians());
            //{ rect2.Style = styles[9]; };
            //vectorMap.Add(rect2);

            //Shape rect3 = Experimental.RotatedRectangle(rect1, rect1.Center() * cent, 45d.ToRadians());
            //{ rect3.Style = styles[9]; };
            //vectorMap.Add(rect3);

            //Shape rect4 = Experimental.RotatedRectangle(rect1, rect1.Center() * cent, 60d.ToRadians());
            //{ rect4.Style = styles[9]; };
            //vectorMap.Add(rect4);

            //Shape rect5 = Experimental.RotatedRectangle(rect1, rect1.Center() * cent, 90d.ToRadians());
            //{ rect5.Style = styles[9]; };
            //vectorMap.Add(rect5);

            //Shape rect6 = Experimental.RotatedRectangleBounds(rect1, rect1.Center() * cent, 20d.ToRadians());
            //{ rect6.Style = styles[9]; };
            //vectorMap.Add(rect6);

            //Shape rect7 = Experimental.RotatedRectangleBounds(rect1, rect1.Center() * cent, 45d.ToRadians());
            //{ rect7.Style = styles[9]; };
            //vectorMap.Add(rect7);

            //Shape rect8 = Experimental.RotatedRectangleBounds(rect1, rect1.Center() * cent, 60d.ToRadians());
            //{ rect8.Style = styles[9]; };
            //vectorMap.Add(rect8);

            //Shape polygon = new Polygon(new List<Point2D>() { new Point2D(20, 100), new Point2D(300, 60), new Point2D(40, 30) })
            //{ Style = styles[3] };
            //vectorMap.Add(polygon);

            //Shape polyline = new Polyline(new List<Point2D>() { new Point2D(10, 40), new Point2D(80, 30), new Point2D(100, 60) })
            //{ Style = styles[4] };
            //vectorMap.Add(polyline);

            //Shape line = new LineSegment(new Point2D(160, 250), new Point2D(130, 145))
            //{ Style = styles[5] };
            //vectorMap.Add(line);

            //Shape ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45)
            //{ Style = styles[6] };
            //vectorMap.Add(ellipse);

            //QuadraticBezier quadBezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150))
            //{ Style = styles[7] };
            //vectorMap.Add(quadBezier);
            //StringBuilder quadBezierLengths = new StringBuilder();
            ////quadBezierLengths.AppendLine("Bezier arc length by segments: \t" + quadBezier.ArcLengthBySegments());
            ////quadBezierLengths.AppendLine("Bezier arc length by integral: \t" + quadBezier.ArcLengthByIntegral());
            ////quadBezierLengths.AppendLine("Bezier arc length by Gauss-Legendre: \t" + quadBezier.ApproxArcLength());
            ////MessageBox.Show(quadBezierLengths.ToString());

            //CubicBezier cubicBezier = new CubicBezier(new Point2D(40, 200), new Point2D(50, 300), new Point2D(90, 200), new Point2D(80, 300))
            //{ Style = styles[8] };
            //vectorMap.Add(cubicBezier);
            ////StringBuilder cubicBezierLengths = new StringBuilder();
            ////cubicBezierLengths.AppendLine("Bezier arc length: \t" + cubicBezier.BezierArcLength());
            ////MessageBox.Show(cubicBezierLengths.ToString());

            Shape set = new PolygonSet(
                new List<Polygon>(
                    new List<Polygon>() {
                        new Polygon( // Boundary
                            new List<Point2D>() {
                                new Point2D(10, 10),
                                new Point2D(300, 10),
                                new Point2D(300, 300),
                                new Point2D(10, 300),
                                // Cut out
                                new Point2D(10, 200),
                                new Point2D(200, 80),
                                new Point2D(10, 150),
                            }
                        ){ Style = styles[6] },
                        new Polygon( // First inner triangle
                            new List<Point2D>() {
                                new Point2D(20, 100),
                                new Point2D(175, 60),
                                new Point2D(40, 30),
                            }
                        ){ Style = styles[6] },
                        new Polygon( // Second inner triangle
                            new List<Point2D>() {
                                new Point2D(250, 150),
                                new Point2D(150, 150),
                                new Point2D(250, 200),
                            }
                        ){ Style = styles[6] },
                    }
                )
            )
            { Style = styles[6] };
            vectorMap.Add(set);

            Shape innerPolygon = new Polygon( // First inner triangle
                            new List<Point2D>() {
                                new Point2D(20, 100),
                                new Point2D(175, 60),
                                new Point2D(40, 30),
                            }
                        ).Offset(10);
            innerPolygon.Style = styles[10];
            vectorMap.Add(innerPolygon);

            Polyline pathPolyline = Experimental.ShortestPath(new Point2D(20, 20), new Point2D(200, 200), (PolygonSet)set);
            pathPolyline.Style = styles[9];
            //Shape polylineSet = new PolylineSet(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline.Offset(-10) })
            //{ Style = styles[10] };
            Polyline pathPolyline2 = pathPolyline.Offset(-10);
            pathPolyline2.Reverse();
            Shape polygonLine = new Polygon(new Polygon(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }))
            { Style = styles[10] };
            vectorMap.Add(polygonLine);
            //vectorMap.Add(polylineSet);
            vectorMap.Add(pathPolyline);

            listBox1.DataSource = vectorMap.Shapes;
            //listBox1.ValueMember = "Name";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Only need to draw the shapes that are on screen.
            foreach (Shape shape in vectorMap[CanvasPanel.Bounds.ToRectangle2D()])
            {
                Renderer.Render(shape, e.Graphics, shape.Style);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            propertyGrid1.SelectedObject = list.SelectedItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            CanvasPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox box = (ToolStripComboBox)sender;
            List<MethodInfo> constructors = EngineReflection.ListStaticFactoryConstructors((Type)box.SelectedItem);
            toolStripComboBoxFactories.ComboBox.DataSource = constructors;
            toolStripComboBoxFactories.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFactories.ComboBox.Items.Count > 0)
            {
                toolStripComboBoxFactories.ComboBox.SelectedItem = toolStripComboBoxFactories.ComboBox.Items[0];
            }
            else
            {
                toolStripComboBoxFactories.ComboBox.Text = string.Empty;
            }
        }

        private void CanvasPanel_Scroll(object sender, ScrollEventArgs e)
        {
            //e.NewValue;
            //e.OldValue;
            //e.ScrollOrientation;
            //e.Type;
            toolStack.MouseScroll((Engine.Tools.ScrollOrientation)e.ScrollOrientation, e.NewValue - e.OldValue);
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //e.Button;
            //e.Clicks;
            //e.Delta;
            //e.Location;
            //e.X;
            //e.Y;
            toolStack.MouseDown((Engine.Tools.MouseButtons)e.Button, e.Clicks);
        }

        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            //e.Button;
            //e.Clicks;
            //e.Delta;
            //e.Location;
            //e.X;
            //e.Y;
            toolStack.MouseUp((Engine.Tools.MouseButtons)e.Button, e.Clicks);
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //e.Button;
            //e.Clicks;
            //e.Delta;
            //e.Location;
            //e.X;
            //e.Y;
            toolStack.MouseMove(new Point2D(e.X, e.Y));
        }

        /// <summary>
        /// Set an arbitrary control to double-buffer.
        /// </summary>
        /// <param name="control">The control to set as double buffered.</param>
        /// <remarks>
        /// Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
        /// </remarks>
        private static void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession) return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(control, true, null);
        }
    }
}
