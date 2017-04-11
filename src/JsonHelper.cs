#define PRETTY_FORMATER

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace SHGame
{
    static class Debug
    { 
        public static void LogError(string msg)
        {
        
        }
    }

    public static class JsonHelper
    {
        static public T loadObjectFromJsonFile<T>(string path)
        {
//             try
//             {
                TextReader reader = new StreamReader(path);
                if (reader == null)
                {
                    Debug.LogError("Cannot find " + path);
                    reader.Close();
                    return default(T);
                }

                T data = ToObject<T>(reader.ReadToEnd());
                if (data == null)
                {
                    Debug.LogError("Cannot read data from " + path);
                }

                reader.Close();

                return data;
//             }
//             catch (System.Exception e)
//             {
//                 return default(T);
//             }

        }

        static public void saveObjectToJsonFile(object data, string path)
        {
#if PRETTY_FORMATER
            string jsonStr = JsonFormatter.PrettyPrint(ToJson(data));
#else
            string jsonStr = JsonMapper.ToJson(data);
#endif
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(path, jsonStr);
        }

        public static string ToPrettyJson(object obj)
        {
            return JsonFormatter.PrettyPrint(ToJson(obj));
        }

        public static string ToJson(object obj)
        {
            return JsonMapper.ToJson(obj);
        }
        public static T ToObject<T>(string json)
        {
            return JsonMapper.ToObject<T>(json);
        }
        public static object CommonToObject(Type t, string json)
        {
            return JsonMapper.CommonToObject(t, json);
        }

	
	
    }
}