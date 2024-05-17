using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance; 
    [Header("Cursor Textures")]
    [SerializeField] Texture2D hoverCursor;
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D clickCursor;
    [Header("Hotspots")]
    [SerializeField] Vector2 defaultHotSpot;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, defaultHotSpot, CursorMode.Auto);
    }
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, defaultHotSpot, CursorMode.Auto);
    }
    private IEnumerator SetClickCursor()
    {
        Cursor.SetCursor(clickCursor, defaultHotSpot, CursorMode.Auto);
        yield return new WaitForSeconds(0.1f);
        Cursor.SetCursor(defaultCursor, defaultHotSpot, CursorMode.Auto);
    }
    public void Click()
    {
        StartCoroutine(SetClickCursor());
    }
}
