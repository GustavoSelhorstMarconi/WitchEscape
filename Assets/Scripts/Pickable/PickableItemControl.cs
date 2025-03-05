using UnityEngine;

public class PickableItemControl : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactText;

    public virtual string GetInteractText()
    {
        return interactText;
    }

    public virtual void PickItem()
    {
    }

    public virtual bool CanInteract()
    {
        return true;
    }
}
