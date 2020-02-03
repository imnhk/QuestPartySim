using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    // 최소 충돌 상대속도
    [SerializeField]
    private const float threshold = 1f;
    private AudioSource audioSource;
    private Rigidbody rb;


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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < threshold)
            return;

        if(audioSource && audioSource.enabled)
            audioSource.Play();

        int impulse = (int)GetImpulse(collision);
        if (impulse > 0)
            GameManager.Instance.game.AddScore(impulse);

        
    }

    float GetImpulse(Collision coll)
    {
        if (rb == null)
            return 0;

        if (coll.rigidbody != null)
            return rb.mass * rb.velocity.magnitude + coll.rigidbody.mass * coll.rigidbody.velocity.magnitude;
        else
        {
            // 움직이지 않는 물체에 부딪힐 경우 (벽, 바닥 등)
            return rb.mass * rb.velocity.magnitude;
        }
    }
}
