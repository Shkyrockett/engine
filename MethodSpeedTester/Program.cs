// <copyright file="Program.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Windows.Forms;

namespace MethodSpeedTester
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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RectanglePointTester());
            //Application.Run(new CirclePointTester());
            //Application.Run(new PolygonPointTester());
            //Application.Run(new FormSpeedTester());
        }
    }
}
