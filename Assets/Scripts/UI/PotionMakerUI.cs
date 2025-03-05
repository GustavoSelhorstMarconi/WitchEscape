using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionMakerUI : MonoBehaviour
{
    public static PotionMakerUI Instance { get; set; }

    [SerializeField]
    private TextMeshProUGUI textPotionName;
    [SerializeField]
    private Transform potionIngredientContainer;
    [SerializeField]
    private Transform potionIngredientTemplate;
    [SerializeField]
    private TextMeshProUGUI timerCreationPotion;
    [SerializeField]
    private Image resultPotionImage;
    [SerializeField]
    private Button createPotionButton;
    [SerializeField]
    private Button nextPotionButton;
    [SerializeField]
    private Button previousPotionButton;

    private PotionMakerControl currentPotionMakerControl;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
    }

    public void SetPotionName(string potionName)
    {
        textPotionName.text = potionName;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HandleCurrentPotionMakerControl(PotionMakerControl potionMakerControl)
    {
        currentPotionMakerControl = potionMakerControl;

        nextPotionButton.onClick.RemoveAllListeners();
        previousPotionButton.onClick.RemoveAllListeners();
        createPotionButton.onClick.RemoveAllListeners();

        nextPotionButton.onClick.AddListener(currentPotionMakerControl.HandleNextPotion);
        previousPotionButton.onClick.AddListener(currentPotionMakerControl.HandlePreviousPotion);
        createPotionButton.onClick.AddListener(() => {
            currentPotionMakerControl.HandleStartTimerCreatePotion();
            LockButtonCreatePotion();
        });
    }

    public void LockButtonCreatePotion()
    {
        nextPotionButton.interactable = false;
        previousPotionButton.interactable = false;
        createPotionButton.interactable = false;
    }

    public void UnlockButtonCreatePotion()
    {
        nextPotionButton.interactable = true;
        previousPotionButton.interactable = true;
        createPotionButton.interactable = true;
    }

    public void UpdateVisual(PotionSO potionSO, List<PotionIngredientSO> pickedIngredients)
    {
        textPotionName.text = potionSO.potionName;
        createPotionButton.interactable = currentPotionMakerControl.IsAllIngredientsPicked();

        foreach (Transform child in potionIngredientContainer)
        {
            if (child == potionIngredientTemplate)
            {
                continue;
            }

            Destroy(child.gameObject);
        }

        List<PotionIngredientSO> potionIngredientSOs = potionSO.ingredients;

        foreach (PotionIngredientSO potionIngredientSO in potionIngredientSOs)
        {
            bool isIngredientPicked = pickedIngredients.Any(x => x.ingredientName == potionIngredientSO.ingredientName);

            Transform recipeTransform = Instantiate(potionIngredientTemplate, potionIngredientContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<PotionIngredientSingleUI>().HandleIngredientOptions(potionIngredientSO, isIngredientPicked);
        }
    }

    public void HandlePotionSituationControl(float time, float timerNormalized)
    {
        timerCreationPotion.text = time.ToString("0.00");

        resultPotionImage.fillAmount = timerNormalized;
    }
}
