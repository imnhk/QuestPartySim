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

    public void PartCollision(PART part, float impulse)
    {

        // 작은 충격 n회 반복 시 플레이어 추격, 잡히면 게임오버.
        if(impulse > 1f)
        {

        }
        // 머리에 큰 충격시 Animator 해제, 게임오버
        if (part == PART.HEAD && impulse > 5f)
        {
            animator.enabled = false;
            GameManager.Instance.GameOver(GameManager.GAMEOVER.KICKED_OUT);
        }
    }

}
