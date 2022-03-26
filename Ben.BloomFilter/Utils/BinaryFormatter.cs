/**
*┌──────────────────────────────────────────────────────────────┐
*│　描   述：二进制数据的持久化 ,二进制数据的读写操作                                                
*│　作   者：Ben                                            
*│　版   本：1.0                                                 
*│　创建时间：2022/3/26 15:55:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Ben.BloomFilter.Utils                                   
*│　类   名：BinaryFormatter   
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ben.BloomFilter.Utils
{
    public class BinarySaveWrite
    {
        private static BinaryFormatter Transfer = new BinaryFormatter();


        public static int BinaryFileSav(string path, object obj)
        {
            //-----二进制文件写入并存储   

            Stream flstr = new FileStream(path, FileMode.Create);
            BinaryWriter sw = new BinaryWriter(flstr, Encoding.Unicode);
            byte[] buffer = ChangeObjectToByte(obj);
            sw.Write(buffer);

            sw.Close();
            flstr.Close();

            return 0;
        }
        /// <summary>    
        /// 序列化，存储用   
        /// </summary>    
        /// <param name="msg">要序列化的对象</param>    
        /// <returns>转化成的byte</returns>    
        private static byte[] ChangeObjectToByte(object obj)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                //----将对象序列化    
                Transfer.Serialize(ms, obj);
                byte[] buffer = ms.GetBuffer();
                return buffer;
            }
            catch (Exception err)
            {
                return null;
            }
        }



        public static object BinaryFileOpen(string path)
        {

            object res;
            Stream flstr = new FileStream(path, FileMode.Open);
            BinaryReader sr = new BinaryReader(flstr, Encoding.Unicode);
            byte[] buffer = sr.ReadBytes((int)flstr.Length);
            res = ChangeByteToObject(buffer);

            sr.Close();
            flstr.Close();

            return res;
        }

        /// <summary>    
        /// 反序列化，读取用   
        /// </summary>    
        /// <param name="buffer">二进制流</param>    
        private static object ChangeByteToObject(byte[] buffer)
        {
            try
            {
                MemoryStream ms = new MemoryStream(buffer, 0, buffer.Length, true, true);
                //----将流反序列化为对象    
                object obj = Transfer.Deserialize(ms);
                return obj;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}
