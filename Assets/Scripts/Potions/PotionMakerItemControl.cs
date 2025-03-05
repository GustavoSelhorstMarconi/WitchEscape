using System;
using UnityEngine;

public class PotionMakerItemControl : MonoBehaviour
{
    public event EventHandler<OnItemsChangeEventArgs> OnItemsEnter;
    public event EventHandler<OnItemsChangeEventArgs> OnItemsExit;

    [SerializeField]
    private PotionMakerControl potionMakerControl;

    public class OnItemsChangeEventArgs : EventArgs
    {
        public PotionIngredientPickableItemControl potionIngredientPickableItemControl;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PotionIngredientPickableItemControl>(out PotionIngredientPickableItemControl potionIngredientPickableItemControl))
        {
            potionIngredientPickableItemControl.SetPotionMakerItemControl(this);

            OnItemsEnter?.Invoke(this, new OnItemsChangeEventArgs()
            {
                potionIngredientPickableItemControl = potionIngredientPickableItemControl
            });
        }
    }

    public void CallOnItemsExit(PotionIngredientPickableItemControl potionIngredientPickableItemControl)
    {
        OnItemsExit?.Invoke(this, new OnItemsChangeEventArgs()
        {
            potionIngredientPickableItemControl = potionIngredientPickableItemControl
        });
    }

    public bool IsCreatingPotion()
    {
        return potionMakerControl.IsCreatingPotion();
    }
}
