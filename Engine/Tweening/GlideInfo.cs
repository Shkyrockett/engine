﻿// <copyright file="GlideInfo.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

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
            PropertyName = info.Name;
            PropertyType = info.PropertyType;
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
            PropertyName = info.Name;
            PropertyType = info.FieldType;
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
            PropertyName = property;

            Type targetType = target as Type ?? target.GetType();

            if ((member = targetType.GetField(property, flags)) != null)
            {
                PropertyType = (member as FieldInfo).FieldType;
            }
            else if ((member = targetType.GetProperty(property, flags)) != null)
            {
                PropertyType = (member as PropertyInfo).PropertyType;
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
        public string PropertyName { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return member is FieldInfo ? (member as FieldInfo).GetValue(target) : (member as PropertyInfo).GetValue(target, null); }
            set
            {
                if (member is FieldInfo) (member as FieldInfo).SetValue(target, value);
                else if (member is PropertyInfo) (member as PropertyInfo).SetValue(target, value, null);
            }
        }

        #endregion
    }
}
