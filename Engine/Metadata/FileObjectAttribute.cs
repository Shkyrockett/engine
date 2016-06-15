// <copyright file="FileObjectAttribute.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//      Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary>
//      Meta-data class used to tag a class to capture in a file objects tree.
// </summary>
// <notes></notes>
// <references>
//      Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/84c42s56.aspx">Writing Custom Attributes</a>.
//      Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/71s1zwct.aspx">Retrieving Information Stored in Attributes</a>.
// </references>

using System;

namespace Engine
{
    /// <summary>
    /// Attribute used to tag a class as a file object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class FileObjectAttribute
        : Attribute
    {
    }
}
