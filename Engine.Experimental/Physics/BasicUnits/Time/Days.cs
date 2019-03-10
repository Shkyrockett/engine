// <copyright file="Days.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// The days struct.
    /// </summary>
    public struct Days
        : ITime
    {
        /// <summary>
        /// The second (const). Value: 86400d.
        /// </summary>
        public const double Second = 86400d;

        /// <summary>
        /// The minute (const). Value: 1440d.
        /// </summary>
        public const double Minute = 1440d;

        /// <summary>
        /// The hour (const). Value: 24d.
        /// </summary>
        public const double Hour = 24d;

        /// <summary>
        /// The day (const). Value: 1d.
        /// </summary>
        public const double Day = 1d;

        /// <summary>
        /// The year (const). Value: 1d / 365.25d.
        /// </summary>
        public const double Year = 1d / 365.25d;

        /// <summary>
        /// Initializes a new instance of the <see cref="Days"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Days(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        public double Seconds
        {
            get { return Value * Second; }
            set { Value = value / Second; }
        }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        public double Minutes
        {
            get { return Value * Minute; }
            set { Value = value / Minute; }
        }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        public double Hours
        {
            get { return Value * Hour; }
            set { Value = value / Hour; }
        }

        /// <summary>
        /// Gets or sets the years.
        /// </summary>
        public double Years
        {
            get { return Value * Year; }
            set { Value = value / Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Days(double value)
            => new Days(value);

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Days);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "days";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} days";
    }
}
