// <copyright file="Spline.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The spline class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polyline))]
    public class Spline
        : Shape
    {
        #region Constants
        /// <summary>
        /// The minimum samples per curve (const). Value: 8.
        /// </summary>
        public const int MinimumSamplesPerCurve = 8;

        /// <summary>
        /// The maximum samples per curve (const). Value: 1024.
        /// </summary>
        public const int MaximumSamplesPerCurve = 1024;
        #endregion Constants

        #region Fields
        /// <summary>
        /// The curves.
        /// </summary>
        private readonly List<CubicBezier> curves;

        /// <summary>
        /// The arclen.
        /// </summary>
        private readonly List<double> arclen;

        /// <summary>
        /// The samples per curve.
        /// </summary>
        private readonly int samplesPerCurve;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Creates an empty spline.
        /// </summary>
        /// <param name="samplesPerCurve">Resolution of the curve. Values 32-256 work well. You may need more or less depending on how big the curves are.</param>
        public Spline(int samplesPerCurve)
        {
            if (samplesPerCurve < MinimumSamplesPerCurve || samplesPerCurve > MaximumSamplesPerCurve)
            {
                throw new InvalidOperationException("samplesPerCurve must be between " + MinimumSamplesPerCurve + " and " + MaximumSamplesPerCurve);
            }

            this.samplesPerCurve = samplesPerCurve;
            curves = new List<CubicBezier>(16);
            Curves = new ReadOnlyCollection<CubicBezier>(curves);
            arclen = new List<double>(16 * samplesPerCurve);
        }

        /// <summary>
        /// Creates a new spline from the given curves.
        /// </summary>
        /// <param name="curves">Curves to create the spline from.</param>
        /// <param name="samplesPerCurve">Resolution of the curve. Values 32-256 work well. You may need more or less depending on how big the curves are.</param>
        public Spline(ICollection<CubicBezier> curves, int samplesPerCurve)
        {
            if (curves is null)
            {
                throw new ArgumentNullException(nameof(curves));
            }

            if (samplesPerCurve < MinimumSamplesPerCurve || samplesPerCurve > MaximumSamplesPerCurve)
            {
                throw new InvalidOperationException("samplesPerCurve must be between " + MinimumSamplesPerCurve + " and " + MaximumSamplesPerCurve);
            }

            this.samplesPerCurve = samplesPerCurve;
            this.curves = new List<CubicBezier>(curves.Count);
            Curves = new ReadOnlyCollection<CubicBezier>(this.curves);
            arclen = new List<double>(this.curves.Count * samplesPerCurve);
            foreach (var curve in curves)
            {
                Add(curve);
            }
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets a read-only view of the current curves collection.
        /// </summary>
        public ReadOnlyCollection<CubicBezier> Curves { get; }

        /// <summary>
        /// Gets the total length of the spline.
        /// </summary>
        public double Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var count = arclen.Count;
                return count == 0 ? 0 : arclen[count - 1];
            }
        }
        #endregion Properties

        #region Mutators
        /// <summary>
        /// Adds a curve to the end of the spline.
        /// </summary>
        public void Add(CubicBezier curve)
        {
            if (curves.Count > 0 && !Primitives.EqualsOrClose(curves[curves.Count - 1].D, curve.A))
            {
                throw new InvalidOperationException("The new curve does at index " + curves.Count + " does not connect with the previous curve at index " + (curves.Count - 1));
            }

            curves.Add(curve);
            for (var i = 0; i < samplesPerCurve; i++) // expand the array since updateArcLengths expects these values to be there
            {
                arclen.Add(0);
            }

            UpdateArcLengths(curves.Count - 1);
        }

        /// <summary>
        /// Modifies a curve in the spline. It must connect with the previous and next curves (if applicable). This requires that the
        /// arc length table be recalculated for that curve and all curves after it -- for example, if you update the first curve in the
        /// spline, each curve after that would need to be recalculated (could avoid this by caching the lengths on a per-curve basis if you're
        /// doing this often, but since the typical case is only updating the last curve, and the entire array needs to be visited anyway, it
        /// wouldn't save much).
        /// </summary>
        /// <param name="index">Index of the curve to update in <see cref="Curves"/>.</param>
        /// <param name="curve">The new curve with which to replace it.</param>
        public void Update(int index, CubicBezier curve)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Negative index");
            }

            if (index >= curves.Count)
            {
                throw new IndexOutOfRangeException("Curve index " + index + " is out of range (there are " + curves.Count + " curves in the spline)");
            }

            if (index > 0 && !Primitives.EqualsOrClose(curves[index - 1].D, curve.A))
            {
                throw new InvalidOperationException("The updated curve at index " + index + " does not connect with the previous curve at index " + (index - 1));
            }

            if (index < curves.Count - 1 && !Primitives.EqualsOrClose(curves[index + 1].A, curve.D))
            {
                throw new InvalidOperationException("The updated curve at index " + index + " does not connect with the next curve at index " + (index + 1));
            }

            curves[index] = curve;
            for (var i = index; i < curves.Count; i++)
            {
                UpdateArcLengths(i);
            }
        }

        /// <summary>
        /// Clears the spline.
        /// </summary>
        public void Clear()
        {
            curves.Clear();
            arclen.Clear();
        }
        #endregion Mutators

        /// <summary>
        /// Gets the position of a point on the spline that's close to the desired point along the spline. For example, if u = 0.5, then a point
        /// that's about halfway through the spline will be returned. The returned point will lie exactly on one of the curves that make up the
        /// spline.
        /// </summary>
        /// <param name="t">How far along the spline to sample (for example, 0.5 will be halfway along the length of the spline). Should be between 0 and 1.</param>
        /// <returns>The position on the spline.</returns>
        public override Point2D Interpolate(double t)
        {
            var pos = GetSamplePosition(t);
            return curves[pos.Index].Interpolate(pos.Time);
        }

        /// <summary>
        /// Gets the curve index and t-value to sample to get a point at the desired part of the spline.
        /// </summary>
        /// <param name="t">How far along the spline to sample (for example, 0.5 will be halfway along the length of the spline). Should be between 0 and 1.</param>
        /// <returns>The position to sample at.</returns>
        public SamplePosition GetSamplePosition(double t)
        {
            if (curves.Count == 0)
            {
                throw new InvalidOperationException("No curves have been added to the spline");
            }

            if (t < 0)
            {
                return new SamplePosition(0, 0);
            }

            if (t > 1)
            {
                return new SamplePosition(curves.Count - 1, 1);
            }

            var total = Length;
            var target = t * total;
            Debug.Assert(target >= 0);

            // Binary search to find largest value <= target
            var index = 0;
            var low = 0;
            var high = arclen.Count - 1;
            double found = float.NaN;
            while (low < high)
            {
                index = (low + high) / 2;
                found = arclen[index];
                if (found < target)
                {
                    low = index + 1;
                }
                else
                {
                    high = index;
                }
            }

            // this should be a rather rare scenario: we're past the end, but this wasn't picked up by the test for u >= 1
            if (index >= arclen.Count - 1)
            {
                return new SamplePosition(curves.Count - 1, 1);
            }

            // this can happen because the binary search can give us either index or index + 1
            if (found > target)
            {
                index--;
            }

            if (index < 0)
            {
                // We're at the beginning of the spline
                var max = arclen[0];
                Debug.Assert(target <= max + Maths.Epsilon);
                var part = target / max;
                var tp = part / samplesPerCurve;
                return new SamplePosition(0, tp);
            }
            else
            {
                // interpolate between two values to see where the index would be if continuous values
                var min = arclen[index];
                var max = arclen[index + 1];
                Debug.Assert(target >= min - Maths.Epsilon && target <= max + Maths.Epsilon);
                var part = target < min ? 0 : target > max ? 1 : (target - min) / (max - min);
                var tp = (((index + 1) % samplesPerCurve) + part) / samplesPerCurve;
                var curveIndex = (index + 1) / samplesPerCurve;
                return new SamplePosition(curveIndex, tp);
            }
        }

        /// <summary>
        /// Updates the internal arc length array for a curve. Expects the list to contain enough elements.
        /// </summary>
        private void UpdateArcLengths(int iCurve)
        {
            var curve = curves[iCurve];
            var nSamples = samplesPerCurve;
            var clen = iCurve > 0 ? arclen[(iCurve * nSamples) - 1] : 0;
            var pp = curve.A;
            Debug.Assert(arclen.Count >= ((iCurve + 1) * nSamples));
            for (var iPoint = 0; iPoint < nSamples; iPoint++)
            {
                var idx = (iCurve * nSamples) + iPoint;
                var t = (iPoint + 1) / (double)nSamples;
                var np = curve.Interpolate(t);
                var d = Measurements.Distance(np, pp);
                clen += d;
                arclen[idx] = clen;
                pp = np;
            }
        }
    }
}
