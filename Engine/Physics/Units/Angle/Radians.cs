using Engine.Geometry;
using System;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[DisplayName(nameof(Radians))]
    public struct Radians
        : IDirection, IFormattable
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Radien = Math.PI / 180d;

        /// <summary>
        /// 
        /// </summary>
        public const double Degree = 180d / Math.PI;

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct.
        /// </summary>
        /// <param name="value"></param>
        public Radians(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct as a clone.
        /// </summary>
        /// <param name="radians"></param>
        public Radians(Radians radians)
        {
            value = radians.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct from a <see cref="Degrees"/>.
        /// </summary>
        /// <param name="degrees"></param>
        public Radians(Degrees degrees)
        {
            value = degrees.Value.ToRadians();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Degrees
        {
            get { return Degree * value; }
            set { this.value = value / Degree; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Inches"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "rad"; } }

        #endregion

        #region Operators 

        /// <summary>
        /// Compares two <see cref="Radians"/> objects. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Radians left, Radians right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Compares two <see cref="Radians"/> objects. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Radians left, Radians right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares two <see cref="Radians"/> objects.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Radians a, Radians b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Radians a, Radians b)
        {
            return (a.Value == b.Value) & (a.Value == b.Value);
        }

        /// <summary>
        /// override object.Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   https://msdn.microsoft.com/en-us/library/ms173147.aspx 
            // and also the guidance for operator== at
            //   https://msdn.microsoft.com/en-us/library/53k8ybth.aspx
            //
            return (obj is Radians || obj is Degrees) && obj is Radians ? Equals(this, (Radians)obj) : Equals(this, ((Degrees)obj).ToRadian());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Radians value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Radians(double value)
        {
            return new Radians(value);
        }

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="Radians"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Radians"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Radians"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Radians Parse(string source)
        {
            Tokenizer tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);

            string firstToken = tokenizer.NextTokenRequired();

            Radians value = new Radians(Convert.ToDouble(firstToken, CultureInfo.InvariantCulture));

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();

            return value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// override object.GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <returns></returns>
        public Radians ToDegrees()
        {
            return value.ToDegrees();
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Radians"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Radians);
            //return string.Format(provider, "{0:" + format + "} rad", value);
            IFormattable formatable = $"{value} rad";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
