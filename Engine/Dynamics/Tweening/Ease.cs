// <copyright file="Ease.cs" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
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
using static Engine.Mathematics;
using System.Diagnostics;

namespace Engine.Tweening
{
    /// <summary>
    /// Static class with useful easer functions that can be used by Tweens.
    /// </summary>
    public static class Ease
    {
        #region Constants
        /// <summary>
        /// The bounce key1 (const). Value: 1d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey1 = 1d / 2.75d;

        /// <summary>
        /// The bounce key2 (const). Value: 2d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey2 = 2d / 2.75d;

        /// <summary>
        /// The bounce key3 (const). Value: 1.5d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey3 = 1.5d / 2.75d;

        /// <summary>
        /// The bounce key4 (const). Value: 2.5d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey4 = 2.5d / 2.75d;

        /// <summary>
        /// The bounce key5 (const). Value: 2.25d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey5 = 2.25d / 2.75d;

        /// <summary>
        /// The bounce key6 (const). Value: 2.625d / 2.75d.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        private const double bounceKey6 = 2.625d / 2.75d;
        #endregion Constants

        #region To and Fro Easing Methods
        /// <summary>
        /// Ease a value to its target and then back with another easing function. Use this to wrap two other easing functions.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer1, Func<double, double> easer2, double b, double c, double d)
            => t => (t < 0.5d) ? ToAndFro(easer1(t), b, c, d) : ToAndFro(easer2(t), b, c, d);

        /// <summary>
        /// Ease a value to its target and then back with another easing function. Use this to wrap two other easing functions.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer1, Func<double, double> easer2)
            => t => (t < 0.5d) ? ToAndFro(easer1(t)) : ToAndFro(easer2(t));

        /// <summary>
        /// Ease a value to its target and then back. Use this to wrap another easing function.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer, double b, double c, double d)
            => t => ToAndFro(easer(t), b, c, d);

        /// <summary>
        /// Ease a value to its target and then back. Use this to wrap another easing function.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<double, double> ToAndFro(Func<double, double> easer)
            => t => ToAndFro(easer(t));

        /// <summary>
        /// Ease a value to its target and then back.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToAndFro(double t, double b, double c, double d)
            => (c * (t < 0.5d ? t * 2d : 1d + ((t - 0.5d) / 0.5d * -1d)) / d) + b;

        /// <summary>
        /// Ease a value to its target and then back.
        /// </summary>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToAndFro(double t)
            => t < 0.5d ? t * 2d : 1d + ((t - 0.5d) / 0.5d * -1d);
        #endregion To and Fro Easing Methods

        #region Parabolic
        /// <summary>
        /// Parabolic to and fro method.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Parabolic(double t, double b, double c, double d)
            => (c * ((-4d * t * t) + (4d * t)) / d) + b;

        /// <summary>
        /// Parabolic to and fro method.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <remarks>
        /// This is the parabola $y=-4t^2+4t+0$ where the Vertex form is: $y=-4(t-1/2)^2+1$.
        /// The x-intercepts are at 0 and 1 respectively, with the peak of the vertex at (1/2, 1) which makes it ideal for scaling.
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Parabolic(double t)
            => (-4d * t * t) + (4d * t);
        #endregion Parabolic

        #region Linear Easing Methods
        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(double t, double b, double c, double d)
            => (c * t / d) + b;

        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(double t)
            => t;
        #endregion Linear Easing Methods

        #region Quadratic Easing Methods
        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadIn(double t, double b, double c, double d)
            => (c * (t /= d) * t) + b;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadIn(double t)
            => t * t;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOut(double t, double b, double c, double d)
            => (-c * (t /= d) * (t - 2d)) + b;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOut(double t)
            => -t * (t - 2d);

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadInOut(double t, double b, double c, double d)
            => ((t /= d / 2) < 1) ? (c / 2 * t * t) + b : (-c / 2 * (((--t) * (t - 2)) - 1)) + b;

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadInOut(double t)
            => t <= 0.5d ? t * t * 2d : 1d - ((--t) * t * 2d);

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? QuadOut(t * 2d, b, c / 2d, d) : QuadIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadOutIn(double t)
            => (t < 0.5d) ? QuadOut(t * 2d) : QuadIn((t * 2d) - 1d);
        #endregion Quadratic Easing Methods

