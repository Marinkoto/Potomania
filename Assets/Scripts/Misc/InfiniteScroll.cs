using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [Header("Components")]
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform content;
    [Header("Parameters")]
    [SerializeField] float itemSize = 100f; // Use itemSize instead of itemHeight to support both directions
    [SerializeField] int firstItemIndex = 0;
    [SerializeField] int lastItemIndex = 0;
    [SerializeField] int maxItems = 20;
    [SerializeField] float smoothScrollSpeed = 5f;
    [SerializeField] bool isHorizontal = false; // Flag to determine if scrolling is horizontal

    private float contentLength;
    private float scrollRectLength;

    void Start()
    {
        CalculateLengths();
    }

    void CalculateLengths()
    {
        contentLength = maxItems * itemSize;
        if (isHorizontal)
        {
            content.sizeDelta = new Vector2(contentLength, content.sizeDelta.y);
            scrollRectLength = scrollRect.GetComponent<RectTransform>().rect.width;
        }
        else
        {
            content.sizeDelta = new Vector2(content.sizeDelta.x, contentLength);
            scrollRectLength = scrollRect.GetComponent<RectTransform>().rect.height;
        }
    }

    public void ScrollToFirstItem()
    {
        float normalizedPos = isHorizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
        if (normalizedPos > 0)
        {
            float pos = firstItemIndex * itemSize;
            normalizedPos = 1f - (pos / (contentLength - scrollRectLength));
            StartCoroutine(SmoothScrollToPosition(normalizedPos));
        }
    }

    public void ScrollToLastItem()
    {
        float normalizedPos = isHorizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
        if (normalizedPos < 1)
        {
            float pos = (lastItemIndex + 1) * itemSize - scrollRectLength;
            normalizedPos = 1f - (pos / (contentLength - scrollRectLength));
            StartCoroutine(SmoothScrollToPosition(normalizedPos));
        }
    }

    IEnumerator SmoothScrollToPosition(float targetPosition)
    {
        float currentPos = isHorizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
        float elapsedTime = 0;
        float duration = Mathf.Abs(targetPosition - currentPos) * smoothScrollSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (isHorizontal)
                scrollRect.horizontalNormalizedPosition = Mathf.Lerp(currentPos, targetPosition, elapsedTime / duration);
            else
                scrollRect.verticalNormalizedPosition = Mathf.Lerp(currentPos, targetPosition, elapsedTime / duration);
            yield return null;
        }
        if (isHorizontal)
            scrollRect.horizontalNormalizedPosition = targetPosition;
        else
            scrollRect.verticalNormalizedPosition = targetPosition;
    }

    public void OnScrollValueChanged()
    {
        float normalizedPos = isHorizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
        float scrollLength = contentLength - scrollRectLength;
        float offset = normalizedPos * scrollLength;

        firstItemIndex = Mathf.Clamp(Mathf.FloorToInt(offset / itemSize), 0, maxItems - 1);
        lastItemIndex = Mathf.Clamp(Mathf.CeilToInt((offset + scrollRectLength) / itemSize) - 1, 0, maxItems - 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.StopMovement();
        scrollRect.enabled = false;
    }
}
