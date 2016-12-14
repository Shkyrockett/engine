// <copyright file="RectangleCellGrid.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// <see cref="RectangleCellGrid"/> class for handling calculating the scaling and positioning of cells in a grid.
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(RectangleCellGrid))]
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

        /// <summary>
        /// The calculated optimal <see cref="Size"/> height and width of the cells in the grid.
        /// </summary>
        [NonSerialized()]
        private Size cellSize;

        /// <summary>
        /// The calculated inner <see cref="Rectangle"/> bounds of the grid.
        /// </summary>
        [NonSerialized()]
        private Rectangle innerBounds;

        /// <summary>
        /// The calculated optimal number of columns the grid can contain for it's height and width.
        /// </summary>
        [NonSerialized()]
        private int columns;

        /// <summary>
        /// The calculated optimal number of rows the grid can contain for it's height and width.
        /// </summary>
        [NonSerialized()]
        private int rows;

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
        public RectangleCellGrid(Rectangle bounds, int count)
            : this(bounds.X, bounds.Y, bounds.Width, bounds.Height, count)
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
        {
            this.x = x;
            this.y = y;
            this.h = width;
            this.v = height;
            this.count = count;
            Recalculate();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of a cell at a given point in the grid.
        /// </summary>
        /// <param name="location">The location of the point in the grid to look up the index of the cell beneath the point.</param>
        /// <returns>The index of the cell under the point in the grid or -1 if a cell is not found.</returns>
        [XmlIgnore]
        public int this[Point location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!innerBounds.Contains(location))
                    return -1;

                // Calculate the index of the item under the point location.
                var value = ((((location.Y - y) / cellSize.Height) % rows) * columns) + (((location.X - x) / cellSize.Width) % columns);

                // Return only valid cells.
                return (value < count) ? value : -1;
            }
        }

        /// <summary>
        /// Gets the <see cref="Rectangle"/> representing the bounding box of the cell at a given index of the grid. 
        /// </summary>
        /// <param name="index">The index of a cell in the grid.</param>
        /// <returns>A <see cref="Point"/> representing the top left corner of the cell at the given index.</returns>
        [XmlIgnore]
        public Rectangle this[int index]
        {
            get
            {
                // ToDo: Implement flow orientation options.
                var point = new Point(x + (index % columns) * cellSize.Width, y + (index / columns) * cellSize.Height);
                return new Rectangle(point, cellSize);
            }
        }

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle"/> to contain the grid. 
        /// </summary>
        [XmlIgnore]
        public new Rectangle Bounds
        {
            get { return new Rectangle(x, y, h, v); }
            set
            {
                x = value.X;
                y = value.Y;
                h = value.Width;
                v = value.Height;
                Recalculate();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                Recalculate();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                Recalculate();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Width
        {
            get { return h; }
            set
            {
                h = value;
                Recalculate();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Height
        {
            get { return v; }
            set
            {
                v = value;
                Recalculate();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the number of cells the grid is to contain.
        /// </summary>
        [XmlAttribute]
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                Recalculate();
                OnPropertyChanged(nameof(Count));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the calculated optimum <see cref="Size"/> height and width of any cell in the grid.
        /// </summary>
        [XmlIgnore]
        public Size CellSize
            => cellSize;

        /// <summary>
        /// Gets the inner-bounding <see cref="Rectangle"/> of the grid. 
        /// </summary>
        [XmlIgnore]
        public Rectangle InnerBounds
            => innerBounds;

        /// <summary>
        /// Gets the calculated optimum number of columns the grid can contain for its height and width.
        /// </summary>
        [XmlIgnore]
        public int Columns
            => columns;

        /// <summary>
        /// Gets the calculated optimum number of rows the grid can contain for its height and width.
        /// </summary>
        [XmlIgnore]
        public int Rows
            => rows;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        private void OnSerializing(StreamingContext context)
        {
            // member2 = "This value went into the data file during serialization.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        private void OnSerialized(StreamingContext context)
        {
            // member2 = "This value was reset after serialization.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        private void OnDeserializing(StreamingContext context)
        {
            // member3 = "This value was set during deserialization";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        private void OnDeserialized(StreamingContext context)
        {
            // member4 = "This value was set after deserialization.";
            Recalculate();
        }

        /// <summary>
        /// Calculate the columns, rows, cell sizes, and inner boundaries for the grid. 
        /// </summary>
        private void Recalculate()
        {
            if (count > 0)
            {
                // Find the best fitting rectangular grid for the number of colors.
                columns = (int)Ceiling(Sqrt((h * count) / v));
                rows = (int)Ceiling((double)count / columns);

                // Calculate the optimum cell size for the grid.
                var cellScale = Min(h / columns, v / rows);

                // Set the size of the cell.
                cellSize = new Size(cellScale, cellScale);

                // Set up the inner boundaries of the grid to the canvas size.
                innerBounds = new Rectangle(new Point(x, y), new Size(columns * cellSize.Width, rows * cellSize.Height));
            }
        }

        /// <summary>
        /// Calculate the columns, rows, cell sizes, and inner boundaries for the grid. 
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        private void Recalculate(Rectangle bounds, int count)
        {
            x = bounds.X;
            y = bounds.Y;
            h = bounds.Width;
            v = bounds.Height;
            this.count = count;
            Recalculate();
        }

        /// <summary>
        /// Converts the attributes of this <see cref="RectangleCellGrid"/> to a human-readable string. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(RectangleCellGrid)}{{Bounds {{{Bounds}}}, Count {count}}}";

        #endregion
    }
}
