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
    [RequireComponent(typeof(ProjectileAttack))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float throwForce;
        [SerializeField] private AmmoInventory characterAmmoInventory;
        [SerializeField] private Dictionary<string, int> characterAmmoDict;
        [SerializeField] private StallBehavior stallBehavior; //change to stall type; add identifier to each stall type 
        [SerializeField] private float rotateSpeed;

        [SerializeField] Animator anim;
        public bool isDead = false;
        

        public bool stopPlayer = false;
        private int currentAmmoCount;
        private int foodIndex = 0;
        private List<string> allFoodTypes = new List<string>();// not required in player controller anymore?
        private Vector2 move;
        private Vector3 moveForce;
        private string activeFoodTypeToThrow;
        private float rotateDirection;
        private bool isInteractable;
        private bool moving = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            characterAmmoInventory = GetComponent<AmmoInventory>();
            characterAmmoDict = characterAmmoInventory.GetAmmoDict();
            foreach (var key in characterAmmoDict.Keys)
            {
                allFoodTypes.Add(key);
            
            }
            activeFoodTypeToThrow = allFoodTypes[0];

            Debug.Log("Starting Velocity is: " + rb.velocity);
        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
            moveForce = new Vector3(-move.x, 0.0f, -move.y);
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            moving = !moving;

            anim.SetBool("isRunning",moving);
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
            //activeFoodTypeToThrow = stallBehavior.GetTableAmmoType().GetFoodType().ToString();
            isInteractable = true;
            stallBehavior.Player = this;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("foodStall")) return;
            stallBehavior.DisplayUseInstructions(false);
            isInteractable = false;
        }

        void OnToggle(InputValue value)
        {
            foodIndex = foodIndex >= allFoodTypes.Count-1 ? 0 : foodIndex+1;
            
            activeFoodTypeToThrow = allFoodTypes[foodIndex];

            //Debug.Log("Active Food Type: " + activeFoodTypeToThrow); // will be easier to test once we have other tables and add other ammos
        }

        public string GetActiveFoodTypeToThrow()
        {
            return activeFoodTypeToThrow;
        }

        void OnThrow()
        {
            //add throw animation here
            isInteractable = false;
            //Debug.Log("Throw");
            characterAmmoInventory.ShootAndRemoveAmmoInventory(activeFoodTypeToThrow);
            anim.SetTrigger("isThrowing");
        }

        private void FixedUpdate()
        {
            if(isDead)
            {
                anim.SetTrigger("isDead");
            }

            rb.velocity = moveForce * moveSpeed; //change to updating transform instead
            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            }

            if(move==Vector2.zero)
            {
                rb.angularVelocity = Vector3.zero;
            }

            #region ammo transform follows player transform
            //if (isInteractable)
            //{
            //    foreach (var pizza in pizzasToThrow)
            //    {
            //        pizza.transform.position = transform.position;
            //    }   
            //}
            #endregion

            //if (!stopPlayer) return;
            

        }

        public void SetAmmoIndex(int i)
        {
            foodIndex = i;
            activeFoodTypeToThrow = allFoodTypes[foodIndex];

        }
    }
}
