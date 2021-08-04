using System;
using System.Runtime.InteropServices;

namespace Filament.Native.MeshIO
{
    public static class MeshReader
    {
        /// <summary>
        /// Loads a filamesh renderable from an in-memory buffer. All the primitives of the decoded renderable are
        /// assigned the specified default material.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_MeshIO_MeshReader_nLoadMeshFromBufferMaterial")]
        public static extern void LoadMeshFromBufferMaterial(IntPtr nativeEngine, byte[] buffer, IntPtr nativeDefaultMaterialInstance, out int renderable, out IntPtr vertexBuffer, out IntPtr outIndexBuffer);
    }
}
