using UnityEngine;

public class KillBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController playerControllerScript = other.gameObject.GetComponent<playerController>();
            playerControllerScript.respawn();
        }
    }
}
