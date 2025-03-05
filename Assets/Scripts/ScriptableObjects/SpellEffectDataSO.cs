using UnityEngine;

[CreateAssetMenu(fileName = "SpellEffectData", menuName = "Scriptable Objects/SpellEffectData")]
public class SpellEffectDataSO : ScriptableObject
{
    public float yAdjustPosition;
    public float manaCost;
    public GameObject spellEffect;
    public Color backgroundColorUI;
}
