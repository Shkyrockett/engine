// <copyright file="WindowsMessages.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Winforms.Direct2D
{
    /// <summary>
    /// The window styles enum.
    /// </summary>
    [Flags]
    public enum WindowStyles
        : uint
    {
        /// <summary>
        /// The WS_OVERLAPPED = 0x00000000.
        /// </summary>
        WS_OVERLAPPED = 0x00000000,

        /// <summary>
        /// The WS_POPUP = 0x80000000.
        /// </summary>
        WS_POPUP = 0x80000000,

        /// <summary>
        /// The WS_CHILD = 0x40000000.
        /// </summary>
        WS_CHILD = 0x40000000,

        /// <summary>
        /// The WS_MINIMIZE = 0x20000000.
        /// </summary>
        WS_MINIMIZE = 0x20000000,

        /// <summary>
        /// The WS_VISIBLE = 0x10000000.
        /// </summary>
        WS_VISIBLE = 0x10000000,

        /// <summary>
        /// The WS_DISABLED = 0x08000000.
        /// </summary>
        WS_DISABLED = 0x08000000,

        /// <summary>
        /// The WS_CLIPSIBLINGS = 0x04000000.
        /// </summary>
        WS_CLIPSIBLINGS = 0x04000000,

        /// <summary>
        /// The WS_CLIPCHILDREN = 0x02000000.
        /// </summary>
        WS_CLIPCHILDREN = 0x02000000,

        /// <summary>
        /// The WS_MAXIMIZE = 0x01000000.
        /// </summary>
        WS_MAXIMIZE = 0x01000000,

        /// <summary>
        /// The WS_BORDER = 0x00800000.
        /// </summary>
        WS_BORDER = 0x00800000,

        /// <summary>
        /// The WS_DLGFRAME = 0x00400000.
        /// </summary>
        WS_DLGFRAME = 0x00400000,

        /// <summary>
        /// The WS_VSCROLL = 0x00200000.
        /// </summary>
        WS_VSCROLL = 0x00200000,

        /// <summary>
        /// The WS_HSCROLL = 0x00100000.
        /// </summary>
        WS_HSCROLL = 0x00100000,

        /// <summary>
        /// The WS_SYSMENU = 0x00080000.
        /// </summary>
        WS_SYSMENU = 0x00080000,

        /// <summary>
        /// The WS_THICKFRAME = 0x00040000.
        /// </summary>
        WS_THICKFRAME = 0x00040000,

        /// <summary>
        /// The WS_GROUP = 0x00020000.
        /// </summary>
        WS_GROUP = 0x00020000,

        /// <summary>
        /// The WS_TABSTOP = 0x00010000.
        /// </summary>
        WS_TABSTOP = 0x00010000,


        /// <summary>
        /// The WS_MINIMIZEBOX = 0x00020000.
        /// </summary>
        WS_MINIMIZEBOX = 0x00020000,

        /// <summary>
        /// The WS_MAXIMIZEBOX = 0x00010000.
        /// </summary>
        WS_MAXIMIZEBOX = 0x00010000,


        /// <summary>
        /// The WS_CAPTION = WS_BORDER | WS_DLGFRAME.
        /// </summary>
        WS_CAPTION = WS_BORDER | WS_DLGFRAME,

        /// <summary>
        /// The WS_TILED = WS_OVERLAPPED.
        /// </summary>
        WS_TILED = WS_OVERLAPPED,

        /// <summary>
        /// The WS_ICONIC = WS_MINIMIZE.
        /// </summary>
        WS_ICONIC = WS_MINIMIZE,

        /// <summary>
        /// The WS_SIZEBOX = WS_THICKFRAME.
        /// </summary>
        WS_SIZEBOX = WS_THICKFRAME,

        /// <summary>
        /// The WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW.
        /// </summary>
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,


        /// <summary>
        /// The WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX.
        /// </summary>
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        /// <summary>
        /// The WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU.
        /// </summary>
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

        /// <summary>
        /// The WS_CHILDWINDOW = WS_CHILD.
        /// </summary>
        WS_CHILDWINDOW = WS_CHILD,

        //Extended Window Styles


        /// <summary>
        /// The WS_EX_DLGMODALFRAME = 0x00000001.
        /// </summary>
        WS_EX_DLGMODALFRAME = 0x00000001,

        /// <summary>
        /// The WS_EX_NOPARENTNOTIFY = 0x00000004.
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 0x00000004,

        /// <summary>
        /// The WS_EX_TOPMOST = 0x00000008.
        /// </summary>
        WS_EX_TOPMOST = 0x00000008,

        /// <summary>
        /// The WS_EX_ACCEPTFILES = 0x00000010.
        /// </summary>
        WS_EX_ACCEPTFILES = 0x00000010,

        /// <summary>
        /// The WS_EX_TRANSPARENT = 0x00000020.
        /// </summary>
        WS_EX_TRANSPARENT = 0x00000020,

        //#if(WINVER >= 0x0400)

        /// <summary>
        /// The WS_EX_MDICHILD = 0x00000040.
        /// </summary>
        WS_EX_MDICHILD = 0x00000040,

        /// <summary>
        /// The WS_EX_TOOLWINDOW = 0x00000080.
        /// </summary>
        WS_EX_TOOLWINDOW = 0x00000080,

        /// <summary>
        /// The WS_EX_WINDOWEDGE = 0x00000100.
        /// </summary>
        WS_EX_WINDOWEDGE = 0x00000100,

        /// <summary>
        /// The WS_EX_CLIENTEDGE = 0x00000200.
        /// </summary>
        WS_EX_CLIENTEDGE = 0x00000200,

        /// <summary>
        /// The WS_EX_CONTEXTHELP = 0x00000400.
        /// </summary>
        WS_EX_CONTEXTHELP = 0x00000400,


        /// <summary>
        /// The WS_EX_RIGHT = 0x00001000.
        /// </summary>
        WS_EX_RIGHT = 0x00001000,

        /// <summary>
        /// The WS_EX_LEFT = 0x00000000.
        /// </summary>
        WS_EX_LEFT = 0x00000000,

        /// <summary>
        /// The WS_EX_RTLREADING = 0x00002000.
        /// </summary>
        WS_EX_RTLREADING = 0x00002000,

        /// <summary>
        /// The WS_EX_LTRREADING = 0x00000000.
        /// </summary>
        WS_EX_LTRREADING = 0x00000000,

        /// <summary>
        /// The WS_EX_LEFTSCROLLBAR = 0x00004000.
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 0x00004000,

        /// <summary>
        /// The WS_EX_RIGHTSCROLLBAR = 0x00000000.
        /// </summary>
        WS_EX_RIGHTSCROLLBAR = 0x00000000,


        /// <summary>
        /// The WS_EX_CONTROLPARENT = 0x00010000.
        /// </summary>
        WS_EX_CONTROLPARENT = 0x00010000,

        /// <summary>
        /// The WS_EX_STATICEDGE = 0x00020000.
        /// </summary>
        WS_EX_STATICEDGE = 0x00020000,

        /// <summary>
        /// The WS_EX_APPWINDOW = 0x00040000.
        /// </summary>
        WS_EX_APPWINDOW = 0x00040000,


        /// <summary>
        /// The WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE.
        /// </summary>
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

        /// <summary>
        /// The WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST.
        /// </summary>
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
        //#endif /* WINVER >= 0x0400 */

        //#if(WIN32WINNT >= 0x0500)

        /// <summary>
        /// The WS_EX_LAYERED = 0x00080000.
        /// </summary>
        WS_EX_LAYERED = 0x00080000,
        //#endif /* WIN32WINNT >= 0x0500 */

        //#if(WINVER >= 0x0500)

        /// <summary>
        /// The WS_EX_NOINHERITLAYOUT = 0x00100000.
        /// </summary>
        WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritance of mirroring by children

        /// <summary>
        /// The WS_EX_LAYOUTRTL = 0x00400000.
        /// </summary>
        WS_EX_LAYOUTRTL = 0x00400000,       // Right to left mirroring

        //#endif /* WINVER >= 0x0500 */
        //#if(WIN32WINNT >= 0x0500)

        /// <summary>
        /// The WS_EX_COMPOSITED = 0x02000000.
        /// </summary>
        WS_EX_COMPOSITED = 0x02000000,

        /// <summary>
        /// The WS_EX_NOACTIVATE = 0x08000000.
        /// </summary>
        WS_EX_NOACTIVATE = 0x08000000
        //#endif /* WIN32WINNT >= 0x0500 */
    }
}
