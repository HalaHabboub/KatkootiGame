using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : MonoBehaviour
{
    public float moveDistance = 1;
    public float colliderDistCheck = 1.1f;

    private bool dead = false;
    private bool canMoveForward = true;
    private bool canMoveBackward = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private Animator anim;
    public ParticleSystem particle = null;
    public GameObject katkooti = null;
    private ParticleSystem ps;

    private Rigidbody rb;

    private bool canFly = false;

    public GameObject guiChallengeOne = null;
    int level = 1;

    //For voice
    public float sensitivity = 100f;
    public float loudness;

    AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = katkooti.GetComponent<Animator>();
        ps = this.GetComponent<ParticleSystem>();
        ps.Pause();


        //for audio
        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = true;


        _audio.Play();

    }


    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (dead) return;
        CheckIfCanMove();
        Move();

        if (canFly)
        {
            fly();
        }
        if (transform.position.y < -1)
        {
            Die();
        }



    }

    void Move()
    {
        if (!dead)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && canMoveForward)
            {
                transform.Translate(Vector3.forward * moveDistance);
                Manager.instance.UpdateDistanceCount();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && canMoveBackward)
            {
                transform.Translate(Vector3.back * moveDistance);
                Manager.instance.currentDistance--; //fixed the issue of counting wrong
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) && canMoveLeft)
            {
                transform.Translate(Vector3.left * moveDistance);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && canMoveRight)
            {
                transform.Translate(Vector3.right * moveDistance);
            }
        }
    }



    public void Die()
    {
        Manager.instance.GameOver();
        dead = true;
        anim.SetBool("dead", true);
        ps.Play();
    }

    void CheckIfCanMove()
    {
        canMoveForward = !Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit1, colliderDistCheck) || (hit1.collider != null && hit1.collider.tag != "collider");
        canMoveBackward = !Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit2, colliderDistCheck) || (hit2.collider != null && hit2.collider.tag != "collider");
        canMoveLeft = !Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit3, colliderDistCheck) || (hit3.collider != null && hit3.collider.tag != "collider");
        canMoveRight = !Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit4, colliderDistCheck) || (hit4.collider != null && hit4.collider.tag != "collider");

        Debug.DrawRay(transform.position, Vector3.forward * colliderDistCheck, Color.red, 0.1f);
        Debug.DrawRay(transform.position, Vector3.back * colliderDistCheck, Color.red, 0.1f);
        Debug.DrawRay(transform.position, Vector3.left * colliderDistCheck, Color.red, 0.1f);
        Debug.DrawRay(transform.position, Vector3.right * colliderDistCheck, Color.red, 0.1f);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("train") || collider.CompareTag("car"))
        {
            Die();
        }
        if (collider.CompareTag("river"))
        {
            changeFly();
            level++; //you pass a level with each river you cross
        }
        if (collider.CompareTag("safeGrass"))
        {
            guiChallengeOne.SetActive(true);
            _audio.mute = false;
        }

    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("river"))
        {
            canFly = false;
            _audio.mute = true;
        }
        if (collider.CompareTag("safeGrass"))
        {
            guiChallengeOne.SetActive(false);

        }

    }

    private void changeFly()
    {
        canFly = true;
    }
    public void fly()
    {
        // if (level == 2)
        // {
        //     transform.Translate(Vector3.forward * 0.05f);
        //     if (Input.GetButtonDown("Jump"))
        //     {
        //         rb.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        //     }

        // }
        // if (level == 1)
        // {
        transform.Translate(Vector3.forward * 0.02f);

        loudness = GetAverageVolume() * sensitivity;

        if (loudness > 4)
        {
            rb.AddForce(Vector3.up * 0.3f, ForceMode.Impulse);
        }
        // }

    }
    float GetAverageVolume()
    {

        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);

        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }

        return (a / 256f);
    }


}
