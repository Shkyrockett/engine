// <copyright file="GraphicsObject.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine.Objects
{
    /// <summary>
    /// Graphic objects base class.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class GraphicsObject
        : IFormattable
    {
        #region Callbacks

        /// <summary>
        /// Action delegate for notifying callbacks on object updates.
        /// </summary>
        internal Action update;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="Area"/> of a <see cref="Shape"/>.
        /// </summary>
        public virtual double Area { get; private set; }

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of a <see cref="Shape"/>.
        /// </summary>
        public virtual double Perimeter { get; private set; }

        /// <summary>
        /// Gets the <see cref="Bounds"/> of a <see cref="Shape"/>.
        /// </summary>
        public virtual Rectangle2D Bounds { get; set; }

        #endregion

        #region Interpolation

        /// <summary>
        /// Interpolates a <see cref="Shape"/>.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual Point2D Interpolate(double t)
        {
            return null;
        }

        /// <summary>
        /// Retrieves a list of points interpolated from a<see cref="Shape"/>.
        /// </summary>
        /// <param name="count">The number of points desired.</param>
        /// <returns></returns>
        public virtual List<Point2D> InterpolatePoints(int count)
        {
            return new List<Point2D>(
                from i in Enumerable.Range(0, count)
                select Interpolate((1d / count) * i));

            //List<Point2D> points = new List<Point2D>();
            //for (int i = 0; i <= count; i++)
            //{
            //    points.Add(Interpolate((1d / count) * i));
            //}
            //return points;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Test whether a point intersects with the object.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A <see cref="bool"/> value indicating whether the point intersects the object.</returns>
        public virtual bool Contains(Point2D point)
        {
            return false;
        }

        /// <summary>
        /// Register one or more methods to call when properties change to the shape.
        /// </summary>
        /// <param name="callback">The method to use.</param>
        /// <returns>A reference to object.</returns>
        internal GraphicsObject OnUpdate(Action callback)
        {
            if (update == null) update = callback;
            else update += callback;
            return this;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //[Pure]
        //public virtual T Clone<T>()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="GraphicsObject"/> inherited class.
        /// </summary>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal virtual string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(GraphicsObject);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(GraphicsObject)}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
