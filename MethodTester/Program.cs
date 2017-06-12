﻿// <copyright file="Program.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Windows.Forms;
using MethodSpeedTester;

namespace MethodTester
{
    /// <summary>
    /// The main entry class for the application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new RectanglePointTester());
            //Application.Run(new CirclePointTester());
            //Application.Run(new PolygonPointTester());
            Application.Run(new FormSpeedTester());
        }
    }
}
