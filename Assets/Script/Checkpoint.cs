using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject particleSystemObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController playerControllerScript = other.gameObject.GetComponent<playerController>();
            playerControllerScript.spawnPoint = spawnPoint.position;

            particleSystemObject.SetActive(false);
        }
    }

    public void renableParticleSystem()
    {
        particleSystemObject.SetActive(true);
    }
}
