internal static partial class Interop
{
    internal static partial class Winmm
    {
        [Flags]
        public enum WaveInOpenFlags
            : uint
        {
            CALLBACK_NULL = 0x00000,
            CALLBACK_FUNCTION = 0x30000,
            CALLBACK_EVENT = 0x50000,
            CALLBACK_WINDOW = 0x10000,
            CALLBACK_THREAD = 0x20000,
            WAVE_FORMAT_QUERY = 1,
            WAVE_MAPPED = 4,
            WAVE_FORMAT_DIRECT = 8
        }
    }
}
