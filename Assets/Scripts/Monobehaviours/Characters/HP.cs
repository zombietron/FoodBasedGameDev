using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int totalHP;

    [SerializeField]
    private int currentHP;

    
    void OnEnable()
    {
        currentHP = totalHP;
    }

    public void ReduceHP(int damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        //Determine what happens after we die. #NoBiggie
    }



}
