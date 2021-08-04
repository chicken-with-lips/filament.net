using System;
using System.IO;
using System.Numerics;
using Filament;
using Filament.DemoApp;
using Filament.MeshIO;
using Filament.SampleData;

namespace RenderTarget
{
    struct Vertex
    {
        public Vector3 Position;
        public Vector2 Uv;
    }

    public enum ReflectionMode
    {
        Renderables,
        Camera,
    }

    class Program
    {
        static void Main(string[] args)
        {
            Texture offscreenColorTexture = null;
            Texture offscreenDepthTexture = null;
            Filament.RenderTarget offscreenRenderTarget = null;
            View offscreenView = null;
            Scene offscreenScene = null;
            Camera offscreenCamera = null;

            int lightEntity = -1;
            Material meshMaterial = null;
            MaterialInstance meshMatInstance = null;
            Mesh monkeyMesh = null;
            int reflectedMonkey = -1;
            Matrix4x4 transform = Matrix4x4.Identity;

            int quadEntity = -1;
            Vector3 quadCenter = Vector3.Zero;
            Vector3 quadNormal = Vector3.Zero;
            VertexBuffer quadVb = null;
            IndexBuffer quadIb = null;
            Material quadMaterial = null;
            MaterialInstance quadMatInstance = null;
            ReflectionMode mode = ReflectionMode.Camera;

            var app = new Application(
                new WindowConfig() {
                    Title = "rendertarget",
                },
                new ApplicationConfig()
            );

            app.Setup = (engine, view, scene) => {
                var tcm = engine.TransformManager;
                var rcm = engine.RenderableManager;
                var vp = view.Viewport;

                var resourceData = new SampleDataLoader();
                var monkeyData = new MonkeyDataLoader();

                // Instantiate offscreen render target.
                offscreenScene = engine.CreateScene();

                offscreenView = engine.CreateView();
                offscreenView.Scene = offscreenScene;
                offscreenView.PostProcessingEnabled = false;

                offscreenColorTexture = TextureBuilder.Create()
                    .WithWidth(vp.Width)
                    .WithHeight(vp.Height)
                    .WithLevels(1)
                    .WithUsage(TextureUsage.ColorAttachment | TextureUsage.Sampleable)
                    .WithFormat(TextureFormat.Rgba8)
                    .Build(engine);

                offscreenDepthTexture = TextureBuilder.Create()
                    .WithWidth(vp.Width)
                    .WithHeight(vp.Height)
                    .WithLevels(1)
                    .WithUsage(TextureUsage.DepthAttachment)
                    .WithFormat(TextureFormat.Depth24)
                    .Build(engine);

                offscreenRenderTarget = RenderTargetBuilder.Create()
                    .WithTexture(AttachmentPoint.Color, offscreenColorTexture)
                    .WithTexture(AttachmentPoint.Depth, offscreenDepthTexture)
                    .Build(engine);

                offscreenView.RenderTarget = offscreenRenderTarget;
                offscreenView.Viewport = new Viewport(0, 0, vp.Width, vp.Height);

                offscreenCamera = engine.CreateCamera(EntityManager.Create());
                offscreenView.Camera = offscreenCamera;

                app.AddOffscreenView(offscreenView);

                // Position and orient the mirror in an interesting way.
                var c = quadCenter = new Vector3(-2, 0, -5);
                var n = quadNormal = Vector3.Normalize(new Vector3(1, 0, 2));
                var u = Vector3.Normalize(Vector3.Cross(quadNormal, new Vector3(0, 1, 0)));
                var v = Vector3.Cross(n, u);
                u = 1.5f * u;
                v = 1.5f * v;

                Vertex[] kQuadVertices = {
                    new() {Position = c - u - v, Uv = new Vector2(1, 0)},
                    new() {Position = c + u - v, Uv = new Vector2(0, 0)},
                    new() {Position = c - u + v, Uv = new Vector2(1, 1)},
                    new() {Position = c + u + v, Uv = new Vector2(0, 1)},
                };

                var vbo = new byte[20 *  4];

                MemoryStream vboStream = new MemoryStream(vbo);
                BinaryWriter vboWriter = new BinaryWriter(vboStream);
                vboWriter.Write(kQuadVertices[0].Position.X);
                vboWriter.Write(kQuadVertices[0].Position.Y);
                vboWriter.Write(kQuadVertices[0].Position.Z);
                vboWriter.Write(kQuadVertices[0].Uv.X);
                vboWriter.Write(kQuadVertices[0].Uv.Y);
                vboWriter.Write(kQuadVertices[1].Position.X);
                vboWriter.Write(kQuadVertices[1].Position.Y);
                vboWriter.Write(kQuadVertices[1].Position.Z);
                vboWriter.Write(kQuadVertices[1].Uv.X);
                vboWriter.Write(kQuadVertices[1].Uv.Y);
                vboWriter.Write(kQuadVertices[2].Position.X);
                vboWriter.Write(kQuadVertices[2].Position.Y);
                vboWriter.Write(kQuadVertices[2].Position.Z);
                vboWriter.Write(kQuadVertices[2].Uv.X);
                vboWriter.Write(kQuadVertices[2].Uv.Y);
                vboWriter.Write(kQuadVertices[3].Position.X);
                vboWriter.Write(kQuadVertices[3].Position.Y);
                vboWriter.Write(kQuadVertices[3].Position.Z);
                vboWriter.Write(kQuadVertices[3].Uv.X);
                vboWriter.Write(kQuadVertices[3].Uv.Y);

                // Create quad vertex buffer.
                quadVb = VertexBufferBuilder.Create()
                    .WithVertexCount(4)
                    .WithBufferCount(1)
                    .WithAttribute(VertexAttribute.Position, 0, ElementType.Float3, 0, 20)
                    .WithAttribute(VertexAttribute.Uv0, 0, ElementType.Float2, 12, 20)
                    .Build(engine);
                quadVb.SetBufferAt(engine, 0, vbo);

                // Create quad index buffer.
                var kQuadIndices = new ushort[] {0, 1, 2, 3, 2, 1};

                quadIb = IndexBufferBuilder.Create()
                    .WithIndexCount(6)
                    .WithBufferType(IndexType.UShort)
                    .Build(engine);
                quadIb.SetBuffer(engine, kQuadIndices);

                // Create quad material and renderable.
                quadMaterial = MaterialBuilder.Create()
                    .WithPackage(resourceData.LoadMirror())
                    .Build(engine);
                quadMatInstance = quadMaterial.CreateInstance();

                var sampler = new TextureSampler(SamplerMinFilter.Linear, SamplerMagFilter.Linear);

                quadMatInstance.SetParameter("albedo", offscreenColorTexture, sampler);
                quadEntity = EntityManager.Create();

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-1, -1, -1),
                            new Vector3(1, 1, 1)
                        )
                    )
                    .WithMaterial(0, quadMatInstance)
                    .WithGeometry(0, PrimitiveType.Triangles, quadVb, quadIb, 0, 6)
                    .WithCulling(false)
                    .WithReceiveShadows(false)
                    .WithCastShadows(false)
                    .Build(engine, quadEntity);

