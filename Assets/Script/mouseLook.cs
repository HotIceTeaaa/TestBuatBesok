using UnityEngine;

public class mouseLook : MonoBehaviour
{
    [SerializeField] private inputReader input;
    public Transform cameraTarget;
    public float mouseSensitivity = 0.15f;
    public float topClamp = 70f;
    public float bottomClamp = -30f;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        if (cameraTarget != null)
        {
            yaw = cameraTarget.rotation.eulerAngles.y;
            pitch = cameraTarget.rotation.eulerAngles.x;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        RotateCameraTarget();
    }

    private void RotateCameraTarget()
    {
        if (cameraTarget == null || input == null)
            return;

        Vector2 lookInput = input.lookVector;

        if (lookInput.sqrMagnitude < 0.01f)
            return;

        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;

        pitch = ClampAngle(pitch, bottomClamp, topClamp);

        cameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}