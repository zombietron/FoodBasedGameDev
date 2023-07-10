using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBomb : MonoBehaviour
{
    PooledObjectBehaviour pool;
    // Start is called before the first frame update
    void OnEnable()
    {
        pool = GetComponent<PooledObjectBehaviour>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if(pool != null)
        {
            var go = pool.GetPizzaBombPool().objectPool.Get(); 
            go.transform.position = transform.position;
        }
    }
}
