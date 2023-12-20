using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JameGam
{
    public class Pickup : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // need to change this to trigger only once
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("hiya");
            }
        }
    }
}
