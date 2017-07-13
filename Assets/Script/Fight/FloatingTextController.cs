using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("myCanvas");
        popupText = Resources.Load<FloatingText>("PopupText/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        if (popupText == null)
        {
            Initialize();
        }
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x, location.position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.GetComponent<FloatingText>().SetText(text);
    }
}
