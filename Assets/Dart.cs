using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody rb;
    private Quaternion initialRotation;
    private Transform initialParent;
    private FixedJoint fixedJoint;

    private bool stuck = false;
    private GameObject stuckObject = null;

    public const float threshold = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedJoint = GetComponent<FixedJoint>();
        fixedJoint.

        initialRotation = Quaternion.Euler(0, -90, 0);
        initialParent = transform.parent;
    }

    void Update()
    {
        if(rb.velocity.magnitude > threshold)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * initialRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < 1f)
            return;

        transform.SetParent(collision.gameObject.transform);
        fixedJoint.connectedBody = collision.rigidbody;
        fixedJoint.breakForce = 2f;
    }

}
