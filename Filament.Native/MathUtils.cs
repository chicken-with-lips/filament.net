using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class MathUtils
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_MathUtils_nPackTangentFrame")]
        public static extern void PackTangentFrame(
            float tangentX, float tangentY, float tangentZ,
            float bitangentX, float bitangentY, float bitangentZ,
            float normalX, float normalY, float normalZ,
            float[] quaternion, int offset);
    }
}
