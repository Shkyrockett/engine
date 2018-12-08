using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// EventQueue data structure.
    /// @author Mahir Iqbal
    /// </summary>
    public class EventQueue
    {
        #region Fields
        /// <summary>
        /// The heap.
        /// </summary>
        private readonly List<SweepEvent> heap;

        /// <summary>
        /// The sorted.
        /// </summary>
        private bool sorted = false;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventQueue"/> class.
        /// </summary>
        public EventQueue()
        {
            heap = new List<SweepEvent>();
            sorted = false;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
            => heap.Count;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool IsEmpty
            => heap.Count == 0;
        #endregion Properties

        // If already sorted use insertionSort on the inserted item.

        /// <summary>
        /// The enqueue.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public void Enqueue(SweepEvent obj)
        {
            if (sorted)
            {
                var length = heap.Count;
                if (length == 0)
                {
                    heap.Add(obj);
                }

                heap.Add(null); // Expand the Vector by one.

                var i = length - 1;
                while (i >= 0 && SegmentComparators.ReverseSweepEventComp(obj, heap[i]) == -1)
                {
                    heap[i + 1] = heap[i];
                    i--;
                }
                heap[i + 1] = obj;
            }
            else
            {
                heap.Add(obj);
            }
        }

        /// <summary>
        /// The dequeue.
        /// </summary>
        /// <returns>The <see cref="SweepEvent"/>.</returns>
        public SweepEvent Dequeue()
        {
            if (!sorted)
            {
                sorted = true;
                heap.Sort(SegmentComparators.ReverseSweepEventComp);
            }
            var pop = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            return pop;
        }

        /// <summary>
        /// The top.
        /// </summary>
        /// <returns>The <see cref="SweepEvent"/>.</returns>
        /// <exception cref="InvalidOperationException">The priority queue is empty.</exception>
        public SweepEvent Top()
        {
            if (Count > 0)
            {
                return heap[0];
            }

            throw new InvalidOperationException("The priority queue is empty.");
        }
    }
}
