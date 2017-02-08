﻿// <copyright file="RectangleCellGrid.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
            : base()
        {
            this.x = x;
            this.y = y;
            this.h = width;
            this.v = height;
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
            width = this.h;
            height = this.v;
            count = this.count;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets the index of a cell at a given point in the grid.
        /// </summary>
        /// <param name="location">The location of the point in the grid to look up the index of the cell beneath the point.</param>
        /// <returns>The index of the cell under the point in the grid or -1 if a cell is not found.</returns>
        [XmlIgnore, SoapIgnore]
        public int this[Point location]
        {
            get
            {
                // Check whether the point is inside the grid.
                if (!InnerBounds.Contains(location))
                    return -1;

                // Calculate the index of the item under the point location.
                var value = ((((location.Y - y) / CellSize.Height) % Rows) * Columns) + (((location.X - x) / CellSize.Width) % Columns);

                // Return only valid cells.
                return (value < count) ? value : -1;
            }
        }

        /// <summary>
        /// Gets the <see cref="Rectangle"/> representing the bounding box of the cell at a given index of the grid. 
        /// </summary>
        /// <param name="index">The index of a cell in the grid.</param>
        /// <returns>A <see cref="Point"/> representing the top left corner of the cell at the given index.</returns>
        [XmlIgnore, SoapIgnore]
        public Rectangle this[int index]
        {
            get
            {
                // ToDo: Implement flow orientation options.
                var point = new Point(x + (index % Columns) * CellSize.Width, y + (index / Columns) * CellSize.Height);
                return new Rectangle(point, CellSize);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the exterior bounding <see cref="Rectangle"/> to contain the grid. 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public new Rectangle Bounds
        {
            get { return new Rectangle(x, y, h, v); }
            set
            {
                x = value.X;
                y = value.Y;
                h = value.Width;
                v = value.Height;
                ClearCache();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
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
        [XmlAttribute, SoapAttribute]
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
        [XmlAttribute, SoapAttribute]
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
        [XmlAttribute, SoapAttribute]
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
        [XmlAttribute, SoapAttribute]
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
        [XmlIgnore, SoapIgnore]
        public int CellScale
            => (int)CachingProperty(() => Min(h / Columns, v / Rows));

        /// <summary>
        /// Gets the calculated optimum <see cref="Size"/> height and width of any cell in the grid.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Size CellSize
            => (Size)CachingProperty(() => new Size(CellScale, CellScale));

        /// <summary>
        /// Gets the inner-bounding <see cref="Rectangle"/> of the grid. 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Rectangle InnerBounds
            => (Rectangle)CachingProperty(() => new Rectangle(new Point(x, y), new Size(Columns * CellSize.Width, Rows * CellSize.Height)));

        /// <summary>
        /// Gets the calculated optimum number of columns the grid can contain for its height and width.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public int Columns
            => (int)CachingProperty(() => (int)Ceiling(Sqrt((h * count) / v)));

        /// <summary>
        /// Gets the calculated optimum number of rows the grid can contain for its height and width.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public int Rows
            => (int)CachingProperty(() => (int)Ceiling((double)count / Columns));

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

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
