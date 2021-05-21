using System.Numerics;

namespace Filament
{
    /// <summary>
    /// An axis aligned 3D box represented by its center and half-extent.
    /// </summary>
    public readonly struct Box
    {
        /// <summary>
        /// Center of the 3D box.
        /// </summary>
        public Vector3 Center { get; }

        /// <summary>
        /// Half extent from the center on all 3 axis.
        /// </summary>
        public Vector3 HalfExtent { get; }

        public Box(Vector3 center, Vector3 halfExtent)
        {
            Center = center;
            HalfExtent = halfExtent;
        }
    }
}
