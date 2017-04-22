// <copyright file="ElementNameAttribute.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://msdn.microsoft.com/en-us/library/84c42s56(v=vs.110).aspx
// </references>

using System;

namespace Engine.Midi
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/84c42s56(v=vs.110).aspx
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ElementNameAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        private string v;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public ElementNameAttribute(string v)
        {
            this.v = v;
        }
    }
}
