// <copyright file="Intersection.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;

namespace Engine.Geometry
{
    /// <summary>
    /// Geometry Intersection Return Structure
    /// </summary>
    /// <structure>Engine.Geometry.Intersection</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Intersection))]
    public class Intersection
    {
        /// <summary>
        /// 
        /// </summary>
        public Intersection()
        {
            Itersecting = false;
            IntersectionPoint = new Point2D[] { Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isIntersection"></param>
        /// <param name="intersectionPoint"></param>
        public Intersection(bool isIntersection, Point2D[] intersectionPoint)
        {
            Itersecting = isIntersection;
            IntersectionPoint = intersectionPoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Type"></param>
        /// <remarks></remarks>
        public Intersection(Point2D[] Locations, bool Intersects, IntersectionType Type)
        {
            IntersectionPoint = Locations;
            Itersecting = Intersects;
            Paralell = false;
            this.Type = Type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Paralell"></param>
        /// <remarks></remarks>
        public Intersection(Point2D[] Locations, bool Intersects, bool Paralell)
        {
            IntersectionPoint = Locations;
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
                Type = IntersectionType.Parallel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Paralell"></param>
        /// <remarks></remarks>
        public Intersection(Point2D Locations, bool Intersects, bool Paralell)
        {
            IntersectionPoint = new Point2D[] { Locations };
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
                Type = IntersectionType.Parallel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Type"></param>
        /// <remarks></remarks>
        public Intersection(Point2D Locations, bool Intersects, IntersectionType Type)
        {
            IntersectionPoint = new Point2D[] {
                     Locations};
            Paralell = false;
            Itersecting = Intersects;
            this.Type = Type;
        }

        /// <summary>
        /// Return Value of whether an intersection occurred
        /// </summary>
        /// <remarks></remarks>
        public bool Itersecting { get; set; }

        /// <summary>
        /// Returns of the point(s) of Intersection
        /// </summary>
        /// <remarks></remarks>
        public Point2D[] IntersectionPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Paralell { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IntersectionType Type { get; set; }
    }
}
