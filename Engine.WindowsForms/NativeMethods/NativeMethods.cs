// <copyright file="NativeMethods.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.InteropServices;

namespace Engine.WindowsForms
{
    /// <summary>
    /// 
    /// </summary>
    public static class NativeMethods
    {
        // define the key code
        /// <summary>
        /// 
        /// </summary>
        public const int MouseEventF_Wheel = 0x0800;

        /// <summary>
        /// 
        /// </summary>
        public const int MouseEventF_HWheel = 0x1000;

        /// <summary>
        /// 
        /// </summary>
        public const int MEMBERID_NIL = (-1),
            MAX_PATH = 260,
            MA_ACTIVATE = 0x0001,
            MA_ACTIVATEANDEAT = 0x0002,
            MA_NOACTIVATE = 0x0003,
            MA_NOACTIVATEANDEAT = 0x0004,
            MM_TEXT = 1,
            MM_ANISOTROPIC = 8,
            MK_LBUTTON = 0x0001,
            MK_RBUTTON = 0x0002,
            MK_SHIFT = 0x0004,
            MK_CONTROL = 0x0008,
            MK_MBUTTON = 0x0010,
            MNC_EXECUTE = 2,
            MNC_SELECT = 3,
            MIIM_STATE = 0x00000001,
            MIIM_ID = 0x00000002,
            MIIM_SUBMENU = 0x00000004,
            MIIM_TYPE = 0x00000010,
            MIIM_DATA = 0x00000020,
            MIIM_STRING = 0x00000040,
            MIIM_BITMAP = 0x00000080,
            MIIM_FTYPE = 0x00000100,
            MB_OK = 0x00000000,
            MF_BYCOMMAND = 0x00000000,
            MF_BYPOSITION = 0x00000400,
            MF_ENABLED = 0x00000000,
            MF_GRAYED = 0x00000001,
            MF_POPUP = 0x00000010,
            MF_SYSMENU = 0x00002000,
            MFS_DISABLED = 0x00000003,
            MFT_MENUBREAK = 0x00000040,
            MFT_SEPARATOR = 0x00000800,
            MFT_RIGHTORDER = 0x00002000,
            MFT_RIGHTJUSTIFY = 0x00004000,
            MDIS_ALLCHILDSTYLES = 0x0001,
            MDITILE_VERTICAL = 0x0000,
            MDITILE_HORIZONTAL = 0x0001,
            MDITILE_SKIPDISABLED = 0x0002,
            MCM_SETMAXSELCOUNT = (0x1000 + 4),
            MCM_SETSELRANGE = (0x1000 + 6),
            MCM_GETMONTHRANGE = (0x1000 + 7),
            MCM_GETMINREQRECT = (0x1000 + 9),
            MCM_SETCOLOR = (0x1000 + 10),
            MCM_SETTODAY = (0x1000 + 12),
            MCM_GETTODAY = (0x1000 + 13),
            MCM_HITTEST = (0x1000 + 14),
            MCM_SETFIRSTDAYOFWEEK = (0x1000 + 15),
            MCM_SETRANGE = (0x1000 + 18),
            MCM_SETMONTHDELTA = (0x1000 + 20),
            MCM_GETMAXTODAYWIDTH = (0x1000 + 21),
            MCHT_TITLE = 0x00010000,
            MCHT_CALENDAR = 0x00020000,
            MCHT_TODAYLINK = 0x00030000,
            MCHT_TITLEBK = (0x00010000),
            MCHT_TITLEMONTH = (0x00010000 | 0x0001),
            MCHT_TITLEYEAR = (0x00010000 | 0x0002),
            MCHT_TITLEBTNNEXT = (0x00010000 | 0x01000000 | 0x0003),
            MCHT_TITLEBTNPREV = (0x00010000 | 0x02000000 | 0x0003),
            MCHT_CALENDARBK = (0x00020000),
            MCHT_CALENDARDATE = (0x00020000 | 0x0001),
            MCHT_CALENDARDATENEXT = ((0x00020000 | 0x0001) | 0x01000000),
            MCHT_CALENDARDATEPREV = ((0x00020000 | 0x0001) | 0x02000000),
            MCHT_CALENDARDAY = (0x00020000 | 0x0002),
            MCHT_CALENDARWEEKNUM = (0x00020000 | 0x0003),
            MCSC_TEXT = 1,
            MCSC_TITLEBK = 2,
            MCSC_TITLETEXT = 3,
            MCSC_MONTHBK = 4,
            MCSC_TRAILINGTEXT = 5,
            MCN_SELCHANGE = ((0 - 750) + 1),
            MCN_GETDAYSTATE = ((0 - 750) + 3),
            MCN_SELECT = ((0 - 750) + 4),
            MCS_DAYSTATE = 0x0001,
            MCS_MULTISELECT = 0x0002,
            MCS_WEEKNUMBERS = 0x0004,
            MCS_NOTODAYCIRCLE = 0x0008,
            MCS_NOTODAY = 0x0010,
            MSAA_MENU_SIG = (unchecked((int)0xAA0DF00D));

        /// <summary>
        /// 
        /// </summary>
        public const int NIM_ADD = 0x00000000,
            NIM_MODIFY = 0x00000001,
            NIM_DELETE = 0x00000002,
            NIF_MESSAGE = 0x00000001,
            NIM_SETVERSION = 0x00000004,
            NIF_ICON = 0x00000002,
            NIF_INFO = 0x00000010,
            NIF_TIP = 0x00000004,
            NIIF_NONE = 0x00000000,
            NIIF_INFO = 0x00000001,
            NIIF_WARNING = 0x00000002,
            NIIF_ERROR = 0x00000003,
            NIN_BALLOONSHOW = (WM_USER + 2),
            NIN_BALLOONHIDE = (WM_USER + 3),
            NIN_BALLOONTIMEOUT = (WM_USER + 4),
            NIN_BALLOONUSERCLICK = (WM_USER + 5),
            NFR_ANSI = 1,
            NFR_UNICODE = 2,
            NM_CLICK = ((0 - 0) - 2),
            NM_DBLCLK = ((0 - 0) - 3),
            NM_RCLICK = ((0 - 0) - 5),
            NM_RDBLCLK = ((0 - 0) - 6),
            NM_CUSTOMDRAW = ((0 - 0) - 12),
            NM_RELEASEDCAPTURE = ((0 - 0) - 16),
            NONANTIALIASED_QUALITY = 3;

