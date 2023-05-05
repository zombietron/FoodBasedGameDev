using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private GameObject placeHolderPizzaStall;
        public bool stopPlayer = false;
        private Vector2 move;
        private Vector3 moveForce;
        private bool isInteractable;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            characterAmmoInventory = GetComponent<AmmoInventory>();
            characterAmmoDict = characterAmmoInventory.GetAmmoDict();

        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
            moveForce = new Vector3(-move.x, 0.0f, -move.y);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform != placeHolderPizzaStall.transform) return;
            //placeHolderPizzaToThrow.SetActive(true);
            isInteractable = true;
        }

        private void OnInteract()
        {
            if (!isInteractable) return;
            //var go = Object.Instantiate(placeHolderPizzaToThrow, transform, true);
            //if (go == null) return;
            Debug.Log(characterAmmoInventory.AddAmmoToDict("pizza", 1));
            //go.transform.position = transform.position;
            //go.SetActive(false);
            Debug.Log("Triggering Interact");


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
            rb.velocity = moveForce * moveSpeed;
            //if (isInteractable)
            //{
            //    foreach (var pizza in pizzasToThrow)
            //    {
            //        pizza.transform.position = transform.position;
            //    }   
            //}

            if (!stopPlayer) return;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }
}
