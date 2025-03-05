using System;
using UnityEngine;

public class PlayerEffectsControl : MonoBehaviour
{
    [SerializeField]
    private float invisibleDuration;

    public static PlayerEffectsControl Instance { get; private set; }

    private bool isInvisible;
    private float currentInvisibleDuration;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleInvisibleEffect();
    }

    private void HandleInvisibleEffect()
    {
        if (isInvisible)
        {
            currentInvisibleDuration -= Time.deltaTime;

            if (currentInvisibleDuration <= 0f)
            {
                isInvisible = false;
            }
        }
    }

    public void ApplyInvisibleEffect()
    {
        isInvisible = true;
        currentInvisibleDuration = invisibleDuration;
    }
}
