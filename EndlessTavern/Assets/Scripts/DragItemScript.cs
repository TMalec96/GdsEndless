using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 screenBoundaries;
    public static GameObject DraggedInstance;
    private bool isDragEnd = false;
    private Vector3 _startPosition;
    private Vector3 _offsetToMouse;
    private float _zDistanceToCamera;

    public bool IsDragEnd { get => isDragEnd; set => isDragEnd = value; }

    public void Start()
    {
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.x));
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
    /// <summary>
    /// Method called on drag start
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        isDragEnd = false;
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.touchCount > 1)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
            ) + _offsetToMouse;
    }
    /// <summary>
    /// Method called on drag end. 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragEnd = true;
        DraggedInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject lastDraggedItem = DraggedInstance;
        StartCoroutine(disposeLastDish(lastDraggedItem));
        DraggedInstance = null;
        _offsetToMouse = Vector3.zero;
    }

    IEnumerator disposeLastDish(GameObject objectToDestroy)
    {
        while (true)
        {
            objectToDestroy.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSecondsRealtime(1);
            Destroy(objectToDestroy);
        }
    }
}
