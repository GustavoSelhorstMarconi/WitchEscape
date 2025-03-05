using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellControl : MonoBehaviour
{
    public static PlayerSpellControl Instance { get; private set; }

    public event EventHandler OnSelectedSpellChanged;

    [SerializeField]
    private int spellAmount;
    [SerializeField]
    private PlayerManaControl playerManaControl;
    [SerializeField]
    private PlayerInteractControl playerInteractControl;

    private List<GenericSpell> spells;
    private int indexSelectedSpell;

    private void Awake()
    {
        Instance = this;

        indexSelectedSpell = 0;
        spells = new List<GenericSpell>();
    }

    private void Update()
    {
        if (GameInput.Instance.GetPlayerChangeSelectedSpell())
        {
            UpdateSelectedSpell();
        }
        if (GameInput.Instance.GetPlayerCastSpell())
        {
            TryCastSpell();
        }
    }

    private void UpdateSelectedSpell()
    {
        indexSelectedSpell = indexSelectedSpell + 1 >= spellAmount ? 0 : indexSelectedSpell + 1;

        OnSelectedSpellChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TryCastSpell()
    {
        if (spells.Count <= indexSelectedSpell)
        {
            return;
        }

        GenericSpell spell = spells[indexSelectedSpell];

        if (!spell.IsInCooldown() && playerManaControl.GetCurrentMana() >= spell.GetManaCost())
        {
            if (spell.GetTypeSpell() == GenericSpell.TypeSpell.CastSpellWhenInteractWithObject)
            {
                IInteractable interactable = playerInteractControl.GetInteractableByType(PlayerInteractControl.InteractType.CastSpellInteract);
                BaseInteract baseInteract = interactable as BaseInteract;

                if (baseInteract != null)
                {
                    spell.CastSpell(false);
                    baseInteract.Interact();
                    playerManaControl.HandleManaValue(spell.GetManaCost());
                }
            }
            else if (spell.GetTypeSpell() == GenericSpell.TypeSpell.CastSpellAniwhere)
            {
                spell.CastSpell(true);
                playerManaControl.HandleManaValue(spell.GetManaCost());
            }
        }
    }

    public int GetIndexSelectedSpell()
    {
        return indexSelectedSpell;
    }

    public int HandleInteractNewSpell(GenericSpell genericSpell)
    {
        if (spells.Count < spellAmount)
        {
            spells.Add(genericSpell);

            return spells.Count - 1;
        }
        else
        {
            spells[indexSelectedSpell] = genericSpell;

            return indexSelectedSpell;
        }
    }
}
