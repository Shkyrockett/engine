﻿// <copyright file="FormSpeedTester.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace MethodSpeedTester;

/// <summary>
/// Speed tester form.
/// </summary>
public partial class FormSpeedTester
    : Form
{
    /// <summary>
    /// List of tests to run.
    /// </summary>
    private readonly List<SpeedTester> tests = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="FormSpeedTester"/> class.
    /// </summary>
    public FormSpeedTester()
    {
        InitializeComponent();

        comboBoxTests.DataSource = ReflectionHelper.ListStaticFactoryConstructorsList(typeof(Experiments), typeof(List<SpeedTester>));
        comboBoxTests.ValueMember = "Name";
        comboBoxTests.SelectedItem = null;

        dataGridView1.DataSource = tests;
    }

    /// <summary>
    /// Event handler for painting the data grid view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void DataGridView1_Paint(object sender, PaintEventArgs e)
    {
        var dgv = sender as DataGridView;

        if (dgv.Rows.Count == 0)
        {
            using var grfx = e.Graphics;
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };
            using var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            grfx.DrawString("No Results", font, Brushes.Blue, dgv.ClientRectangle, format);
        }
    }

    /// <summary>
    /// Event handler for resizing the data grid view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void DataGridView1_Resize(object sender, EventArgs e)
    {
        var dgv = sender as DataGridView;
        if (dgv.Rows.Count == 0)
        {
            dgv.Invalidate();
        }
    }

    /// <summary>
    /// Event handler for changing the sets of tests to run.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ComboBoxTests_SelectionChangeCommitted(object sender, EventArgs e)
    {
        tests.Clear();
        var testSet = comboBoxTests.SelectedItem as MethodInfo;
        tests.AddRange(testSet?.Invoke(null, null) as List<SpeedTester>);

        dataGridView1.DataSource = null;
        dataGridView1.DataSource = tests;

        buttonRun.Enabled = true;

        RunTests();
    }

    /// <summary>
    /// Event handler for clicking the Run button to kick off the speed tests.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonRun_Click(object sender, EventArgs e)
        => RunTests();

    /// <summary>
    /// Event handler for copying the grid data to the clipboard.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonCopy_Click(object sender, EventArgs e)
    {
        try
        {
            dataGridView1.SelectAll();
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
        }
        catch (System.Runtime.InteropServices.ExternalException)
        { }
    }

    /// <summary>
    /// Run the tests.
    /// </summary>
    /// <remarks>
    /// <para>http://stackoverflow.com/questions/6005865/prevent-net-garbage-collection-for-short-period-of-time/6005949#6005949</para>
    /// </remarks>
    private void RunTests()
    {
        dataGridView1.DataSource = null;

        foreach (var test in tests)
        {
            // Run garbage collection to try to keep each test in about the same conditions. 
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            //GC.TryStartNoGCRegion(15728640);
            //GC.TryStartNoGCRegion(268435456);

            // Putting into low latency mode to try to prevent garbage collection in the middle of tests.
            var oldMode = GCSettings.LatencyMode;
            //RuntimeHelpers.PrepareConstrainedRegions();
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;

            // Run test cases.
            test.RunTest((int)numericUpDownTrials.Value);

            // Restoring the latency mode.
            GCSettings.LatencyMode = oldMode;
            //GC.EndNoGCRegion();
        }

        dataGridView1.DataSource = tests;

        // Find the best performer. 
        var minCell = dataGridView1.Rows.Cast<DataGridViewRow>().Aggregate((i, j) => Convert.ToInt32(i.Cells[1].Value, CultureInfo.InvariantCulture) < Convert.ToInt32(j.Cells[1].Value, CultureInfo.InvariantCulture) ? i : j).Cells[1];

        // Find the worst performer.
        var maxCell = dataGridView1.Rows.Cast<DataGridViewRow>().Aggregate((i, j) => Convert.ToInt32(i.Cells[1].Value, CultureInfo.InvariantCulture) > Convert.ToInt32(j.Cells[1].Value, CultureInfo.InvariantCulture) ? i : j).Cells[1];

        maxCell.Style.BackColor = Color.PeachPuff;
        maxCell.Style.ForeColor = Color.DarkRed;
        maxCell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
        minCell.Style.BackColor = Color.MintCream;
        minCell.Style.ForeColor = Color.DarkGreen;
        minCell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);

        buttonCopy.Enabled = true;
    }
}
