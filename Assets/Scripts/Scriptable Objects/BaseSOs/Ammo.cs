
using UnityEngine;

[CreateAssetMenu(fileName ="AmmoScriptableObject", menuName = "ScriptableObjects/Create Ammo")]
public class Ammo : ScriptableObject
{
    private enum FoodType {hotDog, taco, pie, pizza, sandwich};

    [SerializeField]
    private FoodType foodType;

    [SerializeField]
    private GameObject foodMesh;

    [SerializeField]
    private int damageValue;

    [SerializeField]
    private int maxAmmoAmt;
}