        /// <summary>
        /// 
        /// </summary>
        public const int OFN_READONLY = 0x00000001,
            OFN_OVERWRITEPROMPT = 0x00000002,
            OFN_HIDEREADONLY = 0x00000004,
            OFN_NOCHANGEDIR = 0x00000008,
            OFN_SHOWHELP = 0x00000010,
            OFN_ENABLEHOOK = 0x00000020,
            OFN_NOVALIDATE = 0x00000100,
            OFN_ALLOWMULTISELECT = 0x00000200,
            OFN_PATHMUSTEXIST = 0x00000800,
            OFN_FILEMUSTEXIST = 0x00001000,
            OFN_CREATEPROMPT = 0x00002000,
            OFN_EXPLORER = 0x00080000,
            OFN_NODEREFERENCELINKS = 0x00100000,
            OFN_ENABLESIZING = 0x00800000,
            OFN_USESHELLITEM = 0x01000000,
            OLEIVERB_PRIMARY = 0,
            OLEIVERB_SHOW = -1,
            OLEIVERB_HIDE = -3,
            OLEIVERB_UIACTIVATE = -4,
            OLEIVERB_INPLACEACTIVATE = -5,
            OLEIVERB_DISCARDUNDOSTATE = -6,
            OLEIVERB_PROPERTIES = -7,
            OLE_E_INVALIDRECT = unchecked((int)0x8004000D),
            OLE_E_NOCONNECTION = unchecked((int)0x80040004),
            OLE_E_PROMPTSAVECANCELLED = unchecked((int)0x8004000C),
            OLEMISC_RECOMPOSEONRESIZE = 0x00000001,
            OLEMISC_INSIDEOUT = 0x00000080,
            OLEMISC_ACTIVATEWHENVISIBLE = 0x0000100,
            OLEMISC_ACTSLIKEBUTTON = 0x00001000,
            OLEMISC_SETCLIENTSITEFIRST = 0x00020000,
            OBJ_PEN = 1,
            OBJ_BRUSH = 2,
            OBJ_DC = 3,
            OBJ_METADC = 4,
            OBJ_PAL = 5,
            OBJ_FONT = 6,
            OBJ_BITMAP = 7,
            OBJ_REGION = 8,
            OBJ_METAFILE = 9,
            OBJ_MEMDC = 10,
            OBJ_EXTPEN = 11,
            OBJ_ENHMETADC = 12,
            ODS_CHECKED = 0x0008,
            ODS_COMBOBOXEDIT = 0x1000,
            ODS_DEFAULT = 0x0020,
            ODS_DISABLED = 0x0004,
            ODS_FOCUS = 0x0010,
            ODS_GRAYED = 0x0002,
            ODS_HOTLIGHT = 0x0040,
            ODS_INACTIVE = 0x0080,
            ODS_NOACCEL = 0x0100,
            ODS_NOFOCUSRECT = 0x0200,
            ODS_SELECTED = 0x0001,
            OLECLOSE_SAVEIFDIRTY = 0,
            OLECLOSE_PROMPTSAVE = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int PDERR_SETUPFAILURE = 0x1001,
            PDERR_PARSEFAILURE = 0x1002,
            PDERR_RETDEFFAILURE = 0x1003,
            PDERR_LOADDRVFAILURE = 0x1004,
            PDERR_GETDEVMODEFAIL = 0x1005,
            PDERR_INITFAILURE = 0x1006,
            PDERR_NODEVICES = 0x1007,
            PDERR_NODEFAULTPRN = 0x1008,
            PDERR_DNDMMISMATCH = 0x1009,
            PDERR_CREATEICFAILURE = 0x100A,
            PDERR_PRINTERNOTFOUND = 0x100B,
            PDERR_DEFAULTDIFFERENT = 0x100C,
            PD_ALLPAGES = 0x00000000,
            PD_SELECTION = 0x00000001,
            PD_PAGENUMS = 0x00000002,
            PD_NOSELECTION = 0x00000004,
            PD_NOPAGENUMS = 0x00000008,
            PD_COLLATE = 0x00000010,
            PD_PRINTTOFILE = 0x00000020,
            PD_PRINTSETUP = 0x00000040,
            PD_NOWARNING = 0x00000080,
            PD_RETURNDC = 0x00000100,
            PD_RETURNIC = 0x00000200,
            PD_RETURNDEFAULT = 0x00000400,
            PD_SHOWHELP = 0x00000800,
            PD_ENABLEPRINTHOOK = 0x00001000,
            PD_ENABLESETUPHOOK = 0x00002000,
            PD_ENABLEPRINTTEMPLATE = 0x00004000,
            PD_ENABLESETUPTEMPLATE = 0x00008000,
            PD_ENABLEPRINTTEMPLATEHANDLE = 0x00010000,
            PD_ENABLESETUPTEMPLATEHANDLE = 0x00020000,
            PD_USEDEVMODECOPIES = 0x00040000,
            PD_USEDEVMODECOPIESANDCOLLATE = 0x00040000,
            PD_DISABLEPRINTTOFILE = 0x00080000,
            PD_HIDEPRINTTOFILE = 0x00100000,
            PD_NONETWORKBUTTON = 0x00200000,
            PD_CURRENTPAGE = 0x00400000,
            PD_NOCURRENTPAGE = 0x00800000,
            PD_EXCLUSIONFLAGS = 0x01000000,
            PD_USELARGETEMPLATE = 0x10000000,
            PSD_MINMARGINS = 0x00000001,
            PSD_MARGINS = 0x00000002,
            PSD_INHUNDREDTHSOFMILLIMETERS = 0x00000008,
            PSD_DISABLEMARGINS = 0x00000010,
            PSD_DISABLEPRINTER = 0x00000020,
            PSD_DISABLEORIENTATION = 0x00000100,
            PSD_DISABLEPAPER = 0x00000200,
            PSD_SHOWHELP = 0x00000800,
            PSD_ENABLEPAGESETUPHOOK = 0x00002000,
            PSD_NONETWORKBUTTON = 0x00200000,
            PS_SOLID = 0,
            PS_DOT = 2,
            PLANES = 14,
            PRF_CHECKVISIBLE = 0x00000001,
            PRF_NONCLIENT = 0x00000002,
            PRF_CLIENT = 0x00000004,
            PRF_ERASEBKGND = 0x00000008,
            PRF_CHILDREN = 0x00000010,
            PM_NOREMOVE = 0x0000,
            PM_REMOVE = 0x0001,
            PM_NOYIELD = 0x0002,
            PBM_SETRANGE = (0x0400 + 1),
            PBM_SETPOS = (0x0400 + 2),
            PBM_SETSTEP = (0x0400 + 4),
            PBM_SETRANGE32 = (0x0400 + 6),
            PBM_SETBARCOLOR = (0x0400 + 9),
            PBM_SETMARQUEE = (0x0400 + 10),
            PBM_SETBKCOLOR = (0x2000 + 1),
            PSM_SETTITLEA = (0x0400 + 111),
            PSM_SETTITLEW = (0x0400 + 120),
            PSM_SETFINISHTEXTA = (0x0400 + 115),
            PSM_SETFINISHTEXTW = (0x0400 + 121),
            PATCOPY = 0x00F00021,
            PATINVERT = 0x005A0049;

        /// <summary>
        /// 
        /// </summary>
        public const int PBS_SMOOTH = 0x01,
            PBS_MARQUEE = 0x08;

