using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{

    //This is our ObjectPool Object pulled from UnityEngine.Pool
    IObjectPool<GameObject> objectPool;

    //this is the object that we will pool.

    [Tooltip("Put your pooled object here")]
    [SerializeField] GameObject objectToPool;
    
    //How many objects are we going to allow our pool to have?
    [SerializeField] int maxObjectPoolSize = 50;
    
    //This is just for our prewarm or initialization. This list is useless after
    //sorry list.
    List<GameObject> initialObjects;

    void OnEnable()
    {
        if (objectPool != null) return;
        objectPool = new ObjectPool<GameObject>(CreatePooledObject, OnTakeFromPool, 
            OnReturnedToPool, OnDestroyPoolObject, true, 10, maxObjectPoolSize);
        initialObjects = new List<GameObject>();
        InitObjectPool();

    }
    
    /// <summary>
    /// InitObjectPool() Creates the the pool OnEnable if it isn't already
    /// instantiated. Once this is done, it will release all objects back
    /// to the pool so that they can be 
    /// accessed by our other systems. 
    /// The maxObjectPoolSize field in this class will decide how
    /// large the pool is.
    /// </summary>

    public void InitObjectPool()
    {
        for(int i = 0; i<60; i++)
        {
            objectPool.Get();
            Debug.Log("Getting weapons!!!");
        }

        foreach(GameObject go in initialObjects)
        {
            objectPool.Release(go);
        }
        
       
    }

    /// <summary>
    ///This is called only when the pool already contains 
    ///the max number of objects, if it does, then the object 
    ///is destroyed instead of being released to the pool.
    /// </summary>    
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    /// <summary>
    ///     This sends objects back to the pool, 
    ///     currently the object is simply deactivated
    /// </summary>

    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

/// <summary>
/// This defines what happens to the GameObject that
/// we take from our pool. 
/// </summary>
/// <param name="obj">The object we are taking from the pool</param>
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

/// <summary>
/// If we call Pool.Get() and there is no object available
/// an object is created using this function.
/// </summary>
/// <returns>This function returns our created GameObject</returns>
    public GameObject CreatePooledObject()
    {
        var pooledObject = Instantiate<GameObject>(objectToPool);
        pooledObject.transform.parent = transform;
        initialObjects.Add(pooledObject);
        return pooledObject;

    }
}
