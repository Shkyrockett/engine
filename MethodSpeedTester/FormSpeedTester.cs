// <copyright file="FormSpeedTester.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    /// <summary>
    /// Speed tester form.
    /// </summary>
    public partial class FormSpeedTester
        : Form
    {
        /// <summary>
        /// List of tests to run.
        /// </summary>
        private List<SpeedTester> tests = new List<SpeedTester>();

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
        /// Event handler for changing the sets of tests to run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTests_SelectionChangeCommitted(object sender, EventArgs e)
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
        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        /// <summary>
        /// Event handler for copying the grid data to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectAll();
                Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
            }
        }

        /// <summary>
        /// Run the tests.
        /// </summary>
        private void RunTests()
        {
            foreach (SpeedTester test in tests)
                test.RunTest((int)numericUpDownTrials.Value);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tests;

            DataGridViewCell minCell = dataGridView1.Rows.Cast<DataGridViewRow>().Aggregate((i, j) => Convert.ToInt32(i.Cells[1].Value) < Convert.ToInt32(j.Cells[1].Value) ? i : j).Cells[1];
            DataGridViewCell maxCell = dataGridView1.Rows.Cast<DataGridViewRow>().Aggregate((i, j) => Convert.ToInt32(i.Cells[1].Value) > Convert.ToInt32(j.Cells[1].Value) ? i : j).Cells[1];
            maxCell.Style.BackColor = Color.PeachPuff;
            maxCell.Style.ForeColor = Color.DarkRed;
            maxCell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            minCell.Style.BackColor = Color.MintCream;
            minCell.Style.ForeColor = Color.DarkGreen;
            minCell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);

            buttonCopy.Enabled = true;
        }
    }
}
