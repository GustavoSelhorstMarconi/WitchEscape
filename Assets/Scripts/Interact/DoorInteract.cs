using UnityEngine;

public class DoorInteract : BaseInteract
{
    [SerializeField]
    private bool canOpenDoor;

    public void UnlockDoor()
    {
        canOpenDoor = true;
    }
}
