﻿// <copyright file="EditorForm.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine;
using Engine.File.Palettes;
using Engine.Imaging;
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
        private VectorMap vectorMap;

        /// <summary>
        /// Container for actions to take on input.
        /// </summary>
        private ToolStack toolStack;

        /// <summary>
        /// Tweening interpolator for animation.
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

            var val = new(double, int, Point2D)(0, 3, new Point2D());

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

            vectorMap = new VectorMap();
            vectorMap.Tweener = tweener;
            toolStack = new ToolStack(vectorMap);
            toolStack?.RegisterMouseLeftButton(new SelectTop());
            toolStack?.RegisterMouseMiddleButton(new Pan());
            toolStack?.RegisterMouseScroll(new Zoom());

            var styles = new List<ShapeStyle>
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
                new ShapeStyle(new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard,Color.Pink,Color.Transparent)), new Pen(Brushes.Transparent))
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

            //Shape line = new LineSegment(new Point2D(160, 250), new Point2D(130, 145));
            //var lineItem = new GraphicItem(line, styles[5]);
            //vectorMap.Add(lineItem);

            //Shape set = new PolygonSet(
            //    new List<Polygon>(
            //        new List<Polygon> {
            //            new Polygon( // Boundary
            //                new List<Point2D> {
            //                    new Point2D(10, 10),
            //                    new Point2D(300, 10),
            //                    new Point2D(300, 300),
            //                    new Point2D(10, 300),
            //                    // Cut out
            //                    new Point2D(10, 200),
            //                    new Point2D(200, 80),
            //                    new Point2D(10, 150)
            //                }
            //            ),
            //            new Polygon( // First inner triangle
            //                new List<Point2D> {
            //                    new Point2D(20, 100),
            //                    new Point2D(175, 60),
            //                    new Point2D(40, 30)
            //                }
            //            ),
            //            new Polygon( // Second inner triangle
            //                new List<Point2D> {
            //                    new Point2D(250, 150),
            //                    new Point2D(150, 150),
            //                    new Point2D(250, 200)
            //                }
            //            )
            //        }
            //    )
            //);
            //var setItem = new GraphicItem(set, styles[8]);
            //vectorMap.Add(setItem);

            //Shape innerPolygon = new Polygon( // First inner triangle
            //                new List<Point2D> {
            //                    new Point2D(20, 100),
            //                    new Point2D(175, 60),
            //                    new Point2D(40, 30)
            //                }
            //            ).Offset(10);
            //var innerPolygonItem = new GraphicItem(innerPolygon, styles[9]);
            //vectorMap.Add(innerPolygonItem);

            //Polyline pathPolyline = (set as PolygonSet).ShortestPath(new Point2D(20, 20), new Point2D(200, 200));
            //Shape polylineSet = new PolylineSet(new List<Polyline> { pathPolyline.Offset(10), pathPolyline.Offset(-10) });
            //Polyline pathPolyline2 = pathPolyline.Offset(-10);
            //pathPolyline2.Reverse();
            //Shape polygonLine = new Polygon(new Polygon(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }));
            //GraphicItem polygonLineItem = new GraphicItem(polygonLine, styles[9]);
            //var polylineSetItem = new GraphicItem(polylineSet, styles[10]);
            //var pathPolylineItem = new GraphicItem(pathPolyline, styles[10]);
            //vectorMap.Add(polygonLineItem);
            //vectorMap.Add(polylineSetItem);
            //vectorMap.Add(pathPolylineItem);

            //var text = new Text2D("Test Text.", this.Font, new Point2D(100, 100));
            //var textItem = new GraphicItem(text, styles[1]);
            //vectorMap.Add(textItem);

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

            //var ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45d.ToRadians());
            //var ellipseItem = new GraphicItem(ellipse, styles[6]);
            //vectorMap.Add(ellipseItem);

            //var ellipseBounds = Boundings.Ellipse(200, 200, 50, 25, 45d.ToRadians());
            //var ellipseBoundsItem = new GraphicItem(ellipseBounds, styles[10]);
            //vectorMap.Add(ellipseBoundsItem);

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
            //    new Point2D(100d, 75d),
            //    56d, 30d, 0d);
            //var ellipseTweenItem = new GraphicItem(ellipseTween, styles[2]);

            //var rectangleTween = new Rectangle2D(
            //    new Point2D(100, 100),
            //    new Size2D(100, 100));
            //var rectangleTweenItem = new GraphicItem(rectangleTween, styles[2]);

            //double duration = 300;
            //double delay = 20;

            //tweener.Tween(rectangleTween, new { X = 0, Y = 0 }, duration, delay).OnUpdate(UpdateCallback).OnUpdate(() => rectangleTweenItem.Refresh());
            //Tween tt = tweener.Tween(rectangleTween, new { Location = new Point2D(0, 0) }, duration, delay);

            //tweener.Tween(ellipseTween, new { Center = new Point2D(0, 0) }, duration, delay);
            //tweener.Tween(ellipseTween, dests: new { Angle = -360d.ToRadians() }, duration: duration, delay: delay)
            //    .From(new { Angle = 45d.ToRadians() }).Ease(Ease.BackInOut)
            //    .Rotation(RotationUnit.Radians).OnUpdate(UpdateCallback);
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

            //var CircleBounds = Boundings.Circle(100, 200, 50);
            //var CircleBoundsItem = new GraphicItem(CircleBounds, styles[10]);
            //vectorMap.Add(CircleBoundsItem);

            //var parametricCircleArc = new ParametricDelegateCurve(
            //    (x, y, w, h, a, t) => Interpolaters.Arc(x, y, h, 0d, 90d.ToRadians(), t),
            //    (x, y, w, h, a, px, py) => Intersections.ArcSectorPoint(x, y, h, 0d, 90d.ToRadians(), px, py),
            //    new Point2D(150d, 150d), new Size2D(0d, 50d), 0, 0);
            //var parametricCircleArcItem = new GraphicItem(parametricCircleArc, styles[3]);
            //vectorMap.Add(parametricCircleArcItem);

            //var CircleArcBounds = Boundings.Arc(100, 200, 50, 0d, 90d.ToRadians());
            //var CircleArcBoundsItem = new GraphicItem(CircleArcBounds, styles[10]);
            //vectorMap.Add(CircleArcBoundsItem);

            //var cubicBezier = new CubicBezier(new Point2D(50, 50), new Point2D(75, 100), new Point2D(125, 100), new Point2D(150, 50));
            //var cubicBezierItem = new GraphicItem(cubicBezier, styles[1]);

            //var cubicBezierNodes = new NodeRevealer(new List<Point2D> { cubicBezier.A, cubicBezier.B, cubicBezier.C, cubicBezier.D }, 5d);
            //var cubicBezierNodeItem = new GraphicItem(cubicBezierNodes, styles[6]);

            //var quadraticBezier = new QuadraticBezier(new Point2D(50, 100), new Point2D(75, 50), new Point2D(150, 100));
            //var quadraticBezierItem = new GraphicItem(quadraticBezier, styles[1]);

            //var quadraticBezierNodes = new NodeRevealer(new List<Point2D> { quadraticBezier.A, quadraticBezier.B, quadraticBezier.C }, 5d);
            //var quadraticBezierNodeItem = new GraphicItem(quadraticBezierNodes, styles[6]);

            //var cubicBezier2 = new CubicBezier(new Point2D(50, 101), new Point2D(75, 50), new Point2D(125, 100), new Point2D(150, 25));
            //var cubicBezier2Item = new GraphicItem(cubicBezier2, styles[1]);

            //var segment = new LineSegment(new Point2D(20, 70), new Point2D(180, 80));
            //var segmentItem = new GraphicItem(segment, styles[1]);

            //var segmentNodes = new NodeRevealer(new List<Point2D> { segment.A, segment.B }, 5d);
            //var segmentNodeItem = new GraphicItem(segmentNodes, styles[6]);

            //var intersections = Intersections.CubicBezierLineSegment(cubicBezier.A.X, cubicBezier.A.Y, cubicBezier.B.X, cubicBezier.B.Y, cubicBezier.C.X, cubicBezier.C.Y, cubicBezier.D.X, cubicBezier.D.Y, segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
            //var intersectionNodes = new NodeRevealer(intersections, 5d);
            //var intersectionNodesItem = new GraphicItem(intersectionNodes, styles[6]);

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

            //vectorMap.Add(cubicBezierItem);
            //vectorMap.Add(quadraticBezierItem);
            //vectorMap.Add(cubicBezier2Item);
            //vectorMap.Add(segmentItem);
            //vectorMap.Add(cubicBezierNodeItem);
            //vectorMap.Add(quadraticBezierNodeItem);
            //vectorMap.Add(segmentNodeItem);
            //vectorMap.Add(ellipseLineIntersectionNodesItem);
            //vectorMap.Add(intersectionNodesItem);
            //vectorMap.Add(intersection3NodesItem);
            //vectorMap.Add(intersection2NodesItem);

            var figure = new Figure(new Point2D(150d, 200d));
            figure.AddLineSegment(new Point2D(200, 200))
                .AddArc(50d, 50d, 0d, false, false, new Point2D(250d, 250d))
                .AddLineSegment(new Point2D(250, 300))
                .AddArc(50d, 50d, 0d, false, true, new Point2D(200d, 350d))
                .AddLineSegment(new Point2D(150, 350))
                .AddArc(50d, 50d, 0d, true, false, new Point2D(100d, 300d))
                .AddLineSegment(new Point2D(100, 250))
                .AddArc(50d, 50d, 0d, true, true, new Point2D(150d, 200d));
            var figureItem = new GraphicItem(figure, styles[1]);

            var figureBounds = figure.Bounds;
            var figureBoundsItem = new GraphicItem(figureBounds, styles[10]);

            //var parametricPointTesterFigure = new ParametricPointTester(
            //    (px, py) => Containings.FigurePoint(figure, new Point2D(px, py)),
            //    figureBounds.X, figureBounds.Y, figureBounds.Right + 5, figureBounds.Bottom + 5, 5, 5);
            //var parametricPointTesterFigureItem = new GraphicItem(parametricPointTesterFigure, styles[3]);

            //var parametricPointTesterSegment = new ParametricPointTester(
            //    (px, py) => Intersections.LineSegmentPoint(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, px, py),
            //    segment.Bounds.X - 5, segment.Bounds.Y - 5, segment.Bounds.Right + 10, segment.Bounds.Bottom + 10, 5, 5);
            //var parametricPointTesterSegmentItem = new GraphicItem(parametricPointTesterSegment, styles[3]);

            //var parametricPointTesterRectangle = new ParametricPointTester(
            //    (px, py) => Containings.RectanglePoint(figureBounds.Left, figureBounds.Top, figureBounds.Right, figureBounds.Bottom, px, py),
            //    figureBounds.X - 200, figureBounds.Y - 200, figureBounds.Right + 205, figureBounds.Bottom + 205, 5, 5);
            //var parametricPointTesterRectangleItem = new GraphicItem(parametricPointTesterRectangle, styles[3]);

            vectorMap.Add(figureBoundsItem);
            vectorMap.Add(figureItem);
            //vectorMap.Add(parametricPointTesterSegmentItem);
            //vectorMap.Add(parametricPointTesterFigureItem);
            //vectorMap.Add(parametricPointTesterRectangleItem);

            listBox1.DataSource = vectorMap.Items;
            //listBox1.ValueMember = "Name";
        }

        /// <summary>
        /// Tweening update callback.
        /// </summary>
        private void UpdateCallback()
        {
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// Callback for when tweening completes.
        /// </summary>
        private void CompleteCallback()
        {
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
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
            vectorMap.VisibleBounds = new Rectangle2D(panel.Bounds.X, panel.Bounds.Y, panel.Bounds.Width, panel.Bounds.Height);
        }

        private void openToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            openFileDialog1.FileName = vectorFilename;
            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    vectorMap = load(openFileDialog1.FileName) as VectorMap;
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

        private void saveToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            if (vectorFilename == String.Empty)
            {
                saveAs(vectorFilename);
            }
            else
            {
                serialize(saveFileDialog1.FileName, vectorMap);
            }
        }

        private void saveAsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            saveAs(vectorFilename);
        }

        private void saveAs(string filename = "")
        {
            saveFileDialog1.FileName = filename;
            //saveFileDialog1.Filter = ".xml";
            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    serialize(saveFileDialog1.FileName, vectorMap);
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

        private object load(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                return vectorMapSserializer.Deserialize(reader);
            }
        }

        private void serialize(string filename, VectorMap item)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                serialize(tw, item);
            }
        }

        private void serialize(TextWriter writer, VectorMap item)
        {
            vectorMapSserializer.Serialize(writer, item);
        }
    }
}
