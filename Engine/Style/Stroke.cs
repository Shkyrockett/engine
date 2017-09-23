// <copyright file="LineStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
//using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Stroke
        : IStroke
    {
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
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

        [NotifyParentProperty(true)]
        public double Width { get; set; }

        [NotifyParentProperty(true)]
        public double MiterLimit { get; set; }

        [NotifyParentProperty(true)]
        public LineCapStyle StartCap { get; set; }

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

        [NotifyParentProperty(true)]
        public LineCapStyle EndCap { get; set; }

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

        [NotifyParentProperty(true)]
        public IFill Fill { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanging([CallerMemberName] string name = "")
            => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
