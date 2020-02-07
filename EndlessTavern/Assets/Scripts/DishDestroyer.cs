using UnityEngine;

public class DishDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Destroy(obj);
    }
}
