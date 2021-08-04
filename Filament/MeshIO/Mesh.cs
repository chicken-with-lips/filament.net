namespace Filament.MeshIO
{
    public class Mesh
    {
        #region Properties

        public int Renderable { get; }
        public VertexBuffer VertexBuffer { get; }
        public IndexBuffer IndexBuffer { get; }

        #endregion

        #region Methods

        public Mesh(int renderable, VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
        {
            Renderable = renderable;
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }

        #endregion
    }
}
