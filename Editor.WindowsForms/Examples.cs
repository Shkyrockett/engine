﻿// <copyright file="Examples.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;

namespace Editor;

/// <summary>
/// The examples class.
/// </summary>
public static class Examples
{
    /// <summary>
    /// The arc.
    /// </summary>
    public static readonly CircularArc2D Arc = new(new Point2D(100, 100), 100, 60d.DegreesToRadians(), 300d.DegreesToRadians());

    /// <summary>
    /// The ellipse.
    /// </summary>
    public static readonly Ellipse2D Ellipse = new(new Point2D(200, 200), 50, 25, 45d.DegreesToRadians());

    /// <summary>
    /// The elliptic arc.
    /// </summary>
    public static readonly EllipticalArc2D EllpticArc = new(200d, 200d, 100d, 200d, 45d.DegreesToRadians(), -45d.DegreesToRadians(), 90d.DegreesToRadians());

    /// <summary>
    /// The inner polygon.
    /// </summary>
    public static readonly Shape2D InnerPolygon = new PolygonContour2D( // First inner triangle
                    new List<Point2D> {
                        new(20, 100),
                        new(175, 60),
                        new(40, 30)
                    }
                ).Offset(10);

    /// <summary>
    /// The line.
    /// </summary>
    public static readonly LineSegment2D Line = new(new Point2D(160, 250), new Point2D(130, 145));

    /// <summary>
    /// The poly set.
    /// </summary>
    public static readonly Polygon2D PolySet = new(
        new List<PolygonContour2D>(
            new List<PolygonContour2D> {
                new( // Boundary
                    new List<Point2D> {
                        new(10, 10),
                        new(300, 10),
                        new(300, 300),
                        new(10, 300),
                        // Cut out
                        new(10, 200),
                        new(200, 80),
                        new(10, 150)
                    }
                ),
                new( // First inner triangle
                    new List<Point2D> {
                        new(20, 100),
                        new(175, 60),
                        new(40, 30)
                    }
                ),
                new( // Second inner triangle
                    new List<Point2D> {
                        new(250, 150),
                        new(150, 150),
                        new(250, 200)
                    }
                )
            }
        )
    );

    /// <summary>
    /// The poly triangle.
    /// </summary>
    public static readonly Polyline2D PolyTriangle = new(new List<Point2D> { new(10, 40), new(80, 30), new(100, 60) });

    /// <summary>
    /// Intersections test case of a star of David with inside coincident to other outside.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) StarOfDavidWithInsideCoincidentToOtherOutside =
    (
        new Polygon2D {
            Generators.RegularConvexPolygon(100, 100, 100, 3, -Floats<double>.Hau),
            Generators.RegularConvexPolygon(100, 100, 100 * 0.5d, 3, -Floats<double>.Hau),
        },
        new Polygon2D {
            Generators.RegularConvexPolygon(100, 100, 100, 3, Floats<double>.Hau),
            Generators.RegularConvexPolygon(100, 100, 100 * 0.5d, 3, Floats<double>.Hau),
        }
    );

    /// <summary>
    /// Intersections test case of simple rectangles. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) SimpleRectangles =
    (
        new Polygon2D {
            new PolygonContour2D((200, 50), (600, 50), (600, 150), (200, 150)),
        },
        new Polygon2D {
            new PolygonContour2D((300, 150), (500, 150), (500, 200), (300, 200)),
        }
    );

