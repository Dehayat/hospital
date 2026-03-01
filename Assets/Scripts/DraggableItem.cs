using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector3 mouseOffset;

    public void OnBeginDrag(PointerEventData data)
    {
        var rt = GetComponent<RectTransform>();
        var m_DraggingPlane = data.pointerEnter.transform as RectTransform;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out Vector3 globalMousePos);
        mouseOffset = rt.position - globalMousePos;
    }

    public void OnDrag(PointerEventData data)
    {
        var rt = GetComponent<RectTransform>();
        if (data.pointerEnter == null)
        {
            return;
        }
        var m_DraggingPlane = data.pointerEnter.transform as RectTransform;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out Vector3 globalMousePos))
        {
            rt.position = globalMousePos + mouseOffset;
        }
    }
}
