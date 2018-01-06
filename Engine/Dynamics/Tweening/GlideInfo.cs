// <copyright file="GlideInfo.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using System.Reflection;

namespace Engine.Tweening
{
    /// <summary>
    /// The glide info class.
    /// </summary>
    internal class GlideInfo
    {
        #region Constants

        /// <summary>
        /// The flags (const). Value: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static.
        /// </summary>
        private const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        #endregion

        #region Fields

        /// <summary>
        /// The member.
        /// </summary>
        private MemberInfo member;

        /// <summary>
        /// The target.
        /// </summary>
        private object target;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlideInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="info">The info.</param>
        public GlideInfo(object target, PropertyInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.PropertyType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlideInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="info">The info.</param>
        public GlideInfo(object target, FieldInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.FieldType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlideInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="info">The info.</param>
        public GlideInfo(object target, MemberInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.MemberType.GetType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlideInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="writeRequired">The writeRequired.</param>
        /// <exception cref="Exception">readable</exception>
        public GlideInfo(object target, string property, bool writeRequired = true)
        {
            this.target = target;
            MemberName = property;

            var targetType = target as Type ?? target.GetType();

            if ((member = targetType.GetField(property, flags)) != null)
            {
                MemberType = (member as FieldInfo).FieldType;
            }
            else if ((member = targetType.GetProperty(property, flags)) != null)
            {
                MemberType = (member as PropertyInfo).PropertyType;
            }
            else
            {
                // Couldn't find either member type.
                throw new Exception($"Field or {(writeRequired ? "read/write" : "readable")} property '{property}' not found on object of type {targetType.FullName}.");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// Gets the member type.
        /// </summary>
        public Type MemberType { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value
        {
            get { return member is FieldInfo ? (member as FieldInfo).GetValue(target) : (member as PropertyInfo).GetValue(target, null); }
            set
            {
                if (member is FieldInfo)
                    (member as FieldInfo).SetValue(target, value);
                else if (member is PropertyInfo)
                    (member as PropertyInfo).SetValue(target, value, null);
            }
        }

        #endregion
    }
}
