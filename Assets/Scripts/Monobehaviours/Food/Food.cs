using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    int hungerReductionAmount = 50;

    PooledObjectBehaviour poolObjectBehavior;
    private void Start()
    {
        poolObjectBehavior = GetComponent<PooledObjectBehaviour>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("I collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HP>().ReduceHP(hungerReductionAmount);
        }  

        if (poolObjectBehavior != null)
        {
            poolObjectBehavior.ReturnToPool();
        }
    }
}
