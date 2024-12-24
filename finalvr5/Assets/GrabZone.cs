using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    public GameObject grabMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabZone") && grabMove != null)
        {
            grabMove.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabZone") && grabMove != null)
        {
            grabMove.SetActive(false);
        }
    }
}
