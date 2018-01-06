// <copyright file="RectangleCellGrid.cs" company="Shkyrockett" >
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
    /// <see cref="RectangleCellGrid"/> class for handling calculating the scaling and positioning of cells in a grid.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    //[DisplayName(nameof(RectangleCellGrid))]
    public class RectangleCellGrid
        : Shape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private int x;

        /// <summary>
        /// 
        /// </summary>
        private int y;

        /// <summary>
        /// 
        /// </summary>
        private int h;

        /// <summary>
        /// 
        /// </summary>
        private int v;

        /// <summary>
        /// The number of cells the grid should contain.
        /// </summary>
        private int count;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public RectangleCellGrid()
            : this(0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleCellGrid"/> class.
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        public RectangleCellGrid(Rectangle2D bounds, int count)
            : this((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height, count)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="count"></param>
        public RectangleCellGrid(int x, int y, int width, int height, int count)
            : base()
        {
            this.x = x;
            this.y = y;
            h = width;
            v = height;
            this.count = count;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="count"></param>
        public void Deconstruct(out int x, out int y, out int width, out int height, out int count)
        {
            x = this.x;
            y = this.y;
            width = h;
            height = v;
            count = this.count;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets the index of a cell at a given point in the grid.
        /// </summary>
        /// <param name="location">The location of the point in the grid to look up the index of the cell beneath the point.</param>
        /// <returns>The index of the cell under the point in the grid or -1 if a cell is not found.</returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int this[Point2D location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!InnerBounds.Contains(location))
                    return -1;

                // Calculate the index of the item under the point location.
                var value = ((((location.Y - y) / CellSize.Height) % Rows) * Columns) + (((location.X - x) / CellSize.Width) % Columns);

                // Return only valid cells.
                return (value < count) ? (int)value : -1;
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
                var point = new Point2D(x + (index % Columns) * CellSize.Width, y + (index / Columns) * CellSize.Height);
                return new Rectangle2D(point, CellSize);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Width
        {
            get { return h; }
            set
            {
                h = value;
                ClearCache();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public int Height
        {
            get { return v; }
            set
            {
                v = value;
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
            => (int)CachingProperty(() => Min(h / Columns, v / Rows));

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
            => (int)CachingProperty(() => (int)Ceiling(Sqrt((h * count) / v)));

        /// <summary>
        /// Gets the calculated optimum number of rows the grid can contain for its height and width.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Rows
            => (int)CachingProperty(() => (int)Ceiling((double)count / Columns));

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle2D"/> to contain the grid. 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public new Rectangle2D Bounds
        {
            get { return new Rectangle2D(x, y, h, v); }
            set
            {
                x = (int)value.X;
                y = (int)value.Y;
                h = (int)value.Width;
                v = (int)value.Height;
                ClearCache();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RectangleCellGrid)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RectangleCellGrid)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RectangleCellGrid)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RectangleCellGrid)} has been deserialized.");
        //}

        //#endregion

        #region Methods

        /// <summary>
        /// Converts the attributes of this <see cref="RectangleCellGrid"/> to a human-readable string. 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public override string ConvertToString(string format, IFormatProvider provider)
            => $"{nameof(RectangleCellGrid)}{{Bounds{{{Bounds}}}, Count {count}}}";

        #endregion
    }
}
