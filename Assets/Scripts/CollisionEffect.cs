using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    public const string DefaultAudioPath = "SoundBits_FreeSFX/CasualGameSounds/DM-CGS-01";
    // 최소 충돌 상대속도
    public const float thresholdVelocity = 1f;

    private AudioSource audioSource;
    private Rigidbody rb;


    void Awake()
    {
        // 효과음은 Scene 시작 때 재생되거나 Loop되지 않는다
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if(audioSource.clip == null)
        {
            audioSource.clip = Resources.Load<AudioClip>(DefaultAudioPath);
            if(audioSource.clip == null)
            {
                Debug.LogError("Cannot load audioclip " + DefaultAudioPath);
            }
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.minDistance = 0;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < thresholdVelocity)
            return;

        float impulse = GetImpulse(collision);

        if (audioSource && audioSource.enabled)
        {
            audioSource.volume = Mathf.Clamp((impulse / 3), 0, 1);
            audioSource.Play();
        }

        if (impulse >= 1)
            GameManager.Instance.AddScore((int)impulse);  
    }

    // Collision.impulse를 대체하기 위한 충격량 계산
    float GetImpulse(Collision coll)
    {
        if (rb == null)
            return 0;

        if (coll.rigidbody)
            return rb.mass * rb.velocity.magnitude + coll.rigidbody.mass * coll.rigidbody.velocity.magnitude;
        else
            return rb.mass * rb.velocity.magnitude;
    }
}
