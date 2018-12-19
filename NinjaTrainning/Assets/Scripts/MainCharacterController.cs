using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private bool canControlled;

    public float moveSpeed = 50f;
    public float maxSpeed = 200f;
    // Use this for initialization

    private bool CanApplyUpdate()
    {
        if(null == rb)
        {
            rb = GetComponent<Rigidbody>();
            return false;
        }
        if(null == anim)
        {
            anim = GetComponent<Animator>();
            return false;
        }
        return true;
    }
	void Start ()
    {
        CanApplyUpdate();
        canControlled = false;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(false == CanApplyUpdate() || false == canControlled)
        {
            return;
        }

        float inputHorizontal = Input.GetAxis("Horizontal");
        if(0 != inputHorizontal)
        {
            rb.AddForce(Vector3.right * -1 * inputHorizontal * moveSpeed);
            anim.SetFloat("speed", inputHorizontal * moveSpeed);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetFloat("speed", 0);
            anim.SetBool("isIdle", true);
        }
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
	}

    public void OnDeath()
    {
        rb.velocity = Vector3.zero;
        canControlled = false;
        anim.SetTrigger("deathTrigger");
    }

    public void OnBeginGame()
    {
        canControlled = true;
    }
}
