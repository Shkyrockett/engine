using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// The poly bezier2 class.
    /// </summary>
    public class PolyBezier2
    {
        /// <summary>
        /// Poly Bezier
        /// </summary>
        /// <param name="curves"></param>
        public PolyBezier2(List<Bezier> curves)
        {
            Is3d = false;
            if (curves != null)
            {
                Curves = curves;
                Is3d = Curves[0].Is3d;
            }
        }

        /// <summary>
        /// Gets the curves.
        /// </summary>
        public List<Bezier> Curves { get; }

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool Is3d { get; private set; }

        /// <summary>
        /// Add the curve.
        /// </summary>
        /// <param name="curve">The curve.</param>
        public void AddCurve(Bezier curve)
        {
            Curves.Add(curve);
            Is3d = Is3d || curve.Is3d;
        }

        /// <summary>
        /// The length.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public double Length()
            => new List<double>(
            from v in Curves
            select v.Length).Sum();

        /// <summary>
        /// The curve.
        /// </summary>
        /// <param name="idx">The idx.</param>
        /// <returns>The <see cref="Bezier"/>.</returns>
        public Bezier Curve(int idx)
            => Curves[idx];

        /// <summary>
        /// The bbox.
        /// </summary>
        /// <returns>The <see cref="BBox"/>.</returns>
        public BBox Bbox()
        {
            var c = Curves;
            var bbox = c[0].Bbox();
            for (var i = 1; i < c.Count; i++)
                Expandbox(bbox, c[i].Bbox());
            return bbox;
        }

        /// <summary>
        /// The expandbox.
        /// </summary>
        /// <param name="bbox">The bbox.</param>
        /// <param name="bBox">The bBox.</param>
        private static void Expandbox(BBox bbox, BBox bBox)
            => throw new NotImplementedException();

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="T:List{Bezier}"/>.</returns>
        public List<Bezier> Offset(double d)
        {
            var offset = new List<Bezier>();

            foreach(Bezier v in Curves)
                offset.AddRange(v.Offset(d));

            return offset;
        }
    }
}