    /// <summary>
    /// Intersections test case of coincident self intersection. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) CoincidentSelfIntersection =
    (
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (320, 150), (260, 210), (200, 150), (200, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (460, 190), (460, 110), (400, 180), (70,  90)),
            new PolygonContour2D((220, 170), (580, 130), (310, 160), (310, 210), (260, 170), (240, 190)),
        }
    );

    /// <summary>
    /// Intersections test case of coincident self intersection, pt 2. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) CoincidentSelfIntersectionPt2 =
    (
        new Polygon2D {
            new PolygonContour2D((100, 100), (200, 200), (300, 100)),
            new PolygonContour2D((200, 100), (300, 200), (400, 100)),
        },
        new Polygon2D {
            new PolygonContour2D((50, 50), (200, 50), (300, 150)),
        }
    );

    /// <summary>
    /// Intersections test case of assorted polygons. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) AssortedPolygons =
    (
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (320, 150), (260, 210), (200, 150), (200, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (460, 190), (460, 110), (400, 180), (160, 90)),
            new PolygonContour2D((220, 170), (260, 30), (310, 160), (310, 210), (260, 170), (240, 190)),
        }
    );

    /// <summary>
    /// Intersections test case of shared right edge. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) SharedRightEdge =
    (
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (100, 150), (100, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (350, 230), (300, 180), (490, 60)),
        }
    );

    /// <summary>
    /// Intersections test case of simple boxes. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) SimpleBoxes =
    (
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (100, 150), (100, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (280, 150), (280, 60)),
        }
    );

    /// <summary>
    /// Intersections test case of simple self overlap. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) SimpleSelfOverlap =
    (
        new Polygon2D {
            new PolygonContour2D((100, 50), (300, 50), (300, 150), (100, 150)),
        },
        new Polygon2D {
            new PolygonContour2D((300, 150), (400, 150), (200, 50), (300, 50)),
        }
    );

    /// <summary>
    /// Intersections test case of M shape. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) MShape =
    (
        new Polygon2D {
            new PolygonContour2D((570, 60), (570, 150), (60, 150), (60, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 130), (330, 20), (180, 130), (120, 60)),
        }
    );

    /// <summary>
    /// Intersections test case of two triangles with common edge. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) TwoTrianglesWithCommonEdge =
    (
        new Polygon2D {
            new PolygonContour2D((620, 60), (620, 150), (90, 150), (90, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((350, 60), (480, 200), (180, 60)),
            new PolygonContour2D((180, 60), (500, 60), (180, 220)),
        }
    );

    /// <summary>
    /// Intersections test case of two triangles with common edge pt. 2. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) TwoTrianglesWithCommonEdgePt2 =
    (
        new Polygon2D {
            new PolygonContour2D((620, 60), (620, 150), (90, 150), (90, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((400, 60), (270, 120), (210, 60)),
            new PolygonContour2D((210, 60), (530, 60), (210, 220)),
        }
    );

    /// <summary>
    /// Intersections test case of two triangles with common edge pt. 3. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) TwoTrianglesWithCommonEdgePt3 =
    (
        new Polygon2D {
            new PolygonContour2D((620, 60), (620, 150), (90, 150), (90, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((370, 60), (300, 220), (560, 60)),
            new PolygonContour2D((180, 60), (500, 60), (180, 220)),
        }
    );

    /// <summary>
    /// Intersections test case of three triangles. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) ThreeTriangles =
    (
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (320, 150)),
        },
        new Polygon2D {
            new PolygonContour2D((500, 60), (500, 150), (460, 190)),
            new PolygonContour2D((220, 170), (260, 30), (310, 160)),
            new PolygonContour2D((260, 210), (200, 150), (200, 60)),
        }
    );

    /// <summary>
    /// Intersections test case of adjacent edges in status. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) AdjacentEdgesInStatus =
    (
        new Polygon2D {
            new PolygonContour2D((620, 60), (620, 150), (90, 150), (90, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((110, 60), (420, 230), (540, 60)),
            new PolygonContour2D((180, 60), (430, 160), (190, 200)),
        }
    );

    /// <summary>
    /// Intersections test case of triple overlap. From: https://rawgit.com/voidqk/polybooljs/master/dist/demo.html.
    /// </summary>
    public static readonly (Polygon2D poly1, Polygon2D poly2) TripleOverlap =
    (
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (220, 150), (160, 210), (100, 150), (100, 60)),
        },
        new Polygon2D {
            new PolygonContour2D((400, 60), (400, 150), (270, 60), (210, 60), (300, 180), (130, 60)),
            new PolygonContour2D((160, 60), (310, 60), (210, 160), (210, 110), (160, 170), (140, 190)),
        }
    );
}
