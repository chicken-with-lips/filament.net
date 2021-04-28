using OpenTK.Mathematics;

namespace Filament.Extensions
{
    public static class QuaternionExtension
    {
        public static Quaternion Positive(this Quaternion q)
        {
            return (q.W < 0) ? q.Inverted() : q;
        }

        public static Vector4 ToVector4(this Quaternion q)
        {
            return new(q.Xyz, q.W);
        }
    }
}
