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

	//	Returns foodtypes in the dictionary. If all is true, will return regardless of ammo level, else only where there's ammo
	public List<FoodType> GetAvailableAmmo(bool all = false)
	{
		return all ? ammoDictionary.Keys.ToList() : ammoDictionary.Where(ft => ft.Value > 0).ToList().ConvertAll(ft => ft.Key);
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

	public void ShootAndRemoveAmmoInventory(Ammo ammoType)
	{
		//	Make sure the ammo Type is loaded.
		if (ammoDictionary.ContainsKey(ammoType.GetFoodType()))
		{
			if (ammoDictionary[ammoType.GetFoodType()]-- <= 0)
			{
				ammoDictionary[ammoType.GetFoodType()] = 0;
				//Add no remaining ammo sound
				return;
			}
			else
			{
				//add animation trigger here or player?
				//add shooting sound here? - I would recommend not - separation of concerns
			}
		}
	}
}