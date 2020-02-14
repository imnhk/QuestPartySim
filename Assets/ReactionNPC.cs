using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionNPC : MonoBehaviour
{
    private Animator animator;

    private Collider headCollider;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // 작은 충격 n회 반복 시 플레이어 추격, 잡히면 게임오버.


    private void OnCollisionEnter(Collision collision)
    {
        float impulse = GetImpulse(collision);
        Debug.LogError("IMPULSE: " + impulse);

        // 작은 충격시 표정 변화, 플레이어쪽으로 방향 전환
        if(impulse > 1f)
        {

        }
        // 큰 충격시 Animator 해제, 게임오버
        else if (impulse > 3f)
        {
            animator.enabled = false;
            GameManager.Instance.GameOver(GameManager.GAMEOVER.KICKED_OUT);
        }
    }

    float GetImpulse(Collision coll)
    {
        if (coll.rigidbody != null)
            return coll.rigidbody.mass * coll.rigidbody.velocity.magnitude;
        else
            return 0;
    }

}