        /// <summary>
        /// 
        /// </summary>
        public const int QS_KEY = 0x0001,
            QS_MOUSEMOVE = 0x0002,
            QS_MOUSEBUTTON = 0x0004,
            QS_POSTMESSAGE = 0x0008,
            QS_TIMER = 0x0010,
            QS_PAINT = 0x0020,
            QS_SENDMESSAGE = 0x0040,
            QS_HOTKEY = 0x0080,
            QS_ALLPOSTMESSAGE = 0x0100,
            QS_MOUSE = QS_MOUSEMOVE | QS_MOUSEBUTTON,
            QS_INPUT = QS_MOUSE | QS_KEY,
            QS_ALLEVENTS = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY,
            QS_ALLINPUT = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY | QS_SENDMESSAGE;

        /// <summary>
        /// 
        /// </summary>
        public const int MWMO_INPUTAVAILABLE = 0x0004;  // don't use MWMO_WAITALL, see ddb#176342

        /// <summary>
        /// 
        /// </summary>
        public const int RECO_PASTE = 0x00000000;   // paste from clipboard

        /// <summary>
        /// 
        /// </summary>
        public const int RECO_DROP = 0x00000001;    // drop

        /// <summary>
        /// 
        /// </summary>
        public const int RECO_COPY = 0x00000002;    // copy to the clipboard

        /// <summary>
        /// 
        /// </summary>
        public const int RECO_CUT = 0x00000003;     // cut to the clipboard

        /// <summary>
        /// 
        /// </summary>
        public const int RECO_DRAG = 0x00000004;    // drag

        /// <summary>
        /// 
        /// </summary>
        public const int RPC_E_CHANGED_MODE = unchecked((int)0x80010106),
            RPC_E_CANTCALLOUT_ININPUTSYNCCALL = unchecked((int)0x8001010D),
            RGN_AND = 1,
            RGN_XOR = 3,
            RGN_DIFF = 4,
            RDW_INVALIDATE = 0x0001,
            RDW_ERASE = 0x0004,
            RDW_ALLCHILDREN = 0x0080,
            RDW_ERASENOW = 0x0200,
            RDW_UPDATENOW = 0x0100,
            RDW_FRAME = 0x0400,
            RB_INSERTBANDA = (0x0400 + 1),
            RB_INSERTBANDW = (0x0400 + 10);

        /// <summary>
        /// 
        /// </summary>
        public const int stc4 = 0x0443,
            SHGFP_TYPE_CURRENT = 0,
            STGM_READ = 0x00000000,
            STGM_WRITE = 0x00000001,
            STGM_READWRITE = 0x00000002,
            STGM_SHARE_EXCLUSIVE = 0x00000010,
            STGM_CREATE = 0x00001000,
            STGM_TRANSACTED = 0x00010000,
            STGM_CONVERT = 0x00020000,
            STGM_DELETEONRELEASE = 0x04000000,
            STARTF_USESHOWWINDOW = 0x00000001,
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_LEFT = 6,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8,
            SB_TOP = 6,
            SB_BOTTOM = 7,
            SIZE_RESTORED = 0,
            SIZE_MAXIMIZED = 2,
            ESB_ENABLE_BOTH = 0x0000,
            ESB_DISABLE_BOTH = 0x0003,
            SORT_DEFAULT = 0x0,
            SUBLANG_DEFAULT = 0x01,
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_MAX = 10,
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_DRAWFRAME = 0x0020,
            SWP_NOOWNERZORDER = 0x0200,
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
            SM_CXVSCROLL = 2,
            SM_CYHSCROLL = 3,
            SM_CYCAPTION = 4,
            SM_CXBORDER = 5,
            SM_CYBORDER = 6,
            SM_CYVTHUMB = 9,
            SM_CXHTHUMB = 10,
            SM_CXICON = 11,
            SM_CYICON = 12,
            SM_CXCURSOR = 13,
            SM_CYCURSOR = 14,
            SM_CYMENU = 15,
            SM_CYKANJIWINDOW = 18,
            SM_MOUSEPRESENT = 19,
            SM_CYVSCROLL = 20,
            SM_CXHSCROLL = 21,
            SM_DEBUG = 22,
            SM_SWAPBUTTON = 23,
            SM_CXMIN = 28,
            SM_CYMIN = 29,
            SM_CXSIZE = 30,
            SM_CYSIZE = 31,
            SM_CXFRAME = 32,
            SM_CYFRAME = 33,
            SM_CXMINTRACK = 34,
            SM_CYMINTRACK = 35,
            SM_CXDOUBLECLK = 36,
            SM_CYDOUBLECLK = 37,
            SM_CXICONSPACING = 38,
            SM_CYICONSPACING = 39,
            SM_MENUDROPALIGNMENT = 40,
            SM_PENWINDOWS = 41,
            SM_DBCSENABLED = 42,
            SM_CMOUSEBUTTONS = 43,
            SM_CXFIXEDFRAME = 7,
            SM_CYFIXEDFRAME = 8,
            SM_SECURE = 44,
            SM_CXEDGE = 45,
            SM_CYEDGE = 46,
            SM_CXMINSPACING = 47,
            SM_CYMINSPACING = 48,
            SM_CXSMICON = 49,
            SM_CYSMICON = 50,
            SM_CYSMCAPTION = 51,
            SM_CXSMSIZE = 52,
            SM_CYSMSIZE = 53,
            SM_CXMENUSIZE = 54,
            SM_CYMENUSIZE = 55,
            SM_ARRANGE = 56,
            SM_CXMINIMIZED = 57,
            SM_CYMINIMIZED = 58,
            SM_CXMAXTRACK = 59,
            SM_CYMAXTRACK = 60,
            SM_CXMAXIMIZED = 61,
            SM_CYMAXIMIZED = 62,
            SM_NETWORK = 63,
            SM_CLEANBOOT = 67,
            SM_CXDRAG = 68,
            SM_CYDRAG = 69,
            SM_SHOWSOUNDS = 70,
            SM_CXMENUCHECK = 71,
            SM_CYMENUCHECK = 72,
            SM_MIDEASTENABLED = 74,
            SM_MOUSEWHEELPRESENT = 75,
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79,
            SM_CMONITORS = 80,
            SM_SAMEDISPLAYFORMAT = 81,
            SM_REMOTESESSION = 0x1000;

        /// <summary>
        /// 
        /// </summary>
        public const int HLP_FILE = 1,
            HLP_KEYWORD = 2,
            HLP_NAVIGATOR = 3,
            HLP_OBJECT = 4;

