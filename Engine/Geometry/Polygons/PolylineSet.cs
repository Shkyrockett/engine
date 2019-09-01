// <copyright file="PolylineSet.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Set of a open Polyline structures
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PolylineSet))]
    [DebuggerDisplay("{ToString()}")]
    public class PolylineSet
        : Shape, IEnumerable<Polyline>
    {
        #region Fields
        /// <summary>
        /// An array of Polygons representing a set.
        /// </summary>
        private List<Polyline> polylines;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        public PolylineSet()
            : this(new List<Polyline>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSet"/> class from a parameter list.
        /// </summary>
        /// <param name="polylines"></param>
        public PolylineSet(params IEnumerable<Point2D>[] polylines)
            : this(new List<List<Point2D>>(polylines as List<Point2D>[]))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        public PolylineSet(IEnumerable<Polyline> polylines)
        {
            this.polylines = polylines as List<Polyline> ?? new List<Polyline>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        /// <param name="polylines"></param>
        public PolylineSet(IEnumerable<List<Point2D>> polylines)
        {
            this.polylines = new List<Polyline>();

            foreach (var list in polylines)
            {
                this.polylines.Add(new Polyline(list));
            }
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the polylines.
        /// </summary>
        [XmlArray]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Polyline> Polylines
        {
            get { return polylines; }
            set
            {
                polylines = value;
                OnPropertyChanged(nameof(Polylines));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
            => polylines.Count;

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => polylines.Sum(p => p.Perimeter);

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                var bounds = polylines[0].Bounds;

                foreach (var polyline in polylines)
                {
                    bounds.UnionMutate(polyline.Bounds);
                }

                return bounds;
            }
        }
        #endregion Properties

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        public void Add(Polyline polyline)
            => polylines.Add(polyline);
        #endregion Mutators

        #region Methods
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<Polyline> GetEnumerator()
            => polylines.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => polylines.GetEnumerator();

        /// <summary>
        /// Convert the to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(PolylineSet);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PolylineSet)}{{{string.Join(sep.ToString(provider), Polylines)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
