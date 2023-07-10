using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    int hungerReductionAmount = 50;

    PooledObjectBehaviour poolObjectBehavior;

    [SerializeField] PizzaBombProjectiles pizzaParent;
    [SerializeField] AudioSource impactSound;
    private void Start()
    {
        poolObjectBehavior = GetComponent<PooledObjectBehaviour>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        AudioSource.PlayClipAtPoint(impactSound.clip,transform.position);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HP>().ReduceHP(hungerReductionAmount);
        }  


        if (poolObjectBehavior != null)
        {
            if (pizzaParent==null)
            { poolObjectBehavior.ReturnToPool(); }
            else
            {
                Debug.Log("Pizza Slice has collided");
                pizzaParent.DecrementProjectile();
                gameObject.SetActive(false);
            }
        }
    }

}
