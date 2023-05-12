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

    public void UpdateAmmoInventory(Ammo ammoType, int ammoAmt)
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
    }



    //tryAddAmmo (ammo type, amount)
    //check dictionary, if valid, add, else do nothing
}
