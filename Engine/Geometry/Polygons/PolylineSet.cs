// <copyright file="PolylineSet.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PolylineSet))]
    public class PolylineSet
        : Shape, IEnumerable<Polyline>
    {
        #region Fields

        /// <summary>
        /// An array of Polygons representing a set.
        /// </summary>
        /// <remarks></remarks>
        private List<Polyline> polylines;

        #endregion

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
            :this(new List<List<Point2D>>(polylines as List<Point2D>[]))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        public PolylineSet(IEnumerable<Polyline> polylines)
        {
            this.polylines = polylines as List<Polyline>;
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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
            => polylines.Count;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => polylines.Sum(p => p.Perimeter);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = polylines[0].Bounds;

                foreach (Polyline polyline in polylines)
                    bounds.UnionMutate(polyline.Bounds);

                return bounds;
            }
        }

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected new void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected new void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected new void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected new void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public void Add(Polyline polyline) => polylines.Add(polyline);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Polyline> GetEnumerator()
            => polylines.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => polylines.GetEnumerator();

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
            if (this == null) return nameof(PolylineSet);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PolylineSet)}{{{string.Join(sep.ToString(), Polylines)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
