using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JameGam
{
    public class Pickup : MonoBehaviour
    {
        public GameObject myHands; 
        GameObject ObjectIwantToPickUp; 
        [SerializeField] bool hasItem;  
        [SerializeField] bool canpickup; 
        [SerializeField] bool isCrate = false;
        void Start()
        {
            canpickup = false;    
            hasItem = false;
        }

        void Update()
        {
            //Pickup
            if (Input.GetKeyDown(KeyCode.Space))  
            {
                if (hasItem == true)  //Putdown
                {
                    Debug.Log("Putting object down");
                    //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
                    ObjectIwantToPickUp.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                    hasItem = false;
                }
                else //Pickup
                {
                    if (canpickup == true) 
                    {
                        Debug.Log("Picking object up");
                        if (isCrate)
                        {
                            ObjectIwantToPickUp = Instantiate(ObjectIwantToPickUp);
                            isCrate = false;
                        }
                        //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;   //makes the rigidbody not be acted upon by forces
                        ObjectIwantToPickUp.transform.position = myHands.transform.position;
                        ObjectIwantToPickUp.transform.parent = myHands.transform; 
                        hasItem = true;
                    }
                }
            }
            
        }
        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "object") 
            {
                canpickup = true;  
                ObjectIwantToPickUp = other.gameObject;
            }
            if (other.gameObject.tag == "crate") 
            {
                isCrate = true;
                canpickup = true;  
                ObjectIwantToPickUp = other.gameObject.GetComponent<Spawner>().itemToSpawn;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            canpickup = false;

        }

        private void putOnCounterTile()
        {
            //need to check if they're near a "tile" (via collision trigger box?)
            // put it on the ObjectPlacer
        }
    }
}

