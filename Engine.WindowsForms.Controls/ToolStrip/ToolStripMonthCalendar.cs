﻿// <copyright file="ToolStripMonthCalendar.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms.Design;

namespace Engine.WindowsForms;

/// <summary>
/// The tool strip month calendar class.
/// </summary>
/// <remarks>
/// <para>https://msdn.microsoft.com/library/9k5etstz.aspx
/// https://msdn.microsoft.com/library/ms233664.aspx</para>
/// </remarks>
//[ToolboxBitmapAttribute("")]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public partial class ToolStripMonthCalendar
    : ToolStripControlHost
{
    private static readonly MonthCalendar calendar = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolStripMonthCalendar"/> class.
    /// </summary>
    public ToolStripMonthCalendar()
        : base(calendar)
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets the month calendar control.
    /// </summary>
    public MonthCalendar MonthCalendarControl
        => Control as MonthCalendar;

    /// <summary>
    /// Gets or sets the first day of week.
    /// </summary>
    public Day FirstDayOfWeek
    {
        get { return MonthCalendarControl.FirstDayOfWeek; }
        set { MonthCalendarControl.FirstDayOfWeek = value; }
    }

    /// <summary>
    /// Add the bolded date.
    /// </summary>
    /// <param name="dateToBold">The dateToBold.</param>
    public void AddBoldedDate(DateTime dateToBold)
        => MonthCalendarControl.AddBoldedDate(dateToBold);

    /// <summary>
    /// Raises the subscribe control events event.
    /// </summary>
    /// <param name="control">The control.</param>
    protected override void OnSubscribeControlEvents(Control control)
    {
        // Call the base so the base events are connected.
        base.OnSubscribeControlEvents(control);

        // Cast the control to a MonthCalendar control.
        if (control is MonthCalendar monthCalendarControl)

            // Add the event.
            monthCalendarControl.DateChanged += OnDateChanged;
    }

    /// <summary>
    /// Raises the unsubscribe control events event.
    /// </summary>
    /// <param name="control">The control.</param>
    protected override void OnUnsubscribeControlEvents(Control control)
    {
        // Call the base method so the basic events are unsubscripted.
        base.OnUnsubscribeControlEvents(control);

        // Cast the control to a MonthCalendar control.
        if (control is MonthCalendar monthCalendarControl)

            // Remove the event.
            monthCalendarControl.DateChanged -= OnDateChanged;
    }

    /// <summary>
    /// The date changed event of the <see cref="DateRangeEventHandler"/>.
    /// </summary>
    public event DateRangeEventHandler DateChanged;

    /// <summary>
    /// Raises the date changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The date range event arguments.</param>
    private void OnDateChanged(object sender, DateRangeEventArgs e)
        => DateChanged?.Invoke(this, e);
}
