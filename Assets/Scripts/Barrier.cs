using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("train") || collider.CompareTag("car"))
        {
            Destroy(collider.gameObject);
        }

    }

}
