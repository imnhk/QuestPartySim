using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]
    private Vector3 movement;
    [SerializeField, Range(0, 10)]
    private float gravity;
    [SerializeField, Range(0, 2)]
    private float lifetime;

    void Start()
    {
        movement += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.2f));      
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