        #region Cubic Easing Methods
        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicIn(double t, double b, double c, double d)
            => (c * (t /= d) * t * t) + b;

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicIn(double t)
            => t * t * t;

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOut(double t, double b, double c, double d)
            => (c * (((t = (t / d) - 1d) * t * t) + 1d)) + b;

        /// <summary>
        /// Cubic out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOut(double t)
            => 1d + ((--t) * t * t);

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicInOut(double t, double b, double c, double d)
            => ((t /= d / 2d) < 1d) ? (c / 2d * t * t * t) + b : (c / 2d * (((t -= 2d) * t * t) + 2d)) + b;

        /// <summary>
        /// Cubic in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicInOut(double t)
            => t <= 0.5d ? t * t * t * 4d : 1d + ((--t) * t * t * 4d);

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? CubicOut(t * 2d, b, c / 2d, d) : CubicIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicOutIn(double t)
            => (t < 0.5d) ? CubicOut(t * 2d) : CubicIn((t * 2d) - 1d);
        #endregion Cubic Easing Methods

        #region Quartic Easing Methods
        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartIn(double t, double b, double c, double d)
            => (c * (t /= d) * t * t * t) + b;

        /// <summary>
        /// Quart in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartIn(double t)
            => t * t * t * t;

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOut(double t, double b, double c, double d)
            => (-c * (((t = (t / d) - 1d) * t * t * t) - 1d)) + b;

        /// <summary>
        /// Quart out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOut(double t)
            => 1d - ((--t) * t * t * t);

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartInOut(double t, double b, double c, double d)
            => ((t /= d / 2d) < 1d) ? (c / 2d * t * t * t * t) + b : (-c / 2d * (((t -= 2d) * t * t * t) - 2d)) + b;

        /// <summary>
        /// Quart in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartInOut(double t)
            => t <= 0.5d ? t * t * t * t * 8d : ((1d - ((t = (t * 2d) - 2d) * t * t * t)) * 0.5d) + 0.5d;

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? QuartOut(t * 2d, b, c / 2d, d) : QuartIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuartOutIn(double t)
            => (t < 0.5d) ? QuartOut(t * 2d) : QuartIn((t * 2d) - 1d);
        #endregion Quartic Easing Methods

        #region Quintic Easing Methods
        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintIn(double t, double b, double c, double d)
            => (c * (t /= d) * t * t * t * t) + b;

        /// <summary>
        /// Quint in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintIn(double t)
            => t * t * t * t * t;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOut(double t, double b, double c, double d)
            => (c * (((t = (t / d) - 1d) * t * t * t * t) + 1d)) + b;

        /// <summary>
        /// Quint out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOut(double t)
            => ((t -= 1d) * t * t * t * t) + 1d;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintInOut(double t, double b, double c, double d)
            => ((t /= d / 2d) < 1d) ? (c / 2d * t * t * t * t * t) + b : (c / 2d * (((t -= 2d) * t * t * t * t) + 2d)) + b;

        /// <summary>
        /// Quint in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintInOut(double t)
            => ((t *= 2d) < 1d) ? t * t * t * t * t * 0.5d : (((t -= 2d) * t * t * t * t) + 2d) * 0.5d;

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? QuintOut(t * 2d, b, c / 2d, d) : QuintIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuintOutIn(double t)
            => (t < 0.5d) ? QuintOut(t * 2d) : QuintIn((t * 2d) - 1d);
        #endregion Quintic Easing Methods

        #region Exponential Easing Methods
        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoIn(double t, double b, double c, double d)
            => (t == 0d) ? b : (c * Pow(2d, 10d * ((t / d) - 1d))) + b;

        /// <summary>
        /// Exponential in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoIn(double t)
            => Pow(2d, 10d * (t - 1d));

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOut(double t, double b, double c, double d)
            => (t == d) ? b + c : (c * (-Pow(2d, -10d * t / d) + 1d)) + b;

