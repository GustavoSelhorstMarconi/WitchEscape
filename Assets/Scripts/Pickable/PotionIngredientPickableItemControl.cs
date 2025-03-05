using UnityEngine;

public class PotionIngredientPickableItemControl : PickableItemControl
{
    [SerializeField]
    private PotionIngredientSO potionIngredientSO;

    private PotionMakerItemControl potionMakerItemControl;

    public override string GetInteractText()
    {
        return potionIngredientSO.ingredientName;
    }

    public override void PickItem()
    {
        if (potionMakerItemControl != null)
        {
            potionMakerItemControl.CallOnItemsExit(this);
            potionMakerItemControl = null;
        }
    }

    public override bool CanInteract()
    {
        return potionMakerItemControl == null || !potionMakerItemControl.IsCreatingPotion();
    }

    public void SetPotionMakerItemControl(PotionMakerItemControl newPotionMakerItemControl)
    {
        potionMakerItemControl = newPotionMakerItemControl;
    }

    public PotionIngredientSO GetPotionIngredientSO()
    {
        return potionIngredientSO;
    }
}
