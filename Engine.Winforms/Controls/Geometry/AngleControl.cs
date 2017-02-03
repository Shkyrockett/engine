// <copyright file="AngleControl.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Engine.Physics;
using System.Windows.Forms.Design;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AngleControl
        : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public class ValueChangedEventArgs
            : EventArgs
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            public ValueChangedEventArgs(double value)
                => Value = value;

            /// <summary>
            /// 
            /// </summary>
            public double Value { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueChangedDelegate(object sender, ValueChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        [Category("Value")]
        [Description("This event is raised if the value changes.")]
        public event ValueChangedDelegate ValueChanged;

        /// <summary>
        /// 
        /// </summary>
        private Angles method;

        /// <summary>
        /// 
        /// </summary>
        [Category("Value")]
        [DefaultValue(0)]
        [Bindable(true)]
        public AngleControl()
        {
            InitializeComponent();

            method = Angles.Degree;
            numericUpDown.Value = (decimal)Angle.ToDegrees();
            numericUpDown.Maximum = 360;
            numericUpDown.Minimum = -360;
            numericUpDown.Increment = 15;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Angle
        {
            get { return needleControl1.Angle; }
            set { needleControl1.Angle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IWindowsFormsEditorService EditorService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl.SelectedIndex == 0) method = Angles.Degree;
            else if (tabControl.SelectedIndex == 1) method = Angles.Radian;
            switch (method)
            {
                case Angles.Degree:
                    numericUpDown.Increment = 15;
                    numericUpDown.Maximum = 360;
                    numericUpDown.Minimum = -360;
                    numericUpDown.Value = (decimal)Angle.ToDegrees();
                    break;
                case Angles.Radian:
                    numericUpDown.Value = (decimal)Angle;
                    numericUpDown.Increment = (decimal)(15d.ToRadians());
                    numericUpDown.Maximum = (decimal)(2 * Math.PI);
                    numericUpDown.Minimum = -(decimal)(2 * Math.PI);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl1_ValueChanged(object sender, NeedleControl.ValueChangedEventArgs e)
        {
            switch (method)
            {
                case Angles.Degree:
                    numericUpDown.Value = (decimal)Angle.ToDegrees();
                    break;
                case Angles.Radian:
                    numericUpDown.Value = (decimal)Angle;
                    break;
            }

            ValueChanged?.Invoke(this, new ValueChangedEventArgs(e.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl1_ValueCommitted(object sender, NeedleControl.ValueChangedEventArgs e)
            => EditorService?.CloseDropDown();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var nums = sender as NumericUpDown;
            switch (method)
            {
                case Angles.Degree:
                    Angle = ((double)nums.Value).ToRadians();
                    break;
                case Angles.Radian:
                    Angle = (double)nums.Value;
                    break;
            }

            needleControl1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                EditorService.CloseDropDown();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
            => needleControl1.Invalidate();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPageDegrees_Paint(object sender, PaintEventArgs e)
            => needleControl1.Invalidate();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPageRadians_Paint(object sender, PaintEventArgs e)
            => needleControl1.Invalidate();
    }
}
