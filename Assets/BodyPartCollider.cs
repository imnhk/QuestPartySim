using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PART { HEAD, BODY }

public class BodyPartCollider : MonoBehaviour
{
    public ReactionNPC npc;
    public PART part;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impulse = GetImpulse(collision);

        if (impulse < 1f)
            return;
        if (collision.relativeVelocity.magnitude < 1f)
            return;
  

        Debug.Log(part + ", " + impulse + ", " + collision.gameObject.name) ;

        npc.PartCollision(part, impulse);
    }


    float GetImpulse(Collision coll)
    {
        if (coll.rigidbody != null)
            return coll.rigidbody.mass * coll.rigidbody.velocity.magnitude;
        else
            return 0;
    }

}
