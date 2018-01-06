// <copyright file="AdvBrowsableAttribute.cs" company="" >
//     Copyright © 2005 - 2007 Jonathan Mark Porter.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// The adv browsable attribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AdvBrowsableAttribute
        : Attribute
    {
        /// <summary>
        /// The name.
        /// </summary>
        string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvBrowsableAttribute"/> class.
        /// </summary>
        public AdvBrowsableAttribute()
            : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvBrowsableAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AdvBrowsableAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
            => name;

        /// <summary>
        /// Get the disp members.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="PropertyDescriptorCollection"/>.</returns>
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
                    descriptor = att.Name != null ? new AdvPropertyDescriptor(att.Name, info) : new AdvPropertyDescriptor(info);
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
                    descriptor = att.Name != null ? new AdvPropertyDescriptor(att.Name, info) : new AdvPropertyDescriptor(info);
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
