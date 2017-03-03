﻿// <copyright file="PathItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// An abstract class representing a piece of a geometric figure.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(PathArc))]
    [XmlInclude(typeof(PathCardinal))]
    [XmlInclude(typeof(PathCubicBezier))]
    [XmlInclude(typeof(PathLineSegment))]
    [XmlInclude(typeof(PathPoint))]
    [XmlInclude(typeof(PathQuadraticBezier))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class PathItem
    {
        #region Fields

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [NonSerialized()]
        protected Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathItem()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the flag indicating whether the item's position should be 
        /// calculated relative to the last item, or from Origin. 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public bool Relitive { get; set; }

        /// <summary>
        /// Gets or sets a reference to the previous geometric item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PathItem Previous { get; set; }

        /// <summary>
        /// Gets or sets a reference to the next geometric item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PathItem Next { get; set; }

        /// <summary>
        /// Gets or sets the starting coordinates for the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? Start { get; set; }

        /// <summary>
        /// Gets or sets the next to last point of the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? NextToEnd { get; set; }

        /// <summary>
        /// Gets or sets the ending coordinates for the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? End { get; set; }

        /// <summary>
        /// Gets or sets the grips used for this path segment.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract List<Point2D> Grips { get; }

        /// <summary>
        /// Gets the bounding rectangle of this segment of the shape.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public abstract Rectangle2D Bounds { get; }

        /// <summary>
        /// Gets the length of the Path segment.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract double Length { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Point2D Interpolate(double t);

        #endregion

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
    }
}
