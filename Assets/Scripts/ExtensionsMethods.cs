using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionsMethods
{
    public static bool IsPointerOverUIObject(this EventSystem eventSystem)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };

        List<RaycastResult> results = new List<RaycastResult>();

        // Указываем маску слоев для UI
        int layerMask = 1 << LayerMask.NameToLayer("UI");

        // Выполняем Raycast только для UI
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        int count = 0;

        foreach (var item in results)
        {
            // Проверяем, что объект находится на слое UI
            if (item.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                count++;
            }
        }

        return count > 0;
    }
}
