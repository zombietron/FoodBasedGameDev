using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAroundDesignatedAxis : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    public RotationalAxis axis;

    Vector3 rotateAxisVector;

    // Start is called before the first frame update
    void Start()
    {
        setAxisVector();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAxisVector * Time.deltaTime * rotateSpeed);
    }

    void setAxisVector()
    {
        switch (axis)
        {
            case RotationalAxis.x:
                rotateAxisVector = Vector3.right;
                break;
            case RotationalAxis.y:
                rotateAxisVector = Vector3.up;
                break;
            case RotationalAxis.z: 
                rotateAxisVector = Vector3.forward;
                break;

            default:
                rotateAxisVector = Vector3.right;
                break;
        }
    }
}

public enum RotationalAxis 
{
    x,
    y,
    z
}