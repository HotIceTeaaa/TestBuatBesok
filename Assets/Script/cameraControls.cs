using Unity.Cinemachine;
using UnityEngine;

public class cameraControls : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow cmOrbitFollow;
    [SerializeField] private inputReader inputReader;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private float cameraRadius;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    void Start()
    {
        cameraRadius = cmOrbitFollow.Radius;
    }
    void Update()
    {
        controlZoom();
        hideUnhidePlayer();
    }

    private void controlZoom()
    {
        float zoomInput = inputReader.zoomVector.y;

        cameraRadius -= zoomInput * zoomSpeed;
        cameraRadius = Mathf.Clamp(cameraRadius, minDistance, maxDistance);

        cmOrbitFollow.Radius = cameraRadius;
    }

    private void hideUnhidePlayer()
    {
        if(cameraRadius <= 1f)
        {
            playerMesh.SetActive(false);
        }
        else
        {
            playerMesh.SetActive(true);
        }
    }
}
