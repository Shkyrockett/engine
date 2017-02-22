using System;
using System.Collections.Generic;

namespace Engine._Preview
{
    /// <summary>
    /// EventQueue data structure.
    /// @author Mahir Iqbal
    /// </summary>
    public class EventQueue
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private List<SweepEvent> heap;

        /// <summary>
        /// 
        /// </summary>
        private bool sorted = false;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public EventQueue()
        {
            heap = new List<SweepEvent>();
            sorted = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Count
            => heap.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
            => heap.Count == 0;

        #endregion

        // If already sorted use insertionSort on the inserted item.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Enqueue(SweepEvent obj)
        {
            if (sorted)
            {
                var length = heap.Count;
                if (length == 0)
                    heap.Add(obj);

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
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public SweepEvent Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("The priority queue is empty.");
        }
    }
}
