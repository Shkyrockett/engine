// <copyright file="ReverseSweepEventComp.cs" >
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
    public class ReverseSweepEventComp
        : Comparer<SweepEvent>
    {
        /// <summary>
        /// Compare two sweep events
        /// Return 1 means that e1 is placed at the event queue after e2, i.e,, e1 is processed by the algorithm after e2
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public override int Compare(SweepEvent e1, SweepEvent e2)
            => SegmentComparators.ReverseSweepEventComp(e1, e2);
    }
}
