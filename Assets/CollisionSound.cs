using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    // 효과음을 재생할 최소 충돌 속도
    [SerializeField]
    private const float threshold = 1f;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // 효과음은 Scene 시작에 재생 또는 Loop되지 않는다
        if (audioSource)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < threshold)
            return;

        if(audioSource && audioSource.enabled)
            audioSource.Play();
    }
}