        /// <summary>
        /// 
        /// </summary>
        public const int SW_SCROLLCHILDREN = 0x0001,
            SW_INVALIDATE = 0x0002,
            SW_ERASE = 0x0004,
            SW_SMOOTHSCROLL = 0x0010,
            SC_SIZE = 0xF000,
            SC_MINIMIZE = 0xF020,
            SC_MAXIMIZE = 0xF030,
            SC_CLOSE = 0xF060,
            SC_KEYMENU = 0xF100,
            SC_RESTORE = 0xF120,
            SC_MOVE = 0xF010,
            SC_CONTEXTHELP = 0xF180,
            SS_LEFT = 0x00000000,
            SS_CENTER = 0x00000001,
            SS_RIGHT = 0x00000002,
            SS_OWNERDRAW = 0x0000000D,
            SS_NOPREFIX = 0x00000080,
            SS_SUNKEN = 0x00001000,
            SBS_HORZ = 0x0000,
            SBS_VERT = 0x0001,
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = (0x0001 | 0x0002 | 0x0004 | 0x0010),
            SPI_GETFONTSMOOTHING = 0x004A,
            SPI_GETDROPSHADOW = 0x1024,
            SPI_GETFLATMENU = 0x1022,
            SPI_GETFONTSMOOTHINGTYPE = 0x200A,
            SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,
            SPI_ICONHORIZONTALSPACING = 0x000D,
            SPI_ICONVERTICALSPACING = 0x0018,
            // SPI_GETICONMETRICS =        0x002D,
            SPI_GETICONTITLEWRAP = 0x0019,
            SPI_GETICONTITLELOGFONT = 0x001F,
            SPI_GETKEYBOARDCUES = 0x100A,
            SPI_GETKEYBOARDDELAY = 0x0016,
            SPI_GETKEYBOARDPREF = 0x0044,
            SPI_GETKEYBOARDSPEED = 0x000A,
            SPI_GETMOUSEHOVERWIDTH = 0x0062,
            SPI_GETMOUSEHOVERHEIGHT = 0x0064,
            SPI_GETMOUSEHOVERTIME = 0x0066,
            SPI_GETMOUSESPEED = 0x0070,
            SPI_GETMENUDROPALIGNMENT = 0x001B,
            SPI_GETMENUFADE = 0x1012,
            SPI_GETMENUSHOWDELAY = 0x006A,
            SPI_GETCOMBOBOXANIMATION = 0x1004,
            SPI_GETGRADIENTCAPTIONS = 0x1008,
            SPI_GETHOTTRACKING = 0x100E,
            SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,
            SPI_GETMENUANIMATION = 0x1002,
            SPI_GETSELECTIONFADE = 0x1014,
            SPI_GETTOOLTIPANIMATION = 0x1016,
            SPI_GETUIEFFECTS = 0x103E,
            SPI_GETACTIVEWINDOWTRACKING = 0x1000,
            SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,
            SPI_GETANIMATION = 0x0048,
            SPI_GETBORDER = 0x0005,
            SPI_GETCARETWIDTH = 0x2006,
            SM_CYFOCUSBORDER = 84,
            SM_CXFOCUSBORDER = 83,
            SM_CYSIZEFRAME = SM_CYFRAME,
            SM_CXSIZEFRAME = SM_CXFRAME,
            SPI_GETDRAGFULLWINDOWS = 38,
            SPI_GETNONCLIENTMETRICS = 41,
            SPI_GETWORKAREA = 48,
            SPI_GETHIGHCONTRAST = 66,
            SPI_GETDEFAULTINPUTLANG = 89,
            SPI_GETSNAPTODEFBUTTON = 95,
            SPI_GETWHEELSCROLLLINES = 104,
            SBARS_SIZEGRIP = 0x0100,
            SB_SETTEXTA = (0x0400 + 1),
            SB_SETTEXTW = (0x0400 + 11),
            SB_GETTEXTA = (0x0400 + 2),
            SB_GETTEXTW = (0x0400 + 13),
            SB_GETTEXTLENGTHA = (0x0400 + 3),
            SB_GETTEXTLENGTHW = (0x0400 + 12),
            SB_SETPARTS = (0x0400 + 4),
            SB_SIMPLE = (0x0400 + 9),
            SB_GETRECT = (0x0400 + 10),
            SB_SETICON = (0x0400 + 15),
            SB_SETTIPTEXTA = (0x0400 + 16),
            SB_SETTIPTEXTW = (0x0400 + 17),
            SB_GETTIPTEXTA = (0x0400 + 18),
            SB_GETTIPTEXTW = (0x0400 + 19),
            SBT_OWNERDRAW = 0x1000,
            SBT_NOBORDERS = 0x0100,
            SBT_POPOUT = 0x0200,
            SBT_RTLREADING = 0x0400,
            SRCCOPY = 0x00CC0020,
            SRCAND = 0x008800C6, /* dest = source AND dest          */
            SRCPAINT = 0x00EE0086, /* dest = source OR dest           */
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)             */
            STATFLAG_DEFAULT = 0x0,
            STATFLAG_NONAME = 0x1,
            STATFLAG_NOOPEN = 0x2,
            STGC_DEFAULT = 0x0,
            STGC_OVERWRITE = 0x1,
            STGC_ONLYIFCURRENT = 0x2,
            STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 0x4,
            STREAM_SEEK_SET = 0x0,
            STREAM_SEEK_CUR = 0x1,
            STREAM_SEEK_END = 0x2;

        /// <summary>
        /// 
        /// </summary>
        public const int S_OK = 0x00000000;

