// <copyright file="Tweener.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Engine.Tweening
{
    /// <summary>
    /// The tweener class.
    /// </summary>
    [DataContract, Serializable]
    public class Tweener
        : ITweener
    {
        #region Fields
        /// <summary>
        /// The tweens.
        /// </summary>
        private readonly Dictionary<object, List<ITween>> tweens;

        /// <summary>
        /// The to remove.
        /// </summary>
        private readonly List<ITween> toRemove;

        /// <summary>
        /// The to add.
        /// </summary>
        private readonly List<ITween> toAdd;

        /// <summary>
        /// The all tweens.
        /// </summary>
        private readonly List<ITween> allTweens;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes static members of the <see cref="Tweener"/> class.
        /// </summary>
        static Tweener()
        {
            // Add Numeric Lerpers.
            RegisteredLerpers = new Dictionary<Type, ConstructorInfo>();
            var numericTypes = new Type[] {
                    typeof(byte),
                    typeof(sbyte),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(float),
                    typeof(double)
                };

            foreach (var numericType in numericTypes)
            {
                RegisterLerper<NumericLerper>(numericType);
            }

            // Adding custom Lerpers. Normally these would be added to project initializer, but I want these global.
            RegisterLerper<Point2DLerper>(typeof(Point2D));
            RegisterLerper<Point3DLerper>(typeof(Point3D));
            RegisterLerper<Size2DLerper>(typeof(Size2D));
            RegisterLerper<Vector2DLerper>(typeof(Vector2D));
            RegisterLerper<Vector3DLerper>(typeof(Vector3D));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tweener"/> class.
        /// </summary>
        public Tweener()
        {
            tweens = new Dictionary<object, List<ITween>>();
            toRemove = new List<ITween>();
            toAdd = new List<ITween>();
            allTweens = new List<ITween>();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the registered lerpers.
        /// </summary>
        public static Dictionary<Type, ConstructorInfo> RegisteredLerpers { get; set; }
        #endregion Properties

        /// <summary>
        /// Associate a Lerper class with a property type.
        /// </summary>
        /// <typeparam name="TLerper">The Lerper class to use for properties of the given type.</typeparam>
        /// <param name="propertyType">The type of the property to associate the given Lerper with.</param>
        public static void RegisterLerper<TLerper>(Type propertyType) where TLerper : IMemberLerper, new() => RegisterLerper(typeof(TLerper), propertyType);

        /// <summary>
        /// Associate a Lerper type with a property type.
        /// </summary>
        /// <param name="lerperType">The type of the Lerper to use for properties of the given type.</param>
        /// <param name="propertyType">The type of the property to associate the given Lerper with.</param>
        public static void RegisterLerper(Type lerperType, Type propertyType) => RegisteredLerpers[propertyType] = lerperType?.GetConstructor(Type.EmptyTypes);

        /// <summary>
        /// <para>Tweens a set of properties on an object.</para>
        /// <para>To tween instance properties/fields, pass the object.</para>
        /// <para>To tween static properties/fields, pass the type of the object, using typeof(ObjectType) or object.GetType().</para>
        /// </summary>
        /// <param name="target">The object or type to tween.</param>
        /// <param name="dests">The values to tween to, in an anonymous type ( new { prop1 = 100, prop2 = 0} ).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="delay">Delay before the tween starts, in seconds.</param>
        /// <param name="overwrite">Whether preexisting tweens should be overwritten if this tween involves the same properties.</param>
        /// <returns>The tween created, for setting properties on.</returns>
        public ITween Tween<T>(T target, object dests, double duration, double delay = 0, bool overwrite = true) where T : class
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            // Prevent tweening on structs if you cheat by casting target as Object
            var targetType = target.GetType();
            if (targetType.IsValueType)
            {
                throw new Exception("Target of tween cannot be a struct!");
            }

            var tween = new Tween(target, duration, delay, this);
            toAdd.Add(tween);

            // valid in case of manual timer
            if (dests is null)
            {
                return tween;
            }

            var props = dests.GetType().GetProperties();
            for (var i = 0; i < props.Length; ++i)
            {
                if (overwrite && tweens.TryGetValue(target, out var library))
                {
                    for (var j = 0; j < library.Count; j++)
                    {
                        library[j].Cancel(props[i].Name);
                    }
                }

                var property = props[i];
                var info = new MemberAccessor(target, property.Name);
                var to = new MemberAccessor(dests, property.Name, false);
                var lerper = CreateLerper(info.MemberType);

                tween.AddLerp(lerper, info, info.Value, to.Value);
            }

            AddAndRemove();
            return tween;
        }

        /// <summary>
        /// Starts a simple timer for setting up callback scheduling.
        /// </summary>
        /// <param name="duration">How long the timer will run for, in seconds.</param>
        /// <param name="delay">How long to wait before starting the timer, in seconds.</param>
        /// <returns>The tween created, for setting properties.</returns>
        public ITween Timer(double duration, double delay = 0)
        {
            var tween = new Tween(null, duration, delay, this);
            AddAndRemove();
            toAdd.Add(tween);
            return tween;
        }

        /// <summary>
        /// Remove tweens from the tweener without calling their complete functions.
        /// </summary>
        public void Cancel() => toRemove.AddRange(allTweens);

        /// <summary>
        /// Assign tweens their final value and remove them from the tweener.
        /// </summary>
        public void CancelAndComplete()
        {
            foreach (var tween in allTweens)
            {
                tween.CancelAndComplete();
            }
        }

        /// <summary>
        /// Set tweens to pause. They won't update and their delays won't tick down.
        /// </summary>
        public void Pause()
        {
            foreach (var tween in allTweens)
            {
                tween.Pause();
            }
        }

        /// <summary>
        /// Toggle tweens' paused value.
        /// </summary>
        public void PauseToggle()
        {
            foreach (var tween in allTweens)
            {
                tween.PauseToggle();
            }
        }

        /// <summary>
        /// Resumes tweens from a paused state.
        /// </summary>
        public void Resume()
        {
            foreach (var tween in allTweens)
            {
                tween.Resume();
            }
        }

        /// <summary>
        /// Updates the tweener and all objects it contains.
        /// </summary>
        /// <param name="ticksElapsed">Seconds elapsed since last update.</param>
        public void Update(double ticksElapsed)
        {
            foreach (var tween in allTweens)
            {
                tween.Update(ticksElapsed);
            }

            AddAndRemove();
        }

        /// <summary>
        /// Create the lerper.
        /// </summary>
        /// <param name="propertyType">The propertyType.</param>
        /// <returns>The <see cref="MemberLerper"/>.</returns>
        /// <exception cref="Exception"></exception>
        private static IMemberLerper CreateLerper(Type propertyType)
        {
            if (!RegisteredLerpers.TryGetValue(propertyType, out var lerper))
            {
                throw new Exception($"No {nameof(IMemberLerper)} found for type {propertyType.FullName}.");
            }

            return lerper.Invoke(null) as IMemberLerper;
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="tween">The tween.</param>
        internal void Remove(ITween tween) => toRemove.Add(tween);

        /// <summary>
        /// Add the and remove.
        /// </summary>
        private void AddAndRemove()
        {
            foreach (var tween in toAdd)
            {
                allTweens.Add(tween);

                // don't sort timers by target
                if (tween.Target is null)
                {
                    continue;
                }

                if (!tweens.TryGetValue(tween.Target, out var list))
                {
                    tweens[tween.Target] = list = new List<ITween>();
                }

                list.Add(tween);
            }

            foreach (var tween in toRemove)
            {
                allTweens.Remove(tween);

                // don't sort timers by target
                if (tween.Target is null)
                {
                    continue;
                }

                if (tweens.TryGetValue(tween.Target, out var list))
                {
                    list.Remove(tween);
                    if (list.Count == 0)
                    {
                        tweens.Remove(tween.Target);
                    }
                }

                allTweens.Remove(tween);
            }

            toAdd.Clear();
            toRemove.Clear();
        }

        #region Target Control
        /// <summary>
        /// Cancel all tweens with the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to cancel.</param>
        public void TargetCancel(object target)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.Cancel();
                }
            }
        }

        /// <summary>
        /// Cancel tweening named properties on the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to cancel properties on.</param>
        /// <param name="properties">The properties to cancel.</param>
        public void TargetCancel(object target, params string[] properties)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.Cancel(properties);
                }
            }
        }

        /// <summary>
        /// Cancel, complete, and call complete callbacks for all tweens with the given target..
        /// </summary>
        /// <param name="target">The object being tweened that you want to cancel and complete.</param>
        public void TargetCancelAndComplete(object target)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.CancelAndComplete();
                }
            }
        }

        /// <summary>
        /// Pause all tweens with the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to pause.</param>
        public void TargetPause(object target)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.Pause();
                }
            }
        }

        /// <summary>
        /// Toggle the pause state of all tweens with the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to toggle pause.</param>
        public void TargetPauseToggle(object target)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.PauseToggle();
                }
            }
        }

        /// <summary>
        /// Resume all tweens with the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to resume.</param>
        public void TargetResume(object target)
        {
            if (tweens.TryGetValue(target, out var list))
            {
                foreach (var tween in list)
                {
                    tween.Resume();
                }
            }
        }
        #endregion Target Control
    }
}
