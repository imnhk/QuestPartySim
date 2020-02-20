using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimation : MonoBehaviour
{
    public AudioSource audioSrc;
    private Animator animator;
    public GameObject button;
    

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        button.SetActive(false);
        animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        button.SetActive(true);
        animator.SetTrigger("Close");
    }

    public IEnumerator OpenDoorFor(float time)
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Close");
    }
}
