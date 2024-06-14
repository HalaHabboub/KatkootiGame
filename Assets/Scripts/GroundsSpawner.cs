using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundsSpawner : MonoBehaviour
{
    public List<GameObject> grounds = new List<GameObject>(); //the gorunds available
    public List<float> height = new List<float>();
    private int rndRange = 0;
    private float lastPos = 0;
    private float lastScale = 0;
    public int maxPlatforms = 20;

    private List<GameObject> instantiatedGrounds = new List<GameObject>();

    [SerializeField] GameObject katkooti;

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            randomGenerator();
        }
    }

    // Update is called once per frame
    void Update()
    {
        DestroyExcessPlatforms();
    }

    public void randomGenerator()
    {
        int num = Random.Range(0, grounds.Count - 2);
        GameObject newGround = Instantiate(grounds[num]) as GameObject;

        float offset = lastPos + (lastScale * 0.5f);
        offset += (newGround.transform.localScale.z) * 0.5f;

        Vector3 pos = new Vector3(0, height[num], offset);

        newGround.transform.position = pos;

        lastPos = newGround.transform.position.z;
        lastScale = newGround.transform.localScale.z;

        newGround.transform.parent = this.transform;

        instantiatedGrounds.Add(newGround);
    }
    public void challengeGenerator()
    {
        safeGrass();

        GameObject newGround = Instantiate(grounds[5]) as GameObject;

        float offset = lastPos + (lastScale * 0.5f);
        offset += (newGround.transform.localScale.z) * 0.5f;

        Vector3 pos = new Vector3(0, height[5], offset);

        newGround.transform.position = pos;

        lastPos = newGround.transform.position.z;
        lastScale = newGround.transform.localScale.z;

        newGround.transform.parent = this.transform;

        instantiatedGrounds.Add(newGround);
        normalGrass();



    }

    private void safeGrass()
    {
        GameObject safeGrass = Instantiate(grounds[6]) as GameObject;

        float offset = lastPos + (lastScale * 0.5f);
        offset += (safeGrass.transform.localScale.z) * 0.5f;

        Vector3 pos = new Vector3(0, height[6], offset);

        safeGrass.transform.position = pos;

        lastPos = safeGrass.transform.position.z;
        lastScale = safeGrass.transform.localScale.z;

        safeGrass.transform.parent = this.transform;

        instantiatedGrounds.Add(safeGrass);
    }
    private void normalGrass()
    {
        GameObject safeGrass = Instantiate(grounds[4]) as GameObject;

        float offset = lastPos + (lastScale * 0.5f);
        offset += (safeGrass.transform.localScale.z) * 0.5f;

        Vector3 pos = new Vector3(0, height[4], offset);

        safeGrass.transform.position = pos;

        lastPos = safeGrass.transform.position.z;
        lastScale = safeGrass.transform.localScale.z;

        safeGrass.transform.parent = this.transform;

        instantiatedGrounds.Add(safeGrass);
    }

    void DestroyExcessPlatforms()
    {
        if (katkooti.transform.position.x - instantiatedGrounds[0].transform.position.x > 20)
        {
            GameObject oldGround = instantiatedGrounds[0];
            instantiatedGrounds.RemoveAt(0);
            Destroy(oldGround);
        }
    }
}
