using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> platform = new List<GameObject>();
    public List<float> height = new List<float>();
    public int maxPlatforms = 300; // The maximum number of platforms to keep active

    private int rndRange = 0;
    private float lastPos = 0;
    private float lastScale = 0;
    private List<GameObject> instantiatedPlatforms = new List<GameObject>();

    public void RandomGenerator()
    {
        // rndRange = Random.Range(0, platform.Count);

        for (int i = 0; i < platform.Count; i++)
        {
            CreateLevelObject(platform[i], height[i], i);
        }

        DestroyExcessPlatforms();
    }
    public void challengeGenerator()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject newObj = Instantiate(platform[5]) as GameObject;

            float offset = lastPos + (lastScale * 0.5f);
            offset += (newObj.transform.localScale.z) * 0.5f;

            Vector3 pos = new Vector3(0, height[5], offset);

            newObj.transform.position = pos;

            lastPos = newObj.transform.position.z;
            lastScale = newObj.transform.localScale.z;

            newObj.transform.parent = this.transform;

            instantiatedPlatforms.Add(newObj);
        }

        DestroyExcessPlatforms();
    }

    public void CreateLevelObject(GameObject obj, float height, int value)
    {
        if (rndRange == value)
        {
            GameObject newObj = Instantiate(obj) as GameObject;

            float offset = lastPos + (lastScale * 0.5f);
            offset += (newObj.transform.localScale.z) * 0.5f;

            Vector3 pos = new Vector3(0, height, offset);

            newObj.transform.position = pos;

            lastPos = newObj.transform.position.z;
            lastScale = newObj.transform.localScale.z;

            newObj.transform.parent = this.transform;

            instantiatedPlatforms.Add(newObj);
        }
    }

    void Start()
    {
        // Generate initial platforms to cover the start area
        for (int i = 0; i < maxPlatforms / 10; i++)
        {
            RandomGenerator();
        }
    }

    void Update()
    {

    }

    void DestroyExcessPlatforms()
    {
        // Ensure we only keep a maximum number of platforms
        while (instantiatedPlatforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = instantiatedPlatforms[0];
            instantiatedPlatforms.RemoveAt(0);
            Destroy(oldPlatform);
        }
    }
}
