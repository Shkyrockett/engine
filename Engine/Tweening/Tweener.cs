// <copyright file="Tweener.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Tweener
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, ConstructorInfo> registeredLerpers;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<object, List<Tween>> tweens;

        /// <summary>
        /// 
        /// </summary>
        private List<Tween> toRemove;

        /// <summary>
        /// 
        /// </summary>
        private List<Tween> toAdd;

        /// <summary>
        /// 
        /// </summary>
        private List<Tween> allTweens;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        static Tweener()
        {
            // Add Numeric Lerpers.
            registeredLerpers = new Dictionary<Type, ConstructorInfo>();
            var numericTypes = new Type[] {
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
                SetLerper<NumericLerper>(numericType);
            }

            // Adding custom Lerpers. Normally these would be added to project initializer, but I want these global.
            Tweener.SetLerper<Point2DLerper>(typeof(Point2D));
            Tweener.SetLerper<Point3DLerper>(typeof(Point3D));
            Tweener.SetLerper<Size2DLerper>(typeof(Size2D));
            Tweener.SetLerper<Vector2DLerper>(typeof(Vector2D));
            Tweener.SetLerper<Vector3DLerper>(typeof(Vector3D));
        }

        /// <summary>
        /// 
        /// </summary>
        public Tweener()
        {
            tweens = new Dictionary<object, List<Tween>>();
            toRemove = new List<Tween>();
            toAdd = new List<Tween>();
            allTweens = new List<Tween>();
        }

        #endregion

        /// <summary>
        /// Associate a Lerper class with a property type.
        /// </summary>
        /// <typeparam name="TLerper">The Lerper class to use for properties of the given type.</typeparam>
        /// <param name="propertyType">The type of the property to associate the given Lerper with.</param>
        public static void SetLerper<TLerper>(Type propertyType)
            where TLerper
            : Lerper, new()
        {
            registeredLerpers[propertyType] = typeof(TLerper).GetConstructor(Type.EmptyTypes);
        }

        /// <summary>
        /// <para>Tweens a set of properties on an object.</para>
        /// <para>To tween instance properties/fields, pass the object.</para>
        /// <para>To tween static properties/fields, pass the type of the object, using typeof(ObjectType) or object.GetType().</para>
        /// </summary>
        /// <param name="target">The object or type to tween.</param>
        /// <param name="values">The values to tween to, in an anonymous type ( new { prop1 = 100, prop2 = 0} ).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="delay">Delay before the tween starts, in seconds.</param>
        /// <param name="overwrite">Whether preexisting tweens should be overwritten if this tween involves the same properties.</param>
        /// <returns>The tween created, for setting properties on.</returns>
        public Tween Tween<T>(T target, object values, double duration, double delay = 0, bool overwrite = true) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");

            // Prevent tweening on structs if you cheat by casting target as Object
            var targetType = target.GetType();
            if (targetType.IsValueType) throw new Exception("Target of tween cannot be a struct!");

            var tween = new Tween(target, duration, delay, this);
            AddAndRemove();
            toAdd.Add(tween);

            // valid in case of manual timer
            if (values == null) return tween;

            var props = values.GetType().GetProperties();
            for (int i = 0; i < props.Length; ++i)
            {
                List<Tween> library = null;
                if (overwrite && tweens.TryGetValue(target, out library))
                {
                    for (int j = 0; j < library.Count; j++)
                        library[j].Cancel(props[i].Name);
                }

                var property = props[i];
                var info = new GlideInfo(target, property.Name);
                var to = new GlideInfo(values, property.Name, false);
                var lerper = CreateLerper(info.PropertyType);

                tween.AddLerp(lerper, info, info.Value, to.Value);
            }

            return tween;
        }

        /// <summary>
        /// Starts a simple timer for setting up callback scheduling.
        /// </summary>
        /// <param name="duration">How long the timer will run for, in seconds.</param>
        /// <param name="delay">How long to wait before starting the timer, in seconds.</param>
        /// <returns>The tween created, for setting properties.</returns>
        public Tween Timer(double duration, double delay = 0)
        {
            var tween = new Tween(null, duration, delay, this);
            AddAndRemove();
            toAdd.Add(tween);
            return tween;
        }

        /// <summary>
        /// Remove tweens from the tweener without calling their complete functions.
        /// </summary>
        public void Cancel()
        {
            toRemove.AddRange(allTweens);
        }

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
        /// <param name="secondsElapsed">Seconds elapsed since last update.</param>
        public void Update(double secondsElapsed)
        {
            foreach (var tween in allTweens)
            {
                tween.Update(secondsElapsed);
            }

            AddAndRemove();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        private Lerper CreateLerper(Type propertyType)
        {
            ConstructorInfo lerper = null;
            if (!registeredLerpers.TryGetValue(propertyType, out lerper))
            {
                throw new Exception($"No {nameof(Lerper)} found for type {propertyType.FullName}.");
            }

            return lerper.Invoke(null) as Lerper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tween"></param>
        internal void Remove(Tween tween)
        {
            toRemove.Add(tween);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddAndRemove()
        {
            foreach (var tween in toAdd)
            {
                allTweens.Add(tween);

                // don't sort timers by target
                if (tween.Target == null) continue;

                List<Tween> list = null;
                if (!tweens.TryGetValue(tween.Target, out list))
                {
                    tweens[tween.Target] = list = new List<Tween>();
                }

                list.Add(tween);
            }

            foreach (var tween in toRemove)
            {
                allTweens.Remove(tween);

                // don't sort timers by target
                if (tween.Target == null) continue;

                List<Tween> list = null;
                if (tweens.TryGetValue(tween.Target, out list))
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

        #region Target control

        /// <summary>
        /// Cancel all tweens with the given target.
        /// </summary>
        /// <param name="target">The object being tweened that you want to cancel.</param>
        public void TargetCancel(object target)
        {
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
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
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
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
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
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
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
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
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
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
            List<Tween> list;
            if (tweens.TryGetValue(target, out list))
            {
                foreach (var tween in list)
                {
                    tween.Resume();
                }
            }
        }

        #endregion
    }
}
