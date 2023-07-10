using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    [SerializeField]
    private int attackValue;

    private GameObject target;
    public GameObject Target { set { target = value; } get { return target; } }
    public void Attack ()
    {
        target.GetComponent<HP>().ReduceHP(attackValue);
        Debug.Log("Target HP reduced by: " + attackValue);
    }
}