                scene.AddEntity(quadEntity);

                // Instantiate mesh material.
                meshMaterial = MaterialBuilder.Create()
                    .WithPackage(resourceData.LoadAiDefaultMat())
                    .Build(engine);

                var mi = meshMatInstance = meshMaterial.CreateInstance();
                mi.SetParameter("baseColor", RgbType.Linear, new Color(0.8f, 1.0f, 1.0f));
                mi.SetParameter("metallic", 0.0f);
                mi.SetParameter("roughness", 0.4f);
                mi.SetParameter("reflectance", 0.5f);

                // Add monkey into the scene.
                monkeyMesh = MeshReader.LoadFromBuffer(engine, monkeyData.LoadSuzanne(), mi);

                var ti = tcm.GetInstance(monkeyMesh.Renderable);

                transform = Matrix4x4.CreateTranslation(0, 0, -4) * tcm.GetWorldTransform(ti);
                rcm.SetCastShadows(rcm.GetInstance(monkeyMesh.Renderable), false);
                scene.AddEntity(monkeyMesh.Renderable);

                // Create a reflected monkey, which is used only for ReflectionMode::RENDERABLES.
                reflectedMonkey = EntityManager.Create();

                RenderableBuilder.Create()
                    .WithBoundingBox(
                        new Box(
                            new Vector3(-2, -2, -2),
                            new Vector3(2, 2, 2)
                        )
                    )
                    .WithMaterial(0, mi)
                    .WithGeometry(0, PrimitiveType.Triangles, monkeyMesh.VertexBuffer, monkeyMesh.IndexBuffer)
                    .WithReceiveShadows(true)
                    .WithCastShadows(false)
                    .Build(engine, reflectedMonkey);
                mode = SetReflectionMode(offscreenScene, offscreenView, reflectedMonkey, monkeyMesh, ReflectionMode.Camera);

                // Add light source to both scenes.
                // NOTE: this is slightly wrong when the reflection mode is RENDERABLES.
                lightEntity = EntityManager.Create();

