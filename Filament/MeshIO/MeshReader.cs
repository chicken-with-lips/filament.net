namespace Filament.MeshIO
{
    public static class MeshReader
    {
        #region Methods

        /// <summary>
        /// Loads a filamesh renderable from an in-memory buffer.
        /// </summary>
        /// <remarks>
        /// All the primitives of the decoded renderable are assigned the specified default material.
        /// </remarks>
        public static Mesh LoadFromBuffer(Engine engine, byte[] buffer, MaterialInstance defaultMaterial)
        {
            Native.MeshIO.MeshReader.LoadMeshFromBufferMaterial(
                engine.NativePtr, buffer, defaultMaterial.NativePtr,
                out var renderable, out var vertexBuffer, out var indexBuffer
            );

            return new Mesh(
                renderable,
                VertexBuffer.GetOrCreateCache(vertexBuffer),
                IndexBuffer.GetOrCreateCache(indexBuffer)
            );
        }

        #endregion
    }
}
