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

        public int GetInstance(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.TransformManager.GetInstance(NativePtr, entity);
        }

        public Matrix4x4 GetWorldTransform(int instance)
        {
            float[] data = new float[4 * 4];

            Native.TransformManager.GetWorldTransform(NativePtr, instance, data);

            return new Matrix4x4(
                data[0], data[1], data[2], data[3],
                data[4], data[5], data[6], data[7],
                data[8], data[9], data[10], data[11],
                data[12], data[13], data[14], data[15]
            );
        }

        public void SetTransform(int instance, Matrix4x4 localTransform)
        {
            Native.TransformManager.SetTransform(NativePtr, instance, new[] {
                localTransform.M11, localTransform.M12, localTransform.M13, localTransform.M14,
                localTransform.M21, localTransform.M22, localTransform.M23, localTransform.M24,
                localTransform.M31, localTransform.M32, localTransform.M33, localTransform.M34,
                localTransform.M41, localTransform.M42, localTransform.M43, localTransform.M44,
            });
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
            Native.TransformManager.CommitLocalTransformTransaction(NativePtr);
        }

        #endregion
    }
}
