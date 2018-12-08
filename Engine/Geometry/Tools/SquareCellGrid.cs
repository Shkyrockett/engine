// <copyright file="SquareCellGrid.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// <see cref="SquareCellGrid"/> class for handling calculating the scaling and positioning of cells in a grid.
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/39217512/make-a-1d-array-of-a-2d-grid-with-x-and-y-coordinates-x-and-y-are-not-referring
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(SquareCellGrid))]
    public class SquareCellGrid
        : Shape
    {
        #region Fields
        /// <summary>
        /// The x.
        /// </summary>
        private int x;

        /// <summary>
        /// The y.
        /// </summary>
        private int y;

        /// <summary>
        /// The h.
        /// </summary>
        private int width;

        /// <summary>
        /// The v.
        /// </summary>
        private int height;

        /// <summary>
        /// The number of cells the grid should contain.
        /// </summary>
        private int count;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SquareCellGrid"/> class.
        /// </summary>
        public SquareCellGrid()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareCellGrid"/> class.
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        public SquareCellGrid(Rectangle2D bounds, int count)
            : this((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height, count)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareCellGrid"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="count">The count.</param>
        public SquareCellGrid(int x, int y, int width, int height, int count)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.count = count;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstructor.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="count">The count.</param>
        public void Deconstruct(out int x, out int y, out int width, out int height, out int count)
        {
            x = this.x;
            y = this.y;
            width = this.width;
            height = this.height;
            count = this.count;
        }
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// Gets the index of a cell at a given point in the grid.
        /// </summary>
        /// <param name="location">The location of the point in the grid to look up the index of the cell beneath the point.</param>
        /// <returns>
        /// The index of the cell under the point in the grid or -1 if a cell is not found.
        /// </returns>
        /// <remarks>
        /// https://www.cyotek.com/blog/converting-2d-arrays-to-1d-and-accessing-as-either-2d-or-1d
        /// </remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int this[Point2D location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!InnerBounds.Contains(location))
                {
                    return -1;
                }

                // Find the horizontal and vertical integer indexes.
                (var dx, var dy) = ((int)((location.X - x) / CellSize.Width), (int)((location.Y - y) / CellSize.Height));

                var (columns, rows) = (Columns, Rows);

                // Omit any rows or columns that are out of range.
                if (dx < 0 || dx >= columns || dy < 0 || dy >= rows)
                {
                    return -1;
                }

                // Calculate the index of the item under the point location.
                var value = dy * columns + dx;

                // Return only valid cells.
                return (value < count) ? value : -1;
            }
        }

        /// <summary>
        /// Gets the <see cref="Rectangle2D"/> representing the bounding box of the cell at a given index of the grid. 
        /// </summary>
        /// <param name="index">The index of a cell in the grid.</param>
        /// <returns>A <see cref="Point2D"/> representing the top left corner of the cell at the given index.</returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Rectangle2D this[int index]
        {
            get
            {
                // ToDo: Implement flow orientation options.
                var point = new Point2D(
                    x + index % Columns * CellSize.Width,
                    y + index / Columns * CellSize.Height
                    );
                return new Rectangle2D(point, CellSize);
            }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int X
        {
            get { return x; }
            set
            {
                OnPropertyChanging(nameof(X));
                x = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Y
        {
            get { return y; }
            set
            {
                OnPropertyChanging(nameof(Y));
                y = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Width
        {
            get { return width; }
            set
            {
                OnPropertyChanging(nameof(Width));
                width = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Height
        {
            get { return height; }
            set
            {
                OnPropertyChanging(nameof(Height));
                height = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the number of cells the grid is to contain.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Count
        {
            get { return count; }
            set
            {
                OnPropertyChanging(nameof(Count));
                count = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the calculated optimum cell scale.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int CellScale
            => (int)CachingProperty(() => Min(width / Columns, height / Rows));

        /// <summary>
        /// Gets the calculated optimum <see cref="Size2D"/> height and width of any cell in the grid.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Size2D CellSize
            => (Size2D)CachingProperty(() => new Size2D(CellScale, CellScale));

        /// <summary>
        /// Gets the inner-bounding <see cref="Rectangle2D"/> of the grid. 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Rectangle2D InnerBounds
            => (Rectangle2D)CachingProperty(() => new Rectangle2D(new Point2D(x, y), new Size2D(Columns * CellSize.Width, Rows * CellSize.Height)));

        /// <summary>
        /// Gets the calculated optimum number of columns the grid can contain for its height and width.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Columns
            => (int)CachingProperty(() => (int)Ceiling(Sqrt(count)));

        /// <summary>
        /// Gets the calculated optimum number of rows the grid can contain for its height and width.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Rows
            => (int)CachingProperty(() => Columns);

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle2D"/> to contain the grid. 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public new Rectangle2D Bounds
        {
            get { return new Rectangle2D(x, y, width, height); }
            set
            {
                OnPropertyChanging(nameof(Bounds));
                x = (int)value.X;
                y = (int)value.Y;
                width = (int)value.Width;
                height = (int)value.Height;
                ClearCache();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(SquareCellGrid)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(SquareCellGrid)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(SquareCellGrid)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(SquareCellGrid)} has been deserialized.");
        //}

        //#endregion

        #region Methods
        /// <summary>
        /// Converts the attributes of this <see cref="SquareCellGrid"/> to a human-readable string. 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public override string ConvertToString(string format, IFormatProvider provider)
            => $"{nameof(SquareCellGrid)}{{Bounds{{{Bounds}}},Count {count}}}";
        #endregion Methods
    }
}
