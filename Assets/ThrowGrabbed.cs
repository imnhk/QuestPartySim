﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrabbed : MonoBehaviour
{
    public OVRInput.Button throwButton;
    public float throwForce;
    public Vector3 angularVelocity;
    
    private OVRGrabbable ovrGrabbable;


    // Start is called before the first frame update
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
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

        ovrGrabbable.grabbedBy.ForceRelease(ovrGrabbable);
        ovrGrabbable.GrabEnd(throwDirection * Vector3.forward * throwForce, angularVelocity);
    }
}
