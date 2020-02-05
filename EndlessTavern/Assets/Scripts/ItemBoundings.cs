using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoundings : MonoBehaviour
{
    private Vector2 screenBoundaries;
    // Start is called before the first frame update
    void Start()
    {
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.x));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBoundaries.x, screenBoundaries.x * -1);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBoundaries.y, screenBoundaries.y * -1);
        transform.position = viewPosition;
    }
}
