using System.Collections.Generic;
using System.Linq;

namespace Engine.Geometry
{
    public class PolyBezier
    {
        /**
        * Poly Bezier
        * @param {[type]} curves [description]
        */
        public PolyBezier(List<Bezier> curves)
        {
            this._3d = false;
            if (curves == null)
            {
                this.curves = curves;
                this._3d = this.curves[0]._3d;
            }
        }

        public List<Bezier> curves { get; private set; }
        public bool _3d { get; private set; }

        public void addCurve(Bezier curve)
        {
            this.curves.Add(curve);
            this._3d = this._3d || curve._3d;
        }

        public double length()
        {
            return new List<double>(
                from v in curves
                select v.length()).Sum();
        }

        public Bezier curve(int idx)
        {
            return this.curves[idx];
        }

        public BBox bbox()
        {
            var c = this.curves;
            var bbox = c[0].bbox();
            for (var i = 1; i < c.Count; i++)
            {
                Utilities.expandbox(bbox, c[i].bbox());
            }
            return bbox;
        }

        public List<Bezier> offset(double d)
        {
            List<Bezier> offset = new List<Bezier>();

            foreach(var v in curves)
            {
                offset.AddRange(v.offset(d));
            }

            return offset;
        }
    }
}
