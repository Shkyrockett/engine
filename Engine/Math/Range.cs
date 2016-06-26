// <copyright file="Range.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections;
using System.Collections.Generic;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Numeric range class.
    /// </summary>
    public struct Range
        : IEnumerable<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct for iteration of a range from 0 to 1.
        /// </summary>
        /// <param name="count">The number of steps to have between 0 and 1.</param>
        /// <returns>A new <see cref="Range"/> struct set up for 0 to 1 iteration.</returns>
        public static Range IteratorRange(double count)
            => new Range(0d, 1d, 0d, 1d, 1d / count, Overflows.Clamp);

        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct for iteration from the start for a designated length.
        /// </summary>
        /// <param name="start">The start value of the <see cref="Range"/>.</param>
        /// <param name="length">The length of the <see cref="Range"/>.</param>
        /// <param name="count">The number of steps to have between the start and end.</param>
        /// <returns>A new <see cref="Range"/> struct set up for iteration from the start for a designated length.</returns>
        public static Range StartLengthCountRange(double start, double length, int count)
            => new Range(start, start + length, start, start + length, length / count, Overflows.Clamp);

        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct for iteration in Radians.
        /// </summary>
        /// <param name="start">The start angle in radians.</param>
        /// <param name="end">The end angle in radians.</param>
        /// <param name="count">The number of steps to have between the start and end.</param>
        /// <returns>A new <see cref="Range"/> struct set up for iterating radians.</returns>
        public static Range RadiansRange(double start, double end, int count)
            => new Range(start, end, -PI, PI, (end - start) / count, Overflows.Wrap);

        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct for iteration in Degrees.
        /// </summary>
        /// <param name="start">The start angle in degrees.</param>
        /// <param name="end">The end angle in degrees.</param>
        /// <returns>A new <see cref="Range"/> struct set up for iterating degrees.</returns>
        public static Range DegreesRange(double start, double end)
            => new Range(start, end, 0d, 360d, 1d, Overflows.Wrap);

        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct;
        /// </summary>
        /// <param name="min">Minimum value of the <see cref="Range"/>.</param>
        /// <param name="max">Maximum value of the <see cref="Range"/>.</param>
        /// <param name="unitMin">Minimum value of a periodic unit that wraps back to the maximum value.</param>
        /// <param name="unitMax">Maximum value of a periodic unit that wraps back to the minimum value.</param>
        /// <param name="step">Amount to advance for each iteration</param>
        /// <param name="overflow">Overflow rules.</param>
        public Range(double min, double max, double unitMin, double unitMax, double step, Overflows overflow = Overflows.Clamp)
        {
            Min = min;
            Max = max;
            UnitMin = unitMin;
            UnitMax = unitMax;
            Step = step;
            Overflow = overflow;
        }

        /// <summary>
        /// Provides access to the enumeration of the linear interpolation from the <see cref="Min"/> value to the <see cref="Max"/> value.
        /// </summary>
        /// <param name="index">The position between the min and max to interpolate.</param>
        /// <returns>The linear interpolation of a value between the <see cref="Min"/> and <see cref="Max"/> values.</returns>
        public double this[double index]
        {
            get
            {
                switch (Overflow)
                {
                    case Overflows.Clamp:
                        return Interpolaters.Linear(Min, Max, Maths.Clamp(index, UnitMin, UnitMax));
                    case Overflows.Wrap:
                        return Interpolaters.Linear(Min, Max, Maths.Wrap(index, UnitMin, UnitMax));
                    default:
                        return Interpolaters.Linear(Min, Max, index);
                }
            }
        }

        /// <summary>
        /// Gets the minimum value of the <see cref="Range"/>.
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// Gets the maximum value of the <see cref="Range"/>.
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// Gets the minimum value of a periodic unit that wraps back to the <see cref="UnitMax"/> value.
        /// </summary>
        public double UnitMin { get; }

        /// <summary>
        /// Gets the Maximum value of a periodic unit that wraps back to the <see cref="UnitMin"/> value.
        /// </summary>
        public double UnitMax { get; }

        /// <summary>
        /// Gets the amount to advance for each iteration.
        /// </summary>
        public double Step { get; }

        /// <summary>
        /// Gets a value indicating whether to wrap the return value or clamp it between max and min.
        /// </summary>
        public Overflows Overflow { get; }

        /// <summary>
        /// Enumerate all values at the <see cref="Step"/> interval between the <see cref="Max"/> and <see cref="Min"/> values.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (double i = Min; i < Max; i += Step)
                yield return this[i];
        }

        /// <summary>
        /// Enumerate all values at the <see cref="Step"/> interval between the <see cref="Max"/> and <see cref="Min"/> values.
        /// </summary>
        /// <returns></returns>
        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            for (double i = Min; i < Max; i += Step)
                yield return this[i];
        }
    }
}