        /// <summary>
        /// 
        /// </summary>
        public const int S_FALSE = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const int TRANSPARENT = 1,
            OPAQUE = 2,
            TME_HOVER = 0x00000001,
            TME_LEAVE = 0x00000002,
            TPM_LEFTBUTTON = 0x0000,
            TPM_RIGHTBUTTON = 0x0002,
            TPM_LEFTALIGN = 0x0000,
            TPM_RIGHTALIGN = 0x0008,
            TPM_VERTICAL = 0x0040,
            TV_FIRST = 0x1100,
            TBSTATE_CHECKED = 0x01,
            TBSTATE_ENABLED = 0x04,
            TBSTATE_HIDDEN = 0x08,
            TBSTATE_INDETERMINATE = 0x10,
            TBSTYLE_BUTTON = 0x00,
            TBSTYLE_SEP = 0x01,
            TBSTYLE_CHECK = 0x02,
            TBSTYLE_DROPDOWN = 0x08,
            TBSTYLE_TOOLTIPS = 0x0100,
            TBSTYLE_FLAT = 0x0800,
            TBSTYLE_LIST = 0x1000,
            TBSTYLE_EX_DRAWDDARROWS = 0x00000001,
            TB_ENABLEBUTTON = (0x0400 + 1),
            TB_ISBUTTONCHECKED = (0x0400 + 10),
            TB_ISBUTTONINDETERMINATE = (0x0400 + 13),
            TB_ADDBUTTONSA = (0x0400 + 20),
            TB_ADDBUTTONSW = (0x0400 + 68),
            TB_INSERTBUTTONA = (0x0400 + 21),
            TB_INSERTBUTTONW = (0x0400 + 67),
            TB_DELETEBUTTON = (0x0400 + 22),
            TB_GETBUTTON = (0x0400 + 23),
            TB_SAVERESTOREA = (0x0400 + 26),
            TB_SAVERESTOREW = (0x0400 + 76),
            TB_ADDSTRINGA = (0x0400 + 28),
            TB_ADDSTRINGW = (0x0400 + 77),
            TB_BUTTONSTRUCTSIZE = (0x0400 + 30),
            TB_SETBUTTONSIZE = (0x0400 + 31),
            TB_AUTOSIZE = (0x0400 + 33),
            TB_GETROWS = (0x0400 + 40),
            TB_GETBUTTONTEXTA = (0x0400 + 45),
            TB_GETBUTTONTEXTW = (0x0400 + 75),
            TB_SETIMAGELIST = (0x0400 + 48),
            TB_GETRECT = (0x0400 + 51),
            TB_GETBUTTONSIZE = (0x0400 + 58),
            TB_GETBUTTONINFOW = (0x0400 + 63),
            TB_SETBUTTONINFOW = (0x0400 + 64),
            TB_GETBUTTONINFOA = (0x0400 + 65),
            TB_SETBUTTONINFOA = (0x0400 + 66),
            TB_MAPACCELERATORA = (0x0400 + 78),
            TB_SETEXTENDEDSTYLE = (0x0400 + 84),
            TB_MAPACCELERATORW = (0x0400 + 90),
            TB_GETTOOLTIPS = (0x0400 + 35),
            TB_SETTOOLTIPS = (0x0400 + 36),
            TBIF_IMAGE = 0x00000001,
            TBIF_TEXT = 0x00000002,
            TBIF_STATE = 0x00000004,
            TBIF_STYLE = 0x00000008,
            TBIF_COMMAND = 0x00000020,
            TBIF_SIZE = 0x00000040,
            TBN_GETBUTTONINFOA = ((0 - 700) - 0),
            TBN_GETBUTTONINFOW = ((0 - 700) - 20),
            TBN_QUERYINSERT = ((0 - 700) - 6),
            TBN_DROPDOWN = ((0 - 700) - 10),
            TBN_HOTITEMCHANGE = ((0 - 700) - 13),
            TBN_GETDISPINFOA = ((0 - 700) - 16),
            TBN_GETDISPINFOW = ((0 - 700) - 17),
            TBN_GETINFOTIPA = ((0 - 700) - 18),
            TBN_GETINFOTIPW = ((0 - 700) - 19),
            TTS_ALWAYSTIP = 0x01,
            TTS_NOPREFIX = 0x02,
            TTS_NOANIMATE = 0x10,
            TTS_NOFADE = 0x20,
            TTS_BALLOON = 0x40,
            //TTI_NONE                =0,
            //TTI_INFO                =1,
            TTI_WARNING = 2,
            //TTI_ERROR               =3,
            TTF_IDISHWND = 0x0001,
            TTF_RTLREADING = 0x0004,
            TTF_TRACK = 0x0020,
            TTF_CENTERTIP = 0x0002,
            TTF_SUBCLASS = 0x0010,
            TTF_TRANSPARENT = 0x0100,
            TTF_ABSOLUTE = 0x0080,
            TTDT_AUTOMATIC = 0,
            TTDT_RESHOW = 1,
            TTDT_AUTOPOP = 2,
            TTDT_INITIAL = 3,
            TTM_TRACKACTIVATE = (0x0400 + 17),
            TTM_TRACKPOSITION = (0x0400 + 18),
            TTM_ACTIVATE = (0x0400 + 1),
            TTM_POP = (0x0400 + 28),
            TTM_ADJUSTRECT = (0x400 + 31),
            TTM_SETDELAYTIME = (0x0400 + 3),
            TTM_SETTITLEA = (WM_USER + 32),  // wParam = TTI_*, lParam = char* szTitle
            TTM_SETTITLEW = (WM_USER + 33), // wParam = TTI_*, lParam = wchar* szTitle
            TTM_ADDTOOLA = (0x0400 + 4),
            TTM_ADDTOOLW = (0x0400 + 50),
            TTM_DELTOOLA = (0x0400 + 5),
            TTM_DELTOOLW = (0x0400 + 51),
            TTM_NEWTOOLRECTA = (0x0400 + 6),
            TTM_NEWTOOLRECTW = (0x0400 + 52),
            TTM_RELAYEVENT = (0x0400 + 7),
            TTM_GETTIPBKCOLOR = (0x0400 + 22),
            TTM_SETTIPBKCOLOR = (0x0400 + 19),
            TTM_SETTIPTEXTCOLOR = (0x0400 + 20),
            TTM_GETTIPTEXTCOLOR = (0x0400 + 23),
            TTM_GETTOOLINFOA = (0x0400 + 8),
            TTM_GETTOOLINFOW = (0x0400 + 53),
            TTM_SETTOOLINFOA = (0x0400 + 9),
            TTM_SETTOOLINFOW = (0x0400 + 54),
            TTM_HITTESTA = (0x0400 + 10),
            TTM_HITTESTW = (0x0400 + 55),
            TTM_GETTEXTA = (0x0400 + 11),
            TTM_GETTEXTW = (0x0400 + 56),
            TTM_UPDATE = (0x0400 + 29),
            TTM_UPDATETIPTEXTA = (0x0400 + 12),
            TTM_UPDATETIPTEXTW = (0x0400 + 57),
            TTM_ENUMTOOLSA = (0x0400 + 14),
            TTM_ENUMTOOLSW = (0x0400 + 58),
            TTM_GETCURRENTTOOLA = (0x0400 + 15),
            TTM_GETCURRENTTOOLW = (0x0400 + 59),
            TTM_WINDOWFROMPOINT = (0x0400 + 16),
            TTM_GETDELAYTIME = (0x0400 + 21),
            TTM_SETMAXTIPWIDTH = (0x0400 + 24),
            TTN_GETDISPINFOA = ((0 - 520) - 0),
            TTN_GETDISPINFOW = ((0 - 520) - 10),
            TTN_SHOW = ((0 - 520) - 1),
            TTN_POP = ((0 - 520) - 2),
            TTN_NEEDTEXTA = ((0 - 520) - 0),
            TTN_NEEDTEXTW = ((0 - 520) - 10),
            TBS_AUTOTICKS = 0x0001,
            TBS_VERT = 0x0002,
            TBS_TOP = 0x0004,
            TBS_BOTTOM = 0x0000,
            TBS_BOTH = 0x0008,
            TBS_NOTICKS = 0x0010,
            TBM_GETPOS = (0x0400),
            TBM_SETTIC = (0x0400 + 4),
            TBM_SETPOS = (0x0400 + 5),
            TBM_SETRANGE = (0x0400 + 6),
            TBM_SETRANGEMIN = (0x0400 + 7),
            TBM_SETRANGEMAX = (0x0400 + 8),
            TBM_SETTICFREQ = (0x0400 + 20),
            TBM_SETPAGESIZE = (0x0400 + 21),
            TBM_SETLINESIZE = (0x0400 + 23),
            TB_LINEUP = 0,
            TB_LINEDOWN = 1,
            TB_PAGEUP = 2,
            TB_PAGEDOWN = 3,
            TB_THUMBPOSITION = 4,
            TB_THUMBTRACK = 5,
            TB_TOP = 6,
            TB_BOTTOM = 7,
            TB_ENDTRACK = 8,
            TVS_HASBUTTONS = 0x0001,
            TVS_HASLINES = 0x0002,
            TVS_LINESATROOT = 0x0004,
            TVS_EDITLABELS = 0x0008,
            TVS_SHOWSELALWAYS = 0x0020,
            TVS_RTLREADING = 0x0040,
            TVS_CHECKBOXES = 0x0100,
            TVS_TRACKSELECT = 0x0200,
            TVS_FULLROWSELECT = 0x1000,
            TVS_NONEVENHEIGHT = 0x4000,
            TVS_INFOTIP = 0x0800,
            TVS_NOTOOLTIPS = 0x0080,
            TVIF_TEXT = 0x0001,
            TVIF_IMAGE = 0x0002,
            TVIF_PARAM = 0x0004,
            TVIF_STATE = 0x0008,
            TVIF_HANDLE = 0x0010,
            TVIF_SELECTEDIMAGE = 0x0020,
            TVIS_SELECTED = 0x0002,
            TVIS_EXPANDED = 0x0020,
            TVIS_EXPANDEDONCE = 0x0040,
            TVIS_STATEIMAGEMASK = 0xF000,
            TVI_ROOT = (unchecked((int)0xFFFF0000)),
            TVI_FIRST = (unchecked((int)0xFFFF0001)),
            TVM_INSERTITEMA = (0x1100 + 0),
            TVM_INSERTITEMW = (0x1100 + 50),
            TVM_DELETEITEM = (0x1100 + 1),
            TVM_EXPAND = (0x1100 + 2),
            TVE_COLLAPSE = 0x0001,
            TVE_EXPAND = 0x0002,
            TVM_GETITEMRECT = (0x1100 + 4),
            TVM_GETINDENT = (0x1100 + 6),
            TVM_SETINDENT = (0x1100 + 7),
            TVM_GETIMAGELIST = (0x1100 + 8),
            TVM_SETIMAGELIST = (0x1100 + 9),
            TVM_GETNEXTITEM = (0x1100 + 10),
            TVGN_NEXT = 0x0001,
            TVGN_PREVIOUS = 0x0002,
            TVGN_FIRSTVISIBLE = 0x0005,
            TVGN_NEXTVISIBLE = 0x0006,
            TVGN_PREVIOUSVISIBLE = 0x0007,
            TVGN_DROPHILITE = 0x0008,
            TVGN_CARET = 0x0009,
            TVM_SELECTITEM = (0x1100 + 11),
            TVM_GETITEMA = (0x1100 + 12),
            TVM_GETITEMW = (0x1100 + 62),
            TVM_SETITEMA = (0x1100 + 13),
            TVM_SETITEMW = (0x1100 + 63),
            TVM_EDITLABELA = (0x1100 + 14),
            TVM_EDITLABELW = (0x1100 + 65),
            TVM_GETEDITCONTROL = (0x1100 + 15),
            TVM_GETVISIBLECOUNT = (0x1100 + 16),
            TVM_HITTEST = (0x1100 + 17),
            TVM_ENSUREVISIBLE = (0x1100 + 20),
            TVM_ENDEDITLABELNOW = (0x1100 + 22),
            TVM_GETISEARCHSTRINGA = (0x1100 + 23),
            TVM_GETISEARCHSTRINGW = (0x1100 + 64),
            TVM_SETITEMHEIGHT = (0x1100 + 27),
            TVM_GETITEMHEIGHT = (0x1100 + 28),
            TVN_SELCHANGINGA = ((0 - 400) - 1),
            TVN_SELCHANGINGW = ((0 - 400) - 50),
            TVN_GETINFOTIPA = ((0 - 400) - 13),
            TVN_GETINFOTIPW = ((0 - 400) - 14),
            TVN_SELCHANGEDA = ((0 - 400) - 2),
            TVN_SELCHANGEDW = ((0 - 400) - 51),
            TVC_UNKNOWN = 0x0000,
            TVC_BYMOUSE = 0x0001,
            TVC_BYKEYBOARD = 0x0002,
            TVN_GETDISPINFOA = ((0 - 400) - 3),
            TVN_GETDISPINFOW = ((0 - 400) - 52),
            TVN_SETDISPINFOA = ((0 - 400) - 4),
            TVN_SETDISPINFOW = ((0 - 400) - 53),
            TVN_ITEMEXPANDINGA = ((0 - 400) - 5),
            TVN_ITEMEXPANDINGW = ((0 - 400) - 54),
            TVN_ITEMEXPANDEDA = ((0 - 400) - 6),
            TVN_ITEMEXPANDEDW = ((0 - 400) - 55),
            TVN_BEGINDRAGA = ((0 - 400) - 7),
            TVN_BEGINDRAGW = ((0 - 400) - 56),
            TVN_BEGINRDRAGA = ((0 - 400) - 8),
            TVN_BEGINRDRAGW = ((0 - 400) - 57),
            TVN_BEGINLABELEDITA = ((0 - 400) - 10),
            TVN_BEGINLABELEDITW = ((0 - 400) - 59),
            TVN_ENDLABELEDITA = ((0 - 400) - 11),
            TVN_ENDLABELEDITW = ((0 - 400) - 60),
            TCS_BOTTOM = 0x0002,
            TCS_RIGHT = 0x0002,
            TCS_FLATBUTTONS = 0x0008,
            TCS_HOTTRACK = 0x0040,
            TCS_VERTICAL = 0x0080,
            TCS_TABS = 0x0000,
            TCS_BUTTONS = 0x0100,
            TCS_MULTILINE = 0x0200,
            TCS_RIGHTJUSTIFY = 0x0000,
            TCS_FIXEDWIDTH = 0x0400,
            TCS_RAGGEDRIGHT = 0x0800,
            TCS_OWNERDRAWFIXED = 0x2000,
            TCS_TOOLTIPS = 0x4000,
            TCM_SETIMAGELIST = (0x1300 + 3),
            TCIF_TEXT = 0x0001,
            TCIF_IMAGE = 0x0002,
            TCM_GETITEMA = (0x1300 + 5),
            TCM_GETITEMW = (0x1300 + 60),
            TCM_SETITEMA = (0x1300 + 6),
            TCM_SETITEMW = (0x1300 + 61),
            TCM_INSERTITEMA = (0x1300 + 7),
            TCM_INSERTITEMW = (0x1300 + 62),
            TCM_DELETEITEM = (0x1300 + 8),
            TCM_DELETEALLITEMS = (0x1300 + 9),
            TCM_GETITEMRECT = (0x1300 + 10),
            TCM_GETCURSEL = (0x1300 + 11),
            TCM_SETCURSEL = (0x1300 + 12),
            TCM_ADJUSTRECT = (0x1300 + 40),
            TCM_SETITEMSIZE = (0x1300 + 41),
            TCM_SETPADDING = (0x1300 + 43),
            TCM_GETROWCOUNT = (0x1300 + 44),
            TCM_GETTOOLTIPS = (0x1300 + 45),
            TCM_SETTOOLTIPS = (0x1300 + 46),
            TCN_SELCHANGE = ((0 - 550) - 1),
            TCN_SELCHANGING = ((0 - 550) - 2),
            TBSTYLE_WRAPPABLE = 0x0200,
            TVM_SETBKCOLOR = (TV_FIRST + 29),
            TVM_SETTEXTCOLOR = (TV_FIRST + 30),
            TYMED_NULL = 0,
            TVM_GETLINECOLOR = (TV_FIRST + 41),
            TVM_SETLINECOLOR = (TV_FIRST + 40),
            TVM_SETTOOLTIPS = (TV_FIRST + 24),
            TVSIL_STATE = 2,
            TVM_SORTCHILDRENCB = (TV_FIRST + 21),
            TMPF_FIXED_PITCH = 0x01;

