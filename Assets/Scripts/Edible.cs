using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{

    [SerializeField]
    private int size = 3;
    [SerializeField]
    private int eatScore = 100;
    private int leftCount;

    private float eatDelay = 1f;
    private float lastEatTime = 0;

    private AudioSource audioSource;

    private void Start()
    {
        leftCount = size;
        audioSource = GetComponent<AudioSource>();
    }

    public void Eaten()
    {
        if (Time.time < lastEatTime + eatDelay)
            return;
        lastEatTime = Time.time;

        leftCount -= 1;
        GameManager.Instance.AddScore(eatScore);

        if (audioSource)
            audioSource.Play();

        if(leftCount <= 0)
            Destroy(this.gameObject);
    }
}
