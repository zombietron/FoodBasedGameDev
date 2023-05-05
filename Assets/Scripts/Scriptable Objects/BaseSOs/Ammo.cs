
using UnityEngine;

[CreateAssetMenu(fileName ="AmmoScriptableObject", menuName = "ScriptableObjects/Create Ammo")]
public class Ammo : ScriptableObject
{
    
    [SerializeField] private FoodType foodType;

    public FoodType GetFoodType()
    {
        return foodType;
    }
    [SerializeField]
    private GameObject foodMesh;

    [SerializeField]
    private int damageValue;

    [SerializeField]
    private int maxAmmoAmt;
}

public enum FoodType { hotDog, taco, pie, pizza, sandwich, pizzaSlice };

