// <copyright file="SegmentComp.cs" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class SegmentComp
        : Comparer<SweepEvent>
    {
        /// <summary>
        /// le1 and le2 are the left events of line segments (le1.point, le1.otherEvent.point) and (le2.point, le2.otherEvent.point)
        /// </summary>
        /// <param name="le1"></param>
        /// <param name="le2"></param>
        /// <returns></returns>
        public override int Compare(SweepEvent le1, SweepEvent le2)
            => SegmentComparators.SegmentComp(le1, le2);
    }
}
