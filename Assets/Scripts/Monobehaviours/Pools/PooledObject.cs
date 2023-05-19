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
    
    //this will create the pool in OnEnable if the pool is not already 
    //instantiated
    //This uses the maxObjectPoolSize int to define number of objects
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

    /*This is called only when the pool already contains 
     * the max number of objects, if it does, then the object 
     * is destroyed instead of being released to the pool.
     */

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    /* This sends objects back to the pool, currently the object
     * is simply deactivated
     */
    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    /*
     * What happens when we take the object from the pool.
     */
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

   /* This is called when the pool tries to GET() an object
    * and there is no object avialable in the pool
    */
    public GameObject CreatePooledObject()
    {
        var pooledObject = Instantiate<GameObject>(objectToPool);
        pooledObject.transform.parent = transform;
        initialObjects.Add(pooledObject);
        return pooledObject;

    }
}
