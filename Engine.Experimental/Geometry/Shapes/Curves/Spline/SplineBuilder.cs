// <copyright file="SplineBuilder.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine
{
    /// <summary>
    /// Wraps a <see cref="CurveBuilder"/> and <see cref="Spline"/> together. Allows you to add data points as they come in and
    /// generate a smooth spline from them without doing unnecessary computation.
    /// </summary>
    public sealed class SplineBuilder
    {
        #region Fields
        /// <summary>
        /// Underlying curve fitter
        /// </summary>
        private readonly CurveBuilder builder;

        /// <summary>
        /// Underlying spline
        /// </summary>
        private readonly Spline spline;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SplineBuilder"/> class.
        /// </summary>
        /// <param name="pointDistance">The pointDistance.</param>
        /// <param name="error">The error.</param>
        /// <param name="samplesPerCurve">The samplesPerCurve.</param>
        public SplineBuilder(double pointDistance, double error, int samplesPerCurve)
        {
            builder = new CurveBuilder(pointDistance, error);
            spline = new Spline(samplesPerCurve);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// The curves that make up the spline.
        /// </summary>
        public ReadOnlyCollection<CubicBezier> Curves
            => spline.Curves;
        #endregion Properties

        #region Mutators
        /// <summary>
        /// Adds a data point.
        /// </summary>
        /// <param name="p">Data point to add.</param>
        /// <returns>True if the spline was modified.</returns>
        public bool Add(Point2D p)
        {
            var res = builder.AddPoint(p);
            if (!res.WasChanged)
            {
                return false;
            }

            // update spline
            var curves = builder.Curves;
            if (res.WasAdded && curves.Count == 1)
            {
                // first curve
                Debug.Assert(spline.Curves.Count == 0);
                spline.Add(curves[0]);
            }
            else if (res.WasAdded)
            {
                // split
                spline.Update(spline.Curves.Count - 1, curves[res.FirstChangedIndex]);
                for (var i = res.FirstChangedIndex + 1; i < curves.Count; i++)
                {
                    spline.Add(curves[i]);
                }
            }
            else
            {
                // last curve updated
                Debug.Assert(res.FirstChangedIndex == curves.Count - 1);
                spline.Update(spline.Curves.Count - 1, curves[curves.Count - 1]);
            }

            return true;
        }

        /// <summary>
        /// Clears the SplineBuilder.
        /// </summary>
        public void Clear()
        {
            builder.Clear();
            spline.Clear();
        }
        #endregion Mutators

        /// <summary>
        /// Gets the position of a point on the spline that's close to the desired point along the spline. For example, if u = 0.5, then a point
        /// that's about halfway through the spline will be returned. The returned point will lie exactly on one of the curves that make up the
        /// spline.
        /// </summary>
        /// <param name="u">How far along the spline to sample (for example, 0.5 will be halfway along the length of the spline). Should be between 0 and 1.</param>
        /// <returns>The position on the spline.</returns>
        public Point2D Sample(double u)
            => spline.Interpolate(u);

        /// <summary>
        /// Gets the tangent of a point on the spline that's close to the desired point along the spline. For example, if u = 0.5, then the direction vector
        /// that's about halfway through the spline will be returned. The returned value will be a normalized direction vector.
        /// </summary>
        /// <param name="u">How far along the spline to sample (for example, 0.5 will be halfway along the length of the spline). Should be between 0 and 1.</param>
        /// <returns>The position on the spline.</returns>
        public Vector2D Tangent(double u)
        {
            var pos = spline.GetSamplePosition(u);
            return spline.Curves[pos.Index].Tangent(pos.Time);
        }
    }
}
