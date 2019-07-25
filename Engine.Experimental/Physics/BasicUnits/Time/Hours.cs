// <copyright file="Hours.cs" company="Shkyrockett" >
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
    /// The hours struct.
    /// </summary>
    public struct Hours
        : ITime
    {
        /// <summary>
        /// The second (const). Value: 3600d.
        /// </summary>
        public const double Second = 3600d;

        /// <summary>
        /// The minute (const). Value: 60d.
        /// </summary>
        public const double Minute = 60d;

        /// <summary>
        /// The hour (const). Value: 1d.
        /// </summary>
        public const double Hour = 1d;

        /// <summary>
        /// The day (const). Value: 24d.
        /// </summary>
        public const double Day = 24d;

        /// <summary>
        /// The year (const). Value: 365.25d * Day.
        /// </summary>
        public const double Year = 365.25d * Day;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hours"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Hours(double value)
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
        public static implicit operator Hours(double value)
            => new Hours(value);

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Hours);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "h";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} h";
    }
}
