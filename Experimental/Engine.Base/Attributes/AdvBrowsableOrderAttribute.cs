// <copyright file="AdvBrowsableOrderAttribute.cs" company="" >
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

namespace Engine
{
    /// <summary>
    /// The adv browsable order attribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AdvBrowsableOrderAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvBrowsableOrderAttribute"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public AdvBrowsableOrderAttribute(string order)
        {
            Order = order?.Split(',');
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="order"></param>
        //public AdvBrowsableOrderAttribute(params string[] order)
        //{
        //    this.order = order;
        //}

        /// <summary>
        /// Gets the order.
        /// </summary>
        public string[] Order { get; }

        /// <summary>
        /// Get the order.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static string[] GetOrder(Type t)
        {
            var arr = t?.GetCustomAttributes(typeof(AdvBrowsableOrderAttribute), false);
            if (arr?.Length > 0)
            {
                return ((AdvBrowsableOrderAttribute)arr[0]).Order;
            }
            return null;
        }
    }
}
