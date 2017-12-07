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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AdvBrowsableAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        string name;

        /// <summary>
        /// 
        /// </summary>
        public AdvBrowsableAttribute()
            : this(null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public AdvBrowsableAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
            => name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static PropertyDescriptorCollection GetDispMembers(Type t)
        {
            var order = AdvBrowsableOrderAttribute.GetOrder(t);
            var rv = new List<PropertyDescriptor>();
            object[] atts;
            foreach (PropertyInfo info in t.GetProperties())
            {
                atts = info.GetCustomAttributes(typeof(AdvBrowsableAttribute), true);
                if (atts.Length > 0)
                {
                    var att = (AdvBrowsableAttribute)atts[0];
                    AdvPropertyDescriptor descriptor;
                    if (att.Name != null)
                    {
                        descriptor = new AdvPropertyDescriptor(att.Name, info);
                    }
                    else
                    {
                        descriptor = new AdvPropertyDescriptor(info);
                    }
                    atts = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (atts.Length > 0)
                    {
                        var att2 = (DescriptionAttribute)atts[0];
                        descriptor.SetDescription(att2.Description);
                    }
                    rv.Add(descriptor);
                }
            }
            foreach (FieldInfo info in t.GetFields())
            {
                atts = info.GetCustomAttributes(typeof(AdvBrowsableAttribute), true);
                if (atts.Length > 0)
                {
                    var att = (AdvBrowsableAttribute)atts[0];
                    AdvPropertyDescriptor descriptor;
                    if (att.Name != null)
                    {
                        descriptor = new AdvPropertyDescriptor(att.Name, info);
                    }
                    else
                    {
                        descriptor = new AdvPropertyDescriptor(info);
                    }
                    atts = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (atts.Length > 0)
                    {
                        var att2 = (DescriptionAttribute)atts[0];
                        descriptor.SetDescription(att2.Description);
                    }
                    rv.Add(descriptor);
                }
            }
            if (rv.Count == 0)
            {
                return null;
            }
            if (order != null)
            {
                return new PropertyDescriptorCollection(rv.ToArray()).Sort(order);
            }
            return new PropertyDescriptorCollection(rv.ToArray());
        }
    }
}
