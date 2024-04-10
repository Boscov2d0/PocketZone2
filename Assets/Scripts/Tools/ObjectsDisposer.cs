using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDisposer : IDisposable
{
    private List<IDisposable> _disposableObjects;
    private List<GameObject> _gameObjects;
    private bool _isDisposed;

    public void Dispose()
    {
        if (_isDisposed)
            return;

        DisposeGameObjects();
        DisposeDisposableObjects();

        OnDispose();

        _isDisposed = true;

        GC.SuppressFinalize(this);
    }
    private void DisposeDisposableObjects()
    {
        if (_disposableObjects == null)
            return;

        foreach (IDisposable disposableObject in _disposableObjects)
        {
            disposableObject.Dispose();
        }

        _disposableObjects.Clear();
    }
    private void DisposeGameObjects()
    {
        if (_gameObjects == null)
            return;

        foreach (GameObject gameObject in _gameObjects)
            UnityEngine.Object.Destroy(gameObject);

        _gameObjects.Clear();
    }
    protected virtual void OnDispose() { }

    protected void AddController(ObjectsDisposer objectsDisposer) =>
        AddDisposableObject(objectsDisposer);
    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects ??= new List<GameObject>();
        _gameObjects.Add(gameObject);
    }
    private void AddDisposableObject(IDisposable disposable)
    {
        _disposableObjects ??= new List<IDisposable>();
        _disposableObjects.Add(disposable);
    }
}