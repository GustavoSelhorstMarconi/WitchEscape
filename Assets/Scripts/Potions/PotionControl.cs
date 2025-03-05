using UnityEngine;

public class PotionControl : PickableItemControl, IItemUsable
{
    [SerializeField]
    private PotionSO potionSO;
    [SerializeField]
    private Animator potionAnimator;
    [SerializeField]
    private float delayDestroyItem;

    public override string GetInteractText()
    {
        return potionSO.potionName;
    }

    public void UseItem()
    {
        potionAnimator.enabled = true;
        Destroy(gameObject, delayDestroyItem);
    }
}
