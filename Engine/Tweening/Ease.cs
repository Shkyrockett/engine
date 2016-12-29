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

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey1 = 1d / 2.75d;

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey2 = 2d / 2.75d;

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey3 = 1.5d / 2.75d;

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey4 = 2.5d / 2.75d;

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey5 = 2.25d / 2.75d;

        /// <summary>
        /// 
        /// </summary>
        private const double BounceKey6 = 2.625d / 2.75d;

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
            => (Sin(13d * HalfPi * t) * Pow(2d, 10d * (t - 1d)));

        /// <summary>
        /// Elastic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ElasticOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : (Sin(-13d * HalfPi * (t + 1d)) * Pow(2d, -10d * t) + 1d);

        /// <summary>
        /// Elastic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ElasticInOut(double t)
            => (t < 0.5d) ?
                (0.5d * Sin(13d * HalfPi * (2d * t)) * Pow(2d, 10d * ((2d * t) - 1d))) :
                (0.5d * (Sin(-13d * HalfPi * ((2d * t - 1) + 1d)) * Pow(2d, -10d * (2d * t - 1d)) + 2d));

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
            => t <= 0.5d ? t * t * 2d : 1d - (--t) * t * 2d;

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
            => 1d + (--t) * t * t;

        /// <summary>
        /// Cubic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CubeInOut(double t)
            => t <= 0.5d ? t * t * t * 4d : 1d + (--t) * t * t * 4d;

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
            => 1d - (t -= 1d) * t * t * t;

        /// <summary>
        /// Quart in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuartInOut(double t)
            => t <= 0.5d ? t * t * t * t * 8d : (1d - (t = t * 2d - 2d) * t * t * t) / 2d + 0.5d;

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
            => (t -= 1d) * t * t * t * t + 1d;

        /// <summary>
        /// Quint in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double QuintInOut(double t)
            => t *= 2d < 1d ? (t * t * t * t * t) / 2d : ((t -= 2d) * t * t * t * t + 2d) / 2d;

        /// <summary>
        /// Sine in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double SineIn(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : (-Cos(HalfPi * t) + 1d);

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
            => (-Cos(PI * t) / 2d + 0.5d);

        /// <summary>
        /// Bounce in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceIn(double t)
        {
            t = 1d - t;
            if (t < BounceKey1) return (1d - 7.5625d * t * t);
            if (t < BounceKey2) return (1d - (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d));
            if (t < BounceKey4) return (1d - (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d));
            return (1d - (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d));
        }

        /// <summary>
        /// Bounce out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceOut(double t)
        {
            if (t < BounceKey1) return (7.5625d * t * t);
            if (t < BounceKey2) return (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d);
            if (t < BounceKey4) return (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d);
            return (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d);
        }

        /// <summary>
        /// Bounce in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BounceInOut(double t)
        {
            if (t < 0.5d)
            {
                t = 1d - t * 2d;
                if (t < BounceKey1) return ((1d - 7.5625d * t * t) / 2d);
                if (t < BounceKey2) return ((1d - (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75)) / 2d);
                if (t < BounceKey4) return ((1d - (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375)) / 2d);
                return ((1d - (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d)) / 2d);
            }
            t = t * 2d - 1d;
            if (t < BounceKey1) return ((7.5625d * t * t) / 2d + 0.5d);
            if (t < BounceKey2) return ((7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d) / 2d + 0.5d);
            if (t < BounceKey4) return ((7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d) / 2d + 0.5d);
            return ((7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d) / 2d + 0.5d);
        }

        /// <summary>
        /// Circle in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircIn(double t)
            => (-(Sqrt(1d - t * t) - 1d));

        /// <summary>
        /// Circle out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircOut(double t)
            => (Sqrt(1d - (t - 1d) * (t - 1d)));

        /// <summary>
        /// Circle in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double CircInOut(double t)
            => (t <= 0.5d ? (Sqrt(1 - t * t * 4d) - 1d) / -2d : (Sqrt(1d - (t * 2d - 2d) * (t * 2d - 2d)) + 1d) / 2d);

        /// <summary>
        /// Exponential in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoIn(double t)
            => (Pow(2d, 10d * (t - 1d)));

        /// <summary>
        /// Exponential out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : -Pow(2d, -10d * t) + 1d;

        /// <summary>
        /// Exponential in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double ExpoInOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : (t < 0.5d ? Pow(2d, 10d * (t * 2d - 1d)) / 2d : (-Pow(2d, -10d * (t * 2d - 1d)) + 2d) / 2d);

        /// <summary>
        /// Back in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackIn(double t)
            => (t * t * (2.70158d * t - 1.70158d));

        /// <summary>
        /// Back out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackOut(double t)
            => (1d - (--t) * (t) * (-2.70158d * t - 1.70158d));

        /// <summary>
        /// Back in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        public static double BackInOut(double t)
        {
            t *= 2d;
            if (t < 1d) return (t * t * (2.70158d * t - 1.70158d) / 2d);
            t--;
            return ((1d - (--t) * (t) * (-2.70158d * t - 1.70158d)) / 2d + 0.5d);
        }
    }
}
