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
}
