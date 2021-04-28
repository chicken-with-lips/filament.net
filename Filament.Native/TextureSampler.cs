using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class TextureSampler
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_TextureSampler_nCreateSampler")]
        public static extern IntPtr CreateSampler(byte min, byte mag, byte s, byte t, byte r);
    }
}
