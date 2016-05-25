// <copyright file="Triangle.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Triangle")]
    public class Triangle
         : Polygon, IClosedShape
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Triangle()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(Point2D a, Point2D b, Point2D c)
            : base(new List<Point2D>() { a, b, c })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Triangle(List<Point2D> points)
            : base(points)
        {
            if (points.Count > 3) throw new IndexOutOfRangeException();
            if (points.Count < 3) throw new IndexOutOfRangeException();
            Points = points;
        }
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [XmlAttribute()]
        public Point2D A
        {
            get { return Points[0]; }
            set { Points[0] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [XmlAttribute()]
        public Point2D B
        {
            get { return Points[1]; }
            set { Points[1] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [XmlAttribute()]
        public Point2D C
        {
            get { return Points[2]; }
            set { Points[2] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Triangle);
            StringBuilder pts = new StringBuilder();
            foreach (Point2D pt in Points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}}}", nameof(Triangle), pts.ToString());
        }

        #endregion
    }
}
