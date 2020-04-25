using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MidiInCapabilities
        {
            /// <summary>
            /// wMid
            /// </summary>
            private ushort manufacturerId;

            /// <summary>
            /// wPid
            /// </summary>
            private ushort productId;

            /// <summary>
            /// vDriverVersion
            /// </summary>
            private uint driverVersion;

            /// <summary>
            /// Product Name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxProductNameLength)]
            private string productName;

            /// <summary>
            /// Support - Reserved
            /// </summary>
            private int support;

            /// <summary>
            /// The maximum product name length
            /// </summary>
            private const int MaxProductNameLength = 32;

            /// <summary>
            /// Gets the manufacturer of this device
            /// </summary>
            /// <value>
            /// The manufacturer.
            /// </value>
            public Manufacturers Manufacturer => (Manufacturers)manufacturerId;

            /// <summary>
            /// Gets the product identifier (manufacturer specific)
            /// </summary>
            /// <value>
            /// The product identifier.
            /// </value>
            public int ProductId => productId;

            /// <summary>
            /// Gets the product name
            /// </summary>
            /// <value>
            /// The name of the product.
            /// </value>
            public string ProductName => productName;
        }
    }
}
