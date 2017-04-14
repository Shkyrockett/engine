// <copyright file="Ease.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using System.Runtime.CompilerServices;
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

        #region To and Fro Easing Methods

        /// <summary>
        /// Ease a value to its target and then back with another easing function. Use this to wrap two other easing functions.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer1, Func<double, double> easer2)
            => t => (t < 0.5) ? ToAndFro(easer1(t)) : ToAndFro(easer2(t));

        /// <summary>
        /// Ease a value to its target and then back. Use this to wrap another easing function.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer)
            => t => ToAndFro(easer(t));

        /// <summary>
        /// Ease a value to its target and then back.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToAndFro(double t)
            => t < 0.5d ? t * 2d : 1d + ((t - 0.5d) / 0.5d) * -1d;

        #endregion

        #region Linear Easing Methods

        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(double t, double b, double c, double d)
            => c * t / d + b;

        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns>The correct value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(double t)
            => t;

        #endregion

        #region Quadratic Easing Methods

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadIn(double t, double b, double c, double d)
            => c * (t /= d) * t + b;

        /// <summary>
        /// Quadratic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadIn(double t)
            => t * t;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOut(double t, double b, double c, double d)
            => -c * (t /= d) * (t - 2) + b;

        /// <summary>
        /// Quadratic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOut(double t)
            => -t * (t - 2d);

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? c / 2 * t * t + b : -c / 2 * ((--t) * (t - 2) - 1) + b;

        /// <summary>
        /// Quadratic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadInOut(double t)
            => t <= 0.5d ? t * t * 2d : 1d - (--t) * t * 2d;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? QuadOut(t * 2, b, c / 2, d) : QuadIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOutIn(double t)
            => (t < 0.5) ? QuadOut(t * 2) : QuadIn((t * 2) - 1);

        #endregion

        #region Cubic Easing Methods

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicIn(double t, double b, double c, double d)
            => c * (t /= d) * t * t + b;

        /// <summary>
        /// Cubic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicIn(double t)
            => t * t * t;

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOut(double t, double b, double c, double d)
            => c * ((t = t / d - 1) * t * t + 1) + b;

        /// <summary>
        /// Cubic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOut(double t)
            => 1d + (--t) * t * t;

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? c / 2 * t * t * t + b : c / 2 * ((t -= 2) * t * t + 2) + b;

        /// <summary>
        /// Cubic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicInOut(double t)
            => t <= 0.5d ? t * t * t * 4d : 1d + (--t) * t * t * 4d;

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? CubicOut(t * 2, b, c / 2, d) : CubicIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOutIn(double t)
            => (t < 0.5) ? CubicOut(t * 2) : CubicIn((t * 2) - 1);

        #endregion

        #region Quartic Easing Methods

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartIn(double t, double b, double c, double d)
            => c * (t /= d) * t * t * t + b;

        /// <summary>
        /// Quart in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartIn(double t)
            => t * t * t * t;

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOut(double t, double b, double c, double d)
            => -c * ((t = t / d - 1) * t * t * t - 1) + b;

        /// <summary>
        /// Quart out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOut(double t)
            => 1d - (--t) * t * t * t;

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? c / 2 * t * t * t * t + b : -c / 2 * ((t -= 2) * t * t * t - 2) + b;

        /// <summary>
        /// Quart in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartInOut(double t)
            => t <= 0.5d ? t * t * t * t * 8d : (1d - (t = t * 2d - 2d) * t * t * t) * 0.5d + 0.5d;

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? QuartOut(t * 2, b, c / 2, d) : QuartIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOutIn(double t)
            => (t < 0.5) ? QuartOut(t * 2) : QuartIn((t * 2) - 1);

        #endregion

        #region Quintic Easing Methods

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintIn(double t, double b, double c, double d)
            => c * (t /= d) * t * t * t * t + b;

        /// <summary>
        /// Quint in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintIn(double t)
            => t * t * t * t * t;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOut(double t, double b, double c, double d)
            => c * ((t = t / d - 1) * t * t * t * t + 1) + b;

        /// <summary>
        /// Quint out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOut(double t)
            => (t -= 1d) * t * t * t * t + 1d;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? c / 2 * t * t * t * t * t + b : c / 2 * ((t -= 2) * t * t * t * t + 2) + b;

        /// <summary>
        /// Quint in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintInOut(double t)
            => ((t *= 2d) < 1d) ? (t * t * t * t * t) * 0.5d : ((t -= 2d) * t * t * t * t + 2d) * 0.5d;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? QuintOut(t * 2, b, c / 2, d) : QuintIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOutIn(double t)
            => (t < 0.5) ? QuintOut(t * 2) : QuintIn((t * 2) - 1);

        #endregion

        #region Exponential Easing Methods

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoIn(double t, double b, double c, double d)
            => (t == 0) ? b : c * Pow(2, 10 * (t / d - 1)) + b;

        /// <summary>
        /// Exponential in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoIn(double t)
            => (Pow(2d, 10d * (t - 1d)));

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOut(double t, double b, double c, double d)
            => (t == d) ? b + c : c * (-Pow(2, -10 * t / d) + 1) + b;

        /// <summary>
        /// Exponential out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : -Pow(2d, -10d * t) + 1d;

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoInOut(double t, double b, double c, double d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Pow(2, -10 * --t) + 2) + b;
        }

        /// <summary>
        /// Exponential in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoInOut(double t)
            => (Abs(t - 1d) < Epsilon)
            ? 1d : (t < 0.5d ? Pow(2d, 10d * (t * 2d - 1d)) * 0.5d : (-Pow(2d, -10d * (t * 2d - 1d)) + 2d) * 0.5d);

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? ExpoOut(t * 2, b, c / 2, d) : ExpoIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOutIn(double t)
            => (t < 0.5) ? ExpoOut(t * 2) : ExpoIn((t * 2) - 1);

        #endregion

        #region Sine Easing Methods

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineIn(double t, double b, double c, double d)
            => -c * Cos(t / d * (PI / 2)) + c + b;

        /// <summary>
        /// Sine in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineIn(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : (-Cos(Right * t) + 1d);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOut(double t, double b, double c, double d)
            => c * Sin(t / d * (PI / 2)) + b;

        /// <summary>
        /// Sine out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOut(double t)
            => (Sin(Right * t));

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? c / 2 * (Sin(PI * t / 2)) + b : -c / 2 * (Cos(PI * --t / 2) - 2) + b;

        /// <summary>
        /// Sine in and out
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineInOut(double t)
            => (-Cos(PI * t) * 0.5d + 0.5d);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? SineOut(t * 2, b, c / 2, d) : SineIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOutIn(double t)
            => (t < 0.5) ? SineOut(t * 2) : SineIn((t * 2) - 1);

        #endregion

        #region Circular Easing Methods

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircIn(double t, double b, double c, double d)
            => -c * (Sqrt(1 - (t /= d) * t) - 1) + b;

        /// <summary>
        /// Circle in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircIn(double t)
            => (-(Sqrt(1d - t * t) - 1d));

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOut(double t, double b, double c, double d)
            => c * Sqrt(1 - (t = t / d - 1) * t) + b;

        /// <summary>
        /// Circle out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOut(double t)
            => (Sqrt(1d - (t - 1d) * (t - 1d)));

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? -c / 2 * (Sqrt(1 - t * t) - 1) + b : c / 2 * (Sqrt(1 - (t -= 2) * t) + 1) + b;

        /// <summary>
        /// Circle in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircInOut(double t)
            => (t <= 0.5d ? (Sqrt(1 - t * t * 4d) - 1d) * -0.5d : (Sqrt(1d - (t * 2d - 2d) * (t * 2d - 2d)) + 1d) * 0.5d);

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? CircOut(t * 2, b, c / 2, d) : CircIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// Modified from: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOutIn(double t)
            => (t < 0.5) ? CircOut(t * 2) : CircIn((t * 2) - 1);

        #endregion

        #region Elastic Easing Methods

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticIn(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            var p = d * .3;
            var s = p / 4;

            return -(c * Pow(2, 10 * (t -= 1)) * Sin((t * d - s) * (2 * PI) / p)) + b;
        }

        /// <summary>
        /// Elastic in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticIn(double t)
            => (Sin(13d * Right * t) * Pow(2d, 10d * (t - 1d)));

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOut(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            var p = d * 0.3;
            var s = p / 4;

            return (c * Pow(2, -10 * t) * Sin((t * d - s) * (2 * PI) / p) + c + b);
        }

        /// <summary>
        /// Elastic out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : (Sin(-13d * Right * (t + 1d)) * Pow(2d, -10d * t) + 1d);

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            var p = d * (0.3 * 1.5);
            var s = p / 4;

            if (t < 1)
                return -.5 * (c * Pow(2, 10 * (t -= 1d)) * Sin((t * d - s) * (2d * PI) / p)) + b;
            return c * Pow(2, -10 * (t -= 1)) * Sin((t * d - s) * (2d * PI) / p) * 0.5d + c + b;
        }

        /// <summary>
        /// Elastic in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticInOut(double t)
            => (t < 0.5d) ?
                (0.5d * Sin(13d * Right * (2d * t)) * Pow(2d, 10d * ((2d * t) - 1d))) :
                (0.5d * (Sin(-13d * Right * ((2d * t - 1) + 1d)) * Pow(2d, -10d * (2d * t - 1d)) + 2d));

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? ElasticOut(t * 2, b, c / 2, d) : ElasticIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOutIn(double t)
            => (t < 0.5) ? ElasticOut(t * 2) : ElasticIn((t * 2) - 1);

        #endregion

        #region Bounce Easing Methods

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceIn(double t, double b, double c, double d)
            => c - BounceOut(d - t, 0, c, d) + b;

        /// <summary>
        /// Bounce in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceIn(double t)
        {
            t = 1d - t;
            if (t < BounceKey1)
                return (1d - 7.5625d * t * t);
            if (t < BounceKey2)
                return (1d - (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d));
            if (t < BounceKey4)
                return (1d - (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d));
            return (1d - (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d));
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75))
                return c * (7.5625 * t * t) + b;
            else if (t < (2 / 2.75))
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            else if (t < (2.5 / 2.75))
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            else
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
        }

        /// <summary>
        /// Bounce out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOut(double t)
        {
            if (t < BounceKey1)
                return (7.5625d * t * t);
            if (t < BounceKey2)
                return (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d);
            if (t < BounceKey4)
                return (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d);
            return (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d);
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceEaseInOut(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BounceIn(t * 2, 0, c, d) * .5 + b;
            else
                return BounceOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }

        /// <summary>
        /// Bounce in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceInOut(double t)
        {
            if (t < 0.5d)
            {
                t = 1d - t * 2d;
                if (t < BounceKey1)
                    return ((1d - 7.5625d * t * t) * 0.5d);
                if (t < BounceKey2)
                    return ((1d - (7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75)) * 0.5d);
                if (t < BounceKey4)
                    return ((1d - (7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375)) * 0.5d);
                return ((1d - (7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d)) * 0.5d);
            }

            t = t * 2d - 1d;
            if (t < BounceKey1)
                return ((7.5625d * t * t) * 0.5d + 0.5d);
            if (t < BounceKey2)
                return ((7.5625d * (t - BounceKey3) * (t - BounceKey3) + 0.75d) * 0.5d + 0.5d);
            if (t < BounceKey4)
                return ((7.5625d * (t - BounceKey5) * (t - BounceKey5) + 0.9375d) * 0.5d + 0.5d);
            return ((7.5625d * (t - BounceKey6) * (t - BounceKey6) + 0.984375d) * 0.5d + 0.5d);
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? BounceOut(t * 2, b, c / 2, d) : BounceIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOutIn(double t)
            => (t < 0.5) ? BounceOut(t * 2) : BounceIn((t * 2) - 1);

        #endregion

        #region Back Easing Methods

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackIn(double t, double b, double c, double d)
            => c * (t /= d) * t * ((1.70158 + 1) * t - 1.70158) + b;

        /// <summary>
        /// Back in.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackIn(double t)
            => (t * t * (2.70158d * t - 1.70158d));

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOut(double t, double b, double c, double d)
            => c * ((t = t / d - 1) * t * ((1.70158 + 1) * t + 1.70158) + 1) + b;

        /// <summary>
        /// Back out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOut(double t)
            => (1d - (--t) * (t) * (-2.70158d * t - 1.70158d));

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackInOut(double t, double b, double c, double d)
        {
            var s = 1.70158;
            return ((t /= d / 2) < 1) ? c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b : c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        }

        /// <summary>
        /// Back in and out.
        /// </summary>
        /// <param name="t">Time elapsed.</param>
        /// <returns>Eased timescale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackInOut(double t)
        {
            t *= 2d;
            if (t < 1d)
                return (t * t * (2.70158d * t - 1.70158d) * 0.5d);
            t--;
            return ((1d - (--t) * t * (-2.70158d * t - 1.70158d)) * 0.5d + 0.5d);
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOutIn(double t, double b, double c, double d)
            => (t < d / 2) ? BackOut(t * 2, b, c / 2, d) : BackIn((t * 2) - d, b + c / 2, c / 2, d);

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <returns></returns>
        /// <remarks>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOutIn(double t)
            => (t < 0.5) ? BackOut(t * 2) : BackIn((t * 2) - 1);

        #endregion
    }
}
