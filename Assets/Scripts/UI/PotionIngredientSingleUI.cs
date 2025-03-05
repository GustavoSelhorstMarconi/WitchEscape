using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionIngredientSingleUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ingredientName;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private GameObject checkedImage;

    public void HandleIngredientOptions(PotionIngredientSO potionIngredient, bool isChecked)
    {
        ingredientName.text = potionIngredient.ingredientName;
        ingredientName.color = potionIngredient.textColor;
        backgroundImage.color = potionIngredient.ingredientBackgroundColor;
        checkedImage.SetActive(isChecked);
    }
}
