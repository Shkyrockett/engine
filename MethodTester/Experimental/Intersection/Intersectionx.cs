// <copyright file="Intersection.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// Geometry Intersection Return Structure
    /// </summary>
    /// <structure>Engine.Geometry.Intersection</structure>
    /// <remarks></remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Intersectionx))]
    public class Intersectionx
    {
        /// <summary>
        /// 
        /// </summary>
        public Intersectionx()
            :this(false,new Point2D[] { Point2D.Empty })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isIntersection"></param>
        /// <param name="intersectionPoint"></param>
        public Intersectionx(bool isIntersection, Point2D[] intersectionPoint)
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
        public Intersectionx(Point2D[] Locations, bool Intersects, IntersectionState Type)
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
        public Intersectionx(Point2D[] Locations, bool Intersects, bool Paralell)
        {
            IntersectionPoint = Locations;
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
                Type = IntersectionState.Parallel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Paralell"></param>
        /// <remarks></remarks>
        public Intersectionx(Point2D Locations, bool Intersects, bool Paralell)
        {
            IntersectionPoint = new Point2D[] { Locations };
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
                Type = IntersectionState.Parallel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Type"></param>
        /// <remarks></remarks>
        public Intersectionx(Point2D Locations, bool Intersects, IntersectionState Type)
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
        public IntersectionState Type { get; set; }
    }
}
