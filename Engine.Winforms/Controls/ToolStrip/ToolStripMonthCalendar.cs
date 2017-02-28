// <copyright file="ToolStripMonthCalendar.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/library/9k5etstz.aspx
    /// https://msdn.microsoft.com/library/ms233664.aspx
    /// </remarks>
    //[ToolboxBitmapAttribute("")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public partial class ToolStripMonthCalendar
        : ToolStripControlHost
    {
        /// <summary>
        /// 
        /// </summary>
        public ToolStripMonthCalendar()
            : base(new MonthCalendar())
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public MonthCalendar MonthCalendarControl
            => Control as MonthCalendar;

        /// <summary>
        /// 
        /// </summary>
        public Day FirstDayOfWeek
        {
            get { return MonthCalendarControl.FirstDayOfWeek; }
            set { MonthCalendarControl.FirstDayOfWeek = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateToBold"></param>
        public void AddBoldedDate(DateTime dateToBold)
            => MonthCalendarControl.AddBoldedDate(dateToBold);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        protected override void OnSubscribeControlEvents(Control control)
        {
            // Call the base so the base events are connected.
            base.OnSubscribeControlEvents(control);

            // Cast the control to a MonthCalendar control.
            var monthCalendarControl = control as MonthCalendar;

            // Add the event.
            monthCalendarControl.DateChanged += OnDateChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            // Call the base method so the basic events are unsubscripted.
            base.OnUnsubscribeControlEvents(control);

            // Cast the control to a MonthCalendar control.
            var monthCalendarControl = control as MonthCalendar;

            // Remove the event.
            monthCalendarControl.DateChanged -= OnDateChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        public event DateRangeEventHandler DateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDateChanged(object sender, DateRangeEventArgs e)
            => DateChanged?.Invoke(this, e);
    }
}
