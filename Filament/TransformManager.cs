using System;
using OpenTK.Mathematics;

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

        public Matrix4 GetWorldTransform(int instance)
        {
            float[] data = new float[4 * 4];

            Native.TransformManager.GetWorldTransform(NativePtr, instance, data);

            return new Matrix4(
                new Vector4(data[0], data[1], data[2], data[3]),
                new Vector4(data[4], data[5], data[6], data[7]),
                new Vector4(data[8], data[9], data[10], data[11]),
                new Vector4(data[12], data[13], data[14], data[15])
            );
        }

        public void SetTransform(int instance, Matrix4 localTransform)
        {
            Native.TransformManager.SetTransform(NativePtr, instance, new[] {
                localTransform.M11, localTransform.M12, localTransform.M13, localTransform.M14,
                localTransform.M21, localTransform.M22, localTransform.M23, localTransform.M24,
                localTransform.M31, localTransform.M32, localTransform.M33, localTransform.M34,
                localTransform.M41, localTransform.M42, localTransform.M43, localTransform.M44,
            });
        }

        #endregion
    }
}
