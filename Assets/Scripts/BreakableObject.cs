using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    int destroyScore = 100;

    [SerializeField]
    private float damageThreshold = 3;
    [SerializeField]
    private float breakThreshold = 10;

    [SerializeField]
    GameObject brokenObjPrefab;

    Rigidbody rb;
    //Material material;
    Color startColor;
    //int hp;
    float lastCollisionTime = 0;

    ParticleSystem particle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void BreakObject()
    {
        GameManager.Instance.game.DestroyCount += 1;
        GameManager.Instance.game.AddScore(destroyScore);

        if (brokenObjPrefab)
        {
            GameObject.Instantiate(brokenObjPrefab, transform.position, transform.rotation);
        }
        if (particle)
        {
            particle.Play();
        }

        Destroy(this.gameObject);
    }

    private void DamageObject(Collision collision, float impulse)
    {
        int damage = (int)impulse;
        GameManager.Instance.game.AddScore(damage);

        if (particle)
        {
            particle.transform.position = collision.GetContact(0).point;
            particle.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (HadCollisionIn(0.3f))
            return;

        float impulse = GetImpulse(collision);

        if( impulse > breakThreshold )
        {
            BreakObject();
            return;
        }
        
        if (impulse > damageThreshold )
        {
            DamageObject(collision, impulse);
            return;
        }
    }

    bool HadCollisionIn(float minCollisionTime)
    {
        if (Time.time < lastCollisionTime + minCollisionTime)
            return true;
        lastCollisionTime = Time.time;
        return false;
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
