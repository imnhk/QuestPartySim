using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
    }

    public IEnumerator OpenDoorFor(float time)
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Close");
    }
}
