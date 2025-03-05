using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellCardUI : MonoBehaviour
{
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float speedChangeSelected;
    [SerializeField]
    private Image backgroundImage;

    private Vector3 baseScale;
    private bool canChangeScale;
    private float direction;
    private float currentValue;
    private Color baseBackgroundColor;

    private void Start()
    {
        baseBackgroundColor = backgroundImage.color;
        baseScale = transform.localScale;
    }

    private void Update()
    {
        if (canChangeScale)
        {
            ChangeScale();
        }
    }

    private void ChangeScale()
    {
        currentValue = Mathf.MoveTowards(currentValue, direction, Time.deltaTime * speedChangeSelected);

        transform.localScale = Vector3.Lerp(baseScale, baseScale * maxScale, currentValue);

        if (currentValue == direction)
        {
            canChangeScale = false;
        }
    }

    public void StartSelectedCard(bool isSelected)
    {
        canChangeScale = true;

        direction = isSelected ? 1f : 0f;
    }

    public void HandleBackgroundColor(Color backgroundColor)
    {
        backgroundImage.color = backgroundColor;
    }
}
