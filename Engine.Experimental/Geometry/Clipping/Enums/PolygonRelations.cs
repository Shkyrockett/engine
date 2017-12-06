// <copyright file="PolygonRelations.cs" >
//     Copyright © 2012 Francisco Martínez del Río. All rights reserved.
// </copyright>
// <author id="fmartin@ujaen.es">Francisco Martínez del Río</author>
// <license>
//     This code is public domain.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// An enumeration indicating whether a clipping polygon is the subject or clipping polygon.
    /// </summary>
    public enum PolygonRelations
        : byte
    {
        /// <summary>
        /// The geometry is the subject.
        /// </summary>
        Subject,

        /// <summary>
        /// The geometry is the clipping object.
        /// </summary>
        Clipping
    }
}