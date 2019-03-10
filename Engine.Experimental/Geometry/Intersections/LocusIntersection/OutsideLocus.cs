// <copyright file="OutsideLocus.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// <see cref="OutsideLocus"/> is a special case of the <see cref="EmptyLocus"/> where there are no intersections because the shape is outside.
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class OutsideLocus
        : EmptyLocus
    {
    }
}