                LightBuilder.Create(LightType.Sun)
                    .WithColor(Color.ToLinearAccurate(new sRGBColor(0.98f, 0.92f, 0.89f)))
                    .WithIntensity(110000)
                    .WithDirection(new Vector3(0.7f, -1, -0.8f))
                    .WithSunAngularRadius(1.9f)
                    .WithCastShadows(false)
                    .Build(engine, lightEntity);

                scene.AddEntity(lightEntity);
                offscreenScene.AddEntity(lightEntity);
            };

            app.Cleanup = (engine, view, scene) => {
                var camera = offscreenCamera.Entity;
                engine.DestroyCameraComponent(camera);
                EntityManager.Destroy(camera);

                engine.Destroy(reflectedMonkey);
                engine.Destroy(lightEntity);
                engine.Destroy(quadEntity);
                engine.Destroy(meshMatInstance);
                engine.Destroy(meshMaterial);
                engine.Destroy(monkeyMesh.Renderable);
                engine.Destroy(monkeyMesh.VertexBuffer);
                engine.Destroy(monkeyMesh.IndexBuffer);
                engine.Destroy(offscreenColorTexture);
                engine.Destroy(offscreenDepthTexture);
                engine.Destroy(offscreenRenderTarget);
                engine.Destroy(offscreenScene);
                engine.Destroy(offscreenView);
                engine.Destroy(quadVb);
                engine.Destroy(quadIb);
                engine.Destroy(quadMatInstance);
                engine.Destroy(quadMaterial);
            };

            app.PreRender = (engine, view, scene, renderer) => {
                renderer.SetClearOptions(new Vector4(0.1f, 0.2f, 0.4f, 1.0f), true);
            };

            app.Animate = (engine, view, now) => {
                var tcm = engine.TransformManager;

                // Animate the monkey by spinning and sliding back and forth along Z.
                var ti = tcm.GetInstance(monkeyMesh.Renderable);
                var xlate = Matrix4x4.CreateTranslation(new Vector3(0, 0, 0.5f + MathF.Sin(now)));
                var xform = Matrix4x4.CreateRotationY(now) * xlate * transform;
                tcm.SetTransform(ti, xform);

                // Generate a reflection matrix from the plane equation Ax + By + Cz + D = 0.
                var planeNormal = quadNormal;
                var planeEquation = new Vector4(planeNormal, -Vector3.Dot(planeNormal, quadCenter));
                var reflection = BuildReflectionMatrix(planeEquation);

                var camera = view.Camera;
                var model = camera.ModelMatrix;

                offscreenCamera.SetCustomProjection(camera.ProjectionMatrix, camera.CullingProjectionMatrix, camera.Near, camera.CullingFar);

                // Apply the reflection matrix to either the renderable or the camera, depending on mode.
                switch (mode) {
                    case ReflectionMode.Renderables:
                        tcm.SetTransform(tcm.GetInstance(reflectedMonkey), reflection * xform);
                        offscreenCamera.ModelMatrix = model;
                        break;

                    case ReflectionMode.Camera:
                        offscreenCamera.ModelMatrix = model * reflection;
                        break;
                }
            };

            app.Run();
        }

        private static ReflectionMode SetReflectionMode(Scene offscreenScene, View offscreenView, int reflectedMonkey, Mesh monkeyMesh, ReflectionMode mode)
        {
            switch (mode) {
                case ReflectionMode.Renderables:
                    offscreenScene.AddEntity(reflectedMonkey);
                    offscreenScene.AddEntity(reflectedMonkey);
                    offscreenScene.RemoveEntity(monkeyMesh.Renderable);
                    offscreenView.IsFrontFaceWindingInverted = false;
                    break;
                case ReflectionMode.Camera:
                    offscreenScene.AddEntity(monkeyMesh.Renderable);
                    offscreenScene.RemoveEntity(reflectedMonkey);
                    offscreenView.IsFrontFaceWindingInverted = true;
                    break;
            }

            return mode;
        }

        private static Matrix4x4 BuildReflectionMatrix(Vector4 plane)
        {
            var m = new Matrix4x4(
                -2f * plane.X * plane.X + 1f,
                -2f * plane.X * plane.Y,
                -2f * plane.X * plane.Z,
                -2f * plane.X * plane.W,
                -2f * plane.X * plane.Y,
                -2f * plane.Y * plane.Y + 1f,
                -2f * plane.Y * plane.Z,
                -2f * plane.Y * plane.W,
                -2f * plane.Z * plane.X,
                -2f * plane.Z * plane.Y,
                -2f * plane.Z * plane.Z + 1f,
                -2f * plane.Z * plane.W,
                0f, 0f, 0f, 1f
            );

            return Matrix4x4.Transpose(m);
        }
    }
}
