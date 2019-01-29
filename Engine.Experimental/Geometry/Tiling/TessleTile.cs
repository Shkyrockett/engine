using System;
using System.Collections.Generic;

namespace Engine.Experimental
{
    /// <summary>
    /// The Tessle tile class.
    /// </summary>
    /// <remarks>
    /// http://mathstat.slu.edu/escher/index.php/Tessellations_by_Recognizable_Figures
    /// http://www.tessellations.org/tess-symmetry7.shtml
    /// http://www.eschertile.com/
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
        public TessleTile(Point2D center, double scale, double rotation, HeeschTilings heesch, params (double t, double offset)[][] alterations)
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
        public HeeschTilings Heesch { get; set; }

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
                    case HeeschTilings.CCC: // Three rotations.
                    case HeeschTilings.CC3C3:
                    case HeeschTilings.CC4C4:
                    case HeeschTilings.CC6C6:
                    case HeeschTilings.CGG:
                        return TileShape.Triangular;
                    // Quadrilateral
                    case HeeschTilings.TTTT: // Double translation.
                    case HeeschTilings.CCCC: // Four rotations.
                    case HeeschTilings.TCTC:
                    case HeeschTilings.C3C3C3C3:
                    case HeeschTilings.C4C4C4C4:
                    case HeeschTilings.C3C3C6C6:
                    case HeeschTilings.CCGG:
                    case HeeschTilings.TGTG:
                    case HeeschTilings.CGCG:
                    case HeeschTilings.G1G1G2G2:
                    case HeeschTilings.G1G2G1G2:
                        return TileShape.Quadrilateral;
                    // Pentagonal
                    case HeeschTilings.TCTCC:
                    case HeeschTilings.TCTGG:
                    case HeeschTilings.CC3C3C6C6:
                    case HeeschTilings.CC4C4C4C4:
                    case HeeschTilings.CG1G2G1G2:
                        return TileShape.Pentagonal;
                    // Hexagonal
                    case HeeschTilings.TTTTTT: // Triple translation.
                    case HeeschTilings.TCCTCC:
                    case HeeschTilings.TG1G1TG2G2:
                    case HeeschTilings.TG1G2TG2G1:
                    case HeeschTilings.TCCTGG:
                    case HeeschTilings.C3C3C3C3C3C3:
                    case HeeschTilings.CG1CG2G1G2:
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
        private static Point2D[]BuildPolyline(Point2D start, double length, Vector2D direction, (double t, double offset)[] alterations)
        {
            // ToDo: This is a rough guess of what needs to happen. This needs to be tested and corrected.
            var points = new Point2D[alterations.Length + 1];
            points[0] = start;
            var end = Interpolators.Linear(start, (Point2D)(direction * length), 1);
            var i = 1;
            foreach (var (t, offset) in alterations)
            {
                var point = Interpolators.Linear(start, (Point2D)(direction * length), t);
                points[i++] = point + (start.CrossProduct(end) * offset);
            }
            points[points.Length - 1] = end;
            return points;
        }

        /// <summary>
        /// Build the polygon contour.
        /// </summary>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        private PolygonContour BuildPolygonContour()
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
                start = polyline[polyline.Length-1];
            }
            return new PolygonContour(polygon);
        }
        #endregion Methods
    }
}
