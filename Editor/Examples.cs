// <copyright file="Examples.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
        public static Ellipse Ellipse = new Ellipse(new Point2D(200, 200), 50, 25, 45d.ToRadians());
        
        /// <summary>
        /// 
        /// </summary>
        public static EllipticalArc EllpticArc = new EllipticalArc(200d, 200d, 100d, 200d, 45d.ToRadians(), -45d.ToRadians(), 90d.ToRadians());
        
        /// <summary>
        /// 
        /// </summary>
        public static Shape InnerPolygon = new PolygonContour( // First inner triangle
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
        public static Polygon PolySet = new Polygon(
            new List<PolygonContour>(
                new List<PolygonContour> {
                    new PolygonContour( // Boundary
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
                    new PolygonContour( // First inner triangle
                        new List<Point2D> {
                            new Point2D(20, 100),
                            new Point2D(175, 60),
                            new Point2D(40, 30)
                        }
                    ),
                    new PolygonContour( // Second inner triangle
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
        public static Polyline PolyTriangle = new Polyline(new List<Point2D> { new Point2D(10, 40), new Point2D(80, 30), new Point2D(100, 60) });
    }
}
