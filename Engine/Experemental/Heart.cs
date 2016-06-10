using System;
using System.ComponentModel;

namespace Engine.Geometry
{
    /// <summary>
    /// http://csharphelper.com/blog/2016/02/draw-parametric-heart-shaped-curve-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Heart))]
    public class Heart
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "Heart";
    }
}
