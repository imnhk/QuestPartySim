using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    int MaxHp;
    [SerializeField]
    int destroyScore;
    [SerializeField]
    float impulseThreshold;
    [SerializeField]
    float damageMultiplier;

    Rigidbody rb;
    Material material;
    Color startColor;
    int hp;
    float lastCollisionTime;

    AudioSource audioSrc;
    ParticleSystem particle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(rb);

        material = GetComponent<Renderer>().material;
        startColor = material.color;
        Assert.IsNotNull(material);

        audioSrc = GetComponent<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        Assert.IsTrue(MaxHp > 0);
        hp = MaxHp;
        lastCollisionTime = 0;
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.game.DestroyCount += 1;
            GameManager.Instance.game.Score += destroyScore;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time < lastCollisionTime + 0.03f)
            return;
        lastCollisionTime = Time.time;

        float impulse = GetImpulse(collision);
        
        if (impulse > impulseThreshold)
        {
            int damage = (int)(impulse * damageMultiplier);
            hp -= damage;

            material.color = Color.Lerp(Color.black, startColor, (float)hp / MaxHp);

            GameManager.Instance.game.TotalDamage += damage;
            GameManager.Instance.game.Score += damage;
            UIManager.Instance.MakeDamagePopup(collision.GetContact(0).point, damage);
            if (audioSrc)
                audioSrc.Play();
            if (particle)
            {
                particle.transform.position = collision.GetContact(0).point;
                particle.Play();
            }
        }
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
            impulse = rb.mass * rb.velocity.magnitude * 2;
        }

        return impulse;
    }
    
}
