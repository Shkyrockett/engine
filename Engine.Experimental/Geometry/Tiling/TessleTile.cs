﻿using System;
using System.Collections.Generic;

namespace Engine.Experimental
{
    /// <summary>
    /// The Tessle tile class.
    /// </summary>
    /// <remarks>
    /// <para>http://mathstat.slu.edu/escher/index.php/Tessellations_by_Recognizable_Figures
    /// http://www.tessellations.org/tess-symmetry7.shtml
    /// http://www.eschertile.com/</para>
    /// </remarks>
    public class TessleTile
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TessleTile"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="heesch">The heesch.</param>
        /// <param name="alterations">The alterations.</param>
        public TessleTile(Point2D center, double scale, double rotation, HeeschTiling heesch, params (double t, double offset)[][] alterations)
        {
            Center = center;
            Scale = scale;
            Rotation = rotation;
            Heesch = heesch;
            Alterations = alterations;
            var shape = Shape;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public Point2D Center { get; set; }

        /// <summary>
        /// Gets or sets the scale of the tile.
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// Gets or sets the Heesch tiling code.
        /// </summary>
        public HeeschTiling Heesch { get; set; }

        /// <summary>
        /// Gets or sets the alterations.
        /// </summary>
        public (double t, double offset)[][] Alterations { get; set; }

        /// <summary>
        /// Gets the shape.
        /// </summary>
        public TileShape Shape
        {
            get
            {
                // Symmetries 
                // T: Translation
                // G: Glide and Reflection
                // C: Rotation
                switch (Heesch)
                {
                    // Triangular
                    case HeeschTiling.CCC: // Three rotations.
                    case HeeschTiling.CC3C3:
                    case HeeschTiling.CC4C4:
                    case HeeschTiling.CC6C6:
                    case HeeschTiling.CGG:
                        return TileShape.Triangular;
                    // Quadrilateral
                    case HeeschTiling.TTTT: // Double translation.
                    case HeeschTiling.CCCC: // Four rotations.
                    case HeeschTiling.TCTC:
                    case HeeschTiling.C3C3C3C3:
                    case HeeschTiling.C4C4C4C4:
                    case HeeschTiling.C3C3C6C6:
                    case HeeschTiling.CCGG:
                    case HeeschTiling.TGTG:
                    case HeeschTiling.CGCG:
                    case HeeschTiling.G1G1G2G2:
                    case HeeschTiling.G1G2G1G2:
                        return TileShape.Quadrilateral;
                    // Pentagonal
                    case HeeschTiling.TCTCC:
                    case HeeschTiling.TCTGG:
                    case HeeschTiling.CC3C3C6C6:
                    case HeeschTiling.CC4C4C4C4:
                    case HeeschTiling.CG1G2G1G2:
                        return TileShape.Pentagonal;
                    // Hexagonal
                    case HeeschTiling.TTTTTT: // Triple translation.
                    case HeeschTiling.TCCTCC:
                    case HeeschTiling.TG1G1TG2G2:
                    case HeeschTiling.TG1G2TG2G1:
                    case HeeschTiling.TCCTGG:
                    case HeeschTiling.C3C3C3C3C3C3:
                    case HeeschTiling.CG1CG2G1G2:
                        return TileShape.Hexagonal;
                    // Unknown
                    default:
                        return TileShape.Unknown;
                }
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Build the polyline.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="alterations">The alterations.</param>
        /// <returns>The <see cref="Polyline"/>.</returns>
        private static Point2D[] BuildPolyline(Point2D start, double length, Vector2D direction, (double t, double offset)[] alterations)
        {
            // ToDo: This is a rough guess of what needs to happen. This needs to be tested and corrected.
            var points = new Point2D[alterations.Length + 1];
            points[0] = start;
            var end = Interpolators.Linear(1, start, (Point2D)(direction * length));
            var i = 1;
            foreach (var (t, offset) in alterations)
            {
                var point = Interpolators.Linear(t, start, (Point2D)(direction * length));
                points[i++] = point + (start.CrossProduct(end) * offset);
            }
            points[points.Length - 1] = end;
            return points;
        }

        /// <summary>
        /// Build the polygon contour.
        /// </summary>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        private PolygonContour2D BuildPolygonContour()
        {
            // ToDo: This is a rough guess of what needs to happen. This needs to be tested and corrected.
            var corners = (int)Shape;
            var start = new Point2D();
            var polygon = new List<Point2D>();
            foreach (var set in Alterations)
            {
                // ToDo: Figure out how to find the corners and side lengths.
                var len = 1d;
                var dir = new Vector2D();

                var polyline = new Span<Point2D>(BuildPolyline(start, len, dir, set));
                polygon.AddRange(polyline.Slice(1).ToArray());
                start = polyline[polyline.Length - 1];
            }
            return new PolygonContour2D(polygon);
        }
        #endregion Methods
    }
}
