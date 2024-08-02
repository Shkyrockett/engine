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
            private readonly ushort manufacturerId;

            /// <summary>
            /// wPid
            /// </summary>
            private readonly ushort productId;

            /// <summary>
            /// vDriverVersion
            /// </summary>
            private readonly uint driverVersion;

            /// <summary>
            /// Product Name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxProductNameLength)]
            private readonly string productName;

            /// <summary>
            /// Support - Reserved
            /// </summary>
            private readonly int support;

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
            public readonly Manufacturers Manufacturer => (Manufacturers)manufacturerId;

            /// <summary>
            /// Gets the product identifier (manufacturer specific)
            /// </summary>
            /// <value>
            /// The product identifier.
            /// </value>
            public readonly int ProductId => productId;

            /// <summary>
            /// Gets the product name
            /// </summary>
            /// <value>
            /// The name of the product.
            /// </value>
            public readonly string ProductName => productName;
        }
    }
}
