// <copyright file="MatrixTypes.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine.Geometry
{
    /// <summary>
    /// MatrixTypes
    /// </summary>
    /// <remarks>http://referencesource.microsoft.com</remarks>
    [Flags]
    internal enum MatrixTypes
    {
        IDENTITY = 0,
        TRANSLATION = 1,
        SCALING = 2,
        UNKNOWN = 4
    }
}
