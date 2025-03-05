using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "Scriptable Objects/IngredientSO")]
public class PotionIngredientSO : ScriptableObject
{
    public string ingredientName;
    public Color ingredientBackgroundColor;
    public Color textColor;
}