        /// <summary>
        /// Exponential out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : -Pow(2d, -10d * t) + 1d;

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoInOut(double t, double b, double c, double d)
            => (t == 0d) ? b : (t == d) ? b + c : ((t /= d / 2d) < 1d) ? (c / 2d * Pow(2d, 10d * (t - 1d))) + b : (c / 2d * (-Pow(2d, -10d * --t) + 2d)) + b;

        /// <summary>
        /// Exponential in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoInOut(double t)
            => (Abs(t - 1d) < Epsilon)
            ? 1d : (t < 0.5d ? Pow(2d, 10d * ((t * 2d) - 1d)) * 0.5d : (-Pow(2d, -10d * ((t * 2d) - 1d)) + 2d) * 0.5d);

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? ExpoOut(t * 2d, b, c / 2d, d) : ExpoIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ExpoOutIn(double t)
            => (t < 0.5d) ? ExpoOut(t * 2d) : ExpoIn((t * 2d) - 1d);
        #endregion Exponential Easing Methods

        #region Sine Easing Methods
        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineIn(double t, double b, double c, double d)
            => (-c * Cos(t / d * HalfPi)) + c + b;

        /// <summary>
        /// Sine in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineIn(double t)
            => (Abs(t - 1d) < Epsilon)
            ? 1d
            : (-Cos(HalfPi * t) + 1d);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOut(double t, double b, double c, double d)
            => (c * Sin(t / d * HalfPi)) + b;

        /// <summary>
        /// Sine out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOut(double t)
            => Sin(HalfPi * t);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineInOut(double t, double b, double c, double d)
            => ((t /= d * 0.5d) < 1d)
            ? (c * 0.5d * Sin(HalfPi * t)) + b
            : (-c * 0.5d * (Cos(HalfPi * --t) - 2d)) + b;

        /// <summary>
        /// Sine in and out
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineInOut(double t)
            => (-Cos(PI * t) * 0.5d) + 0.5d;

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOutIn(double t, double b, double c, double d)
            => (t < d * 0.5d)
            ? SineOut(t * 2, b, c * 0.5d, d)
            : SineIn((t * 2) - d, b + (c * 0.5d), c * 0.5d, d);

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SineOutIn(double t)
            => (t < 0.5d)
            ? SineOut(t * 2d)
            : SineIn((t * 2d) - 1d);
        #endregion Sine Easing Methods

        #region Circular Easing Methods
        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircIn(double t, double b, double c, double d)
            => (-c * (Sqrt(1d - ((t /= d) * t)) - 1d)) + b;

        /// <summary>
        /// Circle in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircIn(double t)
            => -(Sqrt(1d - (t * t)) - 1d);

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOut(double t, double b, double c, double d)
            => (c * Sqrt(1d - ((t = (t / d) - 1d) * t))) + b;

        /// <summary>
        /// Circle out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOut(double t)
            => Sqrt(1d - ((t - 1d) * (t - 1d)));

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircInOut(double t, double b, double c, double d)
            => ((t /= d / 2d) < 1d) ? (-c / 2d * (Sqrt(1d - (t * t)) - 1d)) + b : (c / 2d * (Sqrt(1d - ((t -= 2d) * t)) + 1d)) + b;

        /// <summary>
        /// Circle in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircInOut(double t)
            => t <= 0.5d ? (Sqrt(1d - (t * t * 4d)) - 1d) * -0.5d : (Sqrt(1d - (((t * 2d) - 2d) * ((t * 2d) - 2d))) + 1d) * 0.5d;

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? CircOut(t * 2d, b, c / 2d, d) : CircIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircOutIn(double t)
            => (t < 0.5d) ? CircOut(t * 2d) : CircIn((t * 2d) - 1d);
        #endregion Circular Easing Methods

