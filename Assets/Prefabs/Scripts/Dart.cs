using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody rb;
    private Quaternion initialRotation;
    private OVRGrabbable grabbable;
    private bool stuck = false;
    private GameObject stuckObject = null;

    public const float threshold = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();

        initialRotation = Quaternion.Euler(0, -90, 0);
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

        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = collision.rigidbody;
        fixedJoint.enableCollision = false;
        fixedJoint.breakForce = 1f;

        rb.isKinematic = true;
        grabbable.enabled = false;
        this.GetComponent<Collider>().enabled = false;
    }

}
