using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoScriptableObject", menuName = "ScriptableObjects/Create Ammo Container")]

public class AmmoContainer : ScriptableObject
{

    public List<Ammo> ammoList;
}
