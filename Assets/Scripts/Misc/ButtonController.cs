using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.instance.SetHoverCursor();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.instance.SetDefaultCursor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorManager.instance.Click();
    }
}
