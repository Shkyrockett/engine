// <copyright file="TestCases.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine._Preview;
using Engine.Imaging;
using Engine.Physics;
using Engine.Tweening;
using Engine.Winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public static class TestCases
    {
        #region Styles

        private static ShapeStyle selectionStyle = new ShapeStyle(new HatchBrush(HatchStyle.SmallCheckerBoard, Color.Pink, Color.Transparent), new Pen(Brushes.Transparent));
        private static ShapeStyle handleStyle = new ShapeStyle(Brushes.Maroon, new Pen(Brushes.MediumPurple));

        private static ShapeStyle azureTransparent = new ShapeStyle(Brushes.Azure, new Pen(Brushes.Transparent));

        private static ShapeStyle paperLikeStyle = new ShapeStyle(Brushes.Bisque, new Pen(Brushes.Beige));
        private static ShapeStyle whiteishStyle = new ShapeStyle(Brushes.AntiqueWhite, new Pen(Brushes.CadetBlue));
        private static ShapeStyle solidPinkStyle = new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum));
        private static ShapeStyle solidCyanStyle = new ShapeStyle(Brushes.DarkCyan, new Pen(Brushes.Cyan));
        private static ShapeStyle solidGreenStyle = new ShapeStyle(Brushes.DarkGreen, new Pen(Brushes.ForestGreen));
        private static ShapeStyle solidPurpleStyle = new ShapeStyle(Brushes.Maroon, new Pen(Brushes.MediumPurple));
        private static ShapeStyle solidLightBlueStyle = new ShapeStyle(Brushes.BlueViolet, new Pen(Brushes.AliceBlue));
        private static ShapeStyle solidLightGreenStyle = new ShapeStyle(Brushes.DarkGoldenrod, new Pen(Brushes.Honeydew));

        private static ShapeStyle intersectionBlue = new ShapeStyle(new SolidBrush(Color.FromArgb(128, Color.Blue)), new SolidBrush(Color.FromArgb(128, Color.Blue)));
        private static ShapeStyle intersectionGreen = new ShapeStyle(new SolidBrush(Color.FromArgb(128, Color.Green)), new SolidBrush(Color.FromArgb(128, Color.Green)));
        private static ShapeStyle intersectionRed = new ShapeStyle(new SolidBrush(Color.FromArgb(128, Color.Red)), new SolidBrush(Color.FromArgb(128, Color.Red)));

        #endregion

        #region Interactive

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        /// <param name="CanvasPanel"></param>
        /// <param name="boundaryItem"></param>
        public static void ResizeRefreshBounds(VectorMap vectorMap, CanvasPanel CanvasPanel, out GraphicItem boundaryItem)
        {
            vectorMap.VisibleBounds = CanvasPanel.ClientRectangle.ToRectangle2D();
            boundaryItem = new GraphicItem(vectorMap.VisibleBounds, solidPinkStyle);
            vectorMap.Add(boundaryItem);
        }

        #endregion

        #region Regression Tests

        /// <summary>
        /// This is a regression test case for an error where the intersection of a horizontal line and a specific Quadratic Bezier might not end up with intersection points. 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void QuadraticBezierHorizontalLineIntersection(VectorMap vectorMap)
        {
            // Horizontal line segment.
            var quadraticSegment = new LineSegment(new Point2D(220, 70), new Point2D(350, 70));
            var quadraticSegmentItem = new GraphicItem(quadraticSegment, solidGreenStyle)
            {
                Name = "Quadratic Bezier Intersecting Line Segment."
            };

            var quadraticSegmentNodeItem = new GraphicItem(new NodeRevealer(quadraticSegment.Points, 5d), handleStyle);

            // Quadratic Bezier where end points are horizontal.
            var quadraticBezier = new QuadraticBezier(new Point2D(250, 50), new Point2D(300, 100), new Point2D(350, 50));
            var quadraticBezierItem = new GraphicItem(quadraticBezier, solidGreenStyle)
            {
                Name = "Quadratic Bezier."
            };

            var quadraticBezierBoundsItem = new GraphicItem(quadraticBezier.Bounds, selectionStyle)
            {
                Name = "Quadratic Bezier Bounds."
            };

            var quadraticBezierNodeItem = new GraphicItem(new NodeRevealer(quadraticBezier.Points, 5d), handleStyle);

            var quadraticIntersections = Intersections.Intersection(quadraticBezier, quadraticSegment);
            var quadraticIntersectionNodes = new NodeRevealer(quadraticIntersections.Points, 5d);
            var quadraticIntersectionNodesItem = new GraphicItem(quadraticIntersectionNodes, handleStyle);

            vectorMap.Add(quadraticBezierBoundsItem);
            vectorMap.Add(quadraticBezierItem);
            vectorMap.Add(quadraticSegmentItem);
            vectorMap.Add(quadraticBezierNodeItem);
            vectorMap.Add(quadraticSegmentNodeItem);
            vectorMap.Add(quadraticIntersectionNodesItem);
        }

        /// <summary>
        /// This is a regression test case for an error where the intersection of a horizontal line and a specific Cubic Bezier might not end up with intersection points. 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void CubicBezierHorizontalLineIntersection(VectorMap vectorMap)
        {
            // Horizontal line segment.
            var cubicSegment = new LineSegment(new Point2D(20, 70), new Point2D(150, 70));
            var cubicSegmentItem = new GraphicItem(cubicSegment, solidGreenStyle)
            {
                Name = "Cubic Bézier Intersecting Line Segment."
            };

            var cubicSegmentNodeItem = new GraphicItem(new NodeRevealer(cubicSegment.Points, 5d), handleStyle);

            // Quadratic Bezier where end points are horizontal and the handles between share another y-axis.
            var cubicBezier = new CubicBezier(new Point2D(50, 50), new Point2D(75, 100), new Point2D(125, 100), new Point2D(150, 50));
            var cubicBezierItem = new GraphicItem(cubicBezier, solidGreenStyle)
            {
                Name = "Cubic Bézier."
            };

            var cubicBezierNodeItem = new GraphicItem(new NodeRevealer(cubicBezier.Points, 5d), handleStyle);

            var cubicBezierBoundsItem = new GraphicItem(cubicBezier.Bounds, selectionStyle)
            {
                Name = "Cubic Bézier Bounds."
            };

            var cubicIntersections = Intersections.Intersection(cubicBezier, cubicSegment);
            var cubicIntersectionNodesItem = new GraphicItem(new NodeRevealer(cubicIntersections.Points, 5d), handleStyle);

            vectorMap.Add(cubicBezierBoundsItem);
            vectorMap.Add(cubicBezierItem);
            vectorMap.Add(cubicSegmentItem);
            vectorMap.Add(cubicBezierNodeItem);
            vectorMap.Add(cubicSegmentNodeItem);
            vectorMap.Add(cubicIntersectionNodesItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        /// <param name="form"></param>
        public static void SegmentIntersections(VectorMap vectorMap, EditorForm form)
        {
            var segment0 = new LineSegment(new Point2D(20, 150), new Point2D(180, 200));
            var segment0Item = new GraphicItem(segment0, solidGreenStyle);
            var segment0NodeItem = new GraphicItem(new NodeRevealer(segment0.Points, 5d), handleStyle);

            var segment1 = new LineSegment(new Point2D(140, 150), new Point2D(150, 250));
            var segment1Item = new GraphicItem(segment1, solidGreenStyle);
            var segment1NodeItem = new GraphicItem(new NodeRevealer(segment1.Points, 5d), handleStyle);

            var segmentIntersection = Intersections.Intersection(segment0, segment1);
            var segmentIntersectionNodes = new NodeRevealer(segmentIntersection.Points, 5d);
            var segmentIntersectionNodesItem = new GraphicItem(segmentIntersectionNodes, handleStyle);

            vectorMap.Add(segment0Item);
            vectorMap.Add(segment1Item);
            vectorMap.Add(segment0NodeItem);
            vectorMap.Add(segment1NodeItem);
            vectorMap.Add(segmentIntersectionNodesItem);

            var point = new ScreenPoint(150, 130);
            var pointItem = new GraphicItem(point, solidGreenStyle)
            {
                Name = "Point Item."
            };
            var pointNodeItem = new GraphicItem(new NodeRevealer(point.Point, 5d), handleStyle);
            var dist1 = Measurements.DistanceLinePoint(segment1.AX, segment1.AY, segment1.BX, segment1.BY, point.Point.X, point.Point.Y);
            var dist2 = Measurements.DistanceLineSegmentPoint(segment1.AX, segment1.AY, segment1.BX, segment1.BY, point.Point.X, point.Point.Y);
            var dist3 = Measurements.ConstrainedDistanceLineSegmentPoint(segment1.AX, segment1.AY, segment1.BX, segment1.BY, point.Point.X, point.Point.Y);

            var text = new Text2D($"{point.ToString()},{Environment.NewLine}Distance to Line: {dist1.ToString()},{Environment.NewLine}Distance to Segment: {dist2.ToString()},{Environment.NewLine}Distance in Range: {dist3?.ToString()}", form.Font, point.Point);
            var textItem = new GraphicItem(text, handleStyle)
            {
                Name = "Point Text."
            };

            vectorMap.Add(pointItem);
            vectorMap.Add(pointNodeItem);
            vectorMap.Add(textItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void IntersectionsTests(VectorMap vectorMap)
        {
            var segment = new LineSegment(new Point2D(20, 150), new Point2D(180, 200));
            var segmentItem = new GraphicItem(segment, solidGreenStyle);
            var segmentNodeItem = new GraphicItem(new NodeRevealer(segment.Points, 5d), handleStyle);

            var quadraticBezier = new QuadraticBezier(new Point2D(50, 100), new Point2D(75, 50), new Point2D(150, 100));
            var quadraticBezierItem = new GraphicItem(quadraticBezier, solidGreenStyle);

            var quadraticBezierNodes = new NodeRevealer(new List<Point2D> { quadraticBezier.A, quadraticBezier.B, quadraticBezier.C }, 5d);
            var quadraticBezierNodeItem = new GraphicItem(quadraticBezierNodes, solidPurpleStyle);

            var cubicBezier = new CubicBezier(new Point2D(50, 50), new Point2D(75, 100), new Point2D(125, 100), new Point2D(150, 50));
            var cubicBezierItem = new GraphicItem(cubicBezier, solidGreenStyle)
            {
                Name = "Cubic Bézier."
            };

            var cubicBezier2 = new CubicBezier(new Point2D(50, 101), new Point2D(75, 50), new Point2D(125, 100), new Point2D(150, 25));
            var cubicBezier2Item = new GraphicItem(cubicBezier2, solidGreenStyle);

            var ellipseTween = new Ellipse(
                new Point2D(100d, 375d),
                56d, 30d, 10d.ToRadians());
            var ellipseTweenItem = new GraphicItem(ellipseTween, solidLightBlueStyle);

            var parametricPointTesterSegment = new ParametricPointTester(
                (px, py) => (Intersections.PointLineSegmentIntersects(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, px, py) == true ? Inclusion.Boundary : Inclusion.Outside),
                segment.Bounds.X - 5, segment.Bounds.Y - 5, segment.Bounds.Right + 10, segment.Bounds.Bottom + 10, 5, 5);
            var parametricPointTesterSegmentItem = new GraphicItem(parametricPointTesterSegment, paperLikeStyle);

            var ellipseLineIntersections = Intersections.EllipseLineSegmentIntersection(
                ellipseTween.Center.X,
                ellipseTween.Center.Y,
                ellipseTween.RX,
                ellipseTween.RY,
                ellipseTween.Angle,
                segment.A.X,
                segment.A.Y,
                segment.B.X,
                segment.B.Y
                );
            var ellipseLineIntersectionNodes = new NodeRevealer(ellipseLineIntersections.Points, 5d);
            var ellipseLineIntersectionNodesItem = new GraphicItem(ellipseLineIntersectionNodes, solidPurpleStyle);

            var intersection3s = Intersections.QuadraticBezierCubicBezierIntersection(quadraticBezier.A.X, quadraticBezier.A.Y, quadraticBezier.B.X, quadraticBezier.B.Y, quadraticBezier.C.X, quadraticBezier.C.Y, cubicBezier.A.X, cubicBezier.A.Y, cubicBezier.B.X, cubicBezier.B.Y, cubicBezier.C.X, cubicBezier.C.Y, cubicBezier.D.X, cubicBezier.D.Y);
            var intersectio3nNodes = new NodeRevealer(intersection3s.Points, 5d);
            var intersection3NodesItem = new GraphicItem(intersectio3nNodes, solidPurpleStyle);

            var intersections2 = Intersections.CubicBezierCubicBezierIntersection(cubicBezier.A.X, cubicBezier.A.Y, cubicBezier.B.X, cubicBezier.B.Y, cubicBezier.C.X, cubicBezier.C.Y, cubicBezier.D.X, cubicBezier.D.Y, cubicBezier2.A.X, cubicBezier2.A.Y, cubicBezier2.B.X, cubicBezier2.B.Y, cubicBezier2.C.X, cubicBezier2.C.Y, cubicBezier2.D.X, cubicBezier2.D.Y);
            var intersection2Nodes = new NodeRevealer(intersections2.Points, 5d);
            var intersection2NodesItem = new GraphicItem(intersection2Nodes, solidPurpleStyle);

            vectorMap.Add(segmentItem);
            vectorMap.Add(quadraticBezierItem);
            vectorMap.Add(cubicBezier2Item);
            vectorMap.Add(quadraticBezierNodeItem);
            vectorMap.Add(ellipseLineIntersectionNodesItem);
            vectorMap.Add(intersection3NodesItem);
            vectorMap.Add(intersection2NodesItem);
            vectorMap.Add(parametricPointTesterSegmentItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void CircularArcBounds(VectorMap vectorMap)
        {
            var arc = new CircularArc(new Point2D(100, 100), 100, -60d.ToRadians(), -300d.ToRadians());
            var chord = new LineSegment(arc.StartPoint, Examples.Arc.EndPoint);
            var arcBounds = new Rectangle2D(arc.Bounds);
            var arcItem = new GraphicItem(arc, whiteishStyle)
            {
                Name = "Arc Shape."
            };
            var chordItem = new GraphicItem(chord, solidCyanStyle)
            {
                Name = "Arc Chord."
            };
            var arcBoundsItem = new GraphicItem(arcBounds, selectionStyle)
            {
                Name = "Arc Bounding Box."
            };
            vectorMap.Add(arcBoundsItem);
            vectorMap.Add(arcItem);
            vectorMap.Add(chordItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void EllipseBound(VectorMap vectorMap)
        {
            var ellipseItem = new GraphicItem(Examples.Ellipse, solidPurpleStyle);
            var ellipseBoundsItem = new GraphicItem(Examples.Ellipse.Bounds, selectionStyle);
            vectorMap.Add(ellipseBoundsItem);
            vectorMap.Add(ellipseItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void EllipticalArcBounds(VectorMap vectorMap)
        {
            var ellpticArcItem = new GraphicItem(Examples.EllpticArc, paperLikeStyle);
            var ellpticArcBoundsItem = new GraphicItem(Examples.EllpticArc.Bounds, selectionStyle);
            vectorMap.Add(ellpticArcBoundsItem);
            vectorMap.Add(Examples.EllpticArc);
        }

        #endregion

        #region Experimental

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void EllipseToBezier(VectorMap vectorMap)
        {
            var ellipse = new Ellipse(150, 100, 100, 50, 0);
            var ellipseItem = new GraphicItem(ellipse, solidGreenStyle)
            {
                Name = "Ellipse"
            };

            var ellipticalArc = new EllipticalArc(ellipse, -30d.ToRadians(), 90d.ToRadians());
            var ellipticalArcItem = new GraphicItem(ellipticalArc, solidCyanStyle)
            {
                Name = "Elliptical Arc"
            };

            var beziers = Conversions.EllipticalArcToCubicBeziers(ellipticalArc);
            var contour = new PathContour(beziers[0].A);

            foreach (var bezier in beziers)
            {
                contour.AddCubicBezier(bezier.B, bezier.C, bezier.D);
            }
            var arcContourItem = new GraphicItem(contour, solidPurpleStyle)
            {
                Name = "Bezier Arc"
            };
            var arcContourNodeItem = new GraphicItem(new NodeRevealer(contour.Grips, 5d), handleStyle)
            {
                Name = "Contour Nodes"
            };

            vectorMap.Add(ellipseItem);
            vectorMap.Add(ellipticalArcItem);
            vectorMap.Add(arcContourItem);
            vectorMap.Add(arcContourNodeItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void CurveFitting(VectorMap vectorMap)
        {
            Contour points = new Contour() { (50, 50), (100, 75), (150, 75), (200, 75) };
            var curves = CurveFit.Fit(points.Points, 8);
            foreach (var curve in curves)
            {
                vectorMap.Add(curve);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void WarpGeometry(VectorMap vectorMap)
        {
            var rect1 = new Rectangle2D(200, 100, 200, 100);
            var rect1Item = new GraphicItem(rect1, solidGreenStyle)
            {
                Name = "Rectangle"
            };

            var triangle = new Contour() { (300, 100), (250, 200), (350, 200) };
            var triangleItem = new GraphicItem(triangle, solidPurpleStyle)
            {
                Name = "Triangle"
            };

            //var scaleDistort = new ScaleDistort(new Size2D(2, 2));
            //var translateDistort = new TranslateDistort(new Vector2D(-rect1.Center.X, -rect1.Center.Y));

            var angle = 60d.ToRadians();
            var xAxis = new Point2D(Math.Cos(angle), Math.Sin(angle));
            var yAxis = new Point2D(-Math.Sin(angle), Math.Cos(angle));

            var matrix = Matrix3x2D.Identity;
            matrix.Translate(-rect1.Center.X, -rect1.Center.Y);
            matrix.Scale(2, 2);
            matrix.Rotate(angle);
            matrix.Translate(rect1.Center.X, rect1.Center.Y);

            var matrixTest = new MatrixDistort(matrix);

            var test = new ParametricPreservingDistort(
                (a) => Distortions.Translate(a, new Vector2D(-rect1.Center.X, -rect1.Center.Y)),
                (a) => Distortions.Rotate(a, rect1.Center, xAxis, yAxis),
                (a) => Distortions.Scale(a, new Size2D(2, 2)),
                (a) => Distortions.Translate(a, new Vector2D(-rect1.Center.X, -rect1.Center.Y))
                );
            var sphereDistort = new SphereDistort(rect1);
            var swirlDistort = new SwirlDistort(rect1.Center);

            var curvedRectangle = matrixTest.Process(sphereDistort.Process(rect1));
            var curvedRectangleItem = new GraphicItem(curvedRectangle, solidCyanStyle)
            {
                Name = "Curved Rectangle"
            };
            var curvedRectangleNodeItem = new GraphicItem(new NodeRevealer(curvedRectangle.Grips, 5d), handleStyle);

            var curvedTriangle = swirlDistort.Process(test.Process(sphereDistort.Process(triangle)));
            var curvedTriangleItem = new GraphicItem(curvedTriangle, solidPurpleStyle)
            {
                Name = "Curved Triangle"
            };
            var curvedTriangleNodeItem = new GraphicItem(new NodeRevealer(curvedTriangle.Grips, 5d), handleStyle);

            var warpGrid = new ParametricWarpGrid((a) => sphereDistort.Process(a), rect1, rect1.Bounds.X, rect1.Bounds.Y, rect1.Bounds.Right, rect1.Bounds.Bottom, 5, 5);
            var warpGridItem = new GraphicItem(warpGrid, handleStyle)
            {
                Name = "Warp"
            };

            vectorMap.Add(curvedRectangleItem);
            vectorMap.Add(rect1Item);
            vectorMap.Add(curvedTriangleItem);
            vectorMap.Add(triangleItem);
            vectorMap.Add(warpGridItem);
            //vectorMap.Add(curvedRectangleNodeItem);
            //vectorMap.Add(curvedTriangleNodeItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void ComplexPolygonClipping(VectorMap vectorMap)
        {
            Polygon poly1 = new Polygon() {
                new Contour() {
                    new Point2D(25, 200),
                    new Point2D(225, 200),
                    new Point2D(125, 375),
                },
                new Contour()
                {
                    new Point2D(125, 225),
                    new Point2D(90, 275),
                    new Point2D(160, 275),
                }
            };
            var poly1Item = new GraphicItem(poly1, intersectionBlue)
            {
                Name = "Polygon 1"
            };

            Polygon poly2 = new Polygon() {
                new Contour() {
                    new Point2D(25,325),
                    new Point2D(225,325),
                    new Point2D(125,150),
                },
                new Contour()
                {
                    new Point2D(125, 290),
                    new Point2D(90, 240),
                    new Point2D(160, 240),
                }
            };
            var poly2Item = new GraphicItem(poly2, intersectionGreen)
            {
                Name = "Polygon 2"
            };

            var clip = new MartinezPolygonClipper(poly1, poly2);
            var poly3 = clip.Compute(ClipingOperations.Intersection);
            var poly3Item = new GraphicItem(poly3, intersectionRed)
            {
                Name = "Polygon 3"
            };

            vectorMap.Add(poly1Item);
            vectorMap.Add(poly2Item);
            vectorMap.Add(poly3Item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PolyClipping(VectorMap vectorMap)
        {
            var PolygonTriangleA = new Contour(new List<Point2D> { (300, 0), (600, 450), (0, 450) });

            var PolygonTriangleB = new Contour(new List<Point2D> { (0, 150), (600, 150), (300, 600) });

            var polyOne = new Polygon() { PolygonTriangleA };
            var polyOneItem = new GraphicItem(polyOne, paperLikeStyle);
            var polyTwo = new Polygon() { PolygonTriangleB };
            var polyTwoItem = new GraphicItem(polyTwo, paperLikeStyle);

            var clip = new MartinezPolygonClipper(polyOne, polyTwo);

            var clips = clip.Compute(ClipingOperations.Intersection);
            var clipsItem = new GraphicItem(clips, solidPurpleStyle);

            vectorMap.Add(polyOneItem);
            vectorMap.Add(polyTwoItem);
            vectorMap.Add(clipsItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PathContourWArcLine(VectorMap vectorMap)
        {
            var figure = new PathContour(new Point2D(150d, 200d));
            figure.AddLineSegment(new Point2D(200, 200))
                .AddArc(50d, 50d, 0d, false, false, new Point2D(250d, 250d))
                .AddLineSegment(new Point2D(250, 300))
                .AddArc(50d, 50d, 0d, false, true, new Point2D(200d, 350d))
                .AddLineSegment(new Point2D(150, 350))
                .AddArc(50d, 50d, 0d, true, false, new Point2D(100d, 300d))
                .AddLineSegment(new Point2D(100, 250))
                .AddArc(50d, 50d, 0d, true, true, new Point2D(150d, 200d));
            var figureItem = new GraphicItem(figure, solidGreenStyle);
            var figureBounds = figure.Bounds;
            var figureBoundsItem = new GraphicItem(figureBounds, selectionStyle);
            var parametricPointTesterFigure = new ParametricPointTester(
                (px, py) => Intersections.GeometryPathContainsPoint(figure, new Point2D(px, py)),
                figureBounds.X, figureBounds.Y, figureBounds.Right + 5, figureBounds.Bottom + 5, 5, 5);
            var parametricPointTesterFigureItem = new GraphicItem(parametricPointTesterFigure, handleStyle);

            var parametricPointTesterRectangle = new ParametricPointTester(
                (px, py) => Intersections.RectangleContainsPoint(figureBounds.Left, figureBounds.Top, figureBounds.Right, figureBounds.Bottom, px, py),
                figureBounds.X - 200, figureBounds.Y - 200, figureBounds.Right + 205, figureBounds.Bottom + 205, 5, 5);
            var parametricPointTesterRectangleItem = new GraphicItem(parametricPointTesterRectangle, handleStyle);

            vectorMap.Add(figureBoundsItem);
            vectorMap.Add(figureItem);
            //vectorMap.Add(parametricPointTesterFigureItem);
            //vectorMap.Add(parametricPointTesterRectangleItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void Pathfinding(VectorMap vectorMap)
        {
            var lineItem = new GraphicItem(Examples.Line, solidCyanStyle)
            {
                Name = "Single Line segment."
            };
            vectorMap.Add(lineItem);

            var setItem = new GraphicItem(Examples.PolySet, whiteishStyle)
            {
                Name = "Polygon"
            };
            vectorMap.Add(setItem);

            var innerPolygonItem = new GraphicItem(Examples.InnerPolygon, azureTransparent)
            {
                Name = "Inner Triangle Contour"
            };
            vectorMap.Add(innerPolygonItem);

            var pathPolyline = (Examples.PolySet as Polygon).ShortestPath(new Point2D(20, 20), new Point2D(200, 200));
            var polylineSet = new PolylineSet(new List<Polyline> { pathPolyline.Offset(10), pathPolyline.Offset(-10) });
            var pathPolyline2 = pathPolyline.Offset(-10);
            pathPolyline2.Reverse();
            var polygonLine = new Contour(new Contour(new List<Polyline>() { pathPolyline.Offset(10), pathPolyline2 }));
            var polygonLineItem = new GraphicItem(polygonLine, azureTransparent)
            {
                Name = "Polygon Contour Line"
            };
            var polylineSetItem = new GraphicItem(polylineSet, selectionStyle)
            {
                Name = "Polyline Set"
            };
            var pathPolylineItem = new GraphicItem(pathPolyline, selectionStyle)
            {
                Name = "Path Polyline"
            };

            vectorMap.Add(polygonLineItem);
            vectorMap.Add(polylineSetItem);
            vectorMap.Add(pathPolylineItem);

            Shape ego = new Circle(pathPolyline.Interpolate(.1), 10);
            var egoItem = new GraphicItem(ego, solidCyanStyle)
            {
                Name = "Ego Circle"
            };
            vectorMap.Add(egoItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PolylineClicking(VectorMap vectorMap)
        {
            var polylineItem = new GraphicItem(Examples.PolyTriangle, paperLikeStyle)
            {
                Name = "Polygon Triangle"
            };
            vectorMap.Add(polylineItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        /// <param name="form"></param>
        public static void TextRendering(VectorMap vectorMap, EditorForm form)
        {
            var text = new Text2D("Test Text.", form.Font, new Point2D(100, 100));
            var textItem = new GraphicItem(text, solidGreenStyle)
            {
                Name = "Text Test"
            };
            vectorMap.Add(textItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void RotatedRectangle(VectorMap vectorMap)
        {
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void SutherlandHodgman(VectorMap vectorMap)
        {
            var triangleI = new Triangle(
                new Point2D(75, 125),
                new Point2D(225, 125),
                new Point2D(150, 250));
            GraphicItem triangleIItem = new GraphicItem(triangleI, solidPinkStyle);
            vectorMap.Add(triangleIItem);
            var rectangle = new Rectangle2D(new Point2D(100, 100), new Size2D(100, 100)).ToPolygon();
            GraphicItem rectangleItem = new GraphicItem(rectangle, solidLightBlueStyle)
            {
                Name = "Square Polygon."
            };
            vectorMap.Add(rectangleItem);
            var intersection = new Contour(Engine.SutherlandHodgman.PolygonPolygon(triangleI.Points, rectangle.Points));
            GraphicItem intersectionItem = new GraphicItem(intersection, paperLikeStyle);
            vectorMap.Add(intersectionItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        /// <param name="form"></param>
        public static void Tweenning(VectorMap vectorMap, EditorForm form)
        {
            var ellipseTween = new Ellipse(
                new Point2D(100d, 375d),
                56d, 30d, 10d.ToRadians());
            var ellipseTweenItem = new GraphicItem(ellipseTween, solidLightBlueStyle);

            double duration = 300;
            double delay = 20;

            vectorMap.Tweener.Tween(ellipseTween, new { Center = new Point2D(0, 0) }, duration, delay);

            var rectangleTween = ellipseTween.Bounds;
            var rectangleTweenItem = new GraphicItem(rectangleTween, selectionStyle);

            vectorMap.Tweener.Tween(ellipseTween, dests: new { Angle = -360d.ToRadians() }, duration: duration, delay: delay)
                .From(new { Angle = 45d.ToRadians() }).Ease(Ease.BackInOut)
                .Rotation(RotationUnit.Radians)
                .OnUpdate(form.UpdateCallback)
                .OnUpdate(() => rectangleTweenItem.Item = ellipseTween.Bounds);
            vectorMap.Tweener.Timer(duration).OnComplete(form.CompleteCallback);

            vectorMap.Add(rectangleTweenItem);
            vectorMap.Add(ellipseTweenItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void ParametricEllipseBounds(VectorMap vectorMap)
        {
            var parametricEllipse = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.UnitPolarEllipse(x, y, w, h, a, t),
                (x, y, w, h, a, px, py) => Intersections.EllipseContainsPoint(x, y, w, h, a, px, py),
                new Point2D(200d, 100d), new Size2D(25d, 50d), 0, 0);
            var parametricEllipseItem = new GraphicItem(parametricEllipse, paperLikeStyle);
            vectorMap.Add(parametricEllipseItem);

            var EllipseBounds = Measurements.EllipseBounds(200, 100, 25, 50);
            var EllipseBoundsItem = new GraphicItem(EllipseBounds, selectionStyle);
            vectorMap.Add(EllipseBoundsItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void ParametricEllipseArc(VectorMap vectorMap)
        {
            double centerX = 200d;
            double centerY = 200d;
            double radius1 = 100d;
            double radius2 = 200d;
            double angle = 45d.ToRadians();
            double startAngle = -45d.ToRadians();
            double sweepAngle = 90d.ToRadians();
            var parametricEllipticArc = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.EllipticalArc(x, y, w, h, a, startAngle, sweepAngle, t),
                (x, y, w, h, a, px, py) => Intersections.EllipticalArcContainsPoint(x, y, w, h, a, startAngle, sweepAngle, px, py),
                new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            var parametricEllipticArcItem = new GraphicItem(parametricEllipticArc, paperLikeStyle);
            var EllpticArcBounds = Measurements.EllipticalArcBounds(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var EllpticArcBoundsItem = new GraphicItem(EllpticArcBounds, selectionStyle);

            vectorMap.Add(EllpticArcBoundsItem);
            vectorMap.Add(parametricEllipticArcItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void ParametricTesting(VectorMap vectorMap)
        {
            double centerX = 200d;
            double centerY = 200d;
            double radius1 = 100d;
            double radius2 = 200d;

            double angle = -30d.ToRadians();
            double startAngle = -60d.ToRadians();
            double sweepAngle = -90d.ToRadians();

            var parametricEllipse = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.UnitPolarEllipse(x, y, w, h, a, t),
                (x, y, w, h, a, px, py) => Intersections.EllipseContainsPoint(x, y, w, h, a, px, py),
                new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            var parametricEllipseItem = new GraphicItem(parametricEllipse, paperLikeStyle);
            vectorMap.Add(parametricEllipseItem);

            var parametricPointTesterEllipticArc = new ParametricPointTester(
                (px, py) => Intersections.EllipticalArcContainsPoint(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle, px, py),
                centerX - (radius1 < radius2 ? radius2 : radius2),
                centerY - (radius1 < radius2 ? radius2 : radius2),
                centerX + (radius1 < radius2 ? radius2 : radius2),
                centerY + (radius1 < radius2 ? radius2 : radius2),
                5, 5);
            var parametricPointTesterEllipticArcItem = new GraphicItem(parametricPointTesterEllipticArc, paperLikeStyle);

            var parametricPointTesterEllipse = new ParametricPointTester(
                (px, py) => Intersections.EllipseContainsPoint(centerX, centerY, radius1, radius2, angle, px, py),
                centerX - (radius1 < radius2 ? radius2 : radius2),
                centerY - (radius1 < radius2 ? radius2 : radius2),
                centerX + (radius1 < radius2 ? radius2 : radius2),
                centerY + (radius1 < radius2 ? radius2 : radius2),
                5, 5);
            var parametricPointTesterEllipseItem = new GraphicItem(parametricPointTesterEllipse, paperLikeStyle);

            var ellipse = new Ellipse(centerX, centerY, radius1, radius2, angle);
            var ellipseItem = new GraphicItem(ellipse, paperLikeStyle);

            var ellipticArc = new EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var ellipticArcItem = new GraphicItem(ellipticArc, paperLikeStyle);

            var ellipseNodes = new Contour(ellipticArc.ExtremePoints);
            var ellipseNodesItem = new GraphicItem(ellipseNodes, selectionStyle);

            Rectangle2D ellpticArcBounds = Measurements.EllipticalArcBounds(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var ellpticArcBoundsItem = new GraphicItem(ellpticArcBounds, selectionStyle);

            var angleLines = new Polyline(ellipticArc.StartPoint, ellipticArc.Center, ellipticArc.EndPoint);
            var angleLinesItem = new GraphicItem(angleLines, selectionStyle);

            var circularArc = new CircularArc(centerX, centerY, radius1, startAngle + angle, sweepAngle);
            var circularArcItem = new GraphicItem(circularArc, paperLikeStyle);

            Rectangle2D circularArcBounds = Measurements.CircularArcBounds(centerX, centerY, radius1, startAngle + angle, sweepAngle);
            var circularArcBoundsItem = new GraphicItem(circularArcBounds, selectionStyle);

            var testAngle = ellipticArc.ExtremeAngles;
            testAngle.Sort();

            var angleVisualizer = new AngleVisualizerTester(centerX, centerY, (radius1 < radius2 ? radius2 : radius1), testAngle, startAngle + angle, sweepAngle);
            var angleVisualizerItem = new GraphicItem(angleVisualizer, paperLikeStyle);

            vectorMap.Add(ellpticArcBoundsItem);
            vectorMap.Add(circularArcBoundsItem);
            vectorMap.Add(ellipseItem);
            vectorMap.Add(ellipticArcItem);
            vectorMap.Add(circularArcItem);
            vectorMap.Add(ellipseNodesItem);
            vectorMap.Add(angleLinesItem);
            vectorMap.Add(angleVisualizerItem);
            vectorMap.Add(parametricPointTesterEllipseItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void ParametricTesting2(VectorMap vectorMap)
        {
            double centerX = 100d;
            double centerY = 200d;
            double radius1 = 100d;
            double radius2 = 200d;
            double angle = -45d.ToRadians();
            double startAngle = -45d.ToRadians();
            double sweepAngle = 90d.ToRadians();

            var EllpticArcBounds = Measurements.EllipticalArcBounds(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var EllpticArcBoundsItem = new GraphicItem(EllpticArcBounds, selectionStyle);
            vectorMap.Add(EllpticArcBoundsItem);

            var ellipticArc = new EllipticalArc(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var ellipticArcItem = new GraphicItem(ellipticArc, paperLikeStyle);
            vectorMap.Add(ellipticArcItem);

            var parametricEllipticArc = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.EllipticalArc(x, y, w, h, a, startAngle, sweepAngle, t),
                (x, y, w, h, a, px, py) => Intersections.EllipticalArcContainsPoint(x, y, w, h, a, startAngle, sweepAngle, px, py),
                new Point2D(centerX, centerY), new Size2D(radius1, radius2), angle, 0);
            var parametricEllipticArcItem = new GraphicItem(parametricEllipticArc, paperLikeStyle);
            vectorMap.Add(parametricEllipticArcItem);

            var parametricEllpticArcBounds = Measurements.EllipticalArcBounds(centerX, centerY, radius1, radius2, angle, startAngle, sweepAngle);
            var parametricEllpticArcBoundsItem = new GraphicItem(parametricEllpticArcBounds, selectionStyle);
            vectorMap.Add(parametricEllipticArcItem);

            var parametricCircle = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.Circle(x, y, h, t),
                (x, y, w, h, a, px, py) => Intersections.CircleContainsPoint(x, y, h, px, py),
                new Point2D(100d, 200d), new Size2D(0d, 50d), 0, 0);
            var parametricCircleItem = new GraphicItem(parametricCircle, paperLikeStyle);
            vectorMap.Add(parametricCircleItem);

            var parametricCircleArc = new ParametricDelegateCurve(
                (x, y, w, h, a, t) => Interpolaters.CircularArc(x, y, h, 0d, 90d.ToRadians(), t),
                (x, y, w, h, a, px, py) => Intersections.CircularArcSectorContainsPoint(x, y, h, 0d, 90d.ToRadians(), px, py),
                new Point2D(150d, 150d), new Size2D(0d, 50d), 0, 0);
            var parametricCircleArcItem = new GraphicItem(parametricCircleArc, paperLikeStyle);
            vectorMap.Add(parametricCircleArcItem);

            var CircleArcBounds = Measurements.CircularArcBounds(100, 200, 50, 0d, 90d.ToRadians());
            var CircleArcBoundsItem = new GraphicItem(CircleArcBounds, selectionStyle);
            vectorMap.Add(CircleArcBoundsItem);

            var ellipseOne = new Ellipse(300, 300, 100, 50, 0);
            var ellipseOneItem = new GraphicItem(ellipseOne, solidGreenStyle)
            {
                Name = "Ellipse Intersecting One."
            };

            var ellipseTwo = new Ellipse(300, 350, 100, 50, 0);
            var ellipseTwoItem = new GraphicItem(ellipseTwo, solidGreenStyle)
            {
                Name = "Ellipse Intersecting One."
            };

            var ellipseIntersections = Intersections.Intersection(ellipseOne, ellipseTwo);
            var ellipseIntersectionNodesItem = new GraphicItem(new NodeRevealer(ellipseIntersections.Points, 5d), solidPurpleStyle);

            vectorMap.Add(ellipseOneItem);
            vectorMap.Add(ellipseTwoItem);
            vectorMap.Add(ellipseIntersectionNodesItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        public static void GridTests(VectorMap vectorMap, Color foreColor, Color backColor)
        {
            var mapStyles = new List<ShapeStyle>
            {
                new ShapeStyle( Brushes.Transparent, new Pen(SystemBrushes.ButtonFace)),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Cross, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DarkDownwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DarkHorizontal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DarkUpwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DarkVertical, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DashedDownwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DashedHorizontal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DashedUpwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DashedVertical, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DiagonalBrick, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DiagonalCross, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Divot, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DottedDiamond, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.DottedGrid, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.ForwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Horizontal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.HorizontalBrick, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LargeCheckerBoard, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LargeConfetti, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LargeGrid, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LightDownwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LightHorizontal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LightUpwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.LightVertical, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Max, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Min, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.NarrowHorizontal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.NarrowVertical, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.OutlinedDiamond, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent05, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent10, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent20, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent25, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent30, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent40, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent50, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent60, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent70, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent75, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent80, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Percent90, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Plaid, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Shingle, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.SmallConfetti, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.SmallGrid, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.SolidDiamond, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Sphere, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Trellis, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Vertical, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Wave, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.Weave, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.WideDownwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.WideUpwardDiagonal, foreColor, backColor))),
                new ShapeStyle( Brushes.Transparent, new Pen(new HatchBrush(HatchStyle.ZigZag, foreColor, backColor))),

                new ShapeStyle(Brushes.Violet ,new Pen(new HatchBrush(HatchStyle.ZigZag, foreColor, backColor)){DashStyle = DashStyle.Dash, Width = 3f}),

                new ShapeStyle(Brushes.Black, new Pen(Brushes.Violet)) { LineStyle = new LineStyle() { Dashstyle = LineDashStyle.Dot }  },
                new ShapeStyle(Brushes.White, new Pen(Brushes.Violet)),

                new ShapeStyle(Brushes.Violet, new Pen(foreColor){DashStyle = DashStyle.Solid, Width = 3f}),
                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 1 }, Width = 3f}),

                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Dash,  Width = 3f}),
                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1 }, Width = 3f}),

                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Dot, Width = 3f}),
                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 1, 1 }, Width = 3f}),

                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.DashDot, Width = 3f}),
                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1, 1, 1 }, Width = 3f}),

                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.DashDotDot, Width = 3f}),
                new ShapeStyle(Brushes.Violet   ,new Pen(foreColor){DashStyle = DashStyle.Custom, DashPattern = new float[] { 3, 1, 1, 1, 1, 1 }, Width = 3f}),

                new ShapeStyle(Brushes.Transparent   ,new Pen(new HatchBrush(HatchStyle.SmallCheckerBoard,Color.Pink,Color.Transparent)))
            };
            var rectangleGrid = new RectangleDCellGrid(50, 50, 350, 350, mapStyles.Count);
            foreach (var style in mapStyles)
            {
                vectorMap.Add(rectangleGrid[mapStyles.IndexOf(style)], style);
            }
        }

        #endregion

        #region Regular stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void TrianglePointingRight(VectorMap vectorMap)
        {
            var trianglePointingRight = new Triangle(new Point2D(10, 10), new Point2D(50, 50), new Point2D(10, 100));
            var triangleItem = new GraphicItem(trianglePointingRight, solidPinkStyle)
            {
                Name = "Triangle pointing right."
            };
            vectorMap.Add(triangleItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PaperPlaneTriangles(VectorMap vectorMap)
        {
            var PaperPlane = new Contour(new List<Point2D>() { new Point2D(20, 100), new Point2D(300, 60), new Point2D(40, 30) });
            var paperPlaneItem = new GraphicItem(PaperPlane, paperLikeStyle)
            {
                Name = "Paper Plane Triangle."
            };
            vectorMap.Add(paperPlaneItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PlainCircle(VectorMap vectorMap)
        {
            var circle = new Circle(new Point2D(200, 200), 100);
            var circleItem = new GraphicItem(circle, solidGreenStyle)
            {
                Name = "Circle."
            };
            vectorMap.Add(circleItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void CircleBounds(VectorMap vectorMap)
        {
            var circleB = new Circle(100, 200, 50);
            var circleBItem = new GraphicItem(circleB, paperLikeStyle);
            var circleBoundsItem = new GraphicItem(circleB.Bounds, selectionStyle);
            vectorMap.Add(circleBoundsItem);
            vectorMap.Add(circleBItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PlainSquare(VectorMap vectorMap)
        {
            var square = new Rectangle2D(new Point2D(100, 100), new Size2D(100, 100));
            var squareItem = new GraphicItem(square, solidGreenStyle)
            {
                Name = "Square."
            };
            vectorMap.Add(squareItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void PlainOval(VectorMap vectorMap)
        {
            var OvalVertical = new Oval(new Point2D(200, 200), new Size2D(100, 200));
            var ovalVerticalItem = new GraphicItem(OvalVertical, solidPurpleStyle)
            {
                Name = "Vertical Oval."
            };
            vectorMap.Add(ovalVerticalItem);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void QuadraticLength(VectorMap vectorMap)
        {
            var quadBezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            var quadBezierItem = new GraphicItem(quadBezier, solidLightGreenStyle);
            var quadBezierBoundsIthem = new GraphicItem(quadBezier.Bounds, selectionStyle);
            vectorMap.Add(quadBezierBoundsIthem);
            vectorMap.Add(quadBezierItem);
            StringBuilder quadBezierLengths = new StringBuilder();
            quadBezierLengths.AppendLine("Bezier arc length by segments: \t" + quadBezier.Length);
            quadBezierLengths.AppendLine("Bezier arc length by integral: \t" + quadBezier.Length);
            quadBezierLengths.AppendLine("Bezier arc length by Gauss-Legendre: \t" + quadBezier.Length);
            MessageBox.Show(quadBezierLengths.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectorMap"></param>
        public static void CubicBezierLength(VectorMap vectorMap)
        {
            var cubeBezier = new CubicBezier(new Point2D(40, 200), new Point2D(50, 300), new Point2D(90, 200), new Point2D(80, 300));
            var cubeBezierItem = new GraphicItem(cubeBezier, whiteishStyle);
            var cubeBezierBoundsItem = new GraphicItem(cubeBezier.Bounds, selectionStyle);
            vectorMap.Add(cubeBezierBoundsItem);
            vectorMap.Add(cubeBezierItem);
            StringBuilder cubeBezierLengths = new StringBuilder();
            cubeBezierLengths.AppendLine("Bezier arc length: \t" + cubeBezier.Length);
            MessageBox.Show(cubeBezierLengths.ToString());
        }

        #endregion
    }
}
