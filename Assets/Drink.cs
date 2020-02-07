using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public float leftTime = 5f;

    private ParticleSystem particle;
    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        float angle = Vector3.Angle(Vector3.up, transform.rotation.eulerAngles);
        if(angle > 85 && leftTime > 0)
        {
            particle.Play();
            leftTime -= Time.fixedDeltaTime;

        }
        else
        {
            particle.Stop();
        }
    }
}
