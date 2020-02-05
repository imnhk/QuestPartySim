using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckObject : MonoBehaviour
{
    [SerializeField]
    private float threshold;
    [SerializeField]
    private int unstuckScore = 100;

    private Rigidbody rb;
    private bool isStuck = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck)
            return;

        if(GetImpulse(collision)> threshold)
        {
            UnStuck();
        }
    }

    public void UnStuck()
    {
        isStuck = false;

        rb.constraints = RigidbodyConstraints.None;
        GameManager.Instance.game.AddScore(unstuckScore);
    }

    
    float GetImpulse(Collision coll)
    {
        float impulse;
        if (coll.rigidbody != null)
        {
            impulse = rb.mass * rb.velocity.magnitude + coll.rigidbody.mass * coll.rigidbody.velocity.magnitude;
        }
        else
        {
            // 움직이지 않는 물체에 부딪힐 경우 (벽, 바닥 등)
            impulse = rb.mass * rb.velocity.magnitude;
        }

        return impulse;
    }
}
