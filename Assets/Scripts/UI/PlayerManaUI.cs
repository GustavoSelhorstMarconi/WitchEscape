using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaUI : MonoBehaviour
{
    [SerializeField]
    private float manaUpdateDuration;
    [SerializeField]
    private Image manaValueImage;

    private float baseManaUpdate;
    private float targetManaUpdate;
    private bool canManaUpdate;
    private float currentManaUpdateTime;

    private void Start()
    {
        PlayerManaControl.Instance.OnUpdateMana += PlayerManaControl_OnUpdateMana;

        UpdateManaValue();
    }

    private void PlayerManaControl_OnUpdateMana(object sender, EventArgs e)
    {
        UpdateManaValue();
    }

    private void Update()
    {
        HandleManaUpdate();
    }

    private void HandleManaUpdate()
    {
        if (canManaUpdate)
        {
            manaValueImage.fillAmount = Mathf.Lerp(baseManaUpdate, targetManaUpdate, currentManaUpdateTime / manaUpdateDuration);
            currentManaUpdateTime += Time.deltaTime;

            if (manaValueImage.fillAmount == targetManaUpdate)
            {
                canManaUpdate = false;
            }
        }
    }

    private void UpdateManaValue()
    {
        baseManaUpdate = manaValueImage.fillAmount;
        currentManaUpdateTime = 0f;
        targetManaUpdate = PlayerManaControl.Instance.GetManaNormalized();
        canManaUpdate = true;
    }

    private void OnDestroy()
    {
        PlayerManaControl.Instance.OnUpdateMana -= PlayerManaControl_OnUpdateMana;
    }
}
