// <copyright file="GlideInfo.cs" >
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
        /// <summary>
        /// 
        /// </summary>
        private static BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private FieldInfo field;

        /// <summary>
        /// 
        /// </summary>
        private PropertyInfo prop;

        /// <summary>
        /// 
        /// </summary>
        private object Target;

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return field != null ? field.GetValue(Target) : prop.GetValue(Target, null); }
            set
            {
                if (field != null) field.SetValue(Target, value);
                else prop.SetValue(Target, value, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        public GlideInfo(object target, PropertyInfo info)
        {
            Target = target;
            prop = info;
            PropertyName = info.Name;
            PropertyType = prop.PropertyType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        public GlideInfo(object target, FieldInfo info)
        {
            Target = target;
            field = info;
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
            Target = target;
            PropertyName = property;

            var targetType = target as Type ?? target.GetType();

            if ((field = targetType.GetField(property, flags)) != null)
            {
                PropertyType = field.FieldType;
            }
            else if ((prop = targetType.GetProperty(property, flags)) != null)
            {
                PropertyType = prop.PropertyType;
            }
            else
            {
                //	Couldn't find either
                throw new Exception(string.Format("Field or {0} property '{1}' not found on object of type {2}.",
                    writeRequired ? "read/write" : "readable",
                    property, targetType.FullName));
            }
        }
    }
}
