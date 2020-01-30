using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude < .2f)
            return;

        if(audioSource && audioSource.enabled)
            audioSource.Play();
    }
}
