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
    /// The IDistortion interface.
    /// </summary>
    public interface IDistortion
    {
        /// <summary>
        /// The distort.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        Point2D Distort(PolycurveContour source, Point2D point);
    }
}
