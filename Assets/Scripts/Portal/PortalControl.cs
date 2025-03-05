using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField]
    private Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FirstPersonPlayerMovement>(out FirstPersonPlayerMovement firstPersonPlayerMovement))
        {
            firstPersonPlayerMovement.MoveToPosition(destination.position);
        }
    }
}
