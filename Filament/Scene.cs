using System;

namespace Filament
{
    public class Scene : FilamentBase<Scene>
    {
        #region Properties

        public int RenderableCount {
            get {
                ThrowExceptionIfDisposed();

                return Native.Scene.GetRenderableCount(NativePtr);
            }
        }

        public int LightCount {
            get {
                ThrowExceptionIfDisposed();

                return Native.Scene.GetLightCount(NativePtr);
            }
        }

        public Skybox Skybox {
            set {
                ThrowExceptionIfDisposed();

                Native.Scene.SetSkybox(NativePtr, value.NativePtr);
            }
        }

        public IndirectLight IndirectLight {
            set {
                ThrowExceptionIfDisposed();

                Native.Scene.SetIndirectLight(NativePtr, value.NativePtr);
            }
        }

        #endregion

        #region Methods

        private Scene(IntPtr ptr) : base(ptr)
        {
        }

        internal static Scene GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Scene(newPtr));
        }

        public void AddEntity(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Scene.AddEntity(NativePtr, entity);
        }

        public void RemoveEntity(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Scene.RemoveEntity(NativePtr, entity);
        }

        #endregion
    }
}
