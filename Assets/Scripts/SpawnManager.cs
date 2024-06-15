using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint = null;

    public float minDelay = 1.5f;
    public float maxDelay = 5.0f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 4.0f;

    public bool notCars = false;
    public int minSpawnCount = 10;
    public int maxSpawnCount = 15;

    private float lastSpawnTime = 0;
    private float nextSpawnDelay = 0;
    private float objectSpeed = 0;

    public List<GameObject> spawnableItems = new List<GameObject>();

    void Start()
    {
        if (notCars)
        {
            int totalSpawnCount = 10;//Random.Range(minSpawnCount, maxSpawnCount);

            for (int i = 0; i < totalSpawnCount; i++)
            {
                SpawnObject();
            }
        }
        else
        {
            objectSpeed = Random.Range(minSpeed, maxSpeed);
            nextSpawnDelay = Random.Range(minDelay, maxDelay);
        }
    }

    void Update()
    {
        if (notCars) return;

        if (Time.time > lastSpawnTime + nextSpawnDelay)
        {
            lastSpawnTime = Time.time;
            nextSpawnDelay = Random.Range(minDelay, maxDelay);
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Debug.Log("Spawn Object");
        int randomIndex = Random.Range(0, spawnableItems.Count);
        GameObject selectedItem = spawnableItems[randomIndex];
        GameObject spawnedObject = Instantiate(selectedItem) as GameObject;
        spawnedObject.transform.position = GetSpawnPosition();

        if (!notCars)
        {
            spawnedObject.GetComponent<Mover>().speed = objectSpeed;
        }

        // Set the parent of the spawned object to the SpawnManager object
        spawnedObject.transform.parent = this.transform;
    }

    Vector3 GetSpawnPosition()
    {
        if (notCars)
        {
            // Adjust the spawn range to be more balanced around the spawnPoint
            float randomXOffset = Random.Range(-7.0f, 30.0f);
            return new Vector3(spawnPoint.position.x + randomXOffset, spawnPoint.position.y, spawnPoint.position.z);
        }
        else
        {
            return spawnPoint.position;
        }
    }
}
