using System;
using System.Numerics;

namespace Filament
{
    public class TransformManager : FilamentBase<TransformManager>
    {
        #region Methods

        private TransformManager(IntPtr ptr) : base(ptr)
        {
        }

        internal static TransformManager GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new TransformManager(newPtr));
        }

        /// <summary>
        /// Returns whether a particular Entity is associated with a component of this TransformManager.
        /// </summary>
        /// <param name="entity">An entity.</param>
        /// <returns>True if this Entity has a component associated with this manager.</returns>
        public bool HasComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.TransformManager.HasComponent(NativePtr, entity);
        }

        /// <summary>
        /// Gets an Instance representing the transform component associated with the given Entity.
        /// </summary>
        /// <param name="entity">An Entity.</param>
        /// <returns>An Instance object, which represents the transform component associated with the entity.</returns>
        public int GetInstance(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.TransformManager.GetInstance(NativePtr, entity);
        }

        /// <summary>
        /// Creates a transform component and associate it with the given entity.
        /// </summary>
        /// <param name="entity">An Entity to associate a transform component to.</param>
        /// <returns>An Instance object, which represents the transform component associated with the entity.</returns>
        public int Create(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.TransformManager.Create(NativePtr, entity);
        }

        /// <summary>
        /// Destroys this component from the given entity, children are orphaned.
        /// </summary>
        /// <remarks>
        /// If this transform had children, these are orphaned, which means their local transform becomes a world
        /// transform. Usually it's nonsensical. It's recommended to make sure that a destroyed transform doesn't have
        /// children.
        /// </remarks>
        /// <param name="entity">An entity.</param>
        public void Destroy(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.TransformManager.Destroy(NativePtr, entity);
        }

        /// <summary>
        /// Return the world transform of a transform component.
        /// </summary>
        /// <param name="instance">The instance of the transform component to query the world transform from.</param>
        /// <returns>
        /// The world transform of the component (i.e. relative to the root). This is the composition of this
        /// component's local transform with its parent's world transform.
        /// </returns>
        public Matrix4x4 GetWorldTransform(int instance)
        {
            ThrowExceptionIfDisposed();

            float[] data = new float[4 * 4];

            Native.TransformManager.GetWorldTransform(NativePtr, instance, data);

            return new Matrix4x4(
                data[0], data[1], data[2], data[3],
                data[4], data[5], data[6], data[7],
                data[8], data[9], data[10], data[11],
                data[12], data[13], data[14], data[15]
            );
        }

        /// <summary>
        /// Sets a local transform of a transform component.
        /// </summary>
        /// <remarks>
        /// This operation can be slow if the hierarchy of transform is too deep, and this will be particularly bad when
        /// updating a lot of transforms. In that case, consider using <see cref="OpenLocalTransformTransaction"/> /
        /// <see cref="CommitLocalTransformTransaction"/>.
        /// </remarks>
        /// <param name="instance">The instance of the transform component to set the local transform to.</param>
        /// <param name="localTransform">The local transform (i.e. relative to the parent).</param>
        public void SetTransform(int instance, Matrix4x4 localTransform)
        {
            ThrowExceptionIfDisposed();

            Native.TransformManager.SetTransform(NativePtr, instance, new[] {
                localTransform.M11, localTransform.M12, localTransform.M13, localTransform.M14,
                localTransform.M21, localTransform.M22, localTransform.M23, localTransform.M24,
                localTransform.M31, localTransform.M32, localTransform.M33, localTransform.M34,
                localTransform.M41, localTransform.M42, localTransform.M43, localTransform.M44,
            });
        }

        /// <summary>
        /// Re-parents an entity to a new one.
        /// </summary>
        /// <remarks>It is an error to re-parent an entity to a descendant and will cause undefined behaviour.</remarks>
        /// <param name="instance">The instance of the transform component to re-parent.</param>
        /// <param name="newParent">The instance of the new parent transform.</param>
        public void SetParent(int instance, int newParent)
        {
            ThrowExceptionIfDisposed();

            Native.TransformManager.SetParent(NativePtr, instance, newParent);
        }

        /// <summary>
        /// Opens a local transform transaction.
        /// </summary>
        /// <remarks>
        /// <para>During a transaction, <see cref="GetWorldTransform"/> can return an invalid transform until
        /// commitLocalTransformTransaction() is called. However, <see cref="SetTransform"/> will perform significantly
        /// better and in constant time.</para>
        /// <para>This is useful when updating many transforms and the transform hierarchy is deep (say more than 4 or 5
        /// levels).</para>
        /// <para><strong>Note:</strong> If the local transform transaction is already open, this is a no-op.</para>
        /// </remarks>
        public void OpenLocalTransformTransaction()
        {
            ThrowExceptionIfDisposed();

            Native.TransformManager.OpenLocalTransformTransaction(NativePtr);
        }

        /// <summary>
        /// Commits the currently open local transform transaction.
        /// </summary>
        /// <remarks>
        /// <para>When this returns, calls to <see cref="GetWorldTransform"/> will return the proper value.</para>
        /// <para><strong>Attention!</strong> Failing to call this method when done updating the local transform will
        /// cause a lot of rendering problems. The system never closes the transaction automatically.</para>
        /// <para><strong>Note:</strong> If the local transform transaction is not open, this is a no-op.</para>
        /// </remarks>
        public void CommitLocalTransformTransaction()
        {
            ThrowExceptionIfDisposed();

            Native.TransformManager.CommitLocalTransformTransaction(NativePtr);
        }

        #endregion
    }
}
