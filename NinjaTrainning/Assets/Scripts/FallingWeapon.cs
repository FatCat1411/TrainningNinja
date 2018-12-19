using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWeapon : MonoBehaviour
{
    private bool hasTouchGround;
    private float slowness;
    public float Slowness
    {
        set
        {
            slowness = value;
            gameObject.GetComponent<ConstantForce>().force = new Vector3(0, slowness, 0);
        }
        get
        {
            return slowness;
        }
    }

	// Use this for initialization
	void Start ()
    {
        hasTouchGround = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Terrain")
        {
            Rigidbody rg = GetComponent<Rigidbody>();
            rg.useGravity = false;
            rg.velocity = Vector3.zero;
            BoxCollider bc = GetComponent<BoxCollider>();
            bc.enabled = false;
            Destroy(gameObject, 3);
            hasTouchGround = true;
            gameObject.GetComponent<ConstantForce>().enabled = false;
        }
        if(false == hasTouchGround && other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            NJ_GameController.Current.OnGameOver();
        }
    }
}
