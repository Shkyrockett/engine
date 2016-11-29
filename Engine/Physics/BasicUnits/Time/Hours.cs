// <copyright file="Hours.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Hours
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Second = 3600d;

        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 60d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 24d;

        /// <summary>
        /// 
        /// </summary>
        public const double Year = 365.25d * Day;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Hours(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Seconds
        {
            get { return Value * Second; }
            set { Value = value / Second; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Minutes
        {
            get { return Value * Minute; }
            set { Value = value / Minute; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// 
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
        public static implicit operator Hours(double value) => new Hours(value);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Hours";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "h";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} h";
    }
}
