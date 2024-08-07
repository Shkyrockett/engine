﻿// <copyright file="FileObjectAttribute.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//      Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <summary>
//      Meta-data class used to tag a class to capture in a graphics objects tree.
// </summary>
// <notes></notes>
// <references>
//      Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/84c42s56.aspx">Writing Custom Attributes</a>.
//      Based on the information found in the MSDN topic <a href="https://msdn.microsoft.com/library/71s1zwct.aspx">Retrieving Information Stored in Attributes</a>.
// </references>

namespace Engine;

/// <summary>
/// Attribute used to tag a class as a graphics object.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public class GraphicsObjectAttribute
    : Attribute
{ }
