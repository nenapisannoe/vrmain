using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    public GameObject grabMove;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player") && grabMove != null)
        {
            Debug.Log("how are you");
            grabMove.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && grabMove != null)
        {
            grabMove.SetActive(false);
        }
    }
}
