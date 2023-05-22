using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    //ammo inventory handles anything that goes into adding or subtracting ammo
    private readonly Dictionary<FoodType, int> ammoDictionary = new();

    [SerializeField]
    private AmmoContainer ammoContainer;

    public int this[FoodType key]
    {
        get { return ammoDictionary.ContainsKey(key) ? ammoDictionary[key] : 0; }
    }

    public bool AddAmmo(FoodType key, int val)
    {
        var maxAmmo = ammoContainer.ammoList.FirstOrDefault(a => a.GetFoodType() == key);
        int maxAmmoAmt = maxAmmo == null ? 0 : maxAmmo.MaxAmmoAmt;

        if (ammoDictionary.ContainsKey(key))
        {
            ammoDictionary[key] = System.Math.Min(ammoDictionary[key] + val, maxAmmoAmt);
            return true;
        }
        else
            return ammoDictionary.TryAdd(key, System.Math.Min(val, maxAmmoAmt));
    }



    // public FoodType UpdateAmmoInventory(Ammo ammoType, int ammoAmt)
    // {
    //     Debug.Log("Update Ammo Inventory entered");
    //     //compare current ammo to max ammo allowed for type
    //     int maxAmmo = ammoAmt;
    //     int currentAmmo;

    //     bool exists = ammoDictionary.TryGetValue(ammoType.GetFoodType().ToString(), out currentAmmo);
    //     Debug.Log(currentAmmo);
    //     if (exists && currentAmmo < maxAmmo)
    //     {
    //         ammoDictionary[ammoType.GetFoodType().ToString()] = maxAmmo;
    //         Debug.Log($"{ammoType.GetFoodType()} has been reloaded and has {ammoDictionary[ammoType.GetFoodType().ToString()]}");
    //     }

    //     return ammoType.GetFoodType();
    // }


    public void ShootAndRemoveAmmoInventory(Ammo ammoType)
    {
        var currentAmmoAmount = ammoDictionary[ammoType.GetFoodType()];
        if (currentAmmoAmount <= 0)
        {
            //Add no remaining ammo sound
            return;
        }
        //add animation trigger here or player?
        //add shooting sound here?
        ammoDictionary[ammoType.GetFoodType()] = currentAmmoAmount - 1;
    }
    
    //tryAddAmmo (ammo type, amount)
    //check dictionary, if valid, add, else do nothing

}