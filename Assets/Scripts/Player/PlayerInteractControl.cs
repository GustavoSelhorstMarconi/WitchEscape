using System;
using UnityEngine;

public class PlayerInteractControl : MonoBehaviour
{
    public static PlayerInteractControl Instance {  get; private set; }

    [SerializeField]
    private float interactRange;
    [SerializeField]
    private LayerMask interactLayer;
    [SerializeField]
    private Transform interactTransform;
    [SerializeField]
    private PlayerPickUpItemControl playerPickUpItemControl;
    [SerializeField]
    private FirstPersonPlayerMovement firstPersonPlayerMovement;
    [SerializeField]
    private PlayerCameraControl playerCameraControl;

    public enum InteractType
    {
        BaseInteract,
        CastSpellInteract,
        SpellInteract
    }

    private bool isInteracting;

    private void Awake()
    {
        Instance = this;
        isInteracting = false;
    }

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (GameInput.Instance.GetPlayerInteract())
        {
            CheckObjectInteractCloser();
        }
        if (GameInput.Instance.GetInteractPickableItem())
        {
            CheckObjectPickableCloser();
        }
    }

    private void CheckObjectPickableCloser()
    {
        Ray ray = GetRayPlayerLooking();

        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable is PickableItemControl && (interactable as PickableItemControl).CanInteract())
                {
                    PickableItemControl pickableItemControl = interactable as PickableItemControl;

                    pickableItemControl.PickItem();
                    playerPickUpItemControl.SetPickedItem(pickableItemControl.transform);
                }
            }
        }
    }

    private void CheckObjectInteractCloser()
    {
        if (isInteracting)
        {
            return;
        }

        Ray ray = GetRayPlayerLooking();
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable) && interactable is not CastSpellInteract)
            {
                if (interactable is BaseInteract)
                {
                    BaseInteract baseInteract = interactable as BaseInteract;

                    switch (baseInteract)
                    {
                        case PotionMakerControl:
                            baseInteract.Interact(this);
                            HandleStartInteract();
                            break;
                        default:
                            baseInteract.Interact();
                            break;
                    }
                }
            }
        }
    }

    public IInteractable GetInteractableByType(InteractType type)
    {
        if (isInteracting)
        {
            return null;
        }

        Ray ray = GetRayPlayerLooking();

        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable baseInteract))
            {
                if (baseInteract.GetType().Name == type.ToString())
                {
                    return baseInteract;
                }
            }
        }

        return null;
    }

    public IInteractable GetInteractable()
    {
        if (isInteracting)
        {
            return null;
        }

        Ray ray = GetRayPlayerLooking();

        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactRange, interactLayer))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }

        return null;
    }

    public void HandleEndInteract()
    {
        isInteracting = false;
        firstPersonPlayerMovement.SetCanMove(true);
        playerCameraControl.EnableCameraRotation();
    }

    public Ray GetRayPlayerLooking()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        return ray;
    }

    private void HandleStartInteract()
    {
        isInteracting = true;
        firstPersonPlayerMovement.SetCanMove(false);
        playerCameraControl.DisableCameraRotation();
    }

    //void OnDrawGizmosSelected()
    //{
    //    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
    //    Gizmos.DrawRay(ray);
    //}
}
