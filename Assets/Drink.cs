using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public float leftTime = 8f;

    private ParticleSystem particle;

    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        float angle = Vector3.Angle(Vector3.up, transform.up);
        // transform.Rotate(new Vector3(0.5f, 0, 0));

        if(angle > 85 && leftTime > 0)
        {
            leftTime -= Time.fixedDeltaTime;

            if (!particle.isPlaying)
            {
                particle.Play();
            }
        }
        else
        {
            if(particle.isPlaying)
                particle.Stop();
        }
    }
}
