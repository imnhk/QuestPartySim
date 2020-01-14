using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Play(Vector3 pos)
    {
        ps.transform.position = pos;
        ps.Play();
    }
}
