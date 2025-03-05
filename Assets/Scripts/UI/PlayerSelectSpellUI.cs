using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectSpellUI : MonoBehaviour
{
    public static PlayerSelectSpellUI Instance { get; private set; }

    [SerializeField]
    private List<SpellCardUI> spellCards;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerSpellControl.Instance.OnSelectedSpellChanged += PlayerSpellControl_OnSelectedSpellChanged;

        HandleSelectedSpell();
    }

    private void PlayerSpellControl_OnSelectedSpellChanged(object sender, System.EventArgs e)
    {
        HandleSelectedSpell();
    }

    private void HandleSelectedSpell()
    {
        int indexSelectedSpell = PlayerSpellControl.Instance.GetIndexSelectedSpell();

        for (int i = 0; i < spellCards.Count; i++)
        {
            spellCards[i].StartSelectedCard(i == indexSelectedSpell);
        }
    }

    public void HandleCardColor(int cardIndex, Color backgroundColor)
    {
        spellCards[cardIndex].HandleBackgroundColor(backgroundColor);
    }
}
