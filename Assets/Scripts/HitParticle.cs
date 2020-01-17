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

    private void OnCollisionEnter(Collision collision)
    {
        Play(collision.GetContact(0).point);
    }
}
