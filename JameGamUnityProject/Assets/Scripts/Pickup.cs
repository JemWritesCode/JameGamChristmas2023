using UnityEngine;

namespace JameGam
{
    public class Pickup : MonoBehaviour
    {
        public GameObject myHands; 
        [SerializeField] GameObject ObjectIwantToPickUp;
        [SerializeField] GameObject itemBeingHeld;
        GameObject CounterCollider;
        [SerializeField] bool hasItem = false;  
        [SerializeField] bool canpickup = false; 
        [SerializeField] bool isCrate = false;
        [SerializeField] bool isNextToCounter = false;

        [field: SerializeField]
        public Transform HeldItemAttachPoint { get; private set; }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))  
            {
                if (checkIfHoldingItem() == true)
                {
                    PutdownItem();
                }
                else
                {
                    Debug.Log("here");
                    PickupItem();
                }
                hasItem = checkIfHoldingItem();
            }
        }

        private void PickupItem()
        {
            if (canpickup == true && checkIfHoldingItem() == false)
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
            }
        }
     
        public void PickupItemFromSpawner(GameObject itemToSpawn) {
          if (IsHoldingItem()) {

          } else {
            Instantiate(itemToSpawn, HeldItemAttachPoint, false);
            Debug.Log($"Spawned item: {itemToSpawn.name}");
          }
        }

        private void PutdownItem()
        {
            Debug.Log("Putting object down");
            itemBeingHeld = checkItemBeingHeld();
            itemBeingHeld.transform.parent = null; // make the object not be a child of the hands
            itemBeingHeld.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (isNextToCounter)
            {
                // this works ok but obvs not right spot ObjectIwantToPickUp.transform.position = CounterCollider.transform.position; //parent.Find("ObjectPlacer").transform.position;
                ObjectIwantToPickUp.transform.position = CounterCollider.transform.parent.Find("ObjectPlacer").transform.position;
            }
            //ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
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
            isCrate = false;
        }

        public bool IsHoldingItem() {
          return HeldItemAttachPoint.childCount > 0;
        }

        public GameObject GetHeldItem() {
          return HeldItemAttachPoint.childCount > 0 ? HeldItemAttachPoint.GetChild(0).gameObject : default;
        }

        private bool checkIfHoldingItem()
        {
            return gameObject.transform.Find("basic_rig/basic_rig Pelvis/basic_rig Spine/basic_rig Spine1/basic_rig L Clavicle/basic_rig L UpperArm/basic_rig L Forearm/basic_rig L Hand/HandHolder").transform.childCount > 0;
        }

        private GameObject checkItemBeingHeld()
        {
            return gameObject.transform.Find("basic_rig/basic_rig Pelvis/basic_rig Spine/basic_rig Spine1/basic_rig L Clavicle/basic_rig L UpperArm/basic_rig L Forearm/basic_rig L Hand/HandHolder").transform.GetChild(0).gameObject;
        }
    }
}

