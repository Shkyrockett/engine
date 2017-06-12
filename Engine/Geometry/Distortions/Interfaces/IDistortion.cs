// <copyright file="IDistortion.cs" >
//    Copyright © 2017 Ben Morris. All rights reserved.
// </copyright>
// <author id="benmorris44">Ben Morris</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/benmorris44/EnvelopeDistortion </remarks>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDistortion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        Point2D Distort(PolycurveContour source, Point2D point);
    }
}
