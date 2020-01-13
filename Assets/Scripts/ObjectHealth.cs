using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectHealth : MonoBehaviour
{
    public int MaxHp;

    Material material;
    Color startColor;
    int hp;
    float lastCollisionTime;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        startColor = material.color;
    }

    void Start()
    {
        Assert.IsTrue(MaxHp > 0);
        hp = MaxHp;
        lastCollisionTime = Time.time;
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (Time.time < lastCollisionTime + 0.1f)
            return;
        lastCollisionTime = Time.time;

        float impulse;
        if (collision.rigidbody != null)
            impulse = collision.rigidbody.mass * collision.relativeVelocity.magnitude;
        else
            impulse = GetComponent<Rigidbody>().mass * collision.relativeVelocity.magnitude;

        if (impulse > 3)
        {
            Debug.Log("impulse " + impulse);
            hp -= (int)impulse * 2;
            material.color = Color.Lerp(Color.black, startColor, (float)hp / MaxHp);
        }
    }
}
