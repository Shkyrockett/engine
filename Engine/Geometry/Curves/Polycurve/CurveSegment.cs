// <copyright file="CurveSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// An abstract class representing a piece of a geometric figure.
    /// </summary>
    [DataContract, Serializable]
    [XmlInclude(typeof(ArcSegment))]
    [XmlInclude(typeof(CardinalSegment))]
    [XmlInclude(typeof(CubicBezierSegment))]
    [XmlInclude(typeof(LineCurveSegment))]
    [XmlInclude(typeof(PointSegment))]
    [XmlInclude(typeof(QuadraticBezierSegment))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class CurveSegment
    {
        #region Fields

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        //[NonSerialized()]
        protected Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CurveSegment()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the flag indicating whether the item's position should be 
        /// calculated relative to the last item, or from Origin. 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Relitive { get; set; }

        /// <summary>
        /// Gets or sets a reference to the previous geometric item.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public CurveSegment Previous { get; set; }

        /// <summary>
        /// Gets or sets a reference to the next geometric item.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public CurveSegment Next { get; set; }

        /// <summary>
        /// Gets or sets the starting coordinates for the item.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public abstract Point2D? Start { get; set; }

        /// <summary>
        /// Gets or sets the next to last point of the item.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public abstract Point2D? NextToEnd { get; set; }

        /// <summary>
        /// Gets or sets the ending coordinates for the item.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public abstract Point2D? End { get; set; }

        /// <summary>
        /// Gets or sets the grips used for this path segment.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public abstract List<Point2D> Grips { get; }

        /// <summary>
        /// Gets the bounding rectangle of this segment of the shape.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public abstract Rectangle2D Bounds { get; }

        /// <summary>
        /// Gets the length of the Path segment.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html
        /// </acknowledgment>
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
