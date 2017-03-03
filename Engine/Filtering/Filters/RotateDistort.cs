// <copyright file="RotateDistort.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class RotateDistort
        : PreservingFilter
    {
        #region Fields

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [NonSerialized()]
        protected Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        /// <summary>
        /// 
        /// </summary>
        private Point2D center;

        /// <summary>
        /// 
        /// </summary>
        private double angle;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="angle"></param>
        public RotateDistort(Point2D center, double angle)
        {
            this.center = center;
            Angle = angle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Point2D Center
        {
            get { return center; }
            set
            {
                center = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D XAxis
            => (Point2D)CachingProperty(() => new Point2D(Math.Cos(Angle), Math.Sin(Angle)));

        /// <summary>
        /// 
        /// </summary>
        public Point2D YAxis
            => (Point2D)CachingProperty(() => new Point2D(-Math.Sin(Angle), Math.Cos(Angle)));

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point2D Process(Point2D point)
            => Distortions.Rotate(point, Center, XAxis, YAxis);

        /// <summary>
        /// This should be run anytime a property of the item is modified.
        /// </summary>
        public void ClearCache()
            => propertyCache.Clear();

        /// <summary>
        /// Private method for caching computationally and memory intensive properties of child objects
        /// so the child object's properties only get touched when necessary.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</remarks>
        protected object CachingProperty(Func<object> property, [CallerMemberName]string name = "")
        {
            if (!propertyCache.ContainsKey(name))
            {
                var value = property.Invoke();
                propertyCache.Add(name, value);
                return value;
            }

            return propertyCache[name];
        }

        #endregion
    }
}
