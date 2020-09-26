using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using UnityEngine;

public static class DataSaver<T> where T : class, new()
{
    public static T Instance
    {
        get => GetOrCreateInstance();
    }
    private static T instance;
    private static readonly string path = $"{Application.persistentDataPath}/{typeof(T).Name}.json";

    public static void Save()
    {
        File.WriteAllText(path, JsonUtility.ToJson(Instance));
    }


    /// <summary>
    /// Clears the data just in runtime.
    /// Note: To clear file you need to call Save() method
    /// </summary>
    public static void Clear()
    {
        instance = null;
    }

    private static T GetOrCreateInstance()
    {
        if (instance == null)
            instance = LoadOrCreate();
        return instance;
    }

    private static T LoadOrCreate()
    {
        if (File.Exists(path))
        {
            try
            {
                var data = JsonUtility.FromJson<T>(File.ReadAllText(path));
                return data;
            }
            catch
            {
                return new T();
            }
        }

        return new T();
    }
}
