using System;
using UnityEngine;

public class GenericSpell : MonoBehaviour
{
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private SpellEffectDataSO spellEffectData;
    [SerializeField]
    private TypeSpell typeSpell;

    private bool isInCooldown;
    private float currentCooldownTimer;

    public enum TypeSpell
    {
        CastSpellAniwhere,
        CastSpellWhenInteractWithObject,
    }

    private void Update()
    {
        HandleCooldown();
    }

    private void HandleCooldown()
    {
        if (isInCooldown)
        {
            currentCooldownTimer -= Time.deltaTime;

            if (currentCooldownTimer <= 0f)
            {
                isInCooldown = false;
            }
        }
    }

    protected virtual void SpellEffect()
    {
        Debug.Log("Efeito genérico.");
    }

    public void CastSpell(bool useEffect)
    {
        isInCooldown = true;
        currentCooldownTimer = cooldownTime;

        Vector3 spawnSpellEffectPosition = transform.position;
        spawnSpellEffectPosition.y -= spellEffectData.yAdjustPosition;

        if (useEffect)
        {
            SpellEffect();
        }

        Instantiate(spellEffectData.spellEffect, spawnSpellEffectPosition, Quaternion.identity);
    }

    public bool IsInCooldown()
    {
        return isInCooldown;
    }

    public float GetManaCost()
    {
        return spellEffectData.manaCost;
    }

    public TypeSpell GetTypeSpell()
    {
        return typeSpell;
    }

    public Color GetSpellColorBackgroundUI()
    {
        return spellEffectData.backgroundColorUI;
    }
}
