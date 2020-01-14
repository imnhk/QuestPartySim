using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{
    float lifetime;
    Vector3 movement;

    void Start()
    {
        lifetime = 0.5f;
        movement = new Vector3(Random.Range(-2f, 2f), Random.Range(5f, 6f), Random.Range(-2f, 2f));
    }

    void Update()
    {
        if (lifetime <= 0)
            Destroy(this.gameObject);

        lifetime -= Time.deltaTime;

        this.transform.Translate(movement * Time.deltaTime);

        movement.y -= 0.3f;
    }
}


