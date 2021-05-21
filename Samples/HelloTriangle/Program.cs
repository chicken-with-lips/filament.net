using System;
using System.Numerics;
using Filament;
using Filament.DemoApp;
using Filament.SampleData;

namespace HelloTriangle
{
    class Program
    {
        private static ushort[] TRIANGLE_INDICES = {0, 1, 2};

        static void Main(string[] args)
        {
            Skybox skybox = null;
            VertexBuffer vertexBuffer = null;
            IndexBuffer indexBuffer = null;
            Material material = null;
            int renderable = -1;
            Camera camera = null;
            int cameraEntity = -1;

            var vbo = new VertexBufferObject();
            vbo.Write(new Vector2(1, 0));
            vbo.Write(0xffff0000u);
            vbo.Write(new Vector2(MathF.Cos(MathF.PI * 2 / 3), MathF.Sin(MathF.PI * 2 / 3)));
            vbo.Write(0xff00ff00u);
            vbo.Write(new Vector2(MathF.Cos(MathF.PI * 4 / 3), MathF.Sin(MathF.PI * 4 / 3)));
            vbo.Write(0xff0000ffu);

            var app = new Application(
                new WindowConfig() {
                    Title = "hellotriangle",
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                skybox = SkyboxBuilder.Create()
                    .WithColor(new Color(0.1f, 0.125f, 0.25f, 1.0f))
                    .Build(engine);

                scene.Skybox = skybox;
                view.PostProcessingEnabled = false;

                vertexBuffer = VertexBufferBuilder.Create()
                    .WithVertexCount(3)
                    .WithBufferCount(1)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float2, 0, 12)
                    .WithAttribute(VertexAttribute.Color, 0, ElementType.UByte4, 8, 12)
                    .WithNormalized(VertexAttribute.Color)
                    .Build(engine);
                vertexBuffer.SetBufferAt(engine, 0, vbo);

                indexBuffer = IndexBufferBuilder.Create()
                    .WithIndexCount(3)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                indexBuffer.SetBuffer(engine, TRIANGLE_INDICES);

                var sampleData = new SampleDataLoader();

                material = MaterialBuilder.Create()
                    .WithPackage(sampleData.LoadBakedColor())
                    .Build(engine);

                renderable = EntityManager.Create();

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithMaterial(0, material.DefaultInstance)
                    .WithGeometry(0, PrimitiveType.Triangles, vertexBuffer, indexBuffer, 0, 3)
                    .WithCulling(false)
                    .WithReceiveShadows(false)
                    .WithCastShadows(false)
                    .Build(engine, renderable);

                scene.AddEntity(renderable);

                cameraEntity = EntityManager.Create();
                camera = engine.CreateCamera(cameraEntity);
                view.Camera = camera;
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(skybox);
                engine.Destroy(renderable);
                engine.Destroy(material);
                engine.Destroy(vertexBuffer);
                engine.Destroy(indexBuffer);
                engine.DestroyCameraComponent(cameraEntity);

                EntityManager.Destroy(cameraEntity);
            };

            app.Animate = (engine, view, now) => {
                var ZOOM = 1.5f;
                var w = view.Viewport.Width;
                var h = view.Viewport.Height;
                ;
                var aspect = (float) w / h;

                camera.SetProjection(Projection.Ortho, -aspect * ZOOM, aspect * ZOOM, -ZOOM, ZOOM, 0, 1);

                var tcm = engine.TransformManager;
                tcm.SetTransform(tcm.GetInstance(renderable), Matrix4x4.CreateFromAxisAngle(Vector3.UnitZ, now));
            };

            app.Run();
        }
    }
}