        #region Elastic Easing Methods
        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticIn(double t, double b, double c, double d)
        {
            if ((t /= d) == 1d)
            {
                return b + c;
            }

            var p = d * 0.3d;
            var s = p / 4d;

            return -(c * Pow(2d, 10d * (t -= 1d)) * Sin(((t * d) - s) * (2d * PI) / p)) + b;
        }

        /// <summary>
        /// Elastic in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticIn(double t)
            => Sin(13d * HalfPi * t) * Pow(2d, 10d * (t - 1d));

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOut(double t, double b, double c, double d)
        {
            if ((t /= d) == 1d)
            {
                return b + c;
            }

            var p = d * 0.3d;
            var s = p / 4d;

            return (c * Pow(2d, -10d * t) * Sin(((t * d) - s) * (2d * PI) / p)) + c + b;
        }

        /// <summary>
        /// Elastic out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOut(double t)
            => (Abs(t - 1d) < Epsilon) ? 1d : ((Sin(-13d * HalfPi * (t + 1d)) * Pow(2d, -10d * t)) + 1d);

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2d) == 2d)
            {
                return b + c;
            }

            var p = d * (0.3d * 1.5d);
            var s = p / 4d;

            if (t < 1d)
            {
                return (-0.5d * (c * Pow(2d, 10d * (t -= 1d)) * Sin(((t * d) - s) * (2d * PI) / p))) + b;
            }

            return (c * Pow(2d, -10d * (t -= 1)) * Sin(((t * d) - s) * (2d * PI) / p) * 0.5d) + c + b;
        }

        /// <summary>
        /// Elastic in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticInOut(double t)
            => (t < 0.5d) ?
                (0.5d * Sin(13d * HalfPi * (2d * t)) * Pow(2d, 10d * ((2d * t) - 1d))) :
                (0.5d * ((Sin(-13d * HalfPi * ((2d * t) - 1 + 1d)) * Pow(2d, -10d * ((2d * t) - 1d))) + 2d));

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? ElasticOut(t * 2d, b, c / 2d, d) : ElasticIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElasticOutIn(double t)
            => (t < 0.5d) ? ElasticOut(t * 2d) : ElasticIn((t * 2d) - 1d);
        #endregion Elastic Easing Methods

        #region Bounce Easing Methods
        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceIn(double t, double b, double c, double d)
            => c - BounceOut(d - t, 0d, c, d) + b;

        /// <summary>
        /// Bounce in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceIn(double t)
        {
            t = 1d - t;
            if (t < bounceKey1)
            {
                return 1d - (7.5625d * t * t);
            }

            if (t < bounceKey2)
            {
                return 1d - ((7.5625d * (t - bounceKey3) * (t - bounceKey3)) + 0.75d);
            }

            if (t < bounceKey4)
            {
                return 1d - ((7.5625d * (t - bounceKey5) * (t - bounceKey5)) + 0.9375d);
            }

            return 1d - ((7.5625d * (t - bounceKey6) * (t - bounceKey6)) + 0.984375d);
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1d / 2.75d))
            {
                return (c * (7.5625d * t * t)) + b;
            }
            else if (t < (2d / 2.75d))
            {
                return (c * ((7.5625d * (t -= 1.5d / 2.75d) * t) + 0.75d)) + b;
            }
            else if (t < (2.5d / 2.75d))
            {
                return (c * ((7.5625d * (t -= 2.25d / 2.75d) * t) + 0.9375d)) + b;
            }
            else
            {
                return (c * ((7.5625d * (t -= 2.625d / 2.75d) * t) + 0.984375d)) + b;
            }
        }

        /// <summary>
        /// Bounce out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOut(double t)
        {
            if (t < bounceKey1)
            {
                return 7.5625d * t * t;
            }

            if (t < bounceKey2)
            {
                return (7.5625d * (t - bounceKey3) * (t - bounceKey3)) + 0.75d;
            }

            if (t < bounceKey4)
            {
                return (7.5625d * (t - bounceKey5) * (t - bounceKey5)) + 0.9375d;
            }

            return (7.5625d * (t - bounceKey6) * (t - bounceKey6)) + 0.984375d;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceEaseInOut(double t, double b, double c, double d)
        {
            if (t < d / 2d)
            {
                return (BounceIn(t * 2d, 0d, c, d) * 0.5d) + b;
            }
            else
            {
                return (BounceOut((t * 2d) - d, 0d, c, d) * 0.5d) + (c * 0.5d) + b;
            }
        }

        /// <summary>
        /// Bounce in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceInOut(double t)
        {
            if (t < 0.5d)
            {
                t = 1d - (t * 2d);
                if (t < bounceKey1)
                {
                    return (1d - (7.5625d * t * t)) * 0.5d;
                }

                if (t < bounceKey2)
                {
                    return (1d - ((7.5625d * (t - bounceKey3) * (t - bounceKey3)) + 0.75)) * 0.5d;
                }

                if (t < bounceKey4)
                {
                    return (1d - ((7.5625d * (t - bounceKey5) * (t - bounceKey5)) + 0.9375)) * 0.5d;
                }

                return (1d - ((7.5625d * (t - bounceKey6) * (t - bounceKey6)) + 0.984375d)) * 0.5d;
            }

            t = (t * 2d) - 1d;
            if (t < bounceKey1)
            {
                return (7.5625d * t * t * 0.5d) + 0.5d;
            }

            if (t < bounceKey2)
            {
                return (((7.5625d * (t - bounceKey3) * (t - bounceKey3)) + 0.75d) * 0.5d) + 0.5d;
            }

            if (t < bounceKey4)
            {
                return (((7.5625d * (t - bounceKey5) * (t - bounceKey5)) + 0.9375d) * 0.5d) + 0.5d;
            }

            return (((7.5625d * (t - bounceKey6) * (t - bounceKey6)) + 0.984375d) * 0.5d) + 0.5d;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? BounceOut(t * 2d, b, c / 2d, d) : BounceIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BounceOutIn(double t)
            => (t < 0.5d) ? BounceOut(t * 2d) : BounceIn((t * 2d) - 1d);
        #endregion Bounce Easing Methods

        #region Back Easing Methods
        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in:
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackIn(double t, double b, double c, double d)
            => (c * (t /= d) * t * (((1.70158d + 1) * t) - 1.70158d)) + b;

        /// <summary>
        /// Back in.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackIn(double t)
            => t * t * ((2.70158d * t) - 1.70158d);

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out:
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOut(double t, double b, double c, double d)
            => (c * (((t = (t / d) - 1d) * t * (((1.70158d + 1d) * t) + 1.70158d)) + 1d)) + b;

        /// <summary>
        /// Back out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOut(double t)
            => 1d - ((--t) * t * ((-2.70158d * t) - 1.70158d));

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out:
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackInOut(double t, double b, double c, double d)
        {
            var s = 1.70158d;
            return ((t /= d / 2d) < 1d) ? (c / 2d * (t * t * ((((s *= 1.525d) + 1d) * t) - s))) + b : (c / 2d * (((t -= 2d) * t * ((((s *= 1.525d) + 1d) * t) + s)) + 2d)) + b;
        }

        /// <summary>
        /// Back in and out.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns>Eased timescale.</returns>
        /// <acknowledgment>
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackInOut(double t)
        {
            t *= 2d;
            if (t < 1d)
            {
                return t * t * ((2.70158d * t) - 1.70158d) * 0.5d;
            }

            t--;
            return ((1d - ((--t) * t * ((-2.70158d * t) - 1.70158d))) * 0.5d) + 0.5d;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        /// <acknowledgment>
        /// From: https://github.com/darrendavid/wpf-animation
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOutIn(double t, double b, double c, double d)
            => (t < d / 2d) ? BackOut(t * 2d, b, c / 2d, d) : BackIn((t * 2d) - d, b + (c / 2d), c / 2d, d);

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in:
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time elapsed in ticks.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/darrendavid/wpf-animation
        /// https://bitbucket.org/jacobalbano/glide
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BackOutIn(double t)
            => (t < 0.5d) ? BackOut(t * 2d) : BackIn((t * 2d) - 1d);
        #endregion Back Easing Methods
    }
}
