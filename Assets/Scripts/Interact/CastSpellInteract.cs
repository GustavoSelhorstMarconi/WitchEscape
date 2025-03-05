using UnityEngine;

public class CastSpellInteract : BaseInteract
{
    [SerializeField]
    private DoorInteract doorInteract;

    public override void Interact(object param = default)
    {
        doorInteract.UnlockDoor();

        gameObject.SetActive(false);
    }
}
