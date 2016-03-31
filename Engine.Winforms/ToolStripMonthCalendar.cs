using System;
using System.ComponentModel;
using System.Drawing;
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
        {
            get { return Control as MonthCalendar; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Day FirstDayOfWeek
        {
            get
            {
                return MonthCalendarControl.FirstDayOfWeek;
            }
            set { MonthCalendarControl.FirstDayOfWeek = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateToBold"></param>
        public void AddBoldedDate(DateTime dateToBold)
        {
            MonthCalendarControl.AddBoldedDate(dateToBold);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        protected override void OnSubscribeControlEvents(Control c)
        {
            // Call the base so the base events are connected.
            base.OnSubscribeControlEvents(c);

            // Cast the control to a MonthCalendar control.
            MonthCalendar monthCalendarControl = (MonthCalendar)c;

            // Add the event.
            monthCalendarControl.DateChanged +=
                new DateRangeEventHandler(OnDateChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        protected override void OnUnsubscribeControlEvents(Control c)
        {
            // Call the base method so the basic events are unsubscripted.
            base.OnUnsubscribeControlEvents(c);

            // Cast the control to a MonthCalendar control.
            MonthCalendar monthCalendarControl = (MonthCalendar)c;

            // Remove the event.
            monthCalendarControl.DateChanged -=
                new DateRangeEventHandler(OnDateChanged);
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
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }
    }
}