        /// <summary>
        /// 
        /// </summary>
        public const int TVHT_NOWHERE = 0x0001,
            TVHT_ONITEMICON = 0x0002,
            TVHT_ONITEMLABEL = 0x0004,
            TVHT_ONITEM = (TVHT_ONITEMICON | TVHT_ONITEMLABEL | TVHT_ONITEMSTATEICON),
            TVHT_ONITEMINDENT = 0x0008,
            TVHT_ONITEMBUTTON = 0x0010,
            TVHT_ONITEMRIGHT = 0x0020,
            TVHT_ONITEMSTATEICON = 0x0040,
            TVHT_ABOVE = 0x0100,
            TVHT_BELOW = 0x0200,
            TVHT_TORIGHT = 0x0400,
            TVHT_TOLEFT = 0x0800;

        /// <summary>
        /// 
        /// </summary>
        public const int UIS_SET = 1,
            UIS_CLEAR = 2,
            UIS_INITIALIZE = 3,
            UISF_HIDEFOCUS = 0x1,
            UISF_HIDEACCEL = 0x2,
            USERCLASSTYPE_FULL = 1,
            USERCLASSTYPE_SHORT = 2,
            USERCLASSTYPE_APPNAME = 3,
            UOI_FLAGS = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int VIEW_E_DRAW = unchecked((int)0x80040140),
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_TAB = 0x09,
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_CAPITAL = 0x14,
            VK_KANA = 0x15,
            VK_ESCAPE = 0x1B,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            VK_INSERT = 0x002D,
            VK_DELETE = 0x002E;

