using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    private ElevatorAnimation elevator;
    void Start()
    {
        elevator = transform.parent.GetComponent<ElevatorAnimation>();   
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "OVRPlayerController")
        {
            StartCoroutine(elevator.OpenDoorFor(3));
        }
    }

}
