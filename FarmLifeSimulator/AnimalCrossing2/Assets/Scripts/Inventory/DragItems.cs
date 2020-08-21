using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform canvas;
    public Transform old;

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        old = transform.parent;
        transform.SetParent(canvas);
        
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            //Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        if(transform.parent == canvas)
        {
            transform.SetParent(old);
        }
    }

}
