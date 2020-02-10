using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public float left = 8f;

    private ParticleSystem particle;
    private AudioSource pourSound;

    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        pourSound = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        float bottleAngle = Vector3.Angle(Vector3.up, transform.up);

        if (bottleAngle > 90 && left > 0)
        {
            if (!particle.isPlaying)
                particle.Play();

            if (!pourSound.isPlaying)
                pourSound.Play();

            left -= Time.fixedDeltaTime;
        }
        else
        {
            if (particle.isPlaying)
                particle.Stop();

            if (pourSound.isPlaying)
                pourSound.Stop();
        }
    }
}
