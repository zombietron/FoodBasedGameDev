using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Object = UnityEngine.Object;

namespace Monobehaviours.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float throwForce;
        [SerializeField] private AmmoInventory characterAmmoInventory;
        [SerializeField] private Dictionary<string, int> characterAmmoDict;
        [SerializeField] private StallBehavior stallBehavior; //change to stall type; add identifier to each stall type 
        public bool stopPlayer = false;
        private int currentAmmoCount; // not required in player controller anymore?
        private Vector2 move;
        private Vector3 moveForce;
        private bool isInteractable;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            characterAmmoInventory = GetComponent<AmmoInventory>();
            characterAmmoDict = characterAmmoInventory.GetAmmoDict();

        }

        private void OnEnable()
        {
            //OnInteractAction +=OnInteract;
        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
            moveForce = new Vector3(-move.x, 0.0f, -move.y);
        }

        public void OnInteract(InputValue value)
        {
            Debug.Log("EVENT FIRED");
            
            if (!isInteractable) return;
            if(value.isPressed)
            {
                stallBehavior.StartAmmoPickupCycle();
                //This should trigger Stallbehaviour to take over instead for HUD animation;
                //can pass context actuated time by passing context control as an argument?
            };
            if(!value.isPressed)
            {
                stallBehavior.StopAmmoPickupCycle();
            };
            #region old temp code
            //switch (stallType.tag)
            //{
            //    case "pizza":
            //        characterAmmoInventory.AddAmmoToDict(stallType.tag, currentAmmoCount + 1);
            //        return;
            //    case "hotDog": return;
            //    case "taco": return;
            //    case "pie": return;
            //}
            //go.transform.position = transform.position;
            //go.SetActive(false);
            #endregion  
            Debug.Log("Triggering Interact");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("foodStall")) return;
            stallBehavior = other.gameObject.GetComponent<StallBehavior>();
            stallBehavior.DisplayUseInstructions(true);
            stallBehavior.Inventory = characterAmmoInventory;
            isInteractable = true;
            Debug.Log("IsInteractable: " + isInteractable);
            //Debug.Log(""); //give control to StallBehaviour; StallBehavior updates the right ammotype in the inventory
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("foodStall")) return;
            stallBehavior.DisplayUseInstructions(false);
            isInteractable = false;
        }

        void OnToggle()
        {
            //TODO: Add Food projectile toggle code
        }

        void OnThrow()
        {
            //var go = pizzasToThrow[0];
            //go.SetActive(true);
            //var goRb = go.GetComponent<Rigidbody>();
            //goRb.velocity = Vector3.forward * throwForce;
            isInteractable = false;
        }

        void OnLook(InputValue value)
        {
            //TODO: Aim to shoot maybe?
        }

        private void FixedUpdate()
        {
            rb.velocity = moveForce * moveSpeed; //change to updating transform instead
            #region ammo transform follows player transform
            //if (isInteractable)
            //{
            //    foreach (var pizza in pizzasToThrow)
            //    {
            //        pizza.transform.position = transform.position;
            //    }   
            //}
            #endregion

            if (!stopPlayer) return;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }
}
