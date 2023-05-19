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
        [SerializeField] private float rotateSpeed;
        public bool stopPlayer = false;
        private int currentAmmoCount;
        private List<string> allFoodTypes = new List<string>();// not required in player controller anymore?
        private Vector2 move;
        private Vector3 moveForce;
        private string activeFoodTypeToThrow;
        private float rotateDirection;
        private bool isInteractable;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            characterAmmoInventory = GetComponent<AmmoInventory>();
            characterAmmoDict = characterAmmoInventory.GetAmmoDict();
            foreach (var key in characterAmmoDict.Keys)
            {
                allFoodTypes.Add(key); 
            }
        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
            moveForce = new Vector3(-move.x, 0.0f, -move.y);
        }

        public void OnInteract(InputValue value)
        {

            if (!isInteractable) return;
            stallBehavior.StartAmmoPickupCycle(value.isPressed);
            // trigger pick up animation?
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("foodStall")) return;
            stallBehavior = other.gameObject.GetComponent<StallBehavior>();
            stallBehavior.DisplayUseInstructions(true);
            stallBehavior.Inventory = characterAmmoInventory;
            activeFoodTypeToThrow = stallBehavior.GetTableAmmoType().GetFoodType().ToString();
            isInteractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("foodStall")) return;
            stallBehavior.DisplayUseInstructions(false);
            isInteractable = false;
        }

        void OnToggle(InputValue value)
        {
            //TODO: Add Food projectile toggle code
            foreach (var foodType in allFoodTypes)
            {
                if (foodType == activeFoodTypeToThrow) continue;
                activeFoodTypeToThrow = foodType;
                return;
            }

            Debug.Log("Active Food Type: " + activeFoodTypeToThrow); // will be easier to test once we have other tables and add other ammos
        }

        void OnRotate(InputValue value)
        {
            Debug.Log("Triggering Rotate");
            rotateDirection = value.Get<float>();
        }
        void OnThrow()
        {
            //add throw animation here
            isInteractable = false;
        }

        void OnLook(InputValue value)
        {
            //TODO: Aim to shoot maybe?
        }

        private void FixedUpdate()
        {
            rb.velocity = moveForce * moveSpeed; //change to updating transform instead
            transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed * rotateDirection));
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
