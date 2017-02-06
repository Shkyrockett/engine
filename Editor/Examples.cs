// <copyright file="Examples.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using System.Collections.Generic;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public class Examples
    {
        /// <summary>
        /// 
        /// </summary>
        public static CircularArc Arc = new CircularArc(new Point2D(100, 100), 100, 60d.ToRadians(), 300d.ToRadians());
        
        /// <summary>
        /// 
        /// </summary>
        public static Circle Circle = new Circle(new Point2D(200, 200), 100);
        
        /// <summary>
        /// 
        /// </summary>
        public static Ellipse Ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45d.ToRadians());
        
        /// <summary>
        /// 
        /// </summary>
        public static EllipticalArc EllpticArc = new EllipticalArc(200d, 200d, 100d, 200d, 45d.ToRadians(), -45d.ToRadians(), 90d.ToRadians());
        
        /// <summary>
        /// 
        /// </summary>
        public static Shape InnerPolygon = new Polygon( // First inner triangle
                        new List<Point2D> {
                            new Point2D(20, 100),
                            new Point2D(175, 60),
                            new Point2D(40, 30)
                        }
                    ).Offset(10);
        
        /// <summary>
        /// 
        /// </summary>
        public static LineSegment Line = new LineSegment(new Point2D(160, 250), new Point2D(130, 145));
        
        /// <summary>
        /// 
        /// </summary>
        public static Oval OvalVertical = new Oval(new Point2D(200, 200), new Size2D(100, 200));
        
        /// <summary>
        /// 
        /// </summary>
        public static Polygon PaperPlane = new Polygon(new List<Point2D>() { new Point2D(20, 100), new Point2D(300, 60), new Point2D(40, 30) });
        
        /// <summary>
        /// 
        /// </summary>
        public static PolygonSet PolySet = new PolygonSet(
            new List<Polygon>(
                new List<Polygon> {
                    new Polygon( // Boundary
                        new List<Point2D> {
                            new Point2D(10, 10),
                            new Point2D(300, 10),
                            new Point2D(300, 300),
                            new Point2D(10, 300),
                            // Cut out
                            new Point2D(10, 200),
                            new Point2D(200, 80),
                            new Point2D(10, 150)
                        }
                    ),
                    new Polygon( // First inner triangle
                        new List<Point2D> {
                            new Point2D(20, 100),
                            new Point2D(175, 60),
                            new Point2D(40, 30)
                        }
                    ),
                    new Polygon( // Second inner triangle
                        new List<Point2D> {
                            new Point2D(250, 150),
                            new Point2D(150, 150),
                            new Point2D(250, 200)
                        }
                    )
                }
            )
        );
        
        /// <summary>
        /// 
        /// </summary>
        public static Polyline PolyTriangle = new Polyline(new List<Point2D>() { new Point2D(10, 40), new Point2D(80, 30), new Point2D(100, 60) });
        
        /// <summary>
        /// 
        /// </summary>
        public static Polygon PolygonTriangleA = new Polygon(new List<Point2D> { (300, 0), (600, 450), (0, 450) });
        
        /// <summary>
        /// 
        /// </summary>
        public static Polygon PolygonTriangleB = new Polygon(new List<Point2D> { (0, 150), (600, 150), (300, 600) });
        
        /// <summary>
        /// 
        /// </summary>
        public static Rectangle2D Square = new Rectangle2D(new Point2D(100, 100), new Size2D(100, 100));
        
        /// <summary>
        /// 
        /// </summary>
        public static Triangle TrianglePointingRight = new Triangle(new Point2D(10, 10), new Point2D(50, 50), new Point2D(10, 100));
    }
}
