// <copyright file="DisplayNameAttribute.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <notes></notes>
// <references>
//   MSDN <a href="https://msdn.microsoft.com/library/84c42s56.aspx">Writing Custom Attributes</a>.
//   MSDN <a href="https://msdn.microsoft.com/library/71s1zwct.aspx">Retrieving Information Stored in Attributes</a>.
// </references>

using System;

namespace Engine.Midi
{
    /// <summary>
    /// Attribute used to attach a display string to the meta-data of a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class DisplayNameAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
    }
}