        /// <summary>
        /// 
        /// </summary>
        public const int WH_JOURNALPLAYBACK = 1,
            WH_GETMESSAGE = 3,
            WH_MOUSE = 7,
            WSF_VISIBLE = 0x0001,
            WM_NULL = 0x0000,
            WM_CREATE = 0x0001,
            WM_DELETEITEM = 0x002D,
            WM_DESTROY = 0x0002,
            WM_MOVE = 0x0003,
            WM_SIZE = 0x0005,
            WM_ACTIVATE = 0x0006,
            WA_INACTIVE = 0,
            WA_ACTIVE = 1,
            WA_CLICKACTIVE = 2,
            WM_SETFOCUS = 0x0007,
            WM_KILLFOCUS = 0x0008,
            WM_ENABLE = 0x000A,
            WM_SETREDRAW = 0x000B,
            WM_SETTEXT = 0x000C,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E,
            WM_PAINT = 0x000F,
            WM_CLOSE = 0x0010,
            WM_QUERYENDSESSION = 0x0011,
            WM_QUIT = 0x0012,
            WM_QUERYOPEN = 0x0013,
            WM_ERASEBKGND = 0x0014,
            WM_SYSCOLORCHANGE = 0x0015,
            WM_ENDSESSION = 0x0016,
            WM_SHOWWINDOW = 0x0018,
            WM_WININICHANGE = 0x001A,
            WM_SETTINGCHANGE = 0x001A,
            WM_DEVMODECHANGE = 0x001B,
            WM_ACTIVATEAPP = 0x001C,
            WM_FONTCHANGE = 0x001D,
            WM_TIMECHANGE = 0x001E,
            WM_CANCELMODE = 0x001F,
            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_CHILDACTIVATE = 0x0022,
            WM_QUEUESYNC = 0x0023,
            WM_GETMINMAXINFO = 0x0024,
            WM_PAINTICON = 0x0026,
            WM_ICONERASEBKGND = 0x0027,
            WM_NEXTDLGCTL = 0x0028,
            WM_SPOOLERSTATUS = 0x002A,
            WM_DRAWITEM = 0x002B,
            WM_MEASUREITEM = 0x002C,
            WM_VKEYTOITEM = 0x002E,
            WM_CHARTOITEM = 0x002F,
            WM_SETFONT = 0x0030,
            WM_GETFONT = 0x0031,
            WM_SETHOTKEY = 0x0032,
            WM_GETHOTKEY = 0x0033,
            WM_QUERYDRAGICON = 0x0037,
            WM_COMPAREITEM = 0x0039,
            WM_GETOBJECT = 0x003D,
            WM_COMPACTING = 0x0041,
            WM_COMMNOTIFY = 0x0044,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,
            WM_POWER = 0x0048,
            WM_COPYDATA = 0x004A,
            WM_CANCELJOURNAL = 0x004B,
            WM_NOTIFY = 0x004E,
            WM_INPUTLANGCHANGEREQUEST = 0x0050,
            WM_INPUTLANGCHANGE = 0x0051,
            WM_TCARD = 0x0052,
            WM_HELP = 0x0053,
            WM_USERCHANGED = 0x0054,
            WM_NOTIFYFORMAT = 0x0055,
            WM_CONTEXTMENU = 0x007B,
            WM_STYLECHANGING = 0x007C,
            WM_STYLECHANGED = 0x007D,
            WM_DISPLAYCHANGE = 0x007E,
            WM_GETICON = 0x007F,
            WM_SETICON = 0x0080,
            WM_NCCREATE = 0x0081,
            WM_NCDESTROY = 0x0082,
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x0084,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,
            WM_GETDLGCODE = 0x0087,
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCMOUSELEAVE = 0x02A2,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,
            WM_NCXBUTTONDOWN = 0x00AB,
            WM_NCXBUTTONUP = 0x00AC,
            WM_NCXBUTTONDBLCLK = 0x00AD,
            WM_KEYFIRST = 0x0100,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_CHAR = 0x0102,
            WM_DEADCHAR = 0x0103,
            WM_CTLCOLOR = 0x0019,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
            WM_SYSCHAR = 0x0106,
            WM_SYSDEADCHAR = 0x0107,
            WM_KEYLAST = 0x0108,
            WM_IME_STARTCOMPOSITION = 0x010D,
            WM_IME_ENDCOMPOSITION = 0x010E,
            WM_IME_COMPOSITION = 0x010F,
            WM_IME_KEYLAST = 0x010F,
            WM_INITDIALOG = 0x0110,
            WM_COMMAND = 0x0111,
            WM_SYSCOMMAND = 0x0112,
            WM_TIMER = 0x0113,
            WM_HSCROLL = 0x0114,
            WM_VSCROLL = 0x0115,
            WM_INITMENU = 0x0116,
            WM_INITMENUPOPUP = 0x0117,
            WM_MENUSELECT = 0x011F,
            WM_MENUCHAR = 0x0120,
            WM_ENTERIDLE = 0x0121,
            WM_UNINITMENUPOPUP = 0x0125,
            WM_CHANGEUISTATE = 0x0127,
            WM_UPDATEUISTATE = 0x0128,
            WM_QUERYUISTATE = 0x0129,
            WM_CTLCOLORMSGBOX = 0x0132,
            WM_CTLCOLOREDIT = 0x0133,
            WM_CTLCOLORLISTBOX = 0x0134,
            WM_CTLCOLORBTN = 0x0135,
            WM_CTLCOLORDLG = 0x0136,
            WM_CTLCOLORSCROLLBAR = 0x0137,
            WM_CTLCOLORSTATIC = 0x0138,
            WM_MOUSEFIRST = 0x0200,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C,
            WM_XBUTTONDBLCLK = 0x020D,
            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSELAST = 0x020A,
            WM_MOUSEHWHEEL = 0x020E;

