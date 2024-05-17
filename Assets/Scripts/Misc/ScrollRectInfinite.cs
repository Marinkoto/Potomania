using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectInfinite : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float itemHeight = 100f; // Height of each item in the scroll view
    public int firstItemIndex = 0; // Index of the first visible item
    public int lastItemIndex = 0; // Index of the last visible item
    public int maxItems = 20; // Total number of items

    private float contentHeight;
    private float scrollRectHeight;

    void Start()
    {
        CalculateHeights();
    }

    void CalculateHeights()
    {
        contentHeight = maxItems * itemHeight;
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);
        scrollRectHeight = scrollRect.GetComponent<RectTransform>().rect.height;
    }

    public void ScrollToFirstItem()
    {
        float yPos = firstItemIndex * itemHeight;
        scrollRect.verticalNormalizedPosition = 1f - (yPos / (contentHeight - scrollRectHeight));
    }

    public void ScrollToLastItem()
    {
        float yPos = (lastItemIndex + 1) * itemHeight - scrollRectHeight;
        scrollRect.verticalNormalizedPosition = 1f - (yPos / (contentHeight - scrollRectHeight));
    }

    public void OnScrollValueChanged()
    {
        float normalizedPosY = scrollRect.verticalNormalizedPosition;
        float scrollHeight = (contentHeight - scrollRectHeight);
        float yOffset = normalizedPosY * scrollHeight;

        firstItemIndex = Mathf.FloorToInt(yOffset / itemHeight);
        lastItemIndex = Mathf.CeilToInt((yOffset + scrollRectHeight) / itemHeight) - 1;

        // Limiting the first item index
        if (firstItemIndex < 0)
            firstItemIndex = 0;

        // Limiting the last item index
        if (lastItemIndex >= maxItems)
            lastItemIndex = maxItems - 1;
    }
}
