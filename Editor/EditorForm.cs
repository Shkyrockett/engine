using Engine;
using Engine.File.Palettes;
using Engine.Geometry;
using Engine.Geometry.Polygons;
using Engine.Imaging;
using Engine.Objects;
using Engine.Tools;
using Engine.Tweening;
using Engine.Winforms;
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
        private VectorMap vectorMap;

        /// <summary>
        /// 
        /// </summary>
        private ToolStack toolStack;

        /// <summary>
        /// 
        /// </summary>
        private int tick = 1;

        /// <summary>
        /// 
        /// </summary>
        private Tweener tweener = new Tweener();

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
                toolStripComboBoxTools.ComboBox.SelectedItem = typeof(SelectTop);

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
                comboBox1.SelectedItem = typeof(SolidBrush);

            //propertyGrid1.SelectedObject = toolStack;

            var val = new Tuple<double, int, Point2D>(0, 3, new Point2D());

            propertyGrid1.SelectedObject = val;
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
            vectorMap.Tweener = tweener;
            toolStack = new ToolStack(vectorMap);
            toolStack?.RegisterMouseLeftButton(new SelectTop());
            toolStack?.RegisterMouseMiddleButton(new Pan());
            toolStack?.RegisterMouseScroll(new Zoom());

            var styles = new List<ShapeStyle>()
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

            //Shape oval = new Oval(new Point2D(200, 200), new Size2D(100, 200));
            //GraphicItem ovalItem = new GraphicItem(oval, styles[6]);
            //vectorMap.Add(ovalItem);

            //Shape polygon = new Polygon(new List<Point2D>() { new Point2D(20, 100), new Point2D(300, 60), new Point2D(40, 30) });
            //GraphicItem polygonItem = new GraphicItem(polygon, styles[3]);
            //vectorMap.Add(polygonItem);

            //Shape polyline = new Polyline(new List<Point2D>() { new Point2D(10, 40), new Point2D(80, 30), new Point2D(100, 60) });
            //GraphicItem polylineItem = new GraphicItem(polyline, styles[4]);
            //vectorMap.Add(polylineItem);

            Shape line = new LineSegment(new Point2D(160, 250), new Point2D(130, 145));
            var lineItem = new GraphicItem(line, styles[5]);
            vectorMap.Add(lineItem);

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
                        ),
                        new Polygon( // First inner triangle
                            new List<Point2D>() {
                                new Point2D(20, 100),
                                new Point2D(175, 60),
                                new Point2D(40, 30),
                            }
                        ),
                        new Polygon( // Second inner triangle
                            new List<Point2D>() {
                                new Point2D(250, 150),
                                new Point2D(150, 150),
                                new Point2D(250, 200),
                            }
                        ),
                    }
                )
            );
            var setItem = new GraphicItem(set, styles[8]);
            vectorMap.Add(setItem);

            Shape innerPolygon = new Polygon( // First inner triangle
                            new List<Point2D>() {
                                new Point2D(20, 100),
                                new Point2D(175, 60),
                                new Point2D(40, 30),
                            }
                        ).Offset(10);
            var innerPolygonItem = new GraphicItem(innerPolygon, styles[9]);
            vectorMap.Add(innerPolygonItem);

            Polyline pathPolyline = (set as PolygonSet).ShortestPath(new Point2D(20, 20), new Point2D(200, 200));
            Shape polylineSet = new PolylineSet(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline.Offset(-10) });
            Polyline pathPolyline2 = pathPolyline.Offset(-10);
            pathPolyline2.Reverse();
            //Shape polygonLine = new Polygon(new Polygon(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }));
            //GraphicItem polygonLineItem = new GraphicItem(polygonLine, styles[9]);
            var polylineSetItem = new GraphicItem(polylineSet, styles[10]);
            var pathPolylineItem = new GraphicItem(pathPolyline, styles[10]);
            //vectorMap.Add(polygonLineItem);
            vectorMap.Add(polylineSetItem);
            vectorMap.Add(pathPolylineItem);

            //Arc arc = new Arc(new Point2D(100, 100), 100, 60d.ToRadians(), 380d.ToRadians());
            //GraphicItem arcItem = new GraphicItem(arc, styles[8]);
            //vectorMap.Add(arcItem);

            //Shape chord = new LineSegment(arc.StartPoint, arc.EndPoint);
            //GraphicItem chordItem = new GraphicItem(chord, styles[5]);
            //vectorMap.Add(chordItem);

            //Rectangle2D arcBounds = new Rectangle2D(arc.Bounds);
            //GraphicItem arcBoundsItem = new GraphicItem(arcBounds, styles[9]);
            //vectorMap.Add(arcBoundsItem);

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

            var ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45d.ToRadians());
            var ellipseItem = new GraphicItem(ellipse, styles[6]);
            vectorMap.Add(ellipseItem);

            //Polygon triangleI = new Triangle(
            //    new Point2D(75, 125),
            //    new Point2D(225, 125),
            //    new Point2D(150, 250));
            //GraphicItem triangleIItem = new GraphicItem(triangleI, styles[0]);
            //vectorMap.Add(triangleIItem);

            //Polygon rectangle = new Rectangle2D(
            //    new Point2D(100, 100),
            //    new Size2D(100, 100)).ToPolygon();
            //GraphicItem rectangleItem = new GraphicItem(rectangle, styles[2]);
            //vectorMap.Add(rectangleItem);

            //Polygon intersection = new Polygon(
            //    Intersections.PolygonPolygon(triangleI.Points, rectangle.Points));
            //GraphicItem intersectionItem = new GraphicItem(intersection, styles[3]);
            //vectorMap.Add(intersectionItem);

            //Ellipse ellipseTween = new Ellipse(
            //    new Point2D(100, 100),
            //    56, 30, 0d);
            //GraphicItem ellipseTweenItem = new GraphicItem(ellipseTween, styles[2]);

            //Rectangle2D rectangleTween = new Rectangle2D(
            //    new Point2D(100, 100),
            //    new Size2D(100, 100));
            //GraphicItem rectangleTweenItem = new GraphicItem(rectangleTween, styles[2]);

            //double duration = 300;
            //double delay = 20;

            ////tweener.Tween(rectangleTween, new { X = 0, Y = 0 }, duration, delay).OnUpdate(UpdateCallback).OnUpdate(() => rectangleTweenItem.Refresh());
            //Tween tt = tweener.Tween(rectangleTween, new { Location = new Point2D(0, 0) }, duration, delay);

            ////tweener.Tween(ellipseTween, new { Center = new Point2D(0, 0) }, duration, delay);
            //tweener.Tween(ellipseTween, dests: new { Angle = -360d.ToRadians() }, duration: duration, delay: delay)
            //    .From(new { Angle = 45d.ToRadians() }).Ease(Ease.BackInOut)
            //    .Rotation(RotationUnit.Radians).OnUpdate(UpdateCallback);
            //tweener.Timer(duration).OnComplete(CompleteCallback);

            //vectorMap.Add(rectangleTweenItem);
            //vectorMap.Add(ellipseTweenItem);

            var parametric = new ParametricDelegateCurve((t) => Interpolaters.Ellipse(new Ellipse(100d,100d,25d,25d,0d), t), Point2D.Empty, Size2D.Empty, 0, 0);
            var parametricItem = new GraphicItem(parametric, styles[3]);
            vectorMap.Add(parametricItem);

            listBox1.DataSource = vectorMap.Items;
            //listBox1.ValueMember = "Name";
        }

        private void UpdateCallback()
        {
            CanvasPanel.Invalidate(true);
        }

        private void CompleteCallback()
        {
            CanvasPanel.Invalidate(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tweener.Update(tick);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = (sender as ListBox)?.SelectedItem as GraphicItem;
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
        private void toolStripComboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            var item = box.SelectedItem as Type;
            object constructor = Activator.CreateInstance(item);
            toolStack?.RegisterMouseLeftButton(constructor as Tool);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            List<MethodInfo> constructors = EngineReflection.ListStaticFactoryConstructors((Type)box.SelectedItem);
            toolStripComboBoxFactories.ComboBox.DataSource = constructors;
            toolStripComboBoxFactories.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFactories.ComboBox.Items.Count > 0)
                toolStripComboBoxFactories.ComboBox.SelectedItem = toolStripComboBoxFactories.ComboBox.Items[0];
            else
                toolStripComboBoxFactories.ComboBox.Text = string.Empty;
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
            foreach (GraphicItem item in vectorMap[CanvasPanel.Bounds.ToRectangle2D()])
            {
                if (vectorMap?.SelectedItems != null && vectorMap.SelectedItems.Contains(item))
                    Renderer.Render(item.Item, e.Graphics, item, new ShapeStyle(Brushes.Aquamarine, Brushes.AliceBlue));
                else
                    Renderer.Render(item.Item, e.Graphics, item);
            }

            if (vectorMap?.RubberbandItems != null)
            {
                foreach (GraphicItem item in vectorMap?.RubberbandItems)
                    Renderer.Render(item.Item, e.Graphics, item, new ShapeStyle(Brushes.Red, Brushes.Red));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            toolStack.MouseDown((Engine.Tools.MouseButtons)e?.Button, e.Clicks);
            //propertyGrid1.Refresh();
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            toolStack.MouseUp((Engine.Tools.MouseButtons)e?.Button, e.Clicks);
            //propertyGrid1.Refresh();
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            toolStack.MouseMove(new Point2D(e.X, e.Y));
            //propertyGrid1.Refresh();
            //CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.VerticalScroll, e.Delta);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseWheelTilt(object sender, MouseEventArgs e)
        {
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.HorizontalScroll, e.Delta);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyDown(object sender, KeyEventArgs e)
        {
            //toolStack.KeyDown((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyUp(object sender, KeyEventArgs e)
        {
            //toolStack.KeyUp((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Resize(object sender, EventArgs e)
        {
            var panel = sender as CanvasPanel;
            vectorMap.VisibleBounds = new Rectangle2D(panel.Bounds.X, panel.Bounds.Y, panel.Bounds.Width, panel.Bounds.Height);
        }
    }
}
