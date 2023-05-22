using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTest : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("I been destroyed");
    }
}
