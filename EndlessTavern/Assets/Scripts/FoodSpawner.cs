using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> dishes = new List<GameObject>();
    [SerializeField]
    float dishSpeed = 6;
    [SerializeField]
    float dishSpawnInterval = 1;
    void Start()
    {
        StartCoroutine(dishServings());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnDish()
    {
        GameObject objectToSpawn = Instantiate(dishes[Random.Range(0, dishes.Count)]) as GameObject;
        objectToSpawn.transform.position = this.gameObject.transform.position;
        objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(-dishSpeed, 0);
    }
    IEnumerator dishServings()
    {
        while (true)
        {
            yield return new WaitForSeconds(dishSpawnInterval);
            spawnDish();
        }
    }
}
