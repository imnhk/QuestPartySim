using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkEffect : MonoBehaviour
{
    private ParticleSystem particle;
    private List<ParticleCollisionEvent> collisionEvents;

    private int drinkCount = 0;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();


        for(int i = 0; i<numCollisionEvents; i++)
        {
            if (other.tag == "Mouth")
            {
                drinkCount++;
                if(drinkCount >= 10)
                {
                    drinkCount = 0;
                    GameManager.Instance.game.AddScore(10);
                }
            }
            else
            {
                
            }

        }

    }
}
