using UnityEngine;

public static class ResourcesLoader
{
    public static TObject InstantiateAndGetObject<TObject>(string path)
    {
        var _prefab = Resources.Load(path);
        GameObject gameObject = GameObject.Instantiate(_prefab) as GameObject;
        return GetObject<TObject>(gameObject);
    }
    public static TObject InstantiateAndGetObject<TObject>(string path, Transform parentObject)
    {
        var _prefab = Resources.Load(path);
        GameObject gameObject = GameObject.Instantiate(_prefab, parentObject) as GameObject;
        return GetObject<TObject>(gameObject);
    }
    public static void InstantiateObject<TObject>(string path)
    {
        var _prefab = Resources.Load(path);
        GameObject gameObject = GameObject.Instantiate(_prefab) as GameObject;
    }
    private static TObject GetObject<TObject>(GameObject gameObject)
    {
        return gameObject.GetComponent<TObject>();
    }
}