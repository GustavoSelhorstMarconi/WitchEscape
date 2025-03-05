using UnityEngine;

public class FirstPersonPlayerMovementTry : MonoBehaviour
{
    private bool canMove = true;

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField, Range(1, 10)]
    private float lookXSpeed;
    [SerializeField, Range(1, 10)]
    private float lookYSpeed;
    [SerializeField, Range(1, 180)]
    private float upperLookLimit;
    [SerializeField, Range(1, 180)]
    private float lowerLookLimit;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private CharacterController characterController;

    private Vector3 movementDirection;
    private Vector2 currentInput;

    private float xRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (CanMove())
        {
            HandleMovementInput();
            HandleMouseLook();

            ApplyFinalMovement();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = GameInput.Instance.GetPlayerMovement() * walkSpeed;

        float moveDirectionY = movementDirection.y;
        movementDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        movementDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        //xRotation -= Input.GetAxis("Mouse Y") * lookYSpeed;
        //xRotation = Mathf.Clamp(xRotation, -upperLookLimit, lowerLookLimit);
        //playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookXSpeed, 0);
    }

    private void ApplyFinalMovement()
    {
        if (!characterController.isGrounded)
        {
            movementDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(movementDirection * Time.deltaTime);
    }

    public bool CanMove()
    {
        return canMove;
    }
}
