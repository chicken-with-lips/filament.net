using System;
using System.IO;
using Filament;
using Filament.DemoApp;
using Filament.SampleData;
using OpenTK.Mathematics;
using StbImageSharp;

namespace ViewTest
{
    struct Vertex
    {
        public Vector2 Position;
        public Vector2 Uv;
    }

    class Program
    {
        private static ushort[] QUAD_INDICES = {
            0, 1, 2,
            3, 2, 1,
        };

        static void Main(string[] args)
        {
            Texture texture = null;
            Skybox skybox = null;
            VertexBuffer vertexBuffer = null;
            IndexBuffer indexBuffer = null;
            Material material = null;
            MaterialInstance materialInstance = null;
            Camera camera = null;
            int cameraEntity = -1;
            int renderable = -1;

            var app = new Application(
                new WindowConfig() {
                    Title = "texturedquad"
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                var path = Path.Combine(app.RootAssetPath, "textures/Moss_01/Moss_01_Color.png");

                if (!File.Exists(path)) {
                    Console.WriteLine("The texture {0} does not exist", path);
                    Environment.Exit(1);
                }

                using (var stream = File.OpenRead(path)) {
                    var imageResult = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    Console.WriteLine("Loaded texture: {0}x{1}", imageResult.Width, imageResult.Height);

                    var pixelBuffer = new PixelBufferDescriptor(imageResult.Data, PixelDataFormat.Rgba, PixelDataType.UByte);

                    texture = TextureBuilder.Create()
                        .WithWidth(imageResult.Width)
                        .WithHeight(imageResult.Height)
                        .WithLevels(1)
                        .WithSampler(TextureSamplerType.Texture2d)
                        .WithFormat(TextureFormat.Rgba8)
                        .Build(engine);
                    texture.SetImage(engine, 0, pixelBuffer);
                }

                // Set up view
                skybox = SkyboxBuilder.Create()
                    .WithColor(new Color(0.1f, 0.125f, 0.25f, 1.0f))
                    .Build(engine);

                scene.Skybox = skybox;
                cameraEntity = EntityManager.Create();
                camera = engine.CreateCamera(cameraEntity);

                view.PostProcessingEnabled = false;
                view.Camera = camera;

                // Create quad renderable
                var vbo = new VertexBufferObject();
                vbo.Write(new Vector2(-1, -1));
                vbo.Write(new Vector2(0, 0));
                vbo.Write(new Vector2(1, -1));
                vbo.Write(new Vector2(1, 0));
                vbo.Write(new Vector2(-1, 1));
                vbo.Write(new Vector2(0, 1));
                vbo.Write(new Vector2(1, 1));
                vbo.Write(new Vector2(1, 1));

                vertexBuffer = VertexBufferBuilder.Create()
                    .WithVertexCount(4)
                    .WithBufferCount(1)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float2, 0, 16)
                    .WithAttribute(VertexAttribute.Uv0, 0, ElementType.Float2, 8, 16)
                    .Build(engine);
                vertexBuffer.SetBufferAt(engine, 0, vbo);

                var sampleData = new SampleDataLoader();

                indexBuffer = IndexBufferBuilder.Create()
                    .WithIndexCount(6)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                indexBuffer.SetBuffer(engine, QUAD_INDICES);

                material = MaterialBuilder.Create()
                    .WithPackage(sampleData.LoadBakedTexture())
                    .Build(engine);

                var sampler = new TextureSampler(SamplerMinFilter.Linear, SamplerMagFilter.Linear);

                materialInstance = material.CreateInstance();
                materialInstance.SetParameter("albedo", texture, sampler);

                renderable = EntityManager.Create();

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithMaterial(0, materialInstance)
                    .WithGeometry(0, PrimitiveType.Triangles, vertexBuffer, indexBuffer, 0, 6)
                    .WithCulling(false)
                    .WithReceiveShadows(false)
                    .WithCastShadows(false)
                    .Build(engine, renderable);

                scene.AddEntity(renderable);
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(skybox);
                engine.Destroy(renderable);
                engine.Destroy(materialInstance);
                engine.Destroy(material);
                engine.Destroy(texture);
                engine.Destroy(vertexBuffer);
                engine.Destroy(indexBuffer);

                engine.DestroyCameraComponent(cameraEntity);
                EntityManager.Destroy(cameraEntity);
            };

            app.Animate = (engine, view, now) => {
                var zoom = 2.0f + 2.0f * (float) MathHelper.Sin(now);
                var width = view.Viewport.Width;
                var height = view.Viewport.Height;
                var aspect = (float) width / (float) height;

                camera.SetProjection(Projection.Ortho,
                    -aspect * zoom, aspect * zoom,
                    -zoom, zoom,
                    -1, 1
                );
            };

            app.Run();
        }
    }
}
