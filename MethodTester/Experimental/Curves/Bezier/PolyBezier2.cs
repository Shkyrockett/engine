using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 
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
            if (curves == null)
            {
                Curves = curves;
                Is3d = Curves[0].Is3d;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Bezier> Curves { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Is3d { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        public void AddCurve(Bezier curve)
        {
            Curves.Add(curve);
            Is3d = Is3d || curve.Is3d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Length()
            => new List<double>(
            from v in Curves
            select v.Length()).Sum();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Bezier Curve(int idx)
            => Curves[idx];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BBox Bbox()
        {
            List<Bezier> c = Curves;
            BBox bbox = c[0].Bbox();
            for (var i = 1; i < c.Count; i++)
                Expandbox(bbox, c[i].Bbox());
            return bbox;
        }

        private void Expandbox(BBox bbox, BBox bBox) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<Bezier> Offset(double d)
        {
            var offset = new List<Bezier>();

            foreach(Bezier v in Curves)
                offset.AddRange(v.Offset(d));

            return offset;
        }
    }
}
