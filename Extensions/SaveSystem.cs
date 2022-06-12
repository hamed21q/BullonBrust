using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save<T>(T data, string name)
    {
        SetupDirectories();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetRoot() + name;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, SerializeObject(data));
        stream.Close();
    }
    public static T Load<T>(string name)
    {
        SetupDirectories();
        try
        {
            string path = GetRoot() + name;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T d = DeserializeObject<T>((byte[])formatter.Deserialize(stream));
                stream.Close();
                Debug.Log(path);
                return d;
            }
            else
            {
                return default(T);
            }
        }
        catch (Exception)
        {
            return default(T);
        }
    }
    public static string DeleteFile(string path)
    {
        if (File.Exists(GetRoot() + path))
        {
            try
            {
                File.Delete(GetRoot() + path);
                return "File deleted";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        return "File doesn't exist";
    }
    public static bool Exists(string name)
    {
        SetupDirectories();
        string path = GetRoot() + name;
        return File.Exists(path);
    }
    public static string GetRoot()
    {
        return Application.persistentDataPath + "/";
    }

    static void SetupDirectories()
    {
        Directory.CreateDirectory(GetRoot() + "projects");
        Directory.CreateDirectory(GetRoot() + "images");
    }

    public static T[] GetAllFiles<T>(string dir)
    {
        string[] temp = new string[0];
        return GetAllFiles<T>(dir, out temp);
    }
    public static T[] GetAllFiles<T>(string dir, out string[] dirs)
    {
        SetupDirectories();
        string path = GetRoot() + dir;
        DirectoryInfo d = new DirectoryInfo(path);
        FileInfo[] fs = d.GetFiles();
        List<T> pr = new List<T>();
        dirs = new string[fs.Length];
        for (int i = 0; i < fs.Length; i++)
        {
            T b = Load<T>(dir + "/" + fs[i].Name);
            dirs[i] = fs[i].Name;
            if (b != null)
                pr.Add(b);
        }

        return pr.ToArray();
    }

    public static void SaveTexture2D(Texture2D texture, string filename)
    {
        File.WriteAllBytes(GetRoot() + "images/" + filename + ".png", texture.SaveTexture2D());
    }
    public static byte[] SaveTexture2D(this Texture2D texture)
    {
        return texture.EncodeToPNG();
    }
    public static Texture2D LoadTexture2D(string filename)
    {
        byte[] bytes = File.ReadAllBytes(GetRoot() + "images/" + filename + ".png");
        return bytes.LoadTexture2D();
    }
    public static Texture2D LoadTexture2D(this byte[] bytes)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        return texture;
    }

    public static byte[] SerializeObject<T>(T serializableObject)
    {
        T obj = serializableObject;

        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
    }

    public static T DeserializeObject<T>(byte[] serilizedBytes)
    {
        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(serilizedBytes))
        {
            return (T)formatter.Deserialize(stream);
        }
    }
    public static void PrintBytes(byte[] b)
    {
        string ree = "";
        foreach (var item in b)
        {
            ree += item;
        }
        Debug.Log(ree);
    }
}
