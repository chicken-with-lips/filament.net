using OpenTK.Mathematics;

namespace Filament.Extensions
{
    public static class Vector4Extension
    {
        public static Vector4h ToHalf(this Vector4 v)
        {
            return new Vector4h(
                new Half(v.X),
                new Half(v.Y),
                new Half(v.Z),
                new Half(v.W)
            );
        }
    }
}
