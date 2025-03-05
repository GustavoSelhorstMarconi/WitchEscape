using System;
using UnityEngine;

public class SpellInteract : BaseInteract
{
    [SerializeField]
    private GameObject spell;

    public override void Interact(object param = default)
    {
        spell.SetActive(true);
        GenericSpell genericSpell = spell.GetComponent<GenericSpell>();

        int indexSpell = PlayerSpellControl.Instance.HandleInteractNewSpell(genericSpell);
        PlayerSelectSpellUI.Instance.HandleCardColor(indexSpell, genericSpell.GetSpellColorBackgroundUI());
        base.Interact();
    }
}
