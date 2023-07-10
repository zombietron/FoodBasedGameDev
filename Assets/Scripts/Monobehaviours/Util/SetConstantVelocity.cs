using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetConstantVelocity : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float objectSpeed;
    [SerializeField] movementDirection direction;

    public enum movementDirection
    {
        forward,
        backward,
        left,
        right
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void InitializeVelocity()
    {
        switch(direction)
        {
            case movementDirection.forward:
                rb.velocity = objectSpeed * transform.forward;
                break;
            case movementDirection.backward:
                rb.velocity = objectSpeed * -transform.forward;
                break;
            case movementDirection.left:
                rb.velocity = objectSpeed * -transform.right;
                break;
            case movementDirection.right:
                rb.velocity = objectSpeed * transform.right;
                break;
            default: 
                break;

        }
    }

    public void SetObjectVelocity(Vector3 forward)
    {
        rb.velocity = objectSpeed * forward;
    }
}
