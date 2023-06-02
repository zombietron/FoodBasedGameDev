using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField]
    float attackSpeed = .1f;

    [SerializeField] List<PooledObject> foodPools;

    [SerializeField]
    Transform spawnPoint;

    public GameObject GetProjectile(string projectile)
    {
        GameObject spawnedFood = null;
        switch(projectile){
            case "hotDog":
                spawnedFood = foodPools[0].objectPool.Get();
                break;
                
            case "taco":
                spawnedFood = foodPools[1].objectPool.Get();
                break;
                
            case "pie":
                spawnedFood = foodPools[2].objectPool.Get();

                break;
               
            case "pizza":
                spawnedFood = foodPools[3].objectPool.Get();
                break;
                
            case "sandwich":
                spawnedFood = foodPools[4].objectPool.Get();
                break;
                
            default: 
                break;

        }

        spawnedFood.transform.position = spawnPoint.position;
        spawnedFood.transform.rotation = spawnPoint.transform.rotation;
        spawnedFood.GetComponent<SetConstantVelocity>().SetObjectVelocity(spawnPoint.transform.forward);
        return spawnedFood;
    }
}
