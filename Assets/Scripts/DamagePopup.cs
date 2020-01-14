using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagePopup : MonoBehaviour
{
    Canvas canvas;
    Vector3 movement;
    [SerializeField, Range(0, 10)]
    float gravity;
    [SerializeField, Range(0, 2)]
    float lifetime;

    void Start()
    {
        movement = new Vector3(Random.Range(-1f, 1f), Random.Range(2f, 3f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        if (lifetime <= 0)
            Destroy(this.gameObject);

        lifetime -= Time.deltaTime;

        this.transform.Translate(movement * Time.deltaTime);

        // 중력가속도
        movement.y -= gravity * Time.deltaTime;
    }
}


