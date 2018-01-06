// <copyright file="Tween.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// The tween class.
    /// </summary>
    public class Tween
    {
        #region Callbacks

        /// <summary>
        /// The ease.
        /// </summary>
        private Func<double, double> ease;

        /// <summary>
        /// The begin.
        /// </summary>
        private Action begin;

        /// <summary>
        /// The update.
        /// </summary>
        private Action update;

        /// <summary>
        /// The complete.
        /// </summary>
        private Action complete;

        #endregion

        #region Timing

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool Paused { get; private set; }

        /// <summary>
        /// The delay.
        /// </summary>
        private double delay;

        /// <summary>
        /// The repeat delay.
        /// </summary>
        private double repeatDelay;

        /// <summary>
        /// The duration.
        /// </summary>
        private double duration;

        /// <summary>
        /// The time.
        /// </summary>
        private double time;

        #endregion

        #region Fields

        /// <summary>
        /// The first update.
        /// </summary>
        private bool firstUpdate;

        /// <summary>
        /// The repeat count.
        /// </summary>
        private int repeatCount;

        /// <summary>
        /// The times repeated.
        /// </summary>
        private int timesRepeated;

        /// <summary>
        /// The behavior.
        /// </summary>
        private LerpBehavior behavior;

        /// <summary>
        /// The vars.
        /// </summary>
        private List<GlideInfo> vars;

        /// <summary>
        /// The lerpers.
        /// </summary>
        private List<Lerper> lerpers;

        /// <summary>
        /// The end.
        /// </summary>
        private List<object> end;

        /// <summary>
        /// The start.
        /// </summary>
        private List<object> start;

        /// <summary>
        /// The var hash.
        /// </summary>
        private Dictionary<string, int> varHash;

        /// <summary>
        /// The parent.
        /// </summary>
        private Tweener Parent;

        /// <summary>
        /// The remover.
        /// </summary>
        private Tweener Remover;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tween"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="delay">The delay.</param>
        /// <param name="parent">The parent.</param>
        internal Tween(object target, double duration, double delay, Tweener parent)
        {
            Target = target;
            this.duration = duration;
            this.delay = delay;
            Parent = parent;
            Remover = parent;

            firstUpdate = true;

            varHash = new Dictionary<string, int>();
            vars = new List<GlideInfo>();
            lerpers = new List<Lerper>();
            start = new List<object>();
            end = new List<object>();
            behavior = LerpBehavior.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The time remaining before the tween ends or repeats.
        /// </summary>
        public double TimeRemaining
            => duration - time;

        /// <summary>
        /// A value between 0 and 1, where 0 means the tween has not been started and 1 means that it has completed.
        /// </summary>
        public double Completion
        {
            get
            {
                var c = time / duration;
                return c < 0 ? 0 : (c > 1 ? 1 : c);
            }
        }

        /// <summary>
        /// Whether the tween is currently looping.
        /// </summary>
        public bool Looping
            => repeatCount != 0;

        /// <summary>
        /// The object this tween targets. Will be null if the tween represents a timer.
        /// </summary>
        public object Target { get; }

        #endregion

        /// <summary>
        /// Add the lerp.
        /// </summary>
        /// <param name="lerper">The lerper.</param>
        /// <param name="info">The info.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        internal void AddLerp(Lerper lerper, GlideInfo info, object from, object to)
        {
            varHash.Add(info.MemberName, vars.Count);
            vars.Add(info);

            start.Add(from);
            end.Add(to);

            lerpers.Add(lerper);
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        internal void Update(double elapsed)
        {
            if (firstUpdate)
            {
                firstUpdate = false;

                var i = vars.Count;
                while (i-- > 0)
                    lerpers[i]?.Initialize(start[i], end[i], behavior);
            }
            else
            {
                if (Paused)
                    return;

                if (delay > 0)
                {
                    delay -= elapsed;
                    if (delay > 0)
                        return;
                }

                if (Abs(time) < Epsilon && timesRepeated == 0 && begin != null)
                    begin();

                time += elapsed;
                var setTimeTo = time;
                var t = time / duration;
                var doComplete = false;

                if (time >= duration)
                {
                    if (repeatCount != 0)
                    {
                        setTimeTo = 0;
                        delay = repeatDelay;
                        timesRepeated++;

                        if (repeatCount > 0)
                            --repeatCount;

                        doComplete |= repeatCount < 0;
                    }
                    else
                    {
                        time = duration;
                        t = 1;
                        Remover.Remove(this);
                        doComplete = true;
                    }
                }

                if (ease != null)
                    t = ease(t);

                var i = vars.Count;
                while (i-- > 0)
                {
                    if (vars[i] != null)
                        vars[i].Value = lerpers[i]?.Interpolate(t, vars[i]?.Value, behavior);
                }

                time = setTimeTo;

                //	If the timer is zero here, we just restarted.
                //	If reflect mode is on, flip start to end
                if (Abs(time) < Epsilon && behavior.HasFlag(LerpBehavior.Reflect))
                    Reverse();

                update?.Invoke();

                if (doComplete && complete != null)
                    complete();
            }
        }

        #region Behavior

        /// <summary>
        /// Apply target values to a starting point before tweening.
        /// </summary>
        /// <param name="values">The values to apply, in an anonymous type ( new { prop1 = 100, prop2 = 0} ).</param>
        /// <returns>A reference to this.</returns>
        public Tween From(object values)
        {
            var props = values.GetType().GetProperties();
            for (var i = 0; i < props.Length; ++i)
            {
                var property = props[i];
                var propValue = property.GetValue(values, null);

                var index = -1;
                if (varHash.TryGetValue(property.Name, out index))
                {
                    //	if we're already tweening this value, adjust the range
                    start[index] = propValue;
                }

                //	if we aren't tweening this value, just set it
                var info = new GlideInfo(Target, property.Name, true)
                {
                    Value = propValue
                };
            }

            return this;
        }

        /// <summary>
        /// Set the easing function.
        /// </summary>
        /// <param name="ease">The Easer to use.</param>
        /// <returns>A reference to this.</returns>
        public Tween Ease(Func<double, double> ease)
        {
            this.ease = ease;
            return this;
        }

        /// <summary>
        /// Set a function to call when the tween begins (useful when using delays). Can be called multiple times for compound callbacks.
        /// </summary>
        /// <param name="callback">The function that will be called when the tween starts, after the delay.</param>
        /// <returns>A reference to this.</returns>
        public Tween OnBegin(Action callback)
        {
            if (begin == null)
                begin = callback;
            else
                begin += callback;
            return this;
        }

        /// <summary>
        /// Set a function to call when the tween finishes. Can be called multiple times for compound callbacks.
        /// If the tween repeats infinitely, this will be called each time; otherwise it will only run when the tween is finished repeating.
        /// </summary>
        /// <param name="callback">The function that will be called on tween completion.</param>
        /// <returns>A reference to this.</returns>
        public Tween OnComplete(Action callback)
        {
            if (complete == null)
                complete = callback;
            else
                complete += callback;
            return this;
        }

        /// <summary>
        /// Set a function to call as the tween updates. Can be called multiple times for compound callbacks.
        /// </summary>
        /// <param name="callback">The function to use.</param>
        /// <returns>A reference to this.</returns>
        public Tween OnUpdate(Action callback)
        {
            if (update == null)
                update = callback;
            else
                update += callback;
            return this;
        }

        /// <summary>
        /// Enable repeating.
        /// </summary>
        /// <param name="times">Number of times to repeat. Leave blank or pass a negative number to repeat infinitely.</param>
        /// <returns>A reference to this.</returns>
        public Tween Repeat(int times = -1)
        {
            repeatCount = times;
            return this;
        }

        /// <summary>
        /// Set a delay for when the tween repeats.
        /// </summary>
        /// <param name="delay">How long to wait before repeating.</param>
        /// <returns>A reference to this.</returns>
        public Tween RepeatDelay(double delay)
        {
            repeatDelay = delay;
            return this;
        }

        /// <summary>
        /// Sets the tween to reverse every other time it repeats. Repeating must be enabled for this to have any effect.
        /// </summary>
        /// <returns>A reference to this.</returns>
        public Tween Reflect()
        {
            behavior |= LerpBehavior.Reflect;
            return this;
        }

        /// <summary>
        /// Swaps the start and end values of the tween.
        /// </summary>
        /// <returns>A reference to this.</returns>
        public Tween Reverse()
        {
            var i = vars.Count;
            while (i-- > 0)
            {
                var s = start[i];
                var e = end[i];

                //	Set start to end and end to start
                start[i] = e;
                end[i] = s;

                lerpers[i].Initialize(e, s, behavior);
            }

            return this;
        }

        /// <summary>
        /// Whether this tween handles rotation.
        /// </summary>
        /// <returns>A reference to this.</returns>
        public Tween Rotation(RotationUnit unit = RotationUnit.Degrees)
        {
            behavior |= LerpBehavior.Rotation;
            behavior |= (unit == RotationUnit.Degrees) ? LerpBehavior.RotationDegrees : LerpBehavior.RotationRadians;

            return this;
        }

        /// <summary>
        /// Whether tweened values should be rounded to integer values.
        /// </summary>
        /// <returns>A reference to this.</returns>
        public Tween Round()
        {
            behavior |= LerpBehavior.Round;
            return this;
        }

        #endregion

        #region Control

        /// <summary>
        /// Cancel tweening given properties.
        /// </summary>
        /// <param name="properties"></param>
        public void Cancel(params string[] properties)
        {
            var canceled = 0;
            for (var i = 0; i < properties.Length; ++i)
            {
                if (!varHash.TryGetValue(properties[i], out int index))
                    continue;

                varHash.Remove(properties[i]);
                vars[index] = null;
                lerpers[index] = null;
                start[index] = null;
                end[index] = null;

                canceled++;
            }

            if (canceled == vars.Count)
                Cancel();
        }

        /// <summary>
        /// Remove tweens from the tweener without calling their complete functions.
        /// </summary>
        public void Cancel()
            => Remover.Remove(this);

        /// <summary>
        /// Assign tweens their final value and remove them from the tweener.
        /// </summary>
        public void CancelAndComplete()
        {
            time = duration;
            update = null;
            Remover.Remove(this);
        }

        /// <summary>
        /// Set tweens to pause. They won't update and their delays won't tick down.
        /// </summary>
        public void Pause()
            => Paused = true;

        /// <summary>
        /// Toggle tweens' paused value.
        /// </summary>
        public void PauseToggle()
            => Paused = !Paused;

        /// <summary>
        /// Resumes tweens from a paused state.
        /// </summary>
        public void Resume()
            => Paused = false;

        #endregion
    }
}
