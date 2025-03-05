using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private GameObject containerGameObject;
    [SerializeField]
    private TextMeshProUGUI interactKeyText;
    [SerializeField]
    private TextMeshProUGUI interactText;

    private void Update()
    {
        HandleInteractUI();
    }

    private void HandleInteractUI()
    {
        IInteractable interactable = PlayerInteractControl.Instance.GetInteractable();

        if (interactable != null)
        {
            Show(interactable);

            interactKeyText.text = interactable is BaseInteract ? "E" : "F";
        }
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable interactable)
    {
        containerGameObject.SetActive(true);
        interactText.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
