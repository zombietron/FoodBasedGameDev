using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StallBehavior : MonoBehaviour
//stall behavior
//controlling the UI components 
//food type
{
	[SerializeField]
	private Ammo tableAmmoType;

	[SerializeField]
	private GameObject collectionPromptText;

	[SerializeField]
	private Image fillRing;

	[SerializeField]
	private float collectionTimerSecs;

	[SerializeField] private AmmoInventory inventory;

	public AmmoInventory Inventory
	{
		get { return inventory; }
		set { inventory = value; }
	}
	private bool inCooldown;

	private bool inCollectionZone;

	private void Start()
	{
		DisplayUseInstructions(false);
		fillRing.fillAmount = 0;
		fillRing.color = Color.white;
		inCooldown = false;
	}

	//Call on to enable/disable use instructions for table
	public void DisplayUseInstructions(bool value)
	{
		if (!inCooldown)
		{
			if (value)
			{
				collectionPromptText.SetActive(true);
			}
			else
			{
				collectionPromptText.SetActive(false);
			}
		}
	}

	#region Testing only, real functionality on player
	private void OnTriggerEnter(Collider other)
	{
		DisplayUseInstructions(true);
		if (other.tag == "Player")
		{
			inventory = other.GetComponent<AmmoInventory>();
			//This needs to be moved to the player I think. 
			inCollectionZone = true;


		}
		if (other.tag != "Player")
			return;
	}

	private void OnTriggerExit(Collider other)
	{
		DisplayUseInstructions(false);
	}

	#endregion

	public IEnumerator CollectAmmo()
	{
		DisplayUseInstructions(false);
		var currentTime = 0f;
		currentTime = collectionTimerSecs;

		while (currentTime <= collectionTimerSecs - 1)
		{
			currentTime++;
			fillRing.fillAmount = currentTime / collectionTimerSecs;
			yield return new WaitForSeconds(1f);
		}
		Debug.Log("Ammo Collected!");
		fillRing.color = Color.green;
		inventory.AddAmmo(tableAmmoType.GetFoodType(), tableAmmoType.MaxAmmoAmt);
		StartCoroutine(CoolDown());
	}

	private IEnumerator CoolDown()
	{
		inCooldown = true;
		yield return new WaitForSeconds(2);
		var coolDownTime = collectionTimerSecs;
		fillRing.color = Color.red;
		while (coolDownTime > 0)
		{
			coolDownTime--;
			fillRing.fillAmount = coolDownTime / collectionTimerSecs;
			yield return new WaitForSeconds(0.5f);
		}
		fillRing.color = Color.white;
		inCooldown = false;
	}

	public void StartAmmoPickupCycle()
	{
		StartCoroutine(CollectAmmo());
	}

	public void StopAmmoPickupCycle()
	{
		StopCoroutine(CollectAmmo());
	}

}
