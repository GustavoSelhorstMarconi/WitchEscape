using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotionMakerControl : BaseInteract
{
    [SerializeField]
    private List<PotionSO> availablePotions;
    [SerializeField]
    private PotionMakerItemControl potionMakerItemControl;
    [SerializeField]
    private Transform placeInstantiatePotion;
    [SerializeField]
    private Collider blockItemsCollider;

    private bool isInteracting;
    private int currentPotionSelected;
    private float currentTimerCreationPotion;
    private bool isCreatingPotion;
    private List<PotionIngredientSO> currentIngredientsNeeded;
    private List<PotionIngredientPickableItemControl> currentPickedIngredients;
    private List<PotionIngredientSO> currentPickedIngredientSOs => currentPickedIngredients
                .Select(x => x.GetPotionIngredientSO())
                .ToList();

    private void Start()
    {
        isInteracting = false;
        isCreatingPotion = false;
        currentPotionSelected = 0;
        currentIngredientsNeeded = availablePotions[currentPotionSelected].ingredients;

        currentPickedIngredients = new List<PotionIngredientPickableItemControl>();
        blockItemsCollider.enabled = false;

        potionMakerItemControl.OnItemsEnter += PotionMakerItemControl_OnItemsEnter;
        potionMakerItemControl.OnItemsExit += PotionMakerItemControl_OnItemsExit;
    }

    private void PotionMakerItemControl_OnItemsEnter(object sender, PotionMakerItemControl.OnItemsChangeEventArgs e)
    {
        if (!currentPickedIngredients.Any(x => x == e.potionIngredientPickableItemControl))
        {
            currentPickedIngredients.Add(e.potionIngredientPickableItemControl);
        }
    }

    private void PotionMakerItemControl_OnItemsExit(object sender, PotionMakerItemControl.OnItemsChangeEventArgs e)
    {
        if (currentPickedIngredients.Any(x => x == e.potionIngredientPickableItemControl))
        {
            currentPickedIngredients.Remove(e.potionIngredientPickableItemControl);
        }
    }

    private void Update()
    {
        CheckInput();

        if (isCreatingPotion)
        {
            HandleTimerCreationPotion();
        }
    }

    public override void Interact(object potionIngredient)
    {
        if (!isInteracting)
        {
            isInteracting = true;
            CursorControl.Instance.SetCursorNotLocked();

            PotionMakerUI.Instance.Show();

            PotionMakerUI.Instance.HandleCurrentPotionMakerControl(this);

            PotionMakerUI.Instance.UpdateVisual(availablePotions[currentPotionSelected], currentPickedIngredientSOs);
        }
    }

    public override void EndInteract()
    {
        isInteracting = false;
        PotionMakerUI.Instance.Hide();
        PlayerInteractControl.Instance.HandleEndInteract();
        CursorControl.Instance.SetCursorLocked();
    }

    public void HandleNextPotion()
    {
        currentPotionSelected = currentPotionSelected + 1 >= availablePotions.Count ? 0 : currentPotionSelected + 1;
        currentIngredientsNeeded = availablePotions[currentPotionSelected].ingredients;

        List<PotionIngredientSO> potionIngredientSOs = currentPickedIngredients
                .Select(x => x.GetPotionIngredientSO())
                .ToList();

        PotionMakerUI.Instance.UpdateVisual(availablePotions[currentPotionSelected], potionIngredientSOs);
    }

    public void HandlePreviousPotion()
    {
        currentPotionSelected = currentPotionSelected - 1 < 0 ? availablePotions.Count - 1 : currentPotionSelected - 1;
        currentIngredientsNeeded = availablePotions[currentPotionSelected].ingredients;

        PotionMakerUI.Instance.UpdateVisual(availablePotions[currentPotionSelected], currentPickedIngredientSOs);
    }

    public void HandleStartTimerCreatePotion()
    {
        currentTimerCreationPotion = availablePotions[currentPotionSelected].maxTimerCreatePotion;
        isCreatingPotion = true;
        blockItemsCollider.enabled = true;
    }

    public float GetCurrentTimerCreationPotionNormalized()
    {
        return 1 - currentTimerCreationPotion / availablePotions[currentPotionSelected].maxTimerCreatePotion;
    }

    public bool IsAllIngredientsPicked()
    {
        bool isAllIngredientsPicked = currentIngredientsNeeded
            .All(x => currentPickedIngredients.Any(y => y.GetPotionIngredientSO().ingredientName == x.ingredientName))
            && currentIngredientsNeeded.Count() == currentPickedIngredients.Count();

        return isAllIngredientsPicked;
    }

    public bool IsCreatingPotion()
    {
        return isCreatingPotion;
    }

    private void HandleTimerCreationPotion()
    {
        currentTimerCreationPotion -= Time.deltaTime;

        if (currentTimerCreationPotion < 0f)
        {
            isCreatingPotion = false;
            PotionMakerUI.Instance.UnlockButtonCreatePotion();
            HandleEndCreationPotion();
        }
        else
        {
            PotionMakerUI.Instance.HandlePotionSituationControl(currentTimerCreationPotion, GetCurrentTimerCreationPotionNormalized());
        }
    }

    private void HandleEndCreationPotion()
    {
        currentPickedIngredients.ForEach(x =>
        {
            Destroy(x.gameObject);
        });

        currentPickedIngredients = new List<PotionIngredientPickableItemControl>();
        GameObject potion = Instantiate(availablePotions[currentPotionSelected].potion, placeInstantiatePotion.position, Quaternion.identity);
        PotionMakerUI.Instance.UpdateVisual(availablePotions[currentPotionSelected], currentPickedIngredientSOs);
        PotionMakerUI.Instance.HandlePotionSituationControl(currentTimerCreationPotion, 0f);
        blockItemsCollider.enabled = false;
    }
}
