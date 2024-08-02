namespace Engine;

/// <summary>
/// The priority queue class.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks> <para>http://stackoverflow.com/a/33888482</para> </remarks>
public class PriorityQueue<T>
{
    #region Fields
    /// <summary>
    /// The comparer.
    /// </summary>
    private readonly IComparer<T> comparer;

    /// <summary>
    /// The heap.
    /// </summary>
    private T[] heap;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    public PriorityQueue()
        : this(null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    public PriorityQueue(int capacity)
        : this(capacity, null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    public PriorityQueue(IComparer<T> comparer)
        : this(16, comparer)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    /// <param name="comparer">The comparer.</param>
    public PriorityQueue(int capacity, IComparer<T> comparer)
    {
        this.comparer = comparer ?? Comparer<T>.Default;
        heap = new T[capacity];
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets the count.
    /// </summary>
    public int Count
        => heap.Length;

    /// <summary>
    /// Gets a value indicating whether 
    /// </summary>
    internal bool IsEmpty
        => heap.Length == 0;
    #endregion Properties

    /// <summary>
    /// Push.
    /// </summary>
    /// <param name="v">The v.</param>
    public void Push(T v)
    {
        if (Count >= heap.Length)
        {
            Array.Resize(ref heap, Count * 2);
        }

        heap[Count] = v;
        SiftUp(Count);
        //SiftUp(Count++);
    }

    /// <summary>
    /// The pop.
    /// </summary>
    /// <returns>The <see cref="Type"/>.</returns>
    public T Pop()
    {
        var v = Top();
        heap[0] = heap[Count];
        //heap[0] = heap[--Count];
        if (Count > 0)
        {
            SiftDown(0);
        }

        return v;
    }

    /// <summary>
    /// The top.
    /// </summary>
    /// <returns>The <see cref="Type"/>.</returns>
    /// <exception cref="InvalidOperationException">The priority queue is empty.</exception>
    public T Top()
    {
        if (Count > 0)
        {
            return heap[0];
        }

        throw new InvalidOperationException("The priority queue is empty.");
    }

    /// <summary>
    /// The sift up.
    /// </summary>
    /// <param name="n">The n.</param>
    private void SiftUp(int n)
    {
        var v = heap[n];
        for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2)
        {
            heap[n] = heap[n2];
        }

        heap[n] = v;
    }

    /// <summary>
    /// The sift down.
    /// </summary>
    /// <param name="n">The n.</param>
    private void SiftDown(int n)
    {
        var v = heap[n];
        for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
        {
            if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0)
            {
                n2++;
            }

            if (comparer.Compare(v, heap[n2]) >= 0)
            {
                break;
            }

            heap[n] = heap[n2];
        }
        heap[n] = v;
    }
}
