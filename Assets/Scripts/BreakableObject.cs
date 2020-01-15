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

    Material material;
    Color startColor;
    int hp;
    float lastCollisionTime;

    HitParticle particle;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        startColor = material.color;
        particle = GetComponent<HitParticle>();
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
            GameManager.Instance.game.destroyCount += 1;
            GameManager.Instance.game.score += destroyScore;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time < lastCollisionTime + 0.05f)
            return;
        lastCollisionTime = Time.time;

        float impulse = GetImpulse(collision);
        
        if (impulse > impulseThreshold)
        {
            int damage = (int)(impulse * damageMultiplier);
            hp -= damage;

            material.color = Color.Lerp(Color.black, startColor, (float)hp / MaxHp);

            particle.Play(collision.GetContact(0).point);

            UIManager.Instance.MakeDamagePopup(collision.GetContact(0).point, damage);
            GameManager.Instance.game.totalDamage += damage;
            GameManager.Instance.game.score += damage;
        }
    }

    float GetImpulse(Collision coll)
    {
        float impulse;
        if (coll.rigidbody != null)
            impulse = coll.rigidbody.mass * coll.relativeVelocity.magnitude;
        else
            impulse = GetComponent<Rigidbody>().mass * coll.relativeVelocity.magnitude;

        return impulse;
    }

}
