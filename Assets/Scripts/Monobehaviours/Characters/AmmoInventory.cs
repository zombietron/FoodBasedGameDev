using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    //ammo inventory handles anything that goes into adding or subtracting ammo
    private Dictionary<string, int> ammoDictionary = new Dictionary<string, int>();
    [SerializeField]
    private AmmoContainer ammoContainer;

    public Dictionary<string,int> GetAmmoDict()
    {
        return ammoDictionary;
    }

    public bool AddAmmoToDict(string key, int val)
    {
        bool added = ammoDictionary.TryAdd(key, val);
        return added;
    }

    private void Start()
    {
        foreach (var item in ammoContainer.ammoList)
        {
            ammoDictionary.Add(item.GetFoodType().ToString(), 0);

        }


    }

    public FoodType UpdateAmmoInventory(Ammo ammoType, int ammoAmt)
    {
        Debug.Log("Update Ammo Inventory entered");
        //compare current ammo to max ammo allowed for type
        int maxAmmo = ammoAmt;
        int currentAmmo;

        bool exists = ammoDictionary.TryGetValue(ammoType.GetFoodType().ToString(), out currentAmmo);
        Debug.Log(currentAmmo);
        if (exists && currentAmmo < maxAmmo)
        {
            ammoDictionary[ammoType.GetFoodType().ToString()] = maxAmmo;
            Debug.Log($"{ammoType.GetFoodType()} has been reloaded and has {ammoDictionary[ammoType.GetFoodType().ToString()]}");
        }

        return ammoType.GetFoodType();
    }

    public void ShootAndRemoveAmmoInventory(Ammo ammoType)
    {
        var currentAmmoAmount = ammoDictionary[ammoType.GetFoodType().ToString()];
        if (currentAmmoAmount <= 0)
        {
            //Add no remaining ammo sound
            return;
        }
        //add animation trigger here or player?
        //add shooting sound here?
        ammoDictionary[ammoType.GetFoodType().ToString()] = currentAmmoAmount - 1;
    }
    //tryAddAmmo (ammo type, amount)
    //check dictionary, if valid, add, else do nothing
}
