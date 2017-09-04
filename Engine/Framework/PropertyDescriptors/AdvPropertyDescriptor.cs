/*
 * Copyright © 2005-2007 Jonathan Mark Porter
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
 * the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.ComponentModel;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvPropertyDescriptor
        : PropertyDescriptor, IEquatable<AdvPropertyDescriptor>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        MemberInfo info;

        /// <summary>
        /// 
        /// </summary>
        FieldInfo field;

        /// <summary>
        /// 
        /// </summary>
        PropertyInfo property;

        /// <summary>
        /// 
        /// </summary>
        string description;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        public AdvPropertyDescriptor(FieldInfo field)
            : this(field.Name, field)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="field"></param>
        public AdvPropertyDescriptor(string name, FieldInfo field)
            : base(name, (Attribute[])field.GetCustomAttributes(typeof(Attribute), true))
        {
            info = field;
            this.field = field;
            description = base.Description;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        public AdvPropertyDescriptor(PropertyInfo property)
            : this(property.Name, property)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        public AdvPropertyDescriptor(string name, PropertyInfo property)
            : base(name, (Attribute[])property.GetCustomAttributes(typeof(Attribute), true))
        {
            info = property;
            this.property = property;
            this.property = property;
            description = base.Description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override string Description
            => description;

        /// <summary>
        /// 
        /// </summary>
        public override bool IsReadOnly
            => !(property == null || !property.CanWrite);

        /// <summary>
        /// 
        /// </summary>
        public override Type ComponentType
            => info.DeclaringType;

        /// <summary>
        /// 
        /// </summary>
        public override Type PropertyType
        {
            get
            {
                if (field == null)
                {
                    return property.PropertyType;
                }
                return field.FieldType;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
            => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(object component)
            => throw new NotSupportedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override object GetValue(object component)
        {
            if (field == null)
            {
                return property.GetValue(component, null);
            }
            return field.GetValue(component);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
        {
            if (field == null)
            {
                property.SetValue(component, value, null);
            }
            else
            {
                field.SetValue(component, value);
            }
            OnValueChanged(component, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
            => true;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => info.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetDescription(string value)
            => description = value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is AdvPropertyDescriptor && Equals((AdvPropertyDescriptor)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AdvPropertyDescriptor other)
            => info.Equals(other.info);

        #endregion
    }
}
