using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{

    [SerializeField]
    private int size = 3;
    private int leftCount;

    private Vector3 defaultScale;

    private float eatDelay = 1f;
    private float lastEatTime = 0;

    private void Start()
    {
        leftCount = size;
        defaultScale = transform.localScale;
    }

    public void Eaten()
    {
        if (Time.time < lastEatTime + eatDelay)
            return;
        lastEatTime = Time.time;

        leftCount -= 1;
        // Debug.Log("Eaten! size " + leftCount);

        if(leftCount <= 0)
            Destroy(this.gameObject);

        transform.localScale = defaultScale * ((float)leftCount / size);

    }
}
