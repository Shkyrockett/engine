﻿// <copyright file="SquareDCellGrid.cs">
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
    /// <see cref="SquareDCellGrid"/> class for handling calculating the scaling and positioning of cells in a grid.
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(SquareDCellGrid))]
    public class SquareDCellGrid
        : Shape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double h;

        /// <summary>
        /// 
        /// </summary>
        private double v;

        /// <summary>
        /// The number of cells the grid should contain.
        /// </summary>
        private int count;

        /// <summary>
        /// The calculated optimal <see cref="Size2D"/> height and width of the cells in the grid.
        /// </summary>
        [NonSerialized()]
        private Size2D cellSize;

        /// <summary>
        /// The calculated inner <see cref="Rectangle2D"/> bounds of the grid.
        /// </summary>
        [NonSerialized()]
        private Rectangle2D innerBounds;

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
        public SquareDCellGrid()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareDCellGrid"/> class.
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        public SquareDCellGrid(Rectangle2D bounds, int count)
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
        public SquareDCellGrid(double x, double y, double width, double height, int count)
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
        public int this[Point2D location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!innerBounds.Contains(location))
                    return -1;

                // Calculate the index of the item under the point location.
                var value = (int)(((((location.Y - y) / cellSize.Height) % rows) * columns) + (((location.X - x) / cellSize.Width) % columns));

                // Return only valid cells.
                return (value < count) ? value : -1;
            }
        }

        /// <summary>
        /// Gets the <see cref="Rectangle2D"/> representing the bounding box of the cell at a given index of the grid. 
        /// </summary>
        /// <param name="index">The index of a cell in the grid.</param>
        /// <returns>A <see cref="Point"/> representing the top left corner of the cell at the given index.</returns>
        [XmlIgnore]
        public Rectangle2D this[int index]
        {
            get
            {
                // ToDo: Implement flow orientation options.
                var point = new Point2D(x + (index % columns) * cellSize.Width, y + (index / columns) * cellSize.Height);
                return new Rectangle2D(point, cellSize);
            }
        }

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle2D"/> to contain the grid. 
        /// </summary>
        [XmlIgnore]
        public new Rectangle2D Bounds
        {
            get { return new Rectangle2D(x, y, h, v); }
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
        public double X
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
        public double Y
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
        public double Width
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
        public double Height
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
        /// Gets the calculated optimum <see cref="Size2D"/> height and width of any cell in the grid.
        /// </summary>
        [XmlIgnore]
        public Size2D CellSize
            => cellSize;

        /// <summary>
        /// Gets the inner-bounding <see cref="Rectangle2D"/> of the grid. 
        /// </summary>
        [XmlIgnore]
        public Rectangle2D InnerBounds
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
                // Find the best fitting square grid for the number of colors.
                columns = (int)Ceiling(Sqrt(count));
                rows = columns;

                // Calculate the optimum cell size for the grid.
                var cellScale = Min(h / columns, v / rows);

                // Set the size of the cell.
                cellSize = new Size2D(cellScale, cellScale);

                // Set up the inner boundaries of the grid to the canvas size.
                innerBounds = new Rectangle2D(new Point2D(x, y), new Size2D(columns * cellSize.Width, rows * cellSize.Height));
            }
        }

        /// <summary>
        /// Calculate the columns, rows, cell sizes, and inner boundaries for the grid. 
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        private void Recalculate(Rectangle2D bounds, int count)
        {
            x = bounds.X;
            y = bounds.Y;
            h = bounds.Width;
            v = bounds.Height;
            this.count = count;
            Recalculate();
        }

        /// <summary>
        /// Converts the attributes of this <see cref="SquareDCellGrid"/> to a human-readable string. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(SquareDCellGrid)}{{Bounds {{{Bounds}}}, Count {count}}}";

        #endregion
    }
}
