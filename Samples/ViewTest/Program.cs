using System.Numerics;
using Filament;
using Filament.DemoApp;

namespace ViewTest
{
    class Program
    {
        private static readonly Vector2[] TRIANGLE_VERTICES = {
            new(1, 0),
            new(-0.5f, 0.866f),
            new(-0.5f, -0.866f)
        };

        private static readonly ushort[] TRIANGLE_INDICES = {0, 1, 2};


        static void Main(string[] args)
        {
            Camera camera = null;
            Skybox skybox = null;
            VertexBuffer vertexBuffer = null;
            IndexBuffer indexBuffer = null;
            int renderable = -1;

            var app = new Application(
                new WindowConfig() {
                    Title = "viewtest"
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                camera = engine.CreateCamera(EntityManager.Create());

                skybox = SkyboxBuilder.Create()
                    .WithColor(new Color(0, 0, 1, 1))
                    .Build(engine);

                scene.Skybox = skybox;
                view.Viewport = new Viewport(100, 100, 512, 512);
                view.Camera = camera;

                vertexBuffer = VertexBufferBuilder.Create()
                    .WithVertexCount(3)
                    .WithBufferCount(1)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float2, 0, 8)
                    .Build(engine);
                vertexBuffer.SetBufferAt(engine, 0, TRIANGLE_VERTICES);

                indexBuffer = IndexBufferBuilder.Create()
                    .WithIndexCount(3)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                indexBuffer.SetBuffer(engine, TRIANGLE_INDICES);

                renderable = EntityManager.Create();

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithGeometry(0, PrimitiveType.Triangles, vertexBuffer, indexBuffer, 0, 3)
                    .Build(engine, renderable);

                scene.AddEntity(renderable);
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(skybox);
                engine.Destroy(renderable);
                engine.Destroy(vertexBuffer);
                engine.Destroy(indexBuffer);
                engine.Destroy(camera.Entity);

                EntityManager.Destroy(camera.Entity);
            };

            app.Run();
        }
    }
}
