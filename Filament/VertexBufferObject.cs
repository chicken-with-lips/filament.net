using System.IO;
using System.Numerics;

namespace Filament
{
    public class VertexBufferObject
    {
        #region Properties

        public long Length => _stream.Length;

        #endregion

        #region Members

        private readonly MemoryStream _stream;
        private readonly BinaryWriter _writer;

        #endregion

        #region Constructor

        public VertexBufferObject()
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);
        }

        public VertexBufferObject(int capacity)
        {
            _stream = new MemoryStream(capacity);
            _writer = new BinaryWriter(_stream);
        }

        public void Write(Vector2 v)
        {
            _writer.Write(v.X);
            _writer.Write(v.Y);
        }

        public void Write(Vector3 v)
        {
            _writer.Write(v.X);
            _writer.Write(v.Y);
            _writer.Write(v.Z);
        }

        public void Write(Vector4 v)
        {
            _writer.Write(v.X);
            _writer.Write(v.Y);
            _writer.Write(v.Z);
            _writer.Write(v.W);
        }

        public void Write(int v)
        {
            _writer.Write(v);
        }

        public void Write(uint v)
        {
            _writer.Write(v);
        }

        public void Write(float v)
        {
            _writer.Write(v);
        }

        public void Write(double v)
        {
            _writer.Write(v);
        }

        public void Write(byte v)
        {
            _writer.Write(v);
        }

        public void Write(Color v)
        {
            _writer.Write(v.R);
            _writer.Write(v.G);
            _writer.Write(v.B);
            _writer.Write(v.A);
        }

        public byte[] ToArray()
        {
            return _stream.GetBuffer();
        }

        #endregion
    }
}
