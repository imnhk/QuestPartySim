using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrabbed : MonoBehaviour
{
    [SerializeField]
    private Vector3 angularVelocity = new Vector3(0, 5, 0);
    
    private OVRInput.Button throwButton;
    private OVRGrabbable ovrGrabbable;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();

        throwButton = OVRInput.Button.PrimaryIndexTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        if (ovrGrabbable.isGrabbed && OVRInput.GetDown(throwButton, ovrGrabbable.grabbedBy.Controller))
        {
            ThrowForward();
        }
    }

    private void ThrowForward()
    {
        Quaternion throwDirection = ovrGrabbable.grabbedBy.throwLine.rotation;
        float throwForce = 3 + (float)GameManager.Instance.game.Str * 2 / (this.rb.mass + 1);

        ovrGrabbable.grabbedBy.ForceRelease(ovrGrabbable);
        ovrGrabbable.GrabEnd(throwDirection * Vector3.forward * throwForce, angularVelocity);
    }
}
