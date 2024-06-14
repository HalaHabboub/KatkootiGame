using System.Runtime.CompilerServices;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;

    public GameObject moverObject = null;

    public float destroyXPosition = 100f; // Set this to the x position threshold for destruction

    private bool isVisible = false;

    void Start()
    {
    }

    void Update()
    {
        if (this.CompareTag("car"))
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        // Check if the object has passed the destroyXPosition
        if (this.transform.position.x == -0.000165844)
        {
            Destroy(gameObject);
        }
    }
}
