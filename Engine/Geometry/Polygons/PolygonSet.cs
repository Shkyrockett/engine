// <copyright file="PolygonSet.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Set of a Closed Polygon structures
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PolygonSet))]
    public class PolygonSet
        : Shape
    {
        #region Private Fields

        /// <summary>
        /// An array of Polygons representing a set.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        private List<Polygon> polygons;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a default instance of the <see cref="PolygonSet"/> class.
        /// </summary>
        public PolygonSet()
            : this(new List<Polygon>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSet"/> class.
        /// </summary>
        public PolygonSet(IEnumerable<Polygon> polygons)
            => this.polygons = polygons as List<Polygon>;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSet"/> class from a parameter list.
        /// </summary>
        /// <param name="polygons"></param>
        public PolygonSet(params IEnumerable<Point2D>[] polygons)
            : this(new List<List<Point2D>>(polygons as List<Point2D>[]))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSet"/> class.
        /// </summary>
        /// <param name="polygons"></param>
        public PolygonSet(IEnumerable<List<Point2D>> polygons)
        {
            this.polygons = new List<Polygon>();

            foreach (var list in polygons)
            {
                this.polygons.Add(new Polygon(list));
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Polygon this[int index]
        {
            get
            {
                return polygons[index];
            }

            set
            {
                polygons[index] = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<Polygon> Polygons
        {
            get { return polygons; }
            set
            {
                polygons = value;
                OnPropertyChanged(nameof(Polygons));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
            => polygons.Count;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore, SoapIgnore]
        public override double Perimeter
            => polygons.Sum(p => p.Perimeter);

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = polygons[0].Bounds;

                foreach (Polygon polygon in polygons)
                    bounds.Union(polygon.Bounds);

                return bounds;
            }
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public void Add(Polygon polygon)
        {
            polygons.Add(polygon);
            update?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public void Add(List<Point2D> polygon)
        {
            polygons.Add(new Polygon(polygon));
            update?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            foreach (var poly in polygons)
            {
                poly.Reverse();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolygonSet Clone()
            => new PolygonSet(Polygons.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(PolygonSet);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PolygonSet)}{{{string.Join(sep.ToString(), Polygons)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
