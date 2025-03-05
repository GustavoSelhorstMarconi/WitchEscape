using UnityEngine;

public class PickItemControl : MonoBehaviour
{
    private void Update()
    {
        if (GameInput.Instance.GetPlayerUseItem())
        {
            HandleUseItem();
        }
    }

    private void HandleUseItem()
    {
        if (transform.childCount == 1)
        {
            if (transform.GetChild(0).TryGetComponent<IItemUsable>(out IItemUsable itemUsable))
            {
                itemUsable.UseItem();
            }
        }
    }
}
