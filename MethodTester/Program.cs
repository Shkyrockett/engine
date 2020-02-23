// <copyright file="Program.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using MethodSpeedTester;
using System;
using System.Windows.Forms;

namespace MethodTester
{
    /// <summary>
    /// The main entry class for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //using (var rectanglePointTester = new RectanglePointTester())
            //{
            //    Application.Run(rectanglePointTester);
            //}
            //using (var circlePointTester = new CirclePointTester())
            //{
            //    Application.Run(circlePointTester);
            //}
            //using (var polygonPointTester = new PolygonPointTester())
            //{
            //    Application.Run(polygonPointTester);
            //}
            using var formSpeedTester = new FormSpeedTester();
            Application.Run(formSpeedTester);
        }
    }
}
