using System.Numerics;

namespace Filament
{
    public static class MathUtils
    {
        public static void PackTangentFrame(Vector3 tangent, Vector3 bitangent, Vector3 normal, float[] destination, int offset = 0)
        {
            Native.MathUtils.PackTangentFrame(
                tangent.X, tangent.Y, tangent.Z,
                bitangent.X, bitangent.Y, bitangent.Z,
                normal.X, normal.Y, normal.Z,
                destination, offset
            );
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }
    }
}
