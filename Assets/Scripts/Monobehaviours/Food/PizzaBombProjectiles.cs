using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBombProjectiles : MonoBehaviour
{
    [SerializeField] List<Transform> slices;
    bool needsInit = true;

    int activeProjectiles;


    private void OnEnable()
    {
        if (needsInit)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<SetConstantVelocity>() != null)
                {
                    slices.Add(transform.GetChild(i));
                }
            }
            
            activeProjectiles = slices.Count;
            Debug.Log(activeProjectiles + " slices are ready to blow");

            needsInit = false;
        }

        InitProjectileVelocity();
    }

    public void InitProjectileVelocity()
    {
        foreach(Transform t in slices)
        {
            var go = t.gameObject;
            go.GetComponent<SetConstantVelocity>().InitializeVelocity();
        }
    }

    public void DecrementProjectile()
    {
        activeProjectiles--;
        Debug.Log("This many slices left: " + activeProjectiles);
        if(activeProjectiles <=0)
        {
            GetComponent<PooledObjectBehaviour>().ReturnToPool();
        }
    }
}
