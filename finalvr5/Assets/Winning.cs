using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winning : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] GameObject text;

    void Start()
    {
        particleEffect.Stop();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            particleEffect.Play();
            text.SetActive(true);
        }
    }
}
