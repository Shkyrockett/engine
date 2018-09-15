// <copyright file="CurveFit.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> Adapted from:https://github.com/burningmime/curves</remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// Implements a least-squares bezier curve fitting routine based on http://tog.acm.org/resources/GraphicsGems/gems/FitCurves.c with a few 
    /// optimizations made by me. You can read the article here: http://read.pudn.com/downloads141/ebook/610086/Graphics_Gems_I.pdf page 626.
    /// To use, call the <see cref="Fit"/> static function and wait for magic to happen.
    /// </summary>
    public sealed class CurveFit
        : CurveFitBase
    {
        #region Fields
        /// <summary>
        /// Use a thread-static instance to prevent multi-threading issues without needing to re-allocate on each run.
        /// </summary>
        [ThreadStatic]
        private static CurveFit instance;

        /// <summary>
        /// Curves we've found so far.
        /// </summary>
        private readonly List<CubicBezier> result = new List<CubicBezier>(16);

        /// <summary>
        /// Shared zero-curve array.
        /// </summary>
        private static readonly CubicBezier[] ZeroCurves = new CubicBezier[0];
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Private constructor so it can't be constructed externally.
        /// </summary>
        private CurveFit()
        { }
        #endregion Constructors

        /// <summary>
        /// Attempts to fit a set of Bézier curves to the given data. It returns a set of curves that form a 
        /// http://en.wikipedia.org/wiki/Composite_B%C3%A9zier_curve with C1 continuity (that is, each curve's start
        /// point is coincident with the previous curve's end point, and the tangent vectors of the start and end
        /// points are going in the same direction, so the curves will join up smoothly). Returns an empty array
        /// if less than two points in input.
        /// 
        /// Input data MUST not contain repeated points (that is, the same point twice in succession). The best way to
        /// ensure this is to call any one of the methods in <see cref="Distortions" />, since all three pre-processing
        /// methods will remove duplicate points. If repeated points are encountered, unexpected behavior can occur.
        /// </summary>
        /// <param name="points">Set of points to fit to.</param>
        /// <param name="maxError">Maximum distance from any data point to a point on the generated curve.</param>
        /// <returns>Fitted curves or an empty list if it could not fit.</returns>
        public static CubicBezier[] Fit(List<Point2D> points, double maxError)
        {
            if (maxError < Epsilon)
                throw new InvalidOperationException("maxError cannot be negative/zero/less than epsilon value");
            if (points is null)
                throw new ArgumentNullException(nameof(points));
            if (points.Count < 2)
                return ZeroCurves; // need at least 2 points to do anything

            var instance = GetInstance();
            try
            {
                // should be cleared after each run
                Debug.Assert(instance.points.Count == 0 && instance.result.Count == 0 &&
                    instance.u.Count == 0 && instance.arclen.Count == 0);

                // initialize arrays
                instance.points.AddRange(points);
                instance.InitializeArcLengths();
                instance.squaredError = maxError * maxError;

                // Find tangents at ends
                var last = points.Count - 1;
                var tanL = instance.GetLeftTangent(last);
                var tanR = instance.GetRightTangent(0);

                // do the actual fit
                instance.FitRecursive(0, last, tanL, tanR);
                return instance.result.ToArray();
            }
            finally
            {
                instance.points.Clear();
                instance.result.Clear();
                instance.arclen.Clear();
                instance.u.Clear();
            }
        }

        /// <summary>
        /// Main fit function that attempts to fit a segment of curve and recurses if unable to.
        /// </summary>
        private void FitRecursive(int first, int last, Vector2D tanL, Vector2D tanR)
        {
            if (FitCurve(first, last, tanL, tanR, out CubicBezier curve, out var split))
            {
                result.Add(curve);
            }
            else
            {
                // If we get here, fitting failed, so we need to recurse
                // first, get mid tangent
                var tanM1 = GetCenterTangent(first, last, split);
                var tanM2 = -tanM1;

                // our end tangents might be based on points outside the new curve (this is possible for mid tangents too
                // but since we need to maintain C1 continuity, it's too late to do anything about it)
                if (first == 0 && split < EndTangentNPoints)
                    tanL = GetLeftTangent(split);
                if (last == points.Count - 1 && split > (points.Count - (EndTangentNPoints + 1)))
                    tanR = GetRightTangent(split);

                // do actual recursion
                FitRecursive(first, split, tanL, tanM1);
                FitRecursive(split, last, tanM2, tanR);
            }
        }

        /// <summary>
        /// Get the instance.
        /// </summary>
        /// <returns>The <see cref="CurveFit"/>.</returns>
        private static CurveFit GetInstance()
            => instance ?? (instance = new CurveFit());
    }
}
