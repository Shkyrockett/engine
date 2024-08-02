using System.Runtime.InteropServices;
using System.Text;

internal static partial class Interop
{
    internal class Helpers
    {
        /// <summary>
        /// The grow string buffer.
        /// </summary>
        /// <param name="nativeMethod">The nativeMethod.</param>
        /// <param name="result">The result.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks>
        /// <para>https://github.com/AArnott/pinvoke</para>
        /// </remarks>
        internal static bool GrowStringBuffer(Func<StringBuilder, bool> nativeMethod, out string result)
        {
            // USB Hid maximum size is 126 wide chars + '\0' = 254 bytes, allocating 256 bytes we should never grow
            // until another HID standard decide otherwise.
            var stringBuilder = new StringBuilder(256);

            // If we ever resize over this value something got really wrong
            const int maximumRealisticSize = 1 * 1024 * 2014;

            while (stringBuilder.Capacity < maximumRealisticSize)
            {
                if (nativeMethod(stringBuilder))
                {
                    result = stringBuilder.ToString();
                    return true;
                }

                if ((Win32ErrorCode)Marshal.GetLastWin32Error() != Win32ErrorCode.ERROR_INVALID_USER_BUFFER)
                {
                    result = null;
                    return false;
                }

                stringBuilder.Capacity *= 2;
            }

            result = null;
            return false;
        }
    }
}
