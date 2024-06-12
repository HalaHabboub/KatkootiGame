using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOnTrigger = false;
    public GameObject moverObject = null;


    private bool isVisible = false;

    void Start()
    {
    }

    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                Debug.Log("Enter: parent on me");
                other.transform.parent = this.transform;
            }

            if (hitBoxOnTrigger)
            {
                Debug.Log("Enter: got hit, game over");
                other.GetComponent<BirdPlayer>().Die();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                Debug.Log("Exit: parent on me");
                other.transform.parent = null;
            }
        }
    }
}
