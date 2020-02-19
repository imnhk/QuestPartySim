using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public float remaining = 8f;
    public float alcohol = 0.001f;

    private ParticleSystem particle;
    private AudioSource pourSound;

    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        pourSound = particle.gameObject.GetComponent<AudioSource>();
        pourSound.loop = true;
        pourSound.minDistance = 0.1f;
        pourSound.maxDistance = 1f;
        pourSound.volume = 0.5f;
    }

    void Update()
    {
        float bottleAngle = Vector3.Angle(Vector3.up, transform.up);

        if (bottleAngle > 90 && remaining > 0)
        {
            if (!particle.isPlaying)
                particle.Play();

            if (!pourSound.isPlaying)
            {
                pourSound.Play();
            }

            remaining -= Time.fixedDeltaTime;
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
