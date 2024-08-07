﻿// <copyright file="GraphicsObject.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// Graphic objects base class.
/// </summary>
[DataContract, Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public abstract class GraphicsObject
    : IFormattable, INotifyPropertyChanging, INotifyPropertyChanged
{
    #region Callbacks
    /// <summary>
    /// Action delegate for notifying callbacks on object updates.
    /// </summary>
    internal Action update;

    /// <summary>
    /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;
    #endregion Callbacks

    #region Fields
    /// <summary>
    /// Property cache for commonly used properties that may take time to calculate.
    /// </summary>
    [XmlIgnore()]
    [NonSerialized()]
    protected Dictionary<object, object> propertyCache = [];
    #endregion Fields

    #region Properties
    /// <summary>
    /// Gets the <see cref="Area"/> of a <see cref="Shape2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The area of the shape.")]
    public virtual double Area { get; set; }

    /// <summary>
    /// Gets the <see cref="Perimeter"/> of a <see cref="Shape2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The perimeter length of the shape.")]
    public virtual double Perimeter { get; set; }

    /// <summary>
    /// Gets the <see cref="Bounds"/> of a <see cref="Shape2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The bounding box of the shape.")]
    public virtual Rectangle2D Bounds { get; set; }
    #endregion Properties

    #region Interpolation
    /// <summary>
    /// Interpolates a <see cref="Shape2D"/>.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public virtual Point2D Interpolate(double t) => new();

    /// <summary>
    /// Retrieves a list of points interpolated from a<see cref="Shape2D"/>.
    /// </summary>
    /// <param name="count">The number of points desired.</param>
    /// <returns></returns>
    public virtual List<Point2D> InterpolatePoints(int count = 100)
    {
        var list = new List<Point2D>(
        from i in Enumerable.Range(0, count)
        select Interpolate(1d / count * i))
        {
            Interpolate(1)
        };
        return list;
    }

    /// <summary>
    /// The interpolate points.
    /// </summary>
    /// <param name="range">The range.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    public virtual List<Point2D> InterpolatePoints(NumericRange range)
    {
        var points = new List<Point2D>();
        foreach (var item in range)
        {
            points.Add(Interpolate(item));
        }

        return points;
    }
    #endregion Interpolation

    #region Methods
    /// <summary>
    /// Test whether a point intersects with the object.
    /// </summary>
    /// <param name="point"></param>
    /// <returns>A <see cref="bool"/> value indicating whether the point intersects the object.</returns>
    public virtual bool Contains(Point2D point) => false;

    /// <summary>
    /// Register one or more methods to call when properties change to the shape.
    /// </summary>
    /// <param name="callback">The method to use.</param>
    /// <returns>A reference to object.</returns>
    internal GraphicsObject OnUpdate(Action callback)
    {
        if (update is null)
        {
            update = callback;
        }
        else
        {
            update += callback;
        }

        return this;
    }

    /// <summary>
    /// Raises the property changing event.
    /// </summary>
    /// <param name="name">The name.</param>
    protected void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

    /// <summary>
    /// Raises the property changed event.
    /// </summary>
    /// <param name="name">The name.</param>
    protected void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    /// <summary>
    /// This should be run anytime a property of the item is modified.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ClearCache() => propertyCache.Clear();

    /// <summary>
    /// Private method for caching computationally and memory intensive properties of child objects
    /// so that the intensive properties only get recalculated and stored when necessary.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected object CachingProperty(Func<object> property, [CallerMemberName] string name = "")
    {
        if (!propertyCache.ContainsKey(name))
        {
            var value = property?.Invoke();
            propertyCache.Add(name, value);
            return value;
        }

        return propertyCache[name];
    }

    ///// <summary>
    /////
    ///// </summary>
    ///// <returns></returns>
    //public virtual T Clone<T>()
    //{
    //    throw new NotImplementedException();
    //}

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="GraphicsObject"/> inherited class.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">ToDo: describe provider parameter on ToString</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ConvertToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider) => ConvertToString(format /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public virtual string ConvertToString(string format, IFormatProvider formatProvider)
    {
        if (this is null)
        {
            return nameof(GraphicsObject);
        }
        //char sep = Tokenizer.GetNumericListSeparator(formatProvider);
        IFormattable formatable = $"{nameof(GraphicsObject)}";
        return formatable.ToString(format, formatProvider);
    }
    #endregion Methods
}
