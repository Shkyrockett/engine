using System.Collections;
using System.Runtime.CompilerServices;

namespace Engine.Experimental;

/// <summary>
/// The array map class.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// <para>https://www.cyotek.com/blog/converting-2d-arrays-to-1d-and-accessing-as-either-2d-or-1d
/// https://softwareengineering.stackexchange.com/questions/212808/treating-a-1d-data-structure-as-2d-grid
/// https://stackoverflow.com/questions/5494974/convert-1d-array-index-to-2d-array-index</para>
/// </remarks>
public class ArrayMap<T>
    : IEnumerable<T>
{
    #region Fields
    /// <summary>
    /// The items.
    /// </summary>
    private T[] items;

    /// <summary>
    /// The size.
    /// </summary>
    private Size2D size;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayMap{T}"/> class.
    /// </summary>
    public ArrayMap()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayMap{T}"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public ArrayMap(int width, int height)
      : this(new Size2D(width, height))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayMap{T}"/> class.
    /// </summary>
    /// <param name="size">The size.</param>
    public ArrayMap(Size2D size)
      : this()
    {
        Size = size;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets the count.
    /// </summary>
    public int Count
        => items.Length;

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    public Size2D Size
    {
        get { return size; }
        set
        {
            size = value;
            items = new T[(int)(size.Width * size.Height)];
        }
    }
    #endregion Properties

    #region Indexers
    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <param name="location">The index location.</param>
    /// <returns>One element of type T.</returns>
    public T this[Point2D location]
    {
        get { return this[(int)location.X, (int)location.Y]; }
        set { this[(int)location.X, (int)location.Y] = value; }
    }

    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <param name="x">The index x.</param>
    /// <param name="y">The index y.</param>
    /// <returns>One element of type T.</returns>
    public T this[int x, int y]
    {
        get { return this[GetItemIndex(x, y)]; }
        set { this[GetItemIndex(x, y)] = value; }
    }

    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <param name="index">The index index.</param>
    /// <returns>One element of type T.</returns>
    public T this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }
    #endregion Indexers

    #region Methods
    /// <summary>
    /// Get the item index.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public int GetItemIndex(Point2D point)
        => GetItemIndex((int)point.X, (int)point.Y);

    /// <summary>
    /// Get the item index.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The <see cref="int"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">X is out of range</exception>
    /// <exception cref="IndexOutOfRangeException">Y is out of range</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public int GetItemIndex(int x, int y)
    {
        if (x < 0 || x >= Size.Width || y < 0 || y >= Size.Height)
        {
            return -1;
        }

        return (int)((y * Size.Width) + x);
    }

    /// <summary>
    /// Get the item location.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">Index is out of range</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D GetItemLocation(int index)
    {
        if (index < 0 || index >= items.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range");
        }

        return new Point2D(index % Size.Width, index / Size.Width);
    }

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in items)
        {
            yield return item;
        }
    }
    #endregion Methods
}
