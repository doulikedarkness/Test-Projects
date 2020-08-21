using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropFromDragItems : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragItems drag = eventData.pointerDrag.GetComponent<DragItems>();
        if(drag != null)
        {
            drag.transform.SetParent(transform);
        }
    }
}
