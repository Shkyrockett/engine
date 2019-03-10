// <copyright file="DisplayNameAttribute.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <notes></notes>
// <references>
//   Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/84c42s56.aspx">Writing Custom Attributes</a>.
//   Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/71s1zwct.aspx">Retrieving Information Stored in Attributes</a>.
// </references>

using System;

namespace Engine
{
    /// <summary>
    /// Attribute used to attach a display string to the meta-data of a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class DisplayNameAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class.
        /// </summary>
        /// <param name="displayName"></param>
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the string used as a display name.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
