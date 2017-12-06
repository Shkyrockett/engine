using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks> http://stackoverflow.com/a/33888482 </remarks>
    public class PriorityQueue<T>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        IComparer<T> comparer;

        /// <summary>
        /// 
        /// </summary>
        T[] heap;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PriorityQueue()
            : this(null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public PriorityQueue(int capacity)
            : this(capacity, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public PriorityQueue(IComparer<T> comparer)
            : this(16, comparer)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            heap = new T[capacity];
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Count
            => heap.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool IsEmpty
            => heap.Length == 0;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void Push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count);
            //SiftUp(Count++);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            var v = Top();
            heap[0] = heap[Count];
            //heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("The priority queue is empty.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2)
                heap[n] = heap[n2];
            heap[n] = v;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparer.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }
    }
}
