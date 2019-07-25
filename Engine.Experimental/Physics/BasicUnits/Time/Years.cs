// <copyright file="Years.cs" company="Shkyrockett" >
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
    /// The years struct.
    /// </summary>
    public struct Years
        : ITime
    {
        /// <summary>
        /// The second (const). Value: 31557600d.
        /// </summary>
        public const double Second = 31557600d;

        /// <summary>
        /// The minute (const). Value: 525960d.
        /// </summary>
        public const double Minute = 525960d;

        /// <summary>
        /// The hour (const). Value: 8766d.
        /// </summary>
        public const double Hour = 8766d;

        /// <summary>
        /// The day (const). Value: 365.25d.
        /// </summary>
        public const double Day = 365.25d;

        /// <summary>
        /// The year (const). Value: 1d.
        /// </summary>
        public const double Year = 1d;

        /// <summary>
        /// Initializes a new instance of the <see cref="Years"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Years(double value)
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
        /// Gets or sets the days.
        /// </summary>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Years(double value)
            => new Years(value);

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Years);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "years";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} years";
    }
}
