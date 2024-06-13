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
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        anim = katkooti.GetComponent<Animator>();
        particleSystem = this.GetComponent<ParticleSystem>();
        particleSystem.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (dead) return;
        CheckIfCanMove();
        Move();
    }

    void Move()
    {
        if (!dead)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && canMoveForward)
            {
                transform.Translate(Vector3.forward * moveDistance);
                SetMoveForwardState();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && canMoveBackward)
            {
                transform.Translate(Vector3.back * moveDistance);
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

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
    }

    public void Die()
    {
        Manager.instance.GameOver();
        dead = true;
        anim.SetBool("dead", true);
        particleSystem.Play();
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
        if (collider.CompareTag("river"))
        {
            Die();
        }
    }


}
