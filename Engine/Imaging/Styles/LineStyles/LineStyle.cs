// <copyright file="LineStyle.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LineStyle
        : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        private float width;

        /// <summary>
        /// 
        /// </summary>
        private LineCapStyle startCap;

        /// <summary>
        /// 
        /// </summary>
        private DashCap dashCap;

        /// <summary>
        /// 
        /// </summary>
        private LineCapStyle endCap;

        /// <summary>
        /// 
        /// </summary>
        private LineDashStyle dashstyle;

        /// <summary>
        /// 
        /// </summary>
        private PenAlignment alignment;

        /// <summary>
        /// 
        /// </summary>
        private LineJoin lineJoin;

        /// <summary>
        /// 
        /// </summary>
        private float miterLimit;

        /// <summary>
        /// 
        /// </summary>
        public LineStyle()
        {
            width = 1;
            startCap = LineCapStyle.Flat;
            dashCap = DashCap.Flat;
            endCap = LineCapStyle.Flat;
            dashstyle = LineDashStyle.Solid;
            alignment = PenAlignment.Center;
            lineJoin = LineJoin.Miter;
            miterLimit = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public LineCapStyle StartCap
        {
            get { return startCap; }
            set
            {
                OnPropertyChanging();
                StartCap = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public DashCap DashCap
        {
            get { return dashCap; }
            set
            {
                OnPropertyChanging();
                dashCap = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public LineCapStyle EndCap
        {
            get { return endCap; }
            set
            {
                OnPropertyChanging();
                endCap = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public LineDashStyle Dashstyle
        {
            get { return dashstyle; }
            set
            {
                OnPropertyChanging();
                dashstyle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public PenAlignment Alignment
        {
            get { return alignment; }
            set
            {
                OnPropertyChanging();
                alignment = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public float Width
        {
            get { return width; }
            set
            {
                OnPropertyChanging();
                width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public LineJoin LineJoin
        {
            get { return lineJoin; }
            set
            {
                OnPropertyChanging();
                lineJoin = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        public float MiterLimit
        {
            get { return miterLimit; }
            set
            {
                OnPropertyChanging();
                miterLimit = value;
                OnPropertyChanged();
            }
        }

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
