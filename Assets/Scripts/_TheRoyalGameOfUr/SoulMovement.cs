using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour {


    Transform orbitPoint;

    public int teamIndex;
    public float speed=2;
    public float orbitRadius=0.5f;

    private void Start()
    {
        if (teamIndex == 0)
        {
            orbitPoint = GameObject.FindGameObjectWithTag("PlayerPool").transform;
        }
        else
        {
            orbitPoint = GameObject.FindGameObjectWithTag("EnemyPool").transform;
        }
    }

    void Update ()
    {
        transform.LookAt(orbitPoint.position);
        transform.position += transform.right*Time.deltaTime * speed;
        if ((transform.position - orbitPoint.position).magnitude > orbitRadius)
        {
            transform.position = Vector3.Lerp(transform.position, orbitPoint.position, speed * Time.deltaTime);
        }
	}
}
