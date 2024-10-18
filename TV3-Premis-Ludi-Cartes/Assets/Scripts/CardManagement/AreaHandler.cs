using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AreaHandler : MonoBehaviour
{
    [Header("States")]
    public bool isHovering;

    // Reference to the UI element's RectTransform
    private RectTransform targetRectTransform;

    // Know if there is any selected card from the other Area (not checking if all the time)
    public GameObject otherArea;

    // Start is called before the first frame update
    void Start()
    {
        targetRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (otherArea.GetComponent<HorizontalCardHolder>().selectedCard != null) { isHovering = IsMouseOverUIElement(); }
    }

    // Function to check if the mouse is over the UI element
    bool IsMouseOverUIElement()
    {
        // Convert mouse position to world point (in RectTransform's local space)
        Vector2 localPoint;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetRectTransform, Input.mousePosition, Camera.main, out localPoint);

        // Return true only if the point is inside the rectangle bounds
        return isInside && targetRectTransform.rect.Contains(localPoint);
    }
}
