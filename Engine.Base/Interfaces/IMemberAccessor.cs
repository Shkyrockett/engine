// <copyright file="IMemberAccessor.cs" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMemberAccessor
    {
        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        string MemberName { get; }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        /// <value>
        /// The type of the member.
        /// </value>
        Type MemberType { get; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        object Target { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        object Value { get; set; }
    }
}