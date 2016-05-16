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
using System.Text;
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

            //Shape triangle = new Triangle(new Point2D(10, 10), new Point2D(50, 50), new Point2D(10, 100));
            //GraphicItem triangleItem = new GraphicItem(triangle, styles[0]);
            //vectorMap.Add(triangleItem);

            //Shape circle = new Circle(new Point2D(200, 200), 100);
            //GraphicItem circleItem = new GraphicItem(circle, styles[1]);
            //vectorMap.Add(circleItem);

            //Rectangle2D rectangle1 = new Rectangle2D(new Point2D(100, 100), new Size2D(100, 100));
            //GraphicItem rectangle1Item = new GraphicItem(rectangle1, styles[2]);
            //vectorMap.Add(rectangle1Item);

            //double cent = 1d;

            //Shape rectangle2 = Experimental.RotatedRectangle(rectangle1, rectangle1.Center() * cent, 20d.ToRadians());
            //GraphicItem rectangle2Item = new GraphicItem(rectangle2, styles[9]);
            //vectorMap.Add(rectangle2Item);

            //Shape rectangle3 = Experimental.RotatedRectangle(rectangle1, rectangle1.Center() * cent, 45d.ToRadians());
            //GraphicItem rectangle3Item = new GraphicItem(rectangle3, styles[9]);
            //vectorMap.Add(rectangle3Item);

            //Shape rectangle4 = Experimental.RotatedRectangle(rectangle1, rectangle1.Center() * cent, 60d.ToRadians());
            //GraphicItem rectangle4Item = new GraphicItem(rectangle4, styles[9]);
            //vectorMap.Add(rectangle4Item);

            //Shape rectangle5 = Experimental.RotatedRectangle(rectangle1, rectangle1.Center() * cent, 90d.ToRadians());
            //GraphicItem rectangle5Item = new GraphicItem(rectangle5, styles[9]);
            //vectorMap.Add(rectangle5Item);

            //Shape rectangle6 = Experimental.RotatedRectangleBounds(rectangle1, rectangle1.Center() * cent, 20d.ToRadians());
            //GraphicItem rectangle6Item = new GraphicItem(rectangle6, styles[9]);
            //vectorMap.Add(rectangle6Item);

            //Shape rectangle7 = Experimental.RotatedRectangleBounds(rectangle1, rectangle1.Center() * cent, 45d.ToRadians());
            //GraphicItem rectangle7Item = new GraphicItem(rectangle7, styles[9]);
            //vectorMap.Add(rectangle7Item);

            //Shape rectangle8 = Experimental.RotatedRectangleBounds(rectangle1, rectangle1.Center() * cent, 60d.ToRadians());
            //GraphicItem rectangle8Item = new GraphicItem(rectangle8, styles[9]);
            //vectorMap.Add(rectangle8Item);

            //Shape polygon = new Polygon(new List<Point2D>() { new Point2D(20, 100), new Point2D(300, 60), new Point2D(40, 30) });
            //GraphicItem polygonItem = new GraphicItem(polygon, styles[3]);
            //vectorMap.Add(polygonItem);

            //Shape polyline = new Polyline(new List<Point2D>() { new Point2D(10, 40), new Point2D(80, 30), new Point2D(100, 60) });
            //GraphicItem polylineItem = new GraphicItem(polyline, styles[4]);
            //vectorMap.Add(polylineItem);

            //Shape line = new LineSegment(new Point2D(160, 250), new Point2D(130, 145));
            //GraphicItem lineItem = new GraphicItem(line, styles[5]);
            //vectorMap.Add(lineItem);

            //Shape ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45);
            //GraphicItem ellipseItem = new GraphicItem(ellipse, styles[6]);
            //vectorMap.Add(ellipseItem);

            //QuadraticBezier quadBezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            //GraphicItem quadBezierItem = new GraphicItem(quadBezier, styles[7]);
            //vectorMap.Add(quadBezierItem);
            ////StringBuilder quadBezierLengths = new StringBuilder();
            ////quadBezierLengths.AppendLine("Bezier arc length by segments: \t" + quadBezier.QuadraticBezierArcLengthBySegments());
            ////quadBezierLengths.AppendLine("Bezier arc length by integral: \t" + quadBezier.QuadraticBezierArcLengthByIntegral());
            ////quadBezierLengths.AppendLine("Bezier arc length by Gauss-Legendre: \t" + quadBezier.QuadraticBezierApproxArcLength());
            ////MessageBox.Show(quadBezierLengths.ToString());

            //CubicBezier cubicBezier = new CubicBezier(new Point2D(40, 200), new Point2D(50, 300), new Point2D(90, 200), new Point2D(80, 300));
            //GraphicItem cubicBezierItem = new GraphicItem(cubicBezier, styles[8]);
            //vectorMap.Add(cubicBezierItem);
            ////StringBuilder cubicBezierLengths = new StringBuilder();
            ////cubicBezierLengths.AppendLine("Bezier arc length: \t" + cubicBezier.CubicBezierLength(100));
            ////MessageBox.Show(cubicBezierLengths.ToString());

            //Shape set = new PolygonSet(
            //    new List<Polygon>(
            //        new List<Polygon>() {
            //            new Polygon( // Boundary
            //                new List<Point2D>() {
            //                    new Point2D(10, 10),
            //                    new Point2D(300, 10),
            //                    new Point2D(300, 300),
            //                    new Point2D(10, 300),
            //                    // Cut out
            //                    new Point2D(10, 200),
            //                    new Point2D(200, 80),
            //                    new Point2D(10, 150),
            //                }
            //            ),
            //            new Polygon( // First inner triangle
            //                new List<Point2D>() {
            //                    new Point2D(20, 100),
            //                    new Point2D(175, 60),
            //                    new Point2D(40, 30),
            //                }
            //            ),
            //            new Polygon( // Second inner triangle
            //                new List<Point2D>() {
            //                    new Point2D(250, 150),
            //                    new Point2D(150, 150),
            //                    new Point2D(250, 200),
            //                }
            //            ),
            //        }
            //    )
            //);
            //GraphicItem setItem = new GraphicItem(set, styles[8]);
            //vectorMap.Add(setItem);

            //Shape innerPolygon = new Polygon( // First inner triangle
            //                new List<Point2D>() {
            //                    new Point2D(20, 100),
            //                    new Point2D(175, 60),
            //                    new Point2D(40, 30),
            //                }
            //            ).Offset(10);
            //GraphicItem innerPolygonItem = new GraphicItem(innerPolygon, styles[9]);
            //vectorMap.Add(innerPolygonItem);

            //Polyline pathPolyline = Experimental.ShortestPath(new Point2D(20, 20), new Point2D(200, 200), (PolygonSet)set);
            //Shape polylineSet = new PolylineSet(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline.Offset(-10) });
            //Polyline pathPolyline2 = pathPolyline.Offset(-10);
            //pathPolyline2.Reverse();
            //Shape polygonLine = new Polygon(new Polygon(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }));
            //GraphicItem polygonLineItem = new GraphicItem(polygonLine, styles[9]);
            //GraphicItem polylineSetItem = new GraphicItem(polylineSet, styles[10]);
            //GraphicItem pathPolylineItem = new GraphicItem(pathPolyline, styles[10]);
            //vectorMap.Add(polygonLineItem);
            //vectorMap.Add(polylineSetItem);
            //vectorMap.Add(pathPolylineItem);

            Arc arc = new Arc(new Point2D(100, 100), 100, 60d.ToRadians(), 380d.ToRadians());
            GraphicItem arcItem = new GraphicItem(arc, styles[8]);
            vectorMap.Add(arcItem);

            Shape chord = new LineSegment(arc.StartPoint, arc.EndPoint);
            GraphicItem chordItem = new GraphicItem(chord, styles[5]);
            vectorMap.Add(chordItem);

            Rectangle2D arcBounds = new Rectangle2D(arc.Bounds);
            GraphicItem arcBoundsItem = new GraphicItem(arcBounds, styles[9]);
            vectorMap.Add(arcBoundsItem);

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
            foreach (GraphicItem shape in vectorMap[CanvasPanel.Bounds.ToRectangle2D()])
            {
                Renderer.Render(shape.Item, e.Graphics, shape.Style);
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
            propertyGrid1.SelectedObject = ((GraphicItem)list.SelectedItem);//?.Item;
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
