using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectBehaviour : MonoBehaviour
{
    PooledObject myObjectPool;

    SpawnController spawnCtrl;
    public void SetObjectPool(PooledObject pool)
    {
        myObjectPool = pool;
    }

    public void SetSpawnController(SpawnController ctrl)
    {
        spawnCtrl = ctrl;
    }

    public void ReturnToPool()
    {
        if (myObjectPool == null) return;
        if (gameObject.CompareTag("Enemy"))
        {
            spawnCtrl.RemoveDestroyedEnemy(transform);
        }
        myObjectPool.objectPool.Release(gameObject);
    }




}
