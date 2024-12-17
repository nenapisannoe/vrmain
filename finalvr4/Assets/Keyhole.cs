using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    public static Action UnlockDoor;
   private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Key"))
    {
        Destroy(other.gameObject);
        if(UnlockDoor != null)
            UnlockDoor.Invoke();
    }
   }
}
