using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    T Pull();
    void Push(T t);
}

public interface IPoolable<T>
{
    void Initialize(Action<T> returnAction);
    void ReturnToPool();
}

public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
{
    private Action<T> pullObject;
    private Action<T> pushObject;
    
    private Stack<T> pooledObjects = new Stack<T>();
    private GameObject prefab;
    public int PooledCount => pooledObjects.Count;
    
    public ObjectPool(GameObject pooledObject, Transform parent, int numToSpawn = 0) // For spawning at start..
    {
        prefab = pooledObject;
        
        Spawn(numToSpawn, parent);

    }

    public ObjectPool(GameObject pooledObject, Action<T> pullObject, Action<T> pushObject, int numToSpawn = 0)
    {
        prefab = pooledObject;
        this.pullObject = pullObject;
        this.pushObject = pushObject;
        Spawn(numToSpawn);
    }

    private void Spawn(int numToSpawn, Transform parent = null)
    {
        T t;
        
        for (int i = 0; i < numToSpawn; i++)
        {
            t = GameObject.Instantiate(prefab).GetComponent<T>();
            t.transform.SetParent(parent);
            pooledObjects.Push(t);
            t.gameObject.SetActive(false);
        }
    }

    #region Pull Methods
    public T Pull()
    {
        T t;
        if (PooledCount > 0)
        {
            t = pooledObjects.Pop();
        }
        else
        {
            t = GameObject.Instantiate(prefab).GetComponent<T>();
        }
        
        t.gameObject.SetActive(true);
        t.Initialize(Push);
        pullObject?.Invoke(t);
        return t;
    }

    public T Pull(Vector3 position)
    {
        T t = Pull();
        t.transform.position = position;
        return t;
    }
    
    public T Pull(Transform parent)
    {
        T t = Pull();
        t.transform.SetParent(parent);
        return t;
    }
    
    
    public T Pull(Vector3 position, Quaternion rotation)
    {
        T t = Pull();
        var transform = t.transform;
        transform.position = position;
        transform.rotation = rotation;
        return t;
    }
    
    public GameObject PullGameObject() => Pull().gameObject;

    public GameObject PullGameObject(Vector3 position)
    {
        GameObject go = Pull().gameObject;
        go.transform.position = position;
        return go;
    }

    public GameObject PullGameObject(Vector3 position, Quaternion rotation)
    {
        GameObject go = Pull().gameObject;
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }

    #endregion

    public void Push(T t)
    {
        pooledObjects.Push(t);
        pushObject?.Invoke(t);
        t.gameObject.SetActive(false);
    }
}

