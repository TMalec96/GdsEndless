using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 screenBoundaries;
    public static GameObject DraggedInstance;
    public void Start()
    {
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.x));
    }
    

    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;

    public void OnPointerClick(PointerEventData eventData)
    {
        DraggedInstance = gameObject;
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggedInstance = gameObject;
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _startPosition = transform.position;
        _zDistanceToCamera = Mathf.Abs(_startPosition.z - Camera.main.transform.position.z);
        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.touchCount > 1)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
            ) + _offsetToMouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(DraggedInstance);
        DraggedInstance = null;
        _offsetToMouse = Vector3.zero;
    }

    
}
