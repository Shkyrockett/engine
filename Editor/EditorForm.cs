// <copyright file="EditorForm.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine.File.Palettes;
using Engine.Imaging;
using Engine.Physics;
using Engine.Tools;
using Engine.Tweening;
using Engine.Winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Editor
{
    /// <summary>
    ///
    /// </summary>
    public partial class EditorForm
        : Form
    {
        /// <summary>
        /// Map containing all of the vector objects.
        /// </summary>
        private VectorMap vectorMap = new VectorMap();

        /// <summary>
        /// Container for actions to take on input.
        /// </summary>
        private ToolStack toolStack;

        /// <summary>
        /// Tweening interpolater for animation.
        /// </summary>
        private Tweener tweener = new Tweener();

        /// <summary>
        /// 
        /// </summary>
        private string vectorFilename = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        XmlSerializer vectorMapSserializer = new XmlSerializer(typeof(VectorMap));

        /// <summary>
        /// Amount to advance the timer every tick
        /// </summary>
        private int tick = 1;

        private GraphicItem boundaryItem = new GraphicItem(Rectangle2D.Empty, new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum)));

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

            var val = (0, 3, new Point2D());

            propertyGrid1.SelectedObject = val;
        }

        /// <summary>
        /// Events to execute when the form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            paletteToolStripItem1.PaletteControl.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });

            vectorMap.Tweener = tweener;
            toolStack = new ToolStack(vectorMap);
            toolStack?.RegisterMouseLeftButton(new SelectTop());
            toolStack?.RegisterMouseMiddleButton(new Pan());
            toolStack?.RegisterMouseScroll(new Zoom());

            var styles = new List<ShapeStyle>
            {
                new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum)),
                new ShapeStyle(Brushes.DarkGreen, new Pen(Brushes.ForestGreen)),
                new ShapeStyle(Brushes.BlueViolet, new Pen(Brushes.AliceBlue)),
                new ShapeStyle(Brushes.Bisque, new Pen(Brushes.Beige)),
                new ShapeStyle(Brushes.Azure, new Pen(Brushes.BlanchedAlmond)),
                new ShapeStyle(Brushes.DarkCyan, new Pen(Brushes.Cyan)),
                new ShapeStyle(Brushes.Maroon, new Pen(Brushes.MediumPurple)),
                new ShapeStyle(Brushes.DarkGoldenrod, new Pen(Brushes.Honeydew)),
                new ShapeStyle(Brushes.AntiqueWhite, new Pen(Brushes.CadetBlue)),
                new ShapeStyle(Brushes.Azure, new Pen(Brushes.Transparent)),
                new ShapeStyle(new HatchBrush(HatchStyle.SmallCheckerBoard,Color.Pink,Color.Transparent), new Pen(Brushes.Transparent))
            };

            vectorMap.VisibleBounds = CanvasPanel.ClientRectangle.ToRectangle2D();
            boundaryItem = new GraphicItem(vectorMap.VisibleBounds, new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum)));
            vectorMap.Add(boundaryItem);

            //var triangleItem = new GraphicItem(Examples.TrianglePointingRight, styles[0])
            //{
            //    Name = "Triangle pointing right."
            //};
            //vectorMap.Add(triangleItem);

            //var circleItem = new GraphicItem(Examples.Circle, styles[1])
            //{
            //    Name = "Circle."
            //};
            //vectorMap.Add(circleItem);

            //var rectangle1Item = new GraphicItem(Examples.Square, styles[2])
            //{
            //    Name = "Square."
            //};
            //vectorMap.Add(rectangle1Item);

            //var ovalVerticalItem = new GraphicItem(Examples.OvalVertical, styles[6])
            //{
            //    Name = "Vertical Oval."
            //};
            //vectorMap.Add(ovalVerticalItem);

            //var paperPlaneItem = new GraphicItem(Examples.PaperPlane, styles[3])
            //{
            //    Name = "Paper Plane Triangle."
            //};
            //vectorMap.Add(paperPlaneItem);

            //var polylineItem = new GraphicItem(Examples.PolyTriangle, styles[4])
            //{
            //    Name = "Polygon Triangle"
            //};
            //vectorMap.Add(polylineItem);

            //var lineItem = new GraphicItem(Examples.Line, styles[5])
            //{
            //    Name = "Single Line segment."
            //};
            //vectorMap.Add(lineItem);

            //var setItem = new GraphicItem(Examples.PolySet, styles[8])
            //{
            //    Name = "Polygon Set"
            //};
            //vectorMap.Add(setItem);

            //var innerPolygonItem = new GraphicItem(Examples.InnerPolygon, styles[9])
            //{
            //    Name = "Inner Polygon Triangle"
            //};
            //vectorMap.Add(innerPolygonItem);

            //var pathPolyline = (Examples.PolySet as PolygonSet).ShortestPath(new Point2D(20, 20), new Point2D(200, 200));
            //var polylineSet = new PolylineSet(new List<Polyline> { pathPolyline.Offset(10), pathPolyline.Offset(-10) });
            //var pathPolyline2 = pathPolyline.Offset(-10);
            //pathPolyline2.Reverse();
            //var polygonLine = new Polygon(new Polygon(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }));
            //var polygonLineItem = new GraphicItem(polygonLine, styles[9])
            //{
            //    Name = "Polygon Line"
            //};
            //var polylineSetItem = new GraphicItem(polylineSet, styles[10])
            //{
            //    Name = "Polyline Set"
            //};
            //var pathPolylineItem = new GraphicItem(pathPolyline, styles[10])
            //{
            //    Name = "Path Polyline"
            //};

            //vectorMap.Add(polygonLineItem);
            //vectorMap.Add(polylineSetItem);
            //vectorMap.Add(pathPolylineItem);

            //Shape ego = new Circle(pathPolyline.Interpolate(.1), 10);
            //var egoItem = new GraphicItem(ego, styles[5])
            //{
            //    Name = "Ego Circle"
            //};
            //vectorMap.Add(egoItem);

            //var text = new Text2D("Test Text.", this.Font, new Point2D(100, 100));
            //var textItem = new GraphicItem(text, styles[1])
            //{
            //    Name = "Text Test"
            //};
            //vectorMap.Add(textItem);

            //var arc = new CircularArc(new Point2D(100, 100), 100, -60d.ToRadians(), -300d.ToRadians());
            //var chord = new LineSegment(arc.StartPoint, Examples.Arc.EndPoint);
            //var arcBounds = new Rectangle2D(arc.Bounds);
            //var arcItem = new GraphicItem(arc, styles[8])
            //{
            //    Name = "Arc Shape."
            //};
            //var chordItem = new GraphicItem(chord, styles[5])
            //{
            //    Name = "Arc Chord."
            //};
            //var arcBoundsItem = new GraphicItem(arcBounds, styles[9])
            //{
            //    Name = "Arc Bounding Box."
            //};
            //vectorMap.Add(arcBoundsItem);
            //vectorMap.Add(arcItem);
            //vectorMap.Add(chordItem);

            //var ellipseItem = new GraphicItem(Examples.Ellipse, styles[6]);
            //var ellipseBoundsItem = new GraphicItem(Examples.Ellipse.Bounds, styles[10]);
            //vectorMap.Add(ellipseBoundsItem);
            //vectorMap.Add(ellipseItem);

            //var ellpticArcItem = new GraphicItem(Examples.EllpticArc, styles[3]);
            //var ellpticArcBoundsItem = new GraphicItem(Examples.EllpticArc.Bounds, styles[10]);
            //vectorMap.Add(ellpticArcBoundsItem);
            //vectorMap.Add(Examples.EllpticArc);

            //var circleB = new Circle(100, 200, 50);
            //var circleBItem = new GraphicItem(circleB, styles[3]);
            //var circleBoundsItem = new GraphicItem(circleB.Bounds, styles[10]);
            //vectorMap.Add(circleBoundsItem);
            //vectorMap.Add(circleBItem);



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

            //var ellipseTween = new Ellipse(
            //    new Point2D(100d, 375d),
            //    56d, 30d, 10d.ToRadians());
            //var ellipseTweenItem = new GraphicItem(ellipseTween, styles[2]);

            //double duration = 300;
            //double delay = 20;

            //tweener.Tween(ellipseTween, new { Center = new Point2D(0, 0) }, duration, delay);

            //var rectangleTween = ellipseTween.Bounds;
            //var rectangleTweenItem = new GraphicItem(rectangleTween, styles[10]);

            //tweener.Tween(ellipseTween, dests: new { Angle = -360d.ToRadians() }, duration: duration, delay: delay)
            //    .From(new { Angle = 45d.ToRadians() }).Ease(Ease.BackInOut)
            //    .Rotation(RotationUnit.Radians)
            //    .OnUpdate(UpdateCallback)
            //    .OnUpdate(() => rectangleTweenItem.Item = ellipseTween.Bounds);
            //tweener.Timer(duration).OnComplete(CompleteCallback);

            //vectorMap.Add(rectangleTweenItem);
            //vectorMap.Add(ellipseTweenItem);

            //var parametricEllipse = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.Ellipse(x, y, w, h, a, t),
            //    (x, y, w, h, a, px, py) => Intersections.EllipsePoint(x, y, w, h, a, px, py),
            //    new Point2D(200d, 100d), new Size2D(25d, 50d), 0, 0);
            //var parametricEllipseItem = new GraphicItem(parametricEllipse, styles[3]);
            //vectorMap.Add(parametricEllipseItem);

            //var EllipseBounds = Boundings.Ellipse(200, 100, 25, 50);
            //var EllipseBoundsItem = new GraphicItem(EllipseBounds, styles[10]);
            //vectorMap.Add(EllipseBoundsItem);

            //double centerX = 200d;
            //double centerY = 200d;
            //double radius1 = 100d;
            //double radius2 = 200d;
            //double angle = 45d.ToRadians();
            //double startAngle = -45d.ToRadians();
            //double sweepAngle = 90d.ToRadians();
            //var parametricEllipticArc = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.EllipticArc(x, y, w, h, a, startAngle, sweepAngle, t),
            //    (x, y, w, h, a, px, py) => Intersections.EllipticSectorPoint(x, y, w, h, a, startAngle, sweepAngle, px, py),
            //    new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            //var parametricEllipticArcItem = new GraphicItem(parametricEllipticArc, styles[3]);
            //vectorMap.Add(parametricEllipticArcItem);

            //var EllpticArcBounds = Boundings.EllpticArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var EllpticArcBoundsItem = new GraphicItem(EllpticArcBounds, styles[10]);
            //vectorMap.Add(EllpticArcBoundsItem);

            //double centerX = 200d;
            //double centerY = 200d;
            //double radius1 = 100d;
            //double radius2 = 200d;

            //double angle = -30d.ToRadians();
            //double startAngle = -60d.ToRadians();
            //double sweepAngle = -90d.ToRadians();

            //var parametricEllipse = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.UnitPolarEllipse(x, y, w, h, a, t),
            //    (x, y, w, h, a, px, py) => Intersections.EllipsePoint(x, y, w, h, a, px, py),
            //    new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            //var parametricEllipseItem = new GraphicItem(parametricEllipse, styles[3]);
            //vectorMap.Add(parametricEllipseItem);

            //var parametricPointTesterEllipticArc = new ParametricPointTester(
            //    (px, py) => Intersections.EllipticArcPoint(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle, px, py),
            //    centerX - (radius1 < radius2 ? radius2 : radius2),
            //    centerY - (radius1 < radius2 ? radius2 : radius2),
            //    centerX + (radius1 < radius2 ? radius2 : radius2),
            //    centerY + (radius1 < radius2 ? radius2 : radius2),
            //    5, 5);
            //var parametricPointTesterEllipticArcItem = new GraphicItem(parametricPointTesterEllipticArc, styles[3]);

            //var parametricPointTesterEllipse = new ParametricPointTester(
            //    (px, py) => Intersections.EllipsePoint(centerX, centerY, radius1, radius2, angle, px, py),
            //    centerX - (radius1 < radius2 ? radius2 : radius2),
            //    centerY - (radius1 < radius2 ? radius2 : radius2),
            //    centerX + (radius1 < radius2 ? radius2 : radius2),
            //    centerY + (radius1 < radius2 ? radius2 : radius2),
            //    5, 5);
            //var parametricPointTesterEllipseItem = new GraphicItem(parametricPointTesterEllipse, styles[3]);

            //var ellipseNodes = new Polygon(Boundings.EllipseExtremes(centerX, centerY, radius1, radius2, angle));
            //var ellipseNodesItem = new GraphicItem(ellipseNodes, styles[10]);

            //var ellipse = new Ellipse(centerX, centerY, radius1, radius2, angle);
            //var ellipseItem = new GraphicItem(ellipse, styles[3]);

            //var ellipticArc = new EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var ellipticArcItem = new GraphicItem(ellipticArc, styles[3]);

            //Rectangle2D ellpticArcBounds = Boundings.EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var ellpticArcBoundsItem = new GraphicItem(ellpticArcBounds, styles[10]);

            //var angleLines = new Polyline(ellipticArc.StartPoint, ellipticArc.Center, ellipticArc.EndPoint);
            //var angleLinesItem = new GraphicItem(angleLines, styles[10]);

            //var circularArc = new CircularArc(centerX, centerY, radius1, startAngle + angle, sweepAngle);
            //var circularArcItem = new GraphicItem(circularArc, styles[3]);

            //Rectangle2D circularArcBounds = Boundings.CircularArc(centerX, centerY, radius1, startAngle + angle, sweepAngle);
            //var circularArcBoundsItem = new GraphicItem(circularArcBounds, styles[10]);

            //var testAngle = ellipticArc.AnglesOfExtremes;
            //testAngle.Sort();

            //var angleVisualizer = new AngleVisualizerTester(centerX, centerY, (radius1 < radius2 ? radius2 : radius1), testAngle, startAngle + angle, sweepAngle);
            //var angleVisualizerItem = new GraphicItem(angleVisualizer, styles[3]);

            //vectorMap.Add(ellpticArcBoundsItem);
            //vectorMap.Add(circularArcBoundsItem);
            //vectorMap.Add(ellipseItem);
            //vectorMap.Add(ellipticArcItem);
            //vectorMap.Add(circularArcItem);
            //vectorMap.Add(ellipseNodesItem);
            //vectorMap.Add(angleLinesItem);
            //vectorMap.Add(angleVisualizerItem);
            //vectorMap.Add(parametricPointTesterItem);

            //double centerX = 100d;
            //double centerY = 200d;
            //double radius1 = 100d;
            //double radius2 = 200d;
            //double angle = -45d.ToRadians();
            //double startAngle = -45d.ToRadians();
            //double sweepAngle = 90d.ToRadians();

            //var EllpticArcBounds = Boundings.EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var EllpticArcBoundsItem = new GraphicItem(EllpticArcBounds, styles[10]);
            //vectorMap.Add(EllpticArcBoundsItem);

            //var ellipticArc = new EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var ellipticArcItem = new GraphicItem(ellipticArc, styles[3]);
            //vectorMap.Add(ellipticArcItem);

            //var parametricEllipticArc = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.EllipticalArc(x, y, w, h, a, startAngle, sweepAngle, t),
            //    (x, y, w, h, a, px, py) => Intersections.EllipticSectorPoint(x, y, w, h, a, startAngle, sweepAngle, px, py),
            //    new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            //var parametricEllipticArcItem = new GraphicItem(parametricEllipticArc, styles[3]);
            //vectorMap.Add(parametricEllipticArcItem);

            //var parametricEllpticArcBounds = Boundings.EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            //var parametricEllpticArcBoundsItem = new GraphicItem(parametricEllpticArcBounds, styles[10]);
            //vectorMap.Add(parametricEllipticArcItem);

            //var parametricCircle = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.Circle(x, y, h, t),
            //    (x, y, w, h, a, px, py) => Intersections.CirclePoint(x, y, h, px, py),
            //    new Point2D(100d, 200d), new Size2D(0d, 50d), 0, 0);
            //var parametricCircleItem = new GraphicItem(parametricCircle, styles[3]);
            //vectorMap.Add(parametricCircleItem);

            //var parametricCircleArc = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.Arc(x, y, h, 0d, 90d.ToRadians(), t),
            //    (x, y, w, h, a, px, py) => Intersections.ArcSectorPoint(x, y, h, 0d, 90d.ToRadians(), px, py),
            //    new Point2D(150d, 150d), new Size2D(0d, 50d), 0, 0);
            //var parametricCircleArcItem = new GraphicItem(parametricCircleArc, styles[3]);
            //vectorMap.Add(parametricCircleArcItem);

            //var CircleArcBounds = Boundings.Arc(100, 200, 50, 0d, 90d.ToRadians());
            //var CircleArcBoundsItem = new GraphicItem(CircleArcBounds, styles[10]);
            //vectorMap.Add(CircleArcBoundsItem);

            var cubicSegment = new LineSegment(new Point2D(20, 70), new Point2D(150, 70));
            var cubicSegmentItem = new GraphicItem(cubicSegment, styles[1])
            {
                Name = "Cubic Bézier Intersecting Line Segment."
            };

            var cubicSegmentNodeItem = new GraphicItem(new NodeRevealer(cubicSegment.Points, 5d), styles[6]);

            var cubicBezier = new BezierSegment(new Point2D(50, 50), new Point2D(75, 100), new Point2D(125, 100), new Point2D(150, 50));
            var cubicBezierItem = new GraphicItem(cubicBezier, styles[1])
            {
                Name = "Cubic Bézier."
            };

            var cubicBezierNodeItem = new GraphicItem(new NodeRevealer(cubicBezier.Points, 5d), styles[6]);

            var cubicBezierBoundsItem = new GraphicItem(cubicBezier.Bounds, styles[10])
            {
                Name = "Cubic Bézier Bounds."
            };

            var cubicIntersections = Intersections.Intersection(cubicBezier, cubicSegment);
            var cubicIntersectionNodesItem = new GraphicItem(new NodeRevealer(cubicIntersections.Points, 5d), styles[6]);

            vectorMap.Add(cubicBezierBoundsItem);
            vectorMap.Add(cubicBezierItem);
            vectorMap.Add(cubicSegmentItem);
            vectorMap.Add(cubicBezierNodeItem);
            vectorMap.Add(cubicSegmentNodeItem);
            vectorMap.Add(cubicIntersectionNodesItem);

            var ellipseOne = new Ellipse(300, 300, 100, 50, 0);
            var ellipseOneItem = new GraphicItem(ellipseOne, styles[1])
            {
                Name = "Ellipse Intersecting One."
            };

            var ellipseTwo = new Ellipse(300, 350, 100, 50, 0);
            var ellipseTwoItem = new GraphicItem(ellipseTwo, styles[1])
            {
                Name = "Ellipse Intersecting One."
            };

            var ellipseIntersections = Intersections.Intersection(ellipseOne, ellipseTwo);
            var ellipseIntersectionNodesItem = new GraphicItem(new NodeRevealer(ellipseIntersections.Points, 5d), styles[6]);

            vectorMap.Add(ellipseOneItem);
            vectorMap.Add(ellipseTwoItem);
            vectorMap.Add(ellipseIntersectionNodesItem);

            //var segment0 = new LineSegment(new Point2D(20, 150), new Point2D(180, 200));
            //var segment0Item = new GraphicItem(segment0, styles[1]);
            //var segment0NodeItem = new GraphicItem(new NodeRevealer(segment0.Points, 5d), styles[6]);

            //var segment1 = new LineSegment(new Point2D(140, 150), new Point2D(150, 250));
            //var segment1Item = new GraphicItem(segment1, styles[1]);
            //var segment1NodeItem = new GraphicItem(new NodeRevealer(segment1.Points, 5d), styles[6]);

            //var segmentIntersection = IntersectionsPreview.Intersection(segment0, segment1);
            //var segmentIntersectionNodes = new NodeRevealer(segmentIntersection, 5d);
            //var segmentIntersectionNodesItem = new GraphicItem(segmentIntersectionNodes, styles[6]);

            //vectorMap.Add(segment0Item);
            //vectorMap.Add(segment1Item);
            //vectorMap.Add(segment0NodeItem);
            //vectorMap.Add(segment1NodeItem);
            //vectorMap.Add(segmentIntersectionNodesItem);

            //var point = new ScreenPoint(150, 130);
            //var pointItem = new GraphicItem(point, styles[1])
            //{
            //    Name = "Point Item."
            //};
            //var pointNodeItem = new GraphicItem(new NodeRevealer(point.Point, 5d), styles[6]);
            //var dist1 = IntersectionsPreview.DistanceLinePoint(segment1, point.Point);
            //var dist2 = IntersectionsPreview.DistanceLineSegmentPoint(segment1, point.Point);
            //var dist3 = IntersectionsPreview.RangeDistanceLineSegmentPoint(segment1, point.Point);

            //var text = new Text2D($"{point.ToString()},{Environment.NewLine}Distance to Line: {dist1.ToString()},{Environment.NewLine}Distance to Segment: {dist2.ToString()},{Environment.NewLine}Distance in Range: {dist3?.ToString()}", this.Font, point.Point);
            //var textItem = new GraphicItem(text, styles[6])
            //{
            //    Name = "Point Text."
            //};

            //vectorMap.Add(pointItem);
            //vectorMap.Add(pointNodeItem);
            //vectorMap.Add(textItem);

            var quadraticSegment = new LineSegment(new Point2D(220, 70), new Point2D(350, 70));
            var quadraticSegmentItem = new GraphicItem(quadraticSegment, styles[1])
            {
                Name = "Quadratic Bezier Intersecting Line Segment."
            };

            var quadraticSegmentNodeItem = new GraphicItem(new NodeRevealer(quadraticSegment.Points, 5d), styles[6]);

            var quadraticBezier = new BezierSegment(new Point2D(250, 50), new Point2D(300, 100), new Point2D(350, 50));
            var quadraticBezierItem = new GraphicItem(quadraticBezier, styles[1])
            {
                Name = "Quadratic Bezier."
            };

            var quadraticBezierBoundsItem = new GraphicItem(quadraticBezier.Bounds, styles[10])
            {
                Name = "Quadratic Bezier Bounds."
            };

            var quadraticBezierNodeItem = new GraphicItem(new NodeRevealer(quadraticBezier.Points, 5d), styles[6]);

            var quadraticIntersections = Intersections.Intersection(quadraticBezier, quadraticSegment);
            var quadraticIntersectionNodes = new NodeRevealer(quadraticIntersections.Points, 5d);
            var quadraticIntersectionNodesItem = new GraphicItem(quadraticIntersectionNodes, styles[6]);

            vectorMap.Add(quadraticBezierBoundsItem);
            vectorMap.Add(quadraticBezierItem);
            vectorMap.Add(quadraticSegmentItem);
            vectorMap.Add(quadraticBezierNodeItem);
            vectorMap.Add(quadraticSegmentNodeItem);
            vectorMap.Add(quadraticIntersectionNodesItem);



            //var quadraticBezier = new QuadraticBezier(new Point2D(50, 100), new Point2D(75, 50), new Point2D(150, 100));
            //var quadraticBezierItem = new GraphicItem(quadraticBezier, styles[1]);

            //var quadraticBezierNodes = new NodeRevealer(new List<Point2D> { quadraticBezier.A, quadraticBezier.B, quadraticBezier.C }, 5d);
            //var quadraticBezierNodeItem = new GraphicItem(quadraticBezierNodes, styles[6]);

            //var cubicBezier2 = new CubicBezier(new Point2D(50, 101), new Point2D(75, 50), new Point2D(125, 100), new Point2D(150, 25));
            //var cubicBezier2Item = new GraphicItem(cubicBezier2, styles[1]);

            //var parametricPointTesterSegment = new ParametricPointTester(
            //    (px, py) => Intersections.LineSegmentPoint(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, px, py),
            //    segment.Bounds.X - 5, segment.Bounds.Y - 5, segment.Bounds.Right + 10, segment.Bounds.Bottom + 10, 5, 5);
            //var parametricPointTesterSegmentItem = new GraphicItem(parametricPointTesterSegment, styles[3]);

            //var ellipseLineIntersections = Intersections.EllipseLineSegment(ellipseTween.Center.X, ellipseTween.Center.Y, ellipseTween.RX, ellipseTween.RY, ellipseTween.Angle, segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
            //var ellipseLineIntersectionNodes = new NodeRevealer(ellipseLineIntersections, 5d);
            //var ellipseLineIntersectionNodesItem = new GraphicItem(ellipseLineIntersectionNodes, styles[6]);

            //var intersection3s = Intersections.intersectBezier2Bezier3(
            //    cubicBezier.A, cubicBezier.B, cubicBezier.C, cubicBezier.D,
            //    quadraticBezier.A, quadraticBezier.B, quadraticBezier.C);
            //var intersectio3nNodes = new NodeRevealer(intersection3s, 5d);
            //var intersection3NodesItem = new GraphicItem(intersectio3nNodes, styles[6]);

            //var intersections2 = Intersections.intersectBezier3Bezier3(
            //    cubicBezier.A, cubicBezier.B, cubicBezier.C, cubicBezier.D,
            //    cubicBezier2.A, cubicBezier2.B, cubicBezier2.C, cubicBezier2.D);
            //var intersection2Nodes = new NodeRevealer(intersections2, 5d);
            //var intersection2NodesItem = new GraphicItem(intersection2Nodes, styles[6]);

            //vectorMap.Add(quadraticBezierItem);
            //vectorMap.Add(cubicBezier2Item);
            //vectorMap.Add(quadraticBezierNodeItem);
            //vectorMap.Add(ellipseLineIntersectionNodesItem);
            //vectorMap.Add(intersection3NodesItem);
            //vectorMap.Add(intersection2NodesItem);
            //vectorMap.Add(parametricPointTesterSegmentItem);

            //var figure = new GeometryPath(new Point2D(150d, 200d));
            //figure.AddLineSegment(new Point2D(200, 200))
            //    .AddArc(50d, 50d, 0d, false, false, new Point2D(250d, 250d))
            //    .AddLineSegment(new Point2D(250, 300))
            //    .AddArc(50d, 50d, 0d, false, true, new Point2D(200d, 350d))
            //    .AddLineSegment(new Point2D(150, 350))
            //    .AddArc(50d, 50d, 0d, true, false, new Point2D(100d, 300d))
            //    .AddLineSegment(new Point2D(100, 250))
            //    .AddArc(50d, 50d, 0d, true, true, new Point2D(150d, 200d));
            //var figureItem = new GraphicItem(figure, styles[1]);
            //var figureBounds = figure.Bounds;
            //var figureBoundsItem = new GraphicItem(figureBounds, styles[10]);

            //var parametricPointTesterFigure = new ParametricPointTester(
            //    (px, py) => Containings.GeometryPathPoint(figure, new Point2D(px, py)),
            //    figureBounds.X, figureBounds.Y, figureBounds.Right + 5, figureBounds.Bottom + 5, 5, 5);
            //var parametricPointTesterFigureItem = new GraphicItem(parametricPointTesterFigure, styles[3]);

            //var parametricPointTesterRectangle = new ParametricPointTester(
            //    (px, py) => Containings.RectanglePoint(figureBounds.Left, figureBounds.Top, figureBounds.Right, figureBounds.Bottom, px, py),
            //    figureBounds.X - 200, figureBounds.Y - 200, figureBounds.Right + 205, figureBounds.Bottom + 205, 5, 5);
            //var parametricPointTesterRectangleItem = new GraphicItem(parametricPointTesterRectangle, styles[3]);

            //vectorMap.Add(figureBoundsItem);
            //vectorMap.Add(figureItem);
            //vectorMap.Add(parametricPointTesterFigureItem);
            //vectorMap.Add(parametricPointTesterRectangleItem);

            //var polyOne = Examples.Triangle1;
            //var polyOneItem = new GraphicItem(polyOne, styles[3]);
            //var polyTwo = Examples.Triangle2;
            //var polyTwoItem = new GraphicItem(polyTwo, styles[3]);

            //var clips = new PolygonSet(Clip.Diff2(polyOne.Points, polyTwo.Points));
            //var clipsItem = new GraphicItem(clips, styles[6]);

            //vectorMap.Add(polyOneItem);
            //vectorMap.Add(polyTwoItem);
            //vectorMap.Add(clipsItem);

            var foreColor = Color.Black;
            var backColor = Color.White;

            var mapStyles = new List<ShapeStyle>
            {
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(SystemBrushes.ButtonFace)),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Cross, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DarkDownwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DarkHorizontal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DarkUpwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DarkVertical, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DashedDownwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DashedHorizontal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DashedUpwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DashedVertical, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DiagonalBrick, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DiagonalCross, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Divot, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DottedDiamond, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.DottedGrid, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.ForwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Horizontal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.HorizontalBrick, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LargeCheckerBoard, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LargeConfetti, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LargeGrid, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LightDownwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LightHorizontal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LightUpwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.LightVertical, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Max, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Min, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.NarrowHorizontal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.NarrowVertical, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.OutlinedDiamond, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent05, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent10, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent20, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent25, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent30, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent40, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent50, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent60, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent70, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent75, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent80, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Percent90, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Plaid, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Shingle, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.SmallConfetti, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.SmallGrid, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.SolidDiamond, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Sphere, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Trellis, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Vertical, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Wave, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.Weave, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.WideDownwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.WideUpwardDiagonal, foreColor, backColor))),
                //new ShapeStyle(new Pen( Brushes.Transparent), new Pen(new HatchBrush(HatchStyle.ZigZag, foreColor, backColor))),

                //new ShapeStyle(new Pen(new HatchBrush(HatchStyle.ZigZag, foreColor, backColor)){DashStyle = DashStyle.Dash, Width = 3f}, new Pen(Brushes.Violet)),

                new ShapeStyle(Brushes.Black, new Pen(Brushes.Violet)) { LineStyle = new LineStyle() { Dashstyle = LineDashStyle.Dot }  },
                new ShapeStyle(Brushes.White, new Pen(Brushes.Violet)),

                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Solid, Width = 3f}, new Pen(Brushes.Violet)),
                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 1 }, Width = 3f}, new Pen(Brushes.Violet)),

                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Dash,  Width = 3f}, new Pen(Brushes.Violet)),
                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1 }, Width = 3f}, new Pen(Brushes.Violet)),

                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Dot, Width = 3f}, new Pen(Brushes.Violet)),
                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 1, 1 }, Width = 3f}, new Pen(Brushes.Violet)),

                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.DashDot, Width = 3f}, new Pen(Brushes.Violet)),
                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1, 1, 1 }, Width = 3f}, new Pen(Brushes.Violet)),

                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.DashDotDot, Width = 3f}, new Pen(Brushes.Violet)),
                //new ShapeStyle(new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1, 1, 1, 1, 1 }, Width = 3f}, new Pen(Brushes.Violet)),
                
                //new ShapeStyle(new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard,Color.Pink,Color.Transparent)), new Pen(Brushes.Transparent))
            };

            //var rectangleGrid = new RectangleDCellGrid(50, 50, 350, 350, mapStyles.Count);

            //foreach (var style in mapStyles)
            //{
            //    vectorMap.Add(rectangleGrid[mapStyles.IndexOf(style)], style);
            //}

            listBox1.DataSource = vectorMap.Items;
            //listBox1.ValueMember = "Name";
        }

        /// <summary>
        /// Tweening update callback.
        /// </summary>
        private void UpdateCallback()
            => CanvasPanel.Invalidate(true);

        /// <summary>
        /// Callback for when tweening completes.
        /// </summary>
        private void CompleteCallback()
            => CanvasPanel.Invalidate(true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Start();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
            => tweener.Update(tick);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
            => propertyGrid1.SelectedObject = (sender as ListBox)?.SelectedItem as GraphicItem;

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
            => CanvasPanel.Invalidate();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripComboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
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
        private void ToolStripComboBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
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
            foreach (GraphicItem item in vectorMap[vectorMap.VisibleBounds])
            {
                if (vectorMap?.SelectedItems != null && vectorMap.SelectedItems.Contains(item))
                    Renderer.Render(item, e.Graphics, new ShapeStyle(Brushes.Aquamarine, Brushes.AliceBlue));
                else
                    Renderer.Render(item, e.Graphics);
            }

            if (vectorMap?.RubberbandItems != null)
            {
                foreach (GraphicItem item in vectorMap?.RubberbandItems)
                    Renderer.Render(item, e.Graphics, new ShapeStyle(Brushes.Red, Brushes.Red));
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
#pragma warning disable IDE0021 // Use expression body for methods
            toolStack.MouseMove(new Point2D(e.X, e.Y));
#pragma warning restore IDE0021 // Use expression body for methods
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
#pragma warning disable IDE0021 // Use expression body for methods
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.VerticalScroll, e.Delta);
#pragma warning restore IDE0021 // Use expression body for methods
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseWheelTilt(object sender, MouseEventArgs e)
        {
#pragma warning disable IDE0021 // Use expression body for methods
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.HorizontalScroll, e.Delta);
#pragma warning restore IDE0021 // Use expression body for methods
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        { }

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
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Resize(object sender, EventArgs e)
        {
            var panel = sender as CanvasPanel;
            if (vectorMap != null)
            {
                vectorMap.VisibleBounds = new Rectangle2D(
                    panel.ClientRectangle.X,
                    panel.ClientRectangle.Y,
                    panel.ClientRectangle.Width - 1,
                    panel.ClientRectangle.Height - 1);
                boundaryItem.Item = vectorMap.VisibleBounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            openFileDialog1.FileName = vectorFilename;
            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    vectorMap = LoadFile(openFileDialog1.FileName) as VectorMap;
                    break;
                case DialogResult.None:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.No:
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            if (vectorFilename == String.Empty)
            {
                SaveAs(vectorFilename);
            }
            else
            {
                Serialize(saveFileDialog1.FileName, vectorMap);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(Object sender, EventArgs e) => SaveAs(vectorFilename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void SaveAs(string filename = "")
        {
            saveFileDialog1.FileName = filename;
            //saveFileDialog1.Filter = ".xml";
            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    Serialize(saveFileDialog1.FileName, vectorMap);
                    break;
                case DialogResult.None:
                case DialogResult.No:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private object LoadFile(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                return vectorMapSserializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="item"></param>
        private void Serialize(string filename, VectorMap item)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                Serialize(tw, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void Serialize(TextWriter writer, VectorMap item)
            => vectorMapSserializer.Serialize(writer, item);
    }
}
