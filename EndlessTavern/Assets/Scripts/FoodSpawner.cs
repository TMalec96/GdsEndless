using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> dishes = new List<GameObject>();
    [SerializeField]
    float dishSpeed = 2;
    [SerializeField]
    float dishSpawnInterval = 1;
    private int lastIndex = 0;
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
        int indexOfDishToSpawn = 0;
        do
        {
            indexOfDishToSpawn = Random.Range(0, dishes.Count);
        } while (indexOfDishToSpawn == lastIndex);
        GameObject objectToSpawn = Instantiate(dishes[indexOfDishToSpawn]) as GameObject;
        objectToSpawn.transform.position = this.gameObject.transform.position;
        objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(-dishSpeed, 0);
        lastIndex = indexOfDishToSpawn;
    }
    IEnumerator dishServings()
    {
        while (true)
        {
            yield return new WaitForSeconds(dishSpawnInterval);
            spawnDish();
        }
    }
    public void setDishSpeed(float speed)
    {
        dishSpeed = speed;
    }
}
