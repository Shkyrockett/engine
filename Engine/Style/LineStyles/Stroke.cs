// <copyright file="LineStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The stroke class.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Stroke
        : IStroke
    {
        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// 
        ///// </summary>
        //private float width;

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineCapStyle startCap;

        ///// <summary>
        ///// 
        ///// </summary>
        //private DashCap dashCap;

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineCapStyle endCap;

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineDashStyle dashstyle;

        ///// <summary>
        ///// 
        ///// </summary>
        //private PenAlignment alignment;

        ///// <summary>
        ///// 
        ///// </summary>
        //private LineJoin lineJoin;

        ///// <summary>
        ///// 
        ///// </summary>
        //private float miterLimit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stroke"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        public Stroke(IFill brush, double width = 1)
        {
            Width = 1;
            MiterLimit = 0;
            Fill = brush;
            StartCap = LineCapStyle.Flat;
            DashCap = LineCapStyle.Flat;
            EndCap = LineCapStyle.Flat;
            DashStyle = LineDashStyle.Solid;
            //Alignment = PenAlignment.Center;
            //LineJoin = LineJoin.Miter;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [NotifyParentProperty(true)]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the miter limit.
        /// </summary>
        [NotifyParentProperty(true)]
        public double MiterLimit { get; set; }

        /// <summary>
        /// Gets or sets the start cap.
        /// </summary>
        [NotifyParentProperty(true)]
        public LineCapStyle StartCap { get; set; }

        /// <summary>
        /// Gets or sets the dash cap.
        /// </summary>
        [NotifyParentProperty(true)]
        public LineCapStyle DashCap { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[NotifyParentProperty(true)]
        //public DashCap DashCap
        //{
        //    get { return dashCap; }
        //    set
        //    {
        //        OnPropertyChanging();
        //        dashCap = value;
        //        OnPropertyChanged();
        //    }
        //}

        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        [NotifyParentProperty(true)]
        public LineCapStyle EndCap { get; set; }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        [NotifyParentProperty(true)]
        public LineDashStyle DashStyle { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[NotifyParentProperty(true)]
        //public PenAlignment Alignment
        //{
        //    get { return alignment; }
        //    set
        //    {
        //        OnPropertyChanging();
        //        alignment = value;
        //        OnPropertyChanged();
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[NotifyParentProperty(true)]
        //public LineJoin LineJoin
        //{
        //    get { return lineJoin; }
        //    set
        //    {
        //        OnPropertyChanging();
        //        lineJoin = value;
        //        OnPropertyChanged();
        //    }
        //}

        /// <summary>
        /// Gets or sets the fill.
        /// </summary>
        [NotifyParentProperty(true)]
        public IFill Fill { get; set; }

        public float[] DashPattern { get; set; }

        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanging([CallerMemberName] string name = "")
            => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
