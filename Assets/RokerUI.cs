using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RokerUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(111);
    }
    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = Input.mousePosition;
        Debug.Log(222);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(333);
    }
}
