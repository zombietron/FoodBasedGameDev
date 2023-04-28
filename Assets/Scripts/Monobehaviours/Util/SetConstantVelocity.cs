using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetConstantVelocity : MonoBehaviour
{
    //This is a big old pointless comment
    /**
     * COMMMENTS!
     */
    Rigidbody rb;
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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeVelocity();
        
    }

    void InitializeVelocity()
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
}
