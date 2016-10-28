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
using static System.Math;

namespace Engine.Geometry
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
        /// The exterior <see cref="Rectangle"/> bounds of the grid.
        /// </summary>
        private Rectangle bounds;

        /// <summary>
        /// The number of cells the grid should contain.
        /// </summary>
        private int count;

        /// <summary>
        /// The calculated optimal <see cref="Size"/> height and width of the cells in the grid.
        /// </summary>
        private Size cellSize;

        /// <summary>
        /// The calculated inner <see cref="Rectangle"/> bounds of the grid.
        /// </summary>
        private Rectangle innerBounds;

        /// <summary>
        /// The calculated optimal number of columns the grid can contain for it's height and width.
        /// </summary>
        private int columns;

        /// <summary>
        /// The calculated optimal number of rows the grid can contain for it's height and width.
        /// </summary>
        private int rows;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleCellGrid"/> class.
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        public RectangleCellGrid(Rectangle bounds, int count)
        {
            this.bounds = bounds;
            this.count = count;
            Recalculate();
        }

        #endregion

        /// <summary>
        /// Gets the index of a cell at a given point in the grid.
        /// </summary>
        /// <param name="location">The location of the point in the grid to look up the index of the cell beneath the point.</param>
        /// <returns>The index of the cell under the point in the grid or -1 if a cell is not found.</returns>
        public int this[Point location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!innerBounds.Contains(location))
                    return -1;

                // Calculate the index of the item under the point location.
                int value = (((location.Y / cellSize.Height) % rows) * columns) + ((location.X / cellSize.Width) % columns);

                // Return only valid cells.
                return (value < count) ? value : -1;
            }
        }

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle"/> to contain the grid. 
        /// </summary>
        public new Rectangle Bounds
        {
            get { return bounds; }
            set
            {
                bounds = value;
                Recalculate();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the number of cells the grid is to contain.
        /// </summary>
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
        public Size CellSize
            => cellSize;

        /// <summary>
        /// Gets the inner-bounding <see cref="Rectangle"/> of the grid. 
        /// </summary>
        public Rectangle InnerBounds
            => innerBounds;

        /// <summary>
        /// Gets the calculated optimum number of columns the grid can contain for its height and width.
        /// </summary>
        public int Columns
            => columns;

        /// <summary>
        /// Gets the calculated optimum number of rows the grid can contain for its height and width.
        /// </summary>
        public int Rows
            => rows;

        /// <summary>
        /// Gets the <see cref="Rectangle"/> representing the bounding box of the cell at a given index of the grid. 
        /// </summary>
        /// <param name="index">The index of a cell in the grid.</param>
        /// <returns>A <see cref="Point"/> representing the top left corner of the cell at the given index.</returns>
        public Rectangle this[int index]
        {
            get
            {
                // ToDo: Implement flow orientation options.
                var point = new Point((index % columns) * cellSize.Width, (index / columns) * cellSize.Height);
                return new Rectangle(point, cellSize);
            }
        }

        /// <summary>
        /// Calculate the columns, rows, cell sizes, and inner boundaries for the grid. 
        /// </summary>
        private void Recalculate()
        {
            if (count > 0)
            {
                // Find the best fitting rectangular grid for the number of colors.
                columns = (int)Ceiling(Sqrt((bounds.Width * count) / bounds.Height));
                rows = (int)Ceiling((double)count / columns);

                // Calculate the optimum cell size for the grid.
                int cellScale = Min(bounds.Width / columns, bounds.Height / rows);

                // Set the size of the cell.
                cellSize = new Size(cellScale, cellScale);

                // Set up the inner boundaries of the grid to the canvas size.
                innerBounds = new Rectangle(Point.Empty, new Size(columns * cellSize.Width, rows * cellSize.Height));
            }
        }

        /// <summary>
        /// Calculate the columns, rows, cell sizes, and inner boundaries for the grid. 
        /// </summary>
        /// <param name="bounds">The exterior bounding rectangle to contain the grid.</param>
        /// <param name="count">The number of cells the grid is to contain.</param>
        private void Recalculate(Rectangle bounds, int count)
        {
            this.bounds = bounds;
            this.count = count;
            Recalculate();
        }

        /// <summary>
        /// Converts the attributes of this <see cref="RectangleCellGrid"/> to a human-readable string. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"RectangleCellGrid{{Bounds {{{bounds}}}, Count {count}}}";
    }
}
