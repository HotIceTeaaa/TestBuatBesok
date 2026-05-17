using UnityEngine;
using UnityEngine.InputSystem;

public class inputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionReference;

    [SerializeField] public Vector2 moveVector;
    [SerializeField] public Vector2 lookVector;
    [SerializeField] public Vector2 zoomVector;
    [SerializeField] public bool isJumpKeyPressed;
    [SerializeField] public bool isDashKeyPressed;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction zoomAction;
    private InputAction jumpAction;
    private InputAction dashAction;


    private void Awake()
    {
        moveAction = inputActionReference.FindAction("Move");
        lookAction = inputActionReference.FindAction("Look");
        zoomAction = inputActionReference.FindAction("CameraZoomControl");
        jumpAction = inputActionReference.FindAction("Jump");
        dashAction = inputActionReference.FindAction("Dash");
    }

    private void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        lookVector = lookAction.ReadValue<Vector2>();
        zoomVector = zoomAction.ReadValue<Vector2>();
        isJumpKeyPressed = jumpAction.IsPressed();
        isDashKeyPressed = dashAction.IsPressed();
    }
}
