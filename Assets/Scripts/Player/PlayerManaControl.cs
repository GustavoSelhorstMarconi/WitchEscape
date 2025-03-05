using System;
using UnityEngine;

public class PlayerManaControl : MonoBehaviour
{
    public static PlayerManaControl Instance { get; private set; }

    public event EventHandler OnUpdateMana;

    [SerializeField]
    private float maxMana;

    private float currentMana;

    private void Awake()
    {
        Instance = this;
        currentMana = maxMana;
    }

    public void HandleManaValue(float amountSpent)
    {
        currentMana = currentMana - amountSpent <= 0f ? 0f : currentMana - amountSpent;

        OnUpdateMana?.Invoke(this, EventArgs.Empty);
    }

    public float GetManaNormalized()
    {
        return currentMana / maxMana;
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }
}
