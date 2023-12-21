using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JameGam
{
    public class Pickup : MonoBehaviour
    {
        public GameObject myHands; //reference to your hands/the position where you want your object to go
        bool canpickup; //a bool to see if you can or cant pick up the item
        GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
        [SerializeField] bool hasItem;  // a bool to see if you have an item in your hand
        void Start()
        {
            canpickup = false;    //setting both to false
            hasItem = false;
        }

        void Update()
        {
            if (canpickup == true) // if you enter thecollider of the objecct
            {
                //Pickup
                if (Input.GetKeyDown(KeyCode.Space))  
                {
                    if (hasItem == true)  //Putdown
                    {
                        //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                        ObjectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands
                        ObjectIwantToPickUp.transform.rotation = Quaternion.Euler(0f,0f,0f); // make sure it's in original rotation
                        hasItem = false;
                    }
                    else //Pickup
                    {
                        //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;   //makes the rigidbody not be acted upon by forces
                        ObjectIwantToPickUp.transform.position = myHands.transform.position; 
                        ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                        hasItem = true;
                    }
                }
            }
        }
        private void OnTriggerEnter(Collider other) // to see when the player enters the collider
        {
            if (other.gameObject.tag == "object") //on the object you want to pick up set the tag to be anything, in this case "object"
            {
                canpickup = true;  //set the pick up bool to true
                ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            }
            if (other.gameObject.tag == "crate") //on the object you want to pick up set the tag to be anything, in this case "object"
            {
                canpickup = true;  //set the pick up bool to true
                ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            }

        }
        private void OnTriggerExit(Collider other)
        {
            canpickup = false; //when you leave the collider set the canpickup bool to false

        }
    }
}

