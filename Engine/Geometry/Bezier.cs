using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Bezierx
    {


        /**
         * Bezier curve constructor. The constructor argument can be one of three things:
         *
         * 1. array/4 of {x:..., y:..., z:...}, z optional
         * 2. numerical array/8 ordered x1,y1,x2,y2,x3,y3,x4,y4
         * 3. numerical array/12 ordered x1,y1,z1,x2,y2,z2,x3,y3,z3,x4,y4,z4
         *
         */
        var Bezier = function(coords) {
    var args = (coords && coords.forEach) ? coords : [].slice.call(arguments);
        var coordlen = false;
    if(typeof args[0] === "object") {
      coordlen = args.length;
      var newargs = [];
        args.forEach(function(point)
        {
        ['x','y','z'].forEach(function(d)
        {
            if (typeof point[d] !== "undefined")
            {
                newargs.push(point[d]);
            }
        });
      });
      args = newargs;
    }
var higher = false;
var len = args.length;
    if (coordlen) {
      if(coordlen>4) {
        if (arguments.length !== 1) {
          throw new Error("Only new Bezier(point[]) is accepted for 4th and higher order curves");
        }
        higher = true;
      }
    } else {
      if(len!==6 && len!==8 && len!==9 && len!==12) {
        if (arguments.length !== 1) {
          throw new Error("Only new Bezier(point[]) is accepted for 4th and higher order curves");
        }
      }
    }
    var _3d = (!higher && (len === 9 || len === 12)) || (coords && coords[0] && typeof coords[0].z !== "undefined");
    this._3d = _3d;
    var points = [];
    for(var idx = 0, step= (_3d ? 3 : 2); idx<len; idx+=step) {
    var point = {
        x: args[idx],
        y: args[idx + 1]
         };
      if(_3d) { point.z = args[idx + 2] };
points.push(point);
}
    this.order = points.length - 1;
    this.points = points;
    var dims = ['x', 'y'];
    if(_3d) dims.push('z');
    this.dims = dims;
    this.dimlen = dims.length;
    (function(curve) {
    var a = utils.align(points, { p1: points[0], p2: points[curve.order]});
    for (var i = 0; i < a.length; i++)
    {
        if (abs(a[i].y) > 0.0001)
        {
            curve._linear = false;
            return;
        }
    }
    curve._linear = true;
}(this));
    this._t1 = 0;
    this._t2 = 1;
    this.update();
};


    }
}
