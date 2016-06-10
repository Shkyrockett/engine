using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IIteratablePathElement
    {
        /// <summary>
        /// 
        /// </summary>
        double Length { get; }

        /// <summary>
        /// 
        /// </summary>
        double InterpolationMin { get; }

        /// <summary>
        /// 
        /// </summary>
        double InterpolationMax { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Point2D Iterpolate(double t);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Point2D> IterpolationChain();
    }
}
