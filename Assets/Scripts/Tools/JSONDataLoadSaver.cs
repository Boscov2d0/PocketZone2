using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public static class JSONDataLoadSaver<T>
{
    public static T Load(string path)
    {
        string fullPath;
        T result;
        string tempJSON = "";

#if UNITY_ANDROID && !UNITY_EDITOR
            fullPath = Application.persistentDataPath + path;

            if (!Directory.Exists(fullPath))
            {
                FileInfo file = new FileInfo(fullPath);
                file.Directory.Create();
            }

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
                string fileJSON = JsonUtility.ToJson("7537");
                File.WriteAllText(fullPath, fileJSON);
            }

            UnityWebRequest reader = new(fullPath);
            reader = UnityWebRequest.Get(fullPath);
            reader.SendWebRequest();
            tempJSON = reader.downloadHandler.text;
#else
        fullPath = Application.streamingAssetsPath + path;
#endif
        if (string.IsNullOrEmpty(tempJSON))
            tempJSON = File.ReadAllText(fullPath);

        result = JsonUtility.FromJson<T>(tempJSON);

        return result;
    }
    public static void SaveData(T data, string path)
    {
        string fullPath;

#if UNITY_ANDROID && !UNITY_EDITOR
            if (Directory.Exists(Application.persistentDataPath))
                fullPath = Application.persistentDataPath + path;
            else
                fullPath = Application.streamingAssetsPath + path;
#else
        fullPath = Application.streamingAssetsPath + path;
#endif
        string fileJSON = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath, fileJSON);
    }
}