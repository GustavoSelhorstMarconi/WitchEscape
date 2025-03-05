using UnityEngine;

public class FirstPersonPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private float playerSpeedMovement;
    [SerializeField]
    private float playerSpeedSprint;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravityValue;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private float playerSpeed;
    private bool canMove;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        canMove = true;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!canMove)
        {
            return;
        }

        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = GameInput.Instance.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move = move.normalized;
        move.y = 0f;

        playerSpeed = !GameInput.Instance.GetPlayerSprint() ? playerSpeedMovement : playerSpeedSprint;

        characterController.Move(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        if (GameInput.Instance.PlayerJump() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void MoveToPosition(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
    }

    public void SetCanMove(bool newCanMove)
    {
        canMove = newCanMove;
    }
}
