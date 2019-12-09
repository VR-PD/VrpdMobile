using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlueNetScanner
{
    public static class Serializer
    {
        /// <summary>
        /// Serialize bytes back to their type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj;
                try
                {
                    obj = bf.Deserialize(ms);
                }
                catch (System.Exception)
                {
                    obj = null;
                }
                return (T)obj;
            }
        }

        /// <summary>
        /// Serialize any object type to a byte array
        /// </summary>
        public static byte[] ToByteArray<T>(T t)
        {
            if (t == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, t);
                return ms.ToArray();
            }
        }
    }
}
