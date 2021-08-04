using System.Numerics;
using Filament;
using Filament.DemoApp;
using Filament.SampleData;

namespace VboTest
{
    class Program
    {
        private static Vector2[] POSITIONS = {
            new(0.5f, 0),
            new(-0.5f, 0.5f),
            new(-0.5f, -0.5f)
        };

        private static uint[] COLORS = {0xffff0000u, 0xff00ff00u, 0xff0000ffu};
        private static ushort[] TRIANGLE_INDICES = {0, 1, 2};


        static void Main(string[] args)
        {
            VertexBuffer vertexBuffer = null;
            IndexBuffer indexBuffer = null;
            Material material = null;
            int renderable = -1;
            int cameraEntity = -1;
            Camera camera = null;

            var app = new Application(
                new WindowConfig() {
                    Title = "vbotest",
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                // Aggregate positions and colors into a single buffer without interleaving.
                var vbo = new VertexBufferObject();
                vbo.Write(new Vector2(0.5f, 0));
                vbo.Write(new Vector2(-0.5f, 0.5f));
                vbo.Write(new Vector2(-0.5f, -0.5f));
                vbo.Write(0xffff0000u);
                vbo.Write(0xff00ff00u);
                vbo.Write(0xff0000ffu);

                // Populate vertex buffer.
                vertexBuffer = VertexBufferBuilder.Create()
                    .WithVertexCount(3)
                    .WithBufferCount(1)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float2, 0, 8)
                    .WithAttribute(VertexAttribute.Color, 0, ElementType.UByte4, 24, 4)
                    .WithNormalized(VertexAttribute.Color)
                    .Build(engine);
                vertexBuffer.SetBufferAt(engine, 0, vbo);

                // Populate index buffer.
                indexBuffer = IndexBufferBuilder.Create()
                    .WithIndexCount(3)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                indexBuffer.SetBuffer(engine, TRIANGLE_INDICES);

                // Construct material.
                var sampleData = new SampleDataLoader();

                material = MaterialBuilder.Create()
                    .WithPackage(sampleData.LoadBakedColor())
                    .Build(engine);

                // Construct renderable.
                RenderableBuilder.Create(1)
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithMaterial(0, material.DefaultInstance)
                    .WithGeometry(0, PrimitiveType.Triangles, vertexBuffer, indexBuffer, 0, 3)
                    .Build(engine, renderable = EntityManager.Create());

                scene.AddEntity(renderable);

                // Replace the FilamentApp camera with identity.
                cameraEntity = EntityManager.Create();
                camera = engine.CreateCamera(cameraEntity);
                view.Camera = camera;
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(renderable);
                engine.Destroy(material);
                engine.Destroy(vertexBuffer);
                engine.Destroy(indexBuffer);
                engine.DestroyCameraComponent(cameraEntity);

                EntityManager.Destroy(cameraEntity);
            };

            app.Run();
        }
    }
}
