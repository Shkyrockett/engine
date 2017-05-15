// <copyright file="GlideInfo.cs" company="Shkyrockett" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
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
    /// 
    /// </summary>
	internal class GlideInfo
    {
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        private const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private MemberInfo member;

        /// <summary>
        /// 
        /// </summary>
        private object target;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        public GlideInfo(object target, PropertyInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.PropertyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        public GlideInfo(object target, FieldInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.FieldType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        public GlideInfo(object target, MemberInfo info)
        {
            this.target = target;
            member = info;
            MemberName = info.Name;
            MemberType = info.MemberType.GetType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="property"></param>
        /// <param name="writeRequired"></param>
        public GlideInfo(object target, string property, bool writeRequired = true)
        {
            this.target = target;
            MemberName = property;

            Type targetType = target as Type ?? target.GetType();

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
        /// 
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type MemberType { get; }

        /// <summary>
        /// 
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
