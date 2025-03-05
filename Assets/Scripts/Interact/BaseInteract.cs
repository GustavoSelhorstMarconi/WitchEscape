using System;
using UnityEngine;

public class BaseInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactText;

    public string GetInteractText()
    {
        return interactText;
    }

    protected void CheckInput()
    {
        if (GameInput.Instance.GetPlayerEndInteract())
        {
            EndInteract();
        }
    }

    public virtual void Interact(object param = default)
    {
        gameObject.SetActive(false);
    }

    public virtual void EndInteract()
    {
        Debug.Log("Acabou a interação.");
    }
}
