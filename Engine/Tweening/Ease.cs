// <copyright file="Ease.cs" company="Shkyrockett" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// Static class with useful easer functions that can be used by Tweens.
    /// </summary>
    public static class Ease
    {
        #region Constants

        private const double B1 = 1d / 2.75d;
        private const double B2 = 2d / 2.75d;
        private const double B3 = 1.5d / 2.75d;
        private const double B4 = 2.5d / 2.75d;
        private const double B5 = 2.25d / 2.75d;
        private const double B6 = 2.625d / 2.75d;

        #endregion

        /// <summary>
        /// Ease a value to its target and then back. Use this to wrap another easing function.
        /// </summary>
        public static Func<double, double> ToAndFro(Func<double, double> easer)
            => t => ToAndFro(easer(t));

        /// <summary>
        /// Ease a value to its target and then back.
        /// </summary>
        public static double ToAndFro(double t)
            => t < 0.5d ? t * 2d : 1d + ((t - 0.5d) / 0.5d) * -1d;

        /// <summary>
        /// Elastic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ElasticIn(double t)
            => (Sin(13 * HalfPi * t) * Pow(2, 10 * (t - 1)));

        /// <summary>
        /// Elastic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ElasticOut(double t)
        {
            if (Abs(t - 1) < Epsilon) return 1;
            return (Sin(-13 * HalfPi * (t + 1)) * Pow(2, -10 * t) + 1);
        }

        /// <summary>
        /// Elastic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ElasticInOut(double t)
        {
            if (t < 0.5)
                return (0.5 * Sin(13 * HalfPi * (2 * t)) * Pow(2, 10 * ((2 * t) - 1)));

            return (0.5 * (Sin(-13 * HalfPi * ((2 * t - 1) + 1)) * Pow(2, -10 * (2 * t - 1)) + 2));
        }

        /// <summary>
        /// Quadratic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuadIn(double t)
            => t * t;

        /// <summary>
        /// Quadratic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuadOut(double t)
            => -t * (t - 2);

        /// <summary>
        /// Quadratic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuadInOut(double t)
            => t <= .5 ? t * t * 2 : 1 - (--t) * t * 2;

        /// <summary>
        /// Cubic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CubeIn(double t)
            => t * t * t;

        /// <summary>
        /// Cubic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CubeOut(double t)
            => 1 + (--t) * t * t;

        /// <summary>
        /// Cubic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CubeInOut(double t)
            => t <= .5 ? t * t * t * 4 : 1 + (--t) * t * t * 4;

        /// <summary>
        /// Quart in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuartIn(double t)
            => t * t * t * t;

        /// <summary>
        /// Quart out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuartOut(double t)
            => 1 - (t -= 1) * t * t * t;

        /// <summary>
        /// Quart in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuartInOut(double t)
            => t <= .5 ? t * t * t * t * 8 : (1 - (t = t * 2 - 2) * t * t * t) / 2 + .5;

        /// <summary>
        /// Quint in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuintIn(double t)
            => t * t * t * t * t;

        /// <summary>
        /// Quint out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuintOut(double t)
            => (t -= 1) * t * t * t * t + 1;

        /// <summary>
        /// Quint in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuintInOut(double t)
            => t *= 2 < 1 ? (t * t * t * t * t) / 2 : ((t -= 2) * t * t * t * t + 2) / 2;

        /// <summary>
        /// Sine in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double SineIn(double t)
        {
            if (Abs(t - 1) < Epsilon) return 1;
            return (-Cos(HalfPi * t) + 1);
        }

        /// <summary>
        /// Sine out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double SineOut(double t)
            => (Sin(HalfPi * t));

        /// <summary>
        /// Sine in and out
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double SineInOut(double t)
            => (-Cos(PI * t) / 2 + .5);

        /// <summary>
        /// Bounce in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceIn(double t)
        {
            t = 1 - t;
            if (t < B1) return (1 - 7.5625 * t * t);
            if (t < B2) return (1 - (7.5625 * (t - B3) * (t - B3) + .75));
            if (t < B4) return (1 - (7.5625 * (t - B5) * (t - B5) + .9375));
            return (1 - (7.5625 * (t - B6) * (t - B6) + .984375));
        }

        /// <summary>
        /// Bounce out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceOut(double t)
        {
            if (t < B1) return (7.5625 * t * t);
            if (t < B2) return (7.5625 * (t - B3) * (t - B3) + .75);
            if (t < B4) return (7.5625 * (t - B5) * (t - B5) + .9375);
            return (7.5625 * (t - B6) * (t - B6) + .984375);
        }

        /// <summary>
        /// Bounce in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceInOut(double t)
        {
            if (t < .5)
            {
                t = 1 - t * 2;
                if (t < B1) return ((1 - 7.5625 * t * t) / 2);
                if (t < B2) return ((1 - (7.5625 * (t - B3) * (t - B3) + .75)) / 2);
                if (t < B4) return ((1 - (7.5625 * (t - B5) * (t - B5) + .9375)) / 2);
                return ((1 - (7.5625 * (t - B6) * (t - B6) + .984375)) / 2);
            }
            t = t * 2 - 1;
            if (t < B1) return ((7.5625 * t * t) / 2 + .5);
            if (t < B2) return ((7.5625 * (t - B3) * (t - B3) + .75) / 2 + .5);
            if (t < B4) return ((7.5625 * (t - B5) * (t - B5) + .9375) / 2 + .5);
            return ((7.5625 * (t - B6) * (t - B6) + .984375) / 2 + .5);
        }

        /// <summary>
        /// Circle in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircIn(double t)
            => (-(Sqrt(1 - t * t) - 1));

        /// <summary>
        /// Circle out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircOut(double t)
            => (Sqrt(1 - (t - 1) * (t - 1)));

        /// <summary>
        /// Circle in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircInOut(double t)
            => (t <= .5 ? (Sqrt(1 - t * t * 4) - 1) / -2 : (Sqrt(1 - (t * 2 - 2) * (t * 2 - 2)) + 1) / 2);

        /// <summary>
        /// Exponential in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoIn(double t)
            => (Pow(2, 10 * (t - 1)));

        /// <summary>
        /// Exponential out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoOut(double t)
        {
            if (Abs(t - 1) < Epsilon) return 1;
            return (-Pow(2, -10 * t) + 1);
        }

        /// <summary>
        /// Exponential in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoInOut(double t)
        {
            if (Abs(t - 1) < Epsilon) return 1;
            return (t < .5 ? Pow(2, 10 * (t * 2 - 1)) / 2 : (-Pow(2, -10 * (t * 2 - 1)) + 2) / 2);
        }

        /// <summary>
        /// Back in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackIn(double t)
            => (t * t * (2.70158 * t - 1.70158));

        /// <summary>
        /// Back out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackOut(double t)
            => (1 - (--t) * (t) * (-2.70158 * t - 1.70158));

        /// <summary>
        /// Back in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackInOut(double t)
        {
            t *= 2;
            if (t < 1) return (t * t * (2.70158 * t - 1.70158) / 2);
            t--;
            return ((1 - (--t) * (t) * (-2.70158 * t - 1.70158)) / 2 + .5);
        }
    }
}
