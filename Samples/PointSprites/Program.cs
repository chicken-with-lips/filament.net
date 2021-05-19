using System;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;
using Filament;
using Filament.DemoApp;
using Filament.Image;
using Filament.SampleData;
using OpenTK.Mathematics;

namespace PointSprites
{
    public struct Vertex
    {
        public Vector2 Position;
        public uint Color;
    }

    class Program
    {
        private const int NUM_POINTS = 100;
        private const int TEXTURE_SIZE = 128;
        private const float MAX_POINT_SIZE = 128.0f;
        private const float MIN_POINT_SIZE = 12.0f;

        static void Main(string[] args)
        {
            VertexBuffer vertexBuffer = null;
            IndexBuffer indexBuffer = null;
            Material material = null;
            MaterialInstance materialInstance = null;
            Texture splatTexture = null;
            int renderable = -1;
            int cameraEntity = -1;
            Camera camera = null;
            Skybox skybox = null;

            var app = new Application(
                new WindowConfig() {
                    Title = "point_sprites",
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                splatTexture = CreateSplatTexture(engine);

                Vertex[] kVertices = new Vertex[NUM_POINTS];
                float[] kPointSizes = new float[NUM_POINTS];
                ushort[] kIndices = new ushort[NUM_POINTS];

                var dtheta = MathHelper.Pi * 2 / NUM_POINTS;
                var dsize = MAX_POINT_SIZE / NUM_POINTS;
                var dcolor = 256.0f / NUM_POINTS;

                byte[] vbo = new byte[NUM_POINTS * (8 + 4)];
                MemoryStream vboStream = new MemoryStream(vbo);
                BinaryWriter vboWriter = new BinaryWriter(vboStream);

                for (int i = 0; i < NUM_POINTS; i++) {
                    float theta = dtheta * i;
                    uint c = (uint) (dcolor * i);
                    kVertices[i].Position.X = (float) MathHelper.Cos(theta);
                    kVertices[i].Position.Y = (float) MathHelper.Sin(theta);
                    kVertices[i].Color = 0xff000000u | c | (c << 8) | (c << 16);
                    kPointSizes[i] = MIN_POINT_SIZE + dsize * i;
                    kIndices[i] = (ushort) i;

                    vboWriter.Write(kVertices[i].Position.X);
                    vboWriter.Write(kVertices[i].Position.Y);
                    vboWriter.Write(kVertices[i].Color);
                }

                vertexBuffer = VertexBufferBuilder.Create()
                    .WithVertexCount(NUM_POINTS)
                    .WithBufferCount(2)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float2, 0, Marshal.SizeOf<Vertex>())
                    .WithAttribute(VertexAttribute.Color, 0, ElementType.UByte4, Marshal.SizeOf<Vector2>(), Marshal.SizeOf<Vertex>())
                    .WithNormalized(VertexAttribute.Color)
                    .WithAttribute(VertexAttribute.Custom0, 1, ElementType.Float, 0, Marshal.SizeOf<float>())
                    .Build(engine);
                vertexBuffer.SetBufferAt(engine, 0, vbo);
                vertexBuffer.SetBufferAt(engine, 1, kPointSizes);

                indexBuffer = IndexBufferBuilder.Create()
                    .WithIndexCount(NUM_POINTS)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                indexBuffer.SetBuffer(engine, kIndices);

                var sampleData = new SampleDataLoader();

                material = MaterialBuilder.Create()
                    .WithPackage(sampleData.LoadPointSprites())
                    .Build(engine);

                renderable = EntityManager.Create();

                materialInstance = material.CreateInstance();
                materialInstance.SetParameter("fade", splatTexture, new TextureSampler(SamplerMinFilter.Linear, SamplerMagFilter.Linear));

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithMaterial(0, materialInstance)
                    .WithGeometry(0, PrimitiveType.Points, vertexBuffer, indexBuffer, 0, NUM_POINTS)
                    .WithCulling(false)
                    .WithReceiveShadows(false)
                    .WithCastShadows(false)
                    .Build(engine, renderable);

                scene.AddEntity(renderable);
                cameraEntity = EntityManager.Create();
                camera = engine.CreateCamera(cameraEntity);

                view.Camera = camera;

                skybox = SkyboxBuilder.Create()
                    .WithColor(new Color(0.1f, 0.125f, 0.25f, 1.0f))
                    .Build(engine);
                scene.Skybox = skybox;
            };

            app.Animate = (engine, view, now) => {
                var zoom = 1.5f;
                var width = view.Viewport.Width;
                var height = view.Viewport.Height;
                var aspect = (float) width / height;

                camera.SetProjection(Projection.Ortho, -aspect * zoom, aspect * zoom, -zoom, zoom, 0, 1);

                var tcm = engine.TransformManager;
                tcm.SetTransform(tcm.GetInstance(renderable), Matrix4.CreateFromAxisAngle(Vector3.UnitZ, now));
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(skybox);
                engine.Destroy(renderable);
                engine.Destroy(materialInstance);
                engine.Destroy(material);
                engine.Destroy(vertexBuffer);
                engine.Destroy(indexBuffer);

                engine.DestroyCameraComponent(cameraEntity);
                EntityManager.Destroy(cameraEntity);
            };

            app.Run();
        }

        private static Texture CreateSplatTexture(Engine engine)
        {
            // To generate a Gaussian splat, create a single-channel 3x3 texture with a bright pixel in
            // its center, then magnify it using a Gaussian filter kernel.
            var splat = new LinearImage(3, 3, 1);
            splat.SetPixelData(1, 1, 0.25f);
            splat = ImageSampler.ResampleImage(splat, TEXTURE_SIZE, TEXTURE_SIZE, ImageSamplerFilter.GaussianScalars);

            var buffer = new PixelBufferDescriptor(splat, PixelDataFormat.R, PixelDataType.Float);

            var texture = TextureBuilder.Create()
                .WithWidth(TEXTURE_SIZE)
                .WithHeight(TEXTURE_SIZE)
                .WithLevels(1)
                .WithSampler(TextureSamplerType.Texture2d)
                .WithFormat(TextureFormat.R32F)
                .Build(engine);
            texture.SetImage(engine, 0, buffer);

            return texture;
        }
    }
}
