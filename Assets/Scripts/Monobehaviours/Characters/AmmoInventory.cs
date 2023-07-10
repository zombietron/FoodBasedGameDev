using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    //ammo inventory handles anything that goes into adding or subtracting ammo
    private Dictionary<string, int> ammoDictionary = new Dictionary<string, int>();
    [SerializeField]
    private AmmoContainer ammoContainer;

    [SerializeField] ProjectileAttack atk;
    public Dictionary<string,int> GetAmmoDict()
    {
        return ammoDictionary;
    }

    public bool AddAmmoToDict(string key, int val)
    {
        bool added = ammoDictionary.TryAdd(key, val);
        return added;
    }

    private void Awake()
    {
        foreach (var item in ammoContainer.ammoList)
        {
            ammoDictionary.Add(item.GetFoodType().ToString(), item.MaxAmmoAmt());
        }


        atk = GetComponent<ProjectileAttack>();

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

    public void ShootAndRemoveAmmoInventory(string key)
    {
        var currentAmmoAmount = ammoDictionary[key];
        Debug.Log(currentAmmoAmount);
        if (currentAmmoAmount <= 0)
        {
            //Add no remaining ammo sound
            return;
        }
        //add animation trigger here or player?
        //add shooting sound here?
        ammoDictionary[key] = currentAmmoAmount - 1;
        atk.GetProjectile(key);
        
    }

    public string GetCurrentAmmoCount(string key)
    {
        return ammoDictionary[key].ToString();
    }

}
