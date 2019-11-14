using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VrpdScanner
{
    public static class Serializer
    {
        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

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
