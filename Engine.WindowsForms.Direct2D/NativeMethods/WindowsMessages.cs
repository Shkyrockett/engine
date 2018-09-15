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
    /// The windows messages enum.
    /// </summary>
    [Flags]
    public enum WindowsMessages
    {
        /// <summary>
        /// The WM_NULL = 0x0000.
        /// </summary>
        WM_NULL = 0x0000,

        /// <summary>
        /// The WM_CREATE = 0x0001.
        /// </summary>
        WM_CREATE = 0x0001,

        /// <summary>
        /// The WM_DESTROY = 0x0002.
        /// </summary>
        WM_DESTROY = 0x0002,

        /// <summary>
        /// The WM_MOVE = 0x0003.
        /// </summary>
        WM_MOVE = 0x0003,

        /// <summary>
        /// The WM_SIZE = 0x0005.
        /// </summary>
        WM_SIZE = 0x0005,

        /// <summary>
        /// The WM_ACTIVATE = 0x0006.
        /// </summary>
        WM_ACTIVATE = 0x0006,

        /// <summary>
        /// The WM_SETFOCUS = 0x0007.
        /// </summary>
        WM_SETFOCUS = 0x0007,

        /// <summary>
        /// The WM_KILLFOCUS = 0x0008.
        /// </summary>
        WM_KILLFOCUS = 0x0008,

        /// <summary>
        /// The WM_ENABLE = 0x000A.
        /// </summary>
        WM_ENABLE = 0x000A,

        /// <summary>
        /// The WM_SETREDRAW = 0x000B.
        /// </summary>
        WM_SETREDRAW = 0x000B,

        /// <summary>
        /// The WM_SETTEXT = 0x000C.
        /// </summary>
        WM_SETTEXT = 0x000C,

        /// <summary>
        /// The WM_GETTEXT = 0x000D.
        /// </summary>
        WM_GETTEXT = 0x000D,

        /// <summary>
        /// The WM_GETTEXTLENGTH = 0x000E.
        /// </summary>
        WM_GETTEXTLENGTH = 0x000E,

        /// <summary>
        /// The WM_PAINT = 0x000F.
        /// </summary>
        WM_PAINT = 0x000F,

        /// <summary>
        /// The WM_CLOSE = 0x0010.
        /// </summary>
        WM_CLOSE = 0x0010,

        /// <summary>
        /// The WM_QUERYENDSESSION = 0x0011.
        /// </summary>
        WM_QUERYENDSESSION = 0x0011,

        /// <summary>
        /// The WM_QUERYOPEN = 0x0013.
        /// </summary>
        WM_QUERYOPEN = 0x0013,

        /// <summary>
        /// The WM_ENDSESSION = 0x0016.
        /// </summary>
        WM_ENDSESSION = 0x0016,

        /// <summary>
        /// The WM_QUIT = 0x0012.
        /// </summary>
        WM_QUIT = 0x0012,

        /// <summary>
        /// The WM_ERASEBKGND = 0x0014.
        /// </summary>
        WM_ERASEBKGND = 0x0014,

        /// <summary>
        /// The WM_SYSCOLORCHANGE = 0x0015.
        /// </summary>
        WM_SYSCOLORCHANGE = 0x0015,

        /// <summary>
        /// The WM_SHOWWINDOW = 0x0018.
        /// </summary>
        WM_SHOWWINDOW = 0x0018,

        /// <summary>
        /// The WM_WININICHANGE = 0x001A.
        /// </summary>
        WM_WININICHANGE = 0x001A,

        /// <summary>
        /// The WM_SETTINGCHANGE = WM_WININICHANGE.
        /// </summary>
        WM_SETTINGCHANGE = WM_WININICHANGE,

        /// <summary>
        /// The WM_DEVMODECHANGE = 0x001B.
        /// </summary>
        WM_DEVMODECHANGE = 0x001B,

        /// <summary>
        /// The WM_ACTIVATEAPP = 0x001C.
        /// </summary>
        WM_ACTIVATEAPP = 0x001C,

        /// <summary>
        /// The WM_FONTCHANGE = 0x001D.
        /// </summary>
        WM_FONTCHANGE = 0x001D,

        /// <summary>
        /// The WM_TIMECHANGE = 0x001E.
        /// </summary>
        WM_TIMECHANGE = 0x001E,

        /// <summary>
        /// The WM_CANCELMODE = 0x001F.
        /// </summary>
        WM_CANCELMODE = 0x001F,

        /// <summary>
        /// The WM_SETCURSOR = 0x0020.
        /// </summary>
        WM_SETCURSOR = 0x0020,

        /// <summary>
        /// The WM_MOUSEACTIVATE = 0x0021.
        /// </summary>
        WM_MOUSEACTIVATE = 0x0021,

        /// <summary>
        /// The WM_CHILDACTIVATE = 0x0022.
        /// </summary>
        WM_CHILDACTIVATE = 0x0022,

        /// <summary>
        /// The WM_QUEUESYNC = 0x0023.
        /// </summary>
        WM_QUEUESYNC = 0x0023,

        /// <summary>
        /// The WM_GETMINMAXINFO = 0x0024.
        /// </summary>
        WM_GETMINMAXINFO = 0x0024,

        /// <summary>
        /// The WM_PAINTICON = 0x0026.
        /// </summary>
        WM_PAINTICON = 0x0026,

        /// <summary>
        /// The WM_ICONERASEBKGND = 0x0027.
        /// </summary>
        WM_ICONERASEBKGND = 0x0027,

        /// <summary>
        /// The WM_NEXTDLGCTL = 0x0028.
        /// </summary>
        WM_NEXTDLGCTL = 0x0028,

        /// <summary>
        /// The WM_SPOOLERSTATUS = 0x002A.
        /// </summary>
        WM_SPOOLERSTATUS = 0x002A,

        /// <summary>
        /// The WM_DRAWITEM = 0x002B.
        /// </summary>
        WM_DRAWITEM = 0x002B,

        /// <summary>
        /// The WM_MEASUREITEM = 0x002C.
        /// </summary>
        WM_MEASUREITEM = 0x002C,

        /// <summary>
        /// The WM_DELETEITEM = 0x002D.
        /// </summary>
        WM_DELETEITEM = 0x002D,

        /// <summary>
        /// The WM_VKEYTOITEM = 0x002E.
        /// </summary>
        WM_VKEYTOITEM = 0x002E,

        /// <summary>
        /// The WM_CHARTOITEM = 0x002F.
        /// </summary>
        WM_CHARTOITEM = 0x002F,

        /// <summary>
        /// The WM_SETFONT = 0x0030.
        /// </summary>
        WM_SETFONT = 0x0030,

        /// <summary>
        /// The WM_GETFONT = 0x0031.
        /// </summary>
        WM_GETFONT = 0x0031,

        /// <summary>
        /// The WM_SETHOTKEY = 0x0032.
        /// </summary>
        WM_SETHOTKEY = 0x0032,

        /// <summary>
        /// The WM_GETHOTKEY = 0x0033.
        /// </summary>
        WM_GETHOTKEY = 0x0033,

        /// <summary>
        /// The WM_QUERYDRAGICON = 0x0037.
        /// </summary>
        WM_QUERYDRAGICON = 0x0037,

        /// <summary>
        /// The WM_COMPAREITEM = 0x0039.
        /// </summary>
        WM_COMPAREITEM = 0x0039,

        /// <summary>
        /// The WM_GETOBJECT = 0x003D.
        /// </summary>
        WM_GETOBJECT = 0x003D,

        /// <summary>
        /// The WM_COMPACTING = 0x0041.
        /// </summary>
        WM_COMPACTING = 0x0041,

        /// <summary>
        /// The WM_COMMNOTIFY = 0x0044.
        /// </summary>
        WM_COMMNOTIFY = 0x0044,

        /// <summary>
        /// The WM_WINDOWPOSCHANGING = 0x0046.
        /// </summary>
        WM_WINDOWPOSCHANGING = 0x0046,

        /// <summary>
        /// The WM_WINDOWPOSCHANGED = 0x0047.
        /// </summary>
        WM_WINDOWPOSCHANGED = 0x0047,

        /// <summary>
        /// The WM_POWER = 0x0048.
        /// </summary>
        WM_POWER = 0x0048,

        /// <summary>
        /// The WM_COPYDATA = 0x004A.
        /// </summary>
        WM_COPYDATA = 0x004A,

        /// <summary>
        /// The WM_CANCELJOURNAL = 0x004B.
        /// </summary>
        WM_CANCELJOURNAL = 0x004B,

        /// <summary>
        /// The WM_NOTIFY = 0x004E.
        /// </summary>
        WM_NOTIFY = 0x004E,

        /// <summary>
        /// The WM_INPUTLANGCHANGEREQUEST = 0x0050.
        /// </summary>
        WM_INPUTLANGCHANGEREQUEST = 0x0050,

        /// <summary>
        /// The WM_INPUTLANGCHANGE = 0x0051.
        /// </summary>
        WM_INPUTLANGCHANGE = 0x0051,

        /// <summary>
        /// The WM_TCARD = 0x0052.
        /// </summary>
        WM_TCARD = 0x0052,

        /// <summary>
        /// The WM_HELP = 0x0053.
        /// </summary>
        WM_HELP = 0x0053,

        /// <summary>
        /// The WM_USERCHANGED = 0x0054.
        /// </summary>
        WM_USERCHANGED = 0x0054,

        /// <summary>
        /// The WM_NOTIFYFORMAT = 0x0055.
        /// </summary>
        WM_NOTIFYFORMAT = 0x0055,

        /// <summary>
        /// The WM_CONTEXTMENU = 0x007B.
        /// </summary>
        WM_CONTEXTMENU = 0x007B,

        /// <summary>
        /// The WM_STYLECHANGING = 0x007C.
        /// </summary>
        WM_STYLECHANGING = 0x007C,

        /// <summary>
        /// The WM_STYLECHANGED = 0x007D.
        /// </summary>
        WM_STYLECHANGED = 0x007D,

        /// <summary>
        /// The WM_DISPLAYCHANGE = 0x007E.
        /// </summary>
        WM_DISPLAYCHANGE = 0x007E,

        /// <summary>
        /// The WM_GETICON = 0x007F.
        /// </summary>
        WM_GETICON = 0x007F,

        /// <summary>
        /// The WM_SETICON = 0x0080.
        /// </summary>
        WM_SETICON = 0x0080,

        /// <summary>
        /// The WM_NCCREATE = 0x0081.
        /// </summary>
        WM_NCCREATE = 0x0081,

        /// <summary>
        /// The WM_NCDESTROY = 0x0082.
        /// </summary>
        WM_NCDESTROY = 0x0082,

        /// <summary>
        /// The WM_NCCALCSIZE = 0x0083.
        /// </summary>
        WM_NCCALCSIZE = 0x0083,

        /// <summary>
        /// The WM_NCHITTEST = 0x0084.
        /// </summary>
        WM_NCHITTEST = 0x0084,

        /// <summary>
        /// The WM_NCPAINT = 0x0085.
        /// </summary>
        WM_NCPAINT = 0x0085,

        /// <summary>
        /// The WM_NCACTIVATE = 0x0086.
        /// </summary>
        WM_NCACTIVATE = 0x0086,

        /// <summary>
        /// The WM_GETDLGCODE = 0x0087.
        /// </summary>
        WM_GETDLGCODE = 0x0087,

        /// <summary>
        /// The WM_SYNCPAINT = 0x0088.
        /// </summary>
        WM_SYNCPAINT = 0x0088,





        /// <summary>
        /// The WM_NCMOUSEMOVE = 0x00A0.
        /// </summary>
        WM_NCMOUSEMOVE = 0x00A0,

        /// <summary>
        /// The WM_NCLBUTTONDOWN = 0x00A1.
        /// </summary>
        WM_NCLBUTTONDOWN = 0x00A1,

        /// <summary>
        /// The WM_NCLBUTTONUP = 0x00A2.
        /// </summary>
        WM_NCLBUTTONUP = 0x00A2,

        /// <summary>
        /// The WM_NCLBUTTONDBLCLK = 0x00A3.
        /// </summary>
        WM_NCLBUTTONDBLCLK = 0x00A3,

        /// <summary>
        /// The WM_NCRBUTTONDOWN = 0x00A4.
        /// </summary>
        WM_NCRBUTTONDOWN = 0x00A4,

        /// <summary>
        /// The WM_NCRBUTTONUP = 0x00A5.
        /// </summary>
        WM_NCRBUTTONUP = 0x00A5,

        /// <summary>
        /// The WM_NCRBUTTONDBLCLK = 0x00A6.
        /// </summary>
        WM_NCRBUTTONDBLCLK = 0x00A6,

        /// <summary>
        /// The WM_NCMBUTTONDOWN = 0x00A7.
        /// </summary>
        WM_NCMBUTTONDOWN = 0x00A7,

        /// <summary>
        /// The WM_NCMBUTTONUP = 0x00A8.
        /// </summary>
        WM_NCMBUTTONUP = 0x00A8,

        /// <summary>
        /// The WM_NCMBUTTONDBLCLK = 0x00A9.
        /// </summary>
        WM_NCMBUTTONDBLCLK = 0x00A9,

        /// <summary>
        /// The WM_NCXBUTTONDOWN = 0x00AB.
        /// </summary>
        WM_NCXBUTTONDOWN = 0x00AB,

        /// <summary>
        /// The WM_NCXBUTTONUP = 0x00AC.
        /// </summary>
        WM_NCXBUTTONUP = 0x00AC,

        /// <summary>
        /// The WM_NCXBUTTONDBLCLK = 0x00AD.
        /// </summary>
        WM_NCXBUTTONDBLCLK = 0x00AD,



        /// <summary>
        /// The WM_INPUT_DEVICE_CHANGE = 0x00FE.
        /// </summary>
        WM_INPUT_DEVICE_CHANGE = 0x00FE,

        /// <summary>
        /// The WM_INPUT = 0x00FF.
        /// </summary>
        WM_INPUT = 0x00FF,



        /// <summary>
        /// The WM_KEYFIRST = 0x0100.
        /// </summary>
        WM_KEYFIRST = 0x0100,

        /// <summary>
        /// The WM_KEYDOWN = 0x0100.
        /// </summary>
        WM_KEYDOWN = 0x0100,

        /// <summary>
        /// The WM_KEYUP = 0x0101.
        /// </summary>
        WM_KEYUP = 0x0101,

        /// <summary>
        /// The WM_CHAR = 0x0102.
        /// </summary>
        WM_CHAR = 0x0102,

        /// <summary>
        /// The WM_DEADCHAR = 0x0103.
        /// </summary>
        WM_DEADCHAR = 0x0103,

        /// <summary>
        /// The WM_SYSKEYDOWN = 0x0104.
        /// </summary>
        WM_SYSKEYDOWN = 0x0104,

        /// <summary>
        /// The WM_SYSKEYUP = 0x0105.
        /// </summary>
        WM_SYSKEYUP = 0x0105,

        /// <summary>
        /// The WM_SYSCHAR = 0x0106.
        /// </summary>
        WM_SYSCHAR = 0x0106,

        /// <summary>
        /// The WM_SYSDEADCHAR = 0x0107.
        /// </summary>
        WM_SYSDEADCHAR = 0x0107,

        /// <summary>
        /// The WM_UNICHAR = 0x0109.
        /// </summary>
        WM_UNICHAR = 0x0109,

        /// <summary>
        /// The WM_KEYLAST = 0x0109.
        /// </summary>
        WM_KEYLAST = 0x0109,



        /// <summary>
        /// The WM_IME_STARTCOMPOSITION = 0x010D.
        /// </summary>
        WM_IME_STARTCOMPOSITION = 0x010D,

        /// <summary>
        /// The WM_IME_ENDCOMPOSITION = 0x010E.
        /// </summary>
        WM_IME_ENDCOMPOSITION = 0x010E,

        /// <summary>
        /// The WM_IME_COMPOSITION = 0x010F.
        /// </summary>
        WM_IME_COMPOSITION = 0x010F,

        /// <summary>
        /// The WM_IME_KEYLAST = 0x010F.
        /// </summary>
        WM_IME_KEYLAST = 0x010F,



        /// <summary>
        /// The WM_INITDIALOG = 0x0110.
        /// </summary>
        WM_INITDIALOG = 0x0110,

        /// <summary>
        /// The WM_COMMAND = 0x0111.
        /// </summary>
        WM_COMMAND = 0x0111,

        /// <summary>
        /// The WM_SYSCOMMAND = 0x0112.
        /// </summary>
        WM_SYSCOMMAND = 0x0112,

        /// <summary>
        /// The WM_TIMER = 0x0113.
        /// </summary>
        WM_TIMER = 0x0113,

        /// <summary>
        /// The WM_HSCROLL = 0x0114.
        /// </summary>
        WM_HSCROLL = 0x0114,

        /// <summary>
        /// The WM_VSCROLL = 0x0115.
        /// </summary>
        WM_VSCROLL = 0x0115,

        /// <summary>
        /// The WM_INITMENU = 0x0116.
        /// </summary>
        WM_INITMENU = 0x0116,

        /// <summary>
        /// The WM_INITMENUPOPUP = 0x0117.
        /// </summary>
        WM_INITMENUPOPUP = 0x0117,

        /// <summary>
        /// The WM_MENUSELECT = 0x011F.
        /// </summary>
        WM_MENUSELECT = 0x011F,

        /// <summary>
        /// The WM_MENUCHAR = 0x0120.
        /// </summary>
        WM_MENUCHAR = 0x0120,

        /// <summary>
        /// The WM_ENTERIDLE = 0x0121.
        /// </summary>
        WM_ENTERIDLE = 0x0121,

        /// <summary>
        /// The WM_MENURBUTTONUP = 0x0122.
        /// </summary>
        WM_MENURBUTTONUP = 0x0122,

        /// <summary>
        /// The WM_MENUDRAG = 0x0123.
        /// </summary>
        WM_MENUDRAG = 0x0123,

        /// <summary>
        /// The WM_MENUGETOBJECT = 0x0124.
        /// </summary>
        WM_MENUGETOBJECT = 0x0124,

        /// <summary>
        /// The WM_UNINITMENUPOPUP = 0x0125.
        /// </summary>
        WM_UNINITMENUPOPUP = 0x0125,

        /// <summary>
        /// The WM_MENUCOMMAND = 0x0126.
        /// </summary>
        WM_MENUCOMMAND = 0x0126,



        /// <summary>
        /// The WM_CHANGEUISTATE = 0x0127.
        /// </summary>
        WM_CHANGEUISTATE = 0x0127,

        /// <summary>
        /// The WM_UPDATEUISTATE = 0x0128.
        /// </summary>
        WM_UPDATEUISTATE = 0x0128,

        /// <summary>
        /// The WM_QUERYUISTATE = 0x0129.
        /// </summary>
        WM_QUERYUISTATE = 0x0129,



        /// <summary>
        /// The WM_CTLCOLORMSGBOX = 0x0132.
        /// </summary>
        WM_CTLCOLORMSGBOX = 0x0132,

        /// <summary>
        /// The WM_CTLCOLOREDIT = 0x0133.
        /// </summary>
        WM_CTLCOLOREDIT = 0x0133,

        /// <summary>
        /// The WM_CTLCOLORLISTBOX = 0x0134.
        /// </summary>
        WM_CTLCOLORLISTBOX = 0x0134,

        /// <summary>
        /// The WM_CTLCOLORBTN = 0x0135.
        /// </summary>
        WM_CTLCOLORBTN = 0x0135,

        /// <summary>
        /// The WM_CTLCOLORDLG = 0x0136.
        /// </summary>
        WM_CTLCOLORDLG = 0x0136,

        /// <summary>
        /// The WM_CTLCOLORSCROLLBAR = 0x0137.
        /// </summary>
        WM_CTLCOLORSCROLLBAR = 0x0137,

        /// <summary>
        /// The WM_CTLCOLORSTATIC = 0x0138.
        /// </summary>
        WM_CTLCOLORSTATIC = 0x0138,

        /// <summary>
        /// The MN_GETHMENU = 0x01E1.
        /// </summary>
        MN_GETHMENU = 0x01E1,



        /// <summary>
        /// The WM_MOUSEFIRST = 0x0200.
        /// </summary>
        WM_MOUSEFIRST = 0x0200,

        /// <summary>
        /// The WM_MOUSEMOVE = 0x0200.
        /// </summary>
        WM_MOUSEMOVE = 0x0200,

        /// <summary>
        /// The WM_LBUTTONDOWN = 0x0201.
        /// </summary>
        WM_LBUTTONDOWN = 0x0201,

        /// <summary>
        /// The WM_LBUTTONUP = 0x0202.
        /// </summary>
        WM_LBUTTONUP = 0x0202,

        /// <summary>
        /// The WM_LBUTTONDBLCLK = 0x0203.
        /// </summary>
        WM_LBUTTONDBLCLK = 0x0203,

        /// <summary>
        /// The WM_RBUTTONDOWN = 0x0204.
        /// </summary>
        WM_RBUTTONDOWN = 0x0204,

        /// <summary>
        /// The WM_RBUTTONUP = 0x0205.
        /// </summary>
        WM_RBUTTONUP = 0x0205,

        /// <summary>
        /// The WM_RBUTTONDBLCLK = 0x0206.
        /// </summary>
        WM_RBUTTONDBLCLK = 0x0206,

        /// <summary>
        /// The WM_MBUTTONDOWN = 0x0207.
        /// </summary>
        WM_MBUTTONDOWN = 0x0207,

        /// <summary>
        /// The WM_MBUTTONUP = 0x0208.
        /// </summary>
        WM_MBUTTONUP = 0x0208,

        /// <summary>
        /// The WM_MBUTTONDBLCLK = 0x0209.
        /// </summary>
        WM_MBUTTONDBLCLK = 0x0209,

        /// <summary>
        /// The WM_MOUSEWHEEL = 0x020A.
        /// </summary>
        WM_MOUSEWHEEL = 0x020A,

        /// <summary>
        /// The WM_XBUTTONDOWN = 0x020B.
        /// </summary>
        WM_XBUTTONDOWN = 0x020B,

        /// <summary>
        /// The WM_XBUTTONUP = 0x020C.
        /// </summary>
        WM_XBUTTONUP = 0x020C,

        /// <summary>
        /// The WM_XBUTTONDBLCLK = 0x020D.
        /// </summary>
        WM_XBUTTONDBLCLK = 0x020D,

        /// <summary>
        /// The WM_MOUSEHWHEEL = 0x020E.
        /// </summary>
        WM_MOUSEHWHEEL = 0x020E,



        /// <summary>
        /// The WM_PARENTNOTIFY = 0x0210.
        /// </summary>
        WM_PARENTNOTIFY = 0x0210,

        /// <summary>
        /// The WM_ENTERMENULOOP = 0x0211.
        /// </summary>
        WM_ENTERMENULOOP = 0x0211,

        /// <summary>
        /// The WM_EXITMENULOOP = 0x0212.
        /// </summary>
        WM_EXITMENULOOP = 0x0212,



        /// <summary>
        /// The WM_NEXTMENU = 0x0213.
        /// </summary>
        WM_NEXTMENU = 0x0213,

        /// <summary>
        /// The WM_SIZING = 0x0214.
        /// </summary>
        WM_SIZING = 0x0214,

        /// <summary>
        /// The WM_CAPTURECHANGED = 0x0215.
        /// </summary>
        WM_CAPTURECHANGED = 0x0215,

        /// <summary>
        /// The WM_MOVING = 0x0216.
        /// </summary>
        WM_MOVING = 0x0216,



        /// <summary>
        /// The WM_POWERBROADCAST = 0x0218.
        /// </summary>
        WM_POWERBROADCAST = 0x0218,



        /// <summary>
        /// The WM_DEVICECHANGE = 0x0219.
        /// </summary>
        WM_DEVICECHANGE = 0x0219,



        /// <summary>
        /// The WM_MDICREATE = 0x0220.
        /// </summary>
        WM_MDICREATE = 0x0220,

        /// <summary>
        /// The WM_MDIDESTROY = 0x0221.
        /// </summary>
        WM_MDIDESTROY = 0x0221,

        /// <summary>
        /// The WM_MDIACTIVATE = 0x0222.
        /// </summary>
        WM_MDIACTIVATE = 0x0222,

        /// <summary>
        /// The WM_MDIRESTORE = 0x0223.
        /// </summary>
        WM_MDIRESTORE = 0x0223,

        /// <summary>
        /// The WM_MDINEXT = 0x0224.
        /// </summary>
        WM_MDINEXT = 0x0224,

        /// <summary>
        /// The WM_MDIMAXIMIZE = 0x0225.
        /// </summary>
        WM_MDIMAXIMIZE = 0x0225,

        /// <summary>
        /// The WM_MDITILE = 0x0226.
        /// </summary>
        WM_MDITILE = 0x0226,

        /// <summary>
        /// The WM_MDICASCADE = 0x0227.
        /// </summary>
        WM_MDICASCADE = 0x0227,

        /// <summary>
        /// The WM_MDIICONARRANGE = 0x0228.
        /// </summary>
        WM_MDIICONARRANGE = 0x0228,

        /// <summary>
        /// The WM_MDIGETACTIVE = 0x0229.
        /// </summary>
        WM_MDIGETACTIVE = 0x0229,





        /// <summary>
        /// The WM_MDISETMENU = 0x0230.
        /// </summary>
        WM_MDISETMENU = 0x0230,

        /// <summary>
        /// The WM_ENTERSIZEMOVE = 0x0231.
        /// </summary>
        WM_ENTERSIZEMOVE = 0x0231,

        /// <summary>
        /// The WM_EXITSIZEMOVE = 0x0232.
        /// </summary>
        WM_EXITSIZEMOVE = 0x0232,

        /// <summary>
        /// The WM_DROPFILES = 0x0233.
        /// </summary>
        WM_DROPFILES = 0x0233,

        /// <summary>
        /// The WM_MDIREFRESHMENU = 0x0234.
        /// </summary>
        WM_MDIREFRESHMENU = 0x0234,



        /// <summary>
        /// The WM_IME_SETCONTEXT = 0x0281.
        /// </summary>
        WM_IME_SETCONTEXT = 0x0281,

        /// <summary>
        /// The WM_IME_NOTIFY = 0x0282.
        /// </summary>
        WM_IME_NOTIFY = 0x0282,

        /// <summary>
        /// The WM_IME_CONTROL = 0x0283.
        /// </summary>
        WM_IME_CONTROL = 0x0283,

        /// <summary>
        /// The WM_IME_COMPOSITIONFULL = 0x0284.
        /// </summary>
        WM_IME_COMPOSITIONFULL = 0x0284,

        /// <summary>
        /// The WM_IME_SELECT = 0x0285.
        /// </summary>
        WM_IME_SELECT = 0x0285,

        /// <summary>
        /// The WM_IME_CHAR = 0x0286.
        /// </summary>
        WM_IME_CHAR = 0x0286,

        /// <summary>
        /// The WM_IME_REQUEST = 0x0288.
        /// </summary>
        WM_IME_REQUEST = 0x0288,

        /// <summary>
        /// The WM_IME_KEYDOWN = 0x0290.
        /// </summary>
        WM_IME_KEYDOWN = 0x0290,

        /// <summary>
        /// The WM_IME_KEYUP = 0x0291.
        /// </summary>
        WM_IME_KEYUP = 0x0291,



        /// <summary>
        /// The WM_MOUSEHOVER = 0x02A1.
        /// </summary>
        WM_MOUSEHOVER = 0x02A1,

        /// <summary>
        /// The WM_MOUSELEAVE = 0x02A3.
        /// </summary>
        WM_MOUSELEAVE = 0x02A3,

        /// <summary>
        /// The WM_NCMOUSEHOVER = 0x02A0.
        /// </summary>
        WM_NCMOUSEHOVER = 0x02A0,

        /// <summary>
        /// The WM_NCMOUSELEAVE = 0x02A2.
        /// </summary>
        WM_NCMOUSELEAVE = 0x02A2,



        /// <summary>
        /// The WM_WTSSESSION_CHANGE = 0x02B1.
        /// </summary>
        WM_WTSSESSION_CHANGE = 0x02B1,



        /// <summary>
        /// The WM_TABLET_FIRST = 0x02c0.
        /// </summary>
        WM_TABLET_FIRST = 0x02c0,

        /// <summary>
        /// The WM_TABLET_LAST = 0x02df.
        /// </summary>
        WM_TABLET_LAST = 0x02df,



        /// <summary>
        /// The WM_CUT = 0x0300.
        /// </summary>
        WM_CUT = 0x0300,

        /// <summary>
        /// The WM_COPY = 0x0301.
        /// </summary>
        WM_COPY = 0x0301,

        /// <summary>
        /// The WM_PASTE = 0x0302.
        /// </summary>
        WM_PASTE = 0x0302,

        /// <summary>
        /// The WM_CLEAR = 0x0303.
        /// </summary>
        WM_CLEAR = 0x0303,

        /// <summary>
        /// The WM_UNDO = 0x0304.
        /// </summary>
        WM_UNDO = 0x0304,

        /// <summary>
        /// The WM_RENDERFORMAT = 0x0305.
        /// </summary>
        WM_RENDERFORMAT = 0x0305,

        /// <summary>
        /// The WM_RENDERALLFORMATS = 0x0306.
        /// </summary>
        WM_RENDERALLFORMATS = 0x0306,

        /// <summary>
        /// The WM_DESTROYCLIPBOARD = 0x0307.
        /// </summary>
        WM_DESTROYCLIPBOARD = 0x0307,

        /// <summary>
        /// The WM_DRAWCLIPBOARD = 0x0308.
        /// </summary>
        WM_DRAWCLIPBOARD = 0x0308,

        /// <summary>
        /// The WM_PAINTCLIPBOARD = 0x0309.
        /// </summary>
        WM_PAINTCLIPBOARD = 0x0309,

        /// <summary>
        /// The WM_VSCROLLCLIPBOARD = 0x030A.
        /// </summary>
        WM_VSCROLLCLIPBOARD = 0x030A,

        /// <summary>
        /// The WM_SIZECLIPBOARD = 0x030B.
        /// </summary>
        WM_SIZECLIPBOARD = 0x030B,

        /// <summary>
        /// The WM_ASKCBFORMATNAME = 0x030C.
        /// </summary>
        WM_ASKCBFORMATNAME = 0x030C,

        /// <summary>
        /// The WM_CHANGECBCHAIN = 0x030D.
        /// </summary>
        WM_CHANGECBCHAIN = 0x030D,

        /// <summary>
        /// The WM_HSCROLLCLIPBOARD = 0x030E.
        /// </summary>
        WM_HSCROLLCLIPBOARD = 0x030E,

        /// <summary>
        /// The WM_QUERYNEWPALETTE = 0x030F.
        /// </summary>
        WM_QUERYNEWPALETTE = 0x030F,

        /// <summary>
        /// The WM_PALETTEISCHANGING = 0x0310.
        /// </summary>
        WM_PALETTEISCHANGING = 0x0310,

        /// <summary>
        /// The WM_PALETTECHANGED = 0x0311.
        /// </summary>
        WM_PALETTECHANGED = 0x0311,

        /// <summary>
        /// The WM_HOTKEY = 0x0312.
        /// </summary>
        WM_HOTKEY = 0x0312,



        /// <summary>
        /// The WM_PRINT = 0x0317.
        /// </summary>
        WM_PRINT = 0x0317,

        /// <summary>
        /// The WM_PRINTCLIENT = 0x0318.
        /// </summary>
        WM_PRINTCLIENT = 0x0318,



        /// <summary>
        /// The WM_APPCOMMAND = 0x0319.
        /// </summary>
        WM_APPCOMMAND = 0x0319,



        /// <summary>
        /// The WM_THEMECHANGED = 0x031A.
        /// </summary>
        WM_THEMECHANGED = 0x031A,



        /// <summary>
        /// The WM_CLIPBOARDUPDATE = 0x031D.
        /// </summary>
        WM_CLIPBOARDUPDATE = 0x031D,



        /// <summary>
        /// The WM_DWMCOMPOSITIONCHANGED = 0x031E.
        /// </summary>
        WM_DWMCOMPOSITIONCHANGED = 0x031E,

        /// <summary>
        /// The WM_DWMNCRENDERINGCHANGED = 0x031F.
        /// </summary>
        WM_DWMNCRENDERINGCHANGED = 0x031F,

        /// <summary>
        /// The WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320.
        /// </summary>
        WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320,

        /// <summary>
        /// The WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321.
        /// </summary>
        WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321,



        /// <summary>
        /// The WM_GETTITLEBARINFOEX = 0x033F.
        /// </summary>
        WM_GETTITLEBARINFOEX = 0x033F,



        /// <summary>
        /// The WM_HANDHELDFIRST = 0x0358.
        /// </summary>
        WM_HANDHELDFIRST = 0x0358,

        /// <summary>
        /// The WM_HANDHELDLAST = 0x035F.
        /// </summary>
        WM_HANDHELDLAST = 0x035F,



        /// <summary>
        /// The WM_AFXFIRST = 0x0360.
        /// </summary>
        WM_AFXFIRST = 0x0360,

        /// <summary>
        /// The WM_AFXLAST = 0x037F.
        /// </summary>
        WM_AFXLAST = 0x037F,



        /// <summary>
        /// The WM_PENWINFIRST = 0x0380.
        /// </summary>
        WM_PENWINFIRST = 0x0380,

        /// <summary>
        /// The WM_PENWINLAST = 0x038F.
        /// </summary>
        WM_PENWINLAST = 0x038F,



        /// <summary>
        /// The WM_APP = 0x8000.
        /// </summary>
        WM_APP = 0x8000,



        /// <summary>
        /// The WM_USER = 0x0400.
        /// </summary>
        WM_USER = 0x0400,



        /// <summary>
        /// The WM_REFLECT = WM_USER + 0x1C00.
        /// </summary>
        WM_REFLECT = WM_USER + 0x1C00,
    }
}
