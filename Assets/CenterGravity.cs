﻿using UnityEngine;
using System.Collections;

public class CenterGravity : MonoBehaviour
{

    public Transform target; // Big object

    Vector3 targetDirection;

    public int radius = 5;
    public int forceAmount = 100;
    public float gravity = 0;
    private Rigidbody rb;

    private float mass;
    private float distance;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Use this for initialization
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {

        targetDirection = target.position - transform.position; // Save direction
        distance = targetDirection.magnitude; // Find distance between this object and target object
        targetDirection = targetDirection.normalized; // Normalize target direction vector

        if (distance < radius)
        {
            rb.AddForce(targetDirection * forceAmount * Time.deltaTime, ForceMode.Acceleration);
        }


    }
}