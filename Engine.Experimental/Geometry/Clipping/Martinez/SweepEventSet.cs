// <copyright file="Connector.cs" >
//     Copyright © 2011 Mahir Iqbal. All rights reserved.
// </copyright>
// <author id="akavel">Mahir Iqbal</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/akavel/martinez-src </remarks>

using System.Collections.Generic;
using static Engine.SegmentComparators;

namespace Engine
{
    /// <summary>
    /// The sweep event set class.
    /// </summary>
    public class SweepEventSet
    {
        /// <summary>
        /// The event set.
        /// </summary>
        public List<SweepEvent> eventSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SweepEventSet"/> class.
        /// </summary>
        public SweepEventSet()
        {
            eventSet = new List<SweepEvent>();
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(SweepEvent key)
        {
            var keyIndex = eventSet.IndexOf(key);
            if (keyIndex == -1)
                return;

            eventSet.Splice(keyIndex, 1);
        }

        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Insert(SweepEvent item)
        {
            var length = eventSet.Count;
            if (length == 0)
            {
                eventSet.Add(item);
                return 0;
            }

            eventSet.Add(null); // Expand the Vector by one.

            var i = length - 1;
            while (i >= 0 && SegmentComp(item, eventSet[i]) == 1)// reverseSC(eventSet[i], item) == 1)
            {
                eventSet[i + 1] = eventSet[i];
                i--;
            }
            eventSet[i + 1] = item;
            return i + 1;

            // Actual insertion sort
            /*for (var j:int = 1; j < eventSet.length; j++)
			{
				var key:SweepEvent = eventSet[j];
				var i:int = j - 1;
				while (i >= 0 && reverseSC(eventSet[i], key) == 1)
				{
					eventSet[i + 1] = eventSet[i];
					i--
				}
				eventSet[i + 1] = key;
			}*/
        }
    }
}
