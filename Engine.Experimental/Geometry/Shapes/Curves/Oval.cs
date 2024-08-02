// <copyright file="Oval.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// The oval class.
/// </summary>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Oval))]
[XmlType(TypeName = "oval")]
public class Oval
    : Shape2D
{
    #region Fields
    /// <summary>
    /// The x.
    /// </summary>
    private double x;

    /// <summary>
    /// The y.
    /// </summary>
    private double y;

    /// <summary>
    /// The h.
    /// </summary>
    private double h;

    /// <summary>
    /// The v.
    /// </summary>
    private double v;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Oval"/> class.
    /// </summary>
    public Oval()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Oval"/> class.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    public Oval(Point2D location, Size2D size)
    {
        x = location.X;
        y = location.Y;
        h = size.Width;
        v = size.Height;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    public Point2D Location
    {
        get { return new Point2D(x, y); }
        set
        {
            x = value.X;
            y = value.Y;
            OnPropertyChanged(nameof(Location));
        }
    }

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    [XmlAttribute(nameof(x))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double X
    {
        get { return x; }
        set
        {
            x = value;
            OnPropertyChanged(nameof(X));
        }
    }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    [XmlAttribute(nameof(y))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Y
    {
        get { return y; }
        set
        {
            y = value;
            OnPropertyChanged(nameof(Y));
        }
    }

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    public Size2D Size
    {
        get { return new Size2D(h, v); }
        set
        {
            h = value.Width;
            v = value.Height;
            OnPropertyChanged(nameof(Size));
        }
    }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [XmlAttribute(nameof(h))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Width
    {
        get { return h; }
        set
        {
            h = value;
            OnPropertyChanged(nameof(Width));
        }
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    [XmlAttribute(nameof(v))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Height
    {
        get { return v; }
        set
        {
            v = value;
            OnPropertyChanged(nameof(Height));
        }
    }

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    public override Rectangle2D Bounds
        => new(x, y, h, v);
    #endregion Properties

    //#region Serialization

    ///// <summary>
    ///// Sends an event indicating that this value went into the data file during serialization.
    ///// </summary>
    ///// <param name="context"></param>
    //[OnSerializing()]
    //private void OnSerializing(StreamingContext context)
    //{
    //    Debug.WriteLine($"{nameof(Oval)} is being serialized.");
    //}

    ///// <summary>
    ///// Sends an event indicating that this value was reset after serialization.
    ///// </summary>
    ///// <param name="context"></param>
    //[OnSerialized()]
    //private void OnSerialized(StreamingContext context)
    //{
    //    Debug.WriteLine($"{nameof(Oval)} has been serialized.");
    //}

    ///// <summary>
    ///// Sends an event indicating that this value was set during deserialization.
    ///// </summary>
    ///// <param name="context"></param>
    //[OnDeserializing()]
    //private void OnDeserializing(StreamingContext context)
    //{
    //    Debug.WriteLine($"{nameof(Oval)} is being deserialized.");
    //}

    ///// <summary>
    ///// Sends an event indicating that this value was set after deserialization.
    ///// </summary>
    ///// <param name="context"></param>
    //[OnDeserialized()]
    //private void OnDeserialized(StreamingContext context)
    //{
    //    Debug.WriteLine($"{nameof(Oval)} has been deserialized.");
    //}

    //#endregion

    #region Methods
    /// <summary>
    /// Creates a string representation of this <see cref="Oval"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public override string ConvertToString(string format, IFormatProvider formatProvider)
    {
        if (this is null)
        {
            return nameof(Oval);
        }

        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        IFormattable formatable = $"{nameof(Oval)}{{{nameof(Location)}={Location}{sep}{nameof(Size)}={Size}}}";
        return formatable.ToString(format, formatProvider);
    }
    #endregion Methods
}