        /// <summary>
        /// 
        /// </summary>
        public const int WHEEL_DELTA = 120,
            WM_PARENTNOTIFY = 0x0210,
            WM_ENTERMENULOOP = 0x0211,
            WM_EXITMENULOOP = 0x0212,
            WM_NEXTMENU = 0x0213,
            WM_SIZING = 0x0214,
            WM_CAPTURECHANGED = 0x0215,
            WM_MOVING = 0x0216,
            WM_POWERBROADCAST = 0x0218,
            WM_DEVICECHANGE = 0x0219,
            WM_IME_SETCONTEXT = 0x0281,
            WM_IME_NOTIFY = 0x0282,
            WM_IME_CONTROL = 0x0283,
            WM_IME_COMPOSITIONFULL = 0x0284,
            WM_IME_SELECT = 0x0285,
            WM_IME_CHAR = 0x0286,
            WM_IME_KEYDOWN = 0x0290,
            WM_IME_KEYUP = 0x0291,
            WM_MDICREATE = 0x0220,
            WM_MDIDESTROY = 0x0221,
            WM_MDIACTIVATE = 0x0222,
            WM_MDIRESTORE = 0x0223,
            WM_MDINEXT = 0x0224,
            WM_MDIMAXIMIZE = 0x0225,
            WM_MDITILE = 0x0226,
            WM_MDICASCADE = 0x0227,
            WM_MDIICONARRANGE = 0x0228,
            WM_MDIGETACTIVE = 0x0229,
            WM_MDISETMENU = 0x0230,
            WM_ENTERSIZEMOVE = 0x0231,
            WM_EXITSIZEMOVE = 0x0232,
            WM_DROPFILES = 0x0233,
            WM_MDIREFRESHMENU = 0x0234,
            WM_MOUSEHOVER = 0x02A1,
            WM_MOUSELEAVE = 0x02A3,
            WM_CUT = 0x0300,
            WM_COPY = 0x0301,
            WM_PASTE = 0x0302,
            WM_CLEAR = 0x0303,
            WM_UNDO = 0x0304,
            WM_RENDERFORMAT = 0x0305,
            WM_RENDERALLFORMATS = 0x0306,
            WM_DESTROYCLIPBOARD = 0x0307,
            WM_DRAWCLIPBOARD = 0x0308,
            WM_PAINTCLIPBOARD = 0x0309,
            WM_VSCROLLCLIPBOARD = 0x030A,
            WM_SIZECLIPBOARD = 0x030B,
            WM_ASKCBFORMATNAME = 0x030C,
            WM_CHANGECBCHAIN = 0x030D,
            WM_HSCROLLCLIPBOARD = 0x030E,
            WM_QUERYNEWPALETTE = 0x030F,
            WM_PALETTEISCHANGING = 0x0310,
            WM_PALETTECHANGED = 0x0311,
            WM_HOTKEY = 0x0312,
            WM_PRINT = 0x0317,
            WM_PRINTCLIENT = 0x0318,
            WM_THEMECHANGED = 0x031A,
            WM_HANDHELDFIRST = 0x0358,
            WM_HANDHELDLAST = 0x035F,
            WM_AFXFIRST = 0x0360,
            WM_AFXLAST = 0x037F,
            WM_PENWINFIRST = 0x0380,
            WM_PENWINLAST = 0x038F,
            WM_APP = unchecked(0x8000),
            WM_USER = 0x0400,
            WM_REFLECT = NativeMethods.WM_USER + 0x1C00,
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = unchecked((int)0x80000000),
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_CAPTION = 0x00C00000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_LAYERED = 0x00080000,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_LAYOUTRTL = 0x00400000,
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            WPF_SETMINPOSITION = 0x0001,
            WM_CHOOSEFONT_GETLOGFONT = (0x0400 + 1);

        // wParam of report message WM_IME_NOTIFY (public\sdk\imm.h)

        /// <summary>
        /// 
        /// </summary>
        public const int
            IMN_CLOSESTATUSWINDOW = 0x0001,
            IMN_OPENSTATUSWINDOW = 0x0002,
            IMN_CHANGECANDIDATE = 0x0003,
            IMN_CLOSECANDIDATE = 0x0004,
            IMN_OPENCANDIDATE = 0x0005,
            IMN_SETCONVERSIONMODE = 0x0006,
            IMN_SETSENTENCEMODE = 0x0007,
            IMN_SETOPENSTATUS = 0x0008,
            IMN_SETCANDIDATEPOS = 0x0009,
            IMN_SETCOMPOSITIONFONT = 0x000A,
            IMN_SETCOMPOSITIONWINDOW = 0x000B,
            IMN_SETSTATUSWINDOWPOS = 0x000C,
            IMN_GUIDELINE = 0x000D,
            IMN_PRIVATE = 0x000E;

        /// <summary>
        /// 
        /// </summary>
        public const int PD_RESULT_CANCEL = 0;

        /// <summary>
        /// 
        /// </summary>
        public const int PD_RESULT_PRINT = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int PD_RESULT_APPLY = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int XBUTTON1 = 0x0001;

        /// <summary>
        /// 
        /// </summary>
        public const int XBUTTON2 = 0x0002;

        // Ctrl key.
        /// <summary>
        /// 
        /// </summary>
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        /// <summary>
        /// 
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x0002;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dwData"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", EntryPoint = "mouse_event", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern void Mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vk"></param>
        /// <param name="scan"></param>
        /// <param name="flags"></param>
        /// <param name="extrainfo"></param>
        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern void Keybd_event(byte vk, byte scan, int flags, IntPtr extrainfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //use dll method for vertical scroll and works fine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="steps"></param>
        internal static void VScrollWheel(int steps) => Mouse_event(MouseEventF_Wheel, 0, 0, (uint)steps, UIntPtr.Zero);

        //use dll method for horizontal scroll and no response
        /// <summary>
        /// 
        /// </summary>
        /// <param name="steps"></param>
        internal static void HScrollWheel(int steps) => Mouse_event(MouseEventF_HWheel, 0, 0, (uint)steps, UIntPtr.Zero);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SignedHIWORD(this int n) => (short)((n >> 0x10) & 0xffff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SignedHIWORD(this IntPtr n) => SignedHIWORD((int)((long)n));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SignedLOWORD(this int n) => (short)(n & 0xffff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SignedLOWORD(this IntPtr n) => SignedLOWORD((int)((long)n));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hr"></param>
        /// <returns></returns>
        public static bool Succeeded(int hr) => (hr >= 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hr"></param>
        /// <returns></returns>
        public static bool Failed(int hr) => (hr < 0);
    }
}
