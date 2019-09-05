using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour {

    public float maxForce=15;
    public float minForce=15;
    public float centreHeight;
    public int teamIndex;

    public bool isRolling = false;

    public Transform[] scorePoints;

    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        isRolling = rb.velocity.magnitude != 0;

        if ((!isRolling)&&(transform.position.y>centreHeight))
        {
            RollDice();
        }        
	}

    public void RollDice()
    {      
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized*Random.Range(minForce,maxForce));
    }
    public void SoftRoll()
    {
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized * 0.1f*Random.Range(minForce, maxForce));
    }
    public bool CheckScore()
    {
        bool score = false;

        foreach (Transform obj in scorePoints)
        {
            if (obj.transform.position.y - transform.position.y > 0)
            {
                score = true;
            }
        }
        return score;
    }
}
