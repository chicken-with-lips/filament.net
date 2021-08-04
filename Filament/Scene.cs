using System;

namespace Filament
{
    /// <summary>
    /// A Scene is a flat container of Renderable and Light instances.
    /// </sumamary>
    /// <remarks>
    /// <para> A Scene doesn't provide a hierarchy of Renderable objects, i.e.: it's not a scene-graph. However, it
    /// manages the list of objects to render and the list of lights. Renderable and Light objects can be added or
    /// removed from a Scene at any time.</para>
    /// <para>A Renderable *must* be added to a Scene in order to be rendered, and the Scene must be provided to a View.</para>*
    /// </remarks>
    public class Scene : FilamentBase<Scene>
    {
        #region Properties

        /// <summary>
        /// Returns the number of Renderable objects in the Scene.
        /// </summary>
        public int RenderableCount {
            get {
                ThrowExceptionIfDisposed();

                return Native.Scene.GetRenderableCount(NativePtr);
            }
        }

        /// <summary>
        /// Returns the total number of Light objects in the Scene.
        /// </summary>
        public int LightCount {
            get {
                ThrowExceptionIfDisposed();

                return Native.Scene.GetLightCount(NativePtr);
            }
        }

        /// <summary>
        /// Gets or sets the Skybox.
        /// </summary>
        public Skybox Skybox {
            set {
                ThrowExceptionIfDisposed();

                Native.Scene.SetSkybox(NativePtr, value.NativePtr);
            }
        }

        /// <summary>
        /// <para>The IndirectLight to use when rendering the Scene.</para>
        /// <para>Currently, a Scene may only have a single IndirectLight. This call replaces the current IndirectLight.</para>
        /// </summary>
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

        /// <summary>
        /// Adds an Entity to the Scene.
        /// </summary>
        /// <param name="entity">The entity is ignored if it doesn't have a Renderable or Light component.</param>
        public void AddEntity(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Scene.AddEntity(NativePtr, entity);
        }

        /// <summary>
        /// Removes the Renderable from the Scene.
        /// </summary>
        /// <param name="entity">The Entity to remove from the Scene. If the specified entity doesn't exist, this call
        /// is ignored.</param>
        public void RemoveEntity(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Scene.RemoveEntity(NativePtr, entity);
        }

        #endregion
    }
}
