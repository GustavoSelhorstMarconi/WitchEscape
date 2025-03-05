using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion data", menuName = "Scriptable Objects/PotionSO")]
public class PotionSO : ScriptableObject
{
    public string potionName;
    public float maxTimerCreatePotion;
    public GameObject potion;
    public List<PotionIngredientSO> ingredients;
}
