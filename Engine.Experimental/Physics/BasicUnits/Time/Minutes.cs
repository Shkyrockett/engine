// <copyright file="Minutes.cs" company="Shkyrockett" >
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
    /// The minutes struct.
    /// </summary>
    public struct Minutes
        : ITime
    {
        /// <summary>
        /// The second (const). Value: 60d.
        /// </summary>
        public const double Second = 60d;

        /// <summary>
        /// The minute (const). Value: 1d.
        /// </summary>
        public const double Minute = 1d;

        /// <summary>
        /// The hour (const). Value: 1d / 60d.
        /// </summary>
        public const double Hour = 1d / 60d;

        /// <summary>
        /// The day (const). Value: 1d / 1440d.
        /// </summary>
        public const double Day = 1d / 1440d;

        /// <summary>
        /// The year (const). Value: 365.25d * Day.
        /// </summary>
        public const double Year = 365.25d * Day;

        /// <summary>
        /// Initializes a new instance of the <see cref="Minutes"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Minutes(double value)
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
        /// Gets or sets the hours.
        /// </summary>
        public double Hours
        {
            get { return Value * Hour; }
            set { Value = value / Hour; }
        }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
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
        public static implicit operator Minutes(double value)
            => new Minutes(value);

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Minutes);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "min";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} min";
    }
}
