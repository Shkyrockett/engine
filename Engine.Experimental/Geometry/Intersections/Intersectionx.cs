// <copyright file="Intersection.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Geometry Intersection Return Structure
    /// </summary>
    /// <structure>Engine.Geometry.Intersection</structure>
    [DataContract, Serializable]
    [GraphicsObject]
    public class Intersectionx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        public Intersectionx()
            : this(false, new Point2D[] { Point2D.Empty })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        /// <param name="isIntersection">The isIntersection.</param>
        /// <param name="intersectionPoint">The intersectionPoint.</param>
        public Intersectionx(bool isIntersection, Point2D[] intersectionPoint)
        {
            Itersecting = isIntersection;
            IntersectionPoint = intersectionPoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        /// <param name="Locations">The Locations.</param>
        /// <param name="Intersects">The Intersects.</param>
        /// <param name="Type">The Type.</param>
        public Intersectionx(Point2D[] Locations, bool Intersects, IntersectionStates Type)
        {
            IntersectionPoint = Locations;
            Itersecting = Intersects;
            Paralell = false;
            this.Type = Type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        /// <param name="Locations">The Locations.</param>
        /// <param name="Intersects">The Intersects.</param>
        /// <param name="Parallel">The Parallel.</param>
        public Intersectionx(Point2D[] Locations, bool Intersects, bool Parallel)
        {
            IntersectionPoint = Locations;
            Itersecting = Intersects;
            Paralell = Parallel;
            if (Parallel)
            {
                Type = IntersectionStates.Parallel;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        /// <param name="Locations">The Locations.</param>
        /// <param name="Intersects">The Intersects.</param>
        /// <param name="Parallel">The Parallel.</param>
        public Intersectionx(Point2D Locations, bool Intersects, bool Parallel)
        {
            IntersectionPoint = new Point2D[] { Locations };
            Itersecting = Intersects;
            Paralell = Parallel;
            if (Parallel)
            {
                Type = IntersectionStates.Parallel;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersectionx"/> class.
        /// </summary>
        /// <param name="Locations">The Locations.</param>
        /// <param name="Intersects">The Intersects.</param>
        /// <param name="Type">The Type.</param>
        public Intersectionx(Point2D Locations, bool Intersects, IntersectionStates Type)
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
        public bool Itersecting { get; set; }

        /// <summary>
        /// Returns of the point(s) of Intersection
        /// </summary>
        public Point2D[] IntersectionPoint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool Paralell { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public IntersectionStates Type { get; set; }
    }
}
