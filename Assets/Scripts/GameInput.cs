using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private GameInputActions gameInputActions;

    private void Awake()
    {
        Instance = this;

        gameInputActions = new GameInputActions();

        gameInputActions.Enable();
    }

    public Vector2 GetPlayerMovement()
    {
        return gameInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return gameInputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJump()
    {
        return gameInputActions.Player.Jump.triggered;
    }

    public bool GetPlayerSprint()
    {
        return gameInputActions.Player.Sprint.IsPressed();
    }

    public bool GetPlayerChangeSelectedSpell()
    {
        return gameInputActions.Player.ChangeSelectedSpell.triggered;
    }

    public bool GetPlayerCastSpell()
    {
        return gameInputActions.Player.CastSpell.triggered;
    }

    public bool GetPlayerInteract()
    {
        return gameInputActions.Player.Interact.triggered;
    }

    public bool GetPlayerEndInteract()
    {
        return gameInputActions.Player.EndInteract.triggered;
    }

    public bool GetInteractPickableItem()
    {
        return gameInputActions.Player.InteractPickableItem.triggered;
    }

    public bool GetPlayerDropItem()
    {
        return gameInputActions.Player.DropItem.triggered;
    }

    public bool GetPlayerUseItem()
    {
        return gameInputActions.Player.UseItem.triggered;
    }

    private void OnDisable()
    {
        gameInputActions.Disable();
    }
}
