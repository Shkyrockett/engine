using System.Collections.Generic;
using System.Linq;
using static Engine.Geometry.Utilities;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class PolyBezier
    {
        /// <summary>
        /// Poly Bezier
        /// </summary>
        /// <param name="curves"></param>
        public PolyBezier(List<Bezier> curves)
        {
            _3d = false;
            if (curves == null)
            {
                this.curves = curves;
                _3d = this.curves[0]._3d;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Bezier> curves { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool _3d { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        public void addCurve(Bezier curve)
        {
            curves.Add(curve);
            _3d = _3d || curve._3d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double length()
            => new List<double>(
            from v in curves
            select v.length()).Sum();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Bezier curve(int idx)
            => curves[idx];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BBox bbox()
        {
            List<Bezier> c = curves;
            BBox bbox = c[0].bbox();
            for (int i = 1; i < c.Count; i++)
                expandbox(bbox, c[i].bbox());
            return bbox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<Bezier> offset(double d)
        {
            var offset = new List<Bezier>();

            foreach(Bezier v in curves)
                offset.AddRange(v.offset(d));

            return offset;
        }
    }
}
