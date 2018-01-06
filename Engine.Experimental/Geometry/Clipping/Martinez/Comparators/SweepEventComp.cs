// <copyright file="SegmentComp.cs" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> http://www4.ujaen.es/~fmartin/bool_op.html </remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The sweep event comp class.
    /// </summary>
    public class SweepEventComp
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
        => SegmentComparators.SweepEventComp(e1, e2);
    }
}
