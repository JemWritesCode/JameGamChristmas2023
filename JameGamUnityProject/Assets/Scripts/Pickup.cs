using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JameGam
{
    public class Pickup : MonoBehaviour
    {
        public GameObject myHands; 
        GameObject ObjectIwantToPickUp;
        GameObject CounterCollider;
        [SerializeField] bool hasItem = false;  
        [SerializeField] bool canpickup = false; 
        [SerializeField] bool isCrate = false;
        [SerializeField] bool isNextToCounter = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))  
            {
                if (hasItem == true)
                {
                    PutdownItem();
                }
                else
                {
                    PickupItem();
                }
            }
        }

        private void PickupItem()
        {
            if (canpickup == true && hasItem == false)
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

        private void PutdownItem()
        {
            Debug.Log("Putting object down");
            ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
            ObjectIwantToPickUp.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (isNextToCounter)
            {
                // this works ok but obvs not right spot ObjectIwantToPickUp.transform.position = CounterCollider.transform.position; //parent.Find("ObjectPlacer").transform.position;
                ObjectIwantToPickUp.transform.position = CounterCollider.transform.parent.Find("ObjectPlacer").transform.position;
            }
            //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
            hasItem = false;
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
            if (other.gameObject.tag == "CounterTile")
            {
                isNextToCounter = true;
                CounterCollider = other.gameObject; 
            }

        }
        private void OnTriggerExit(Collider other)
        {
            canpickup = false;
            isNextToCounter = false;
        }
    }
}

