using UnityEngine;

public class ThrowGrabbed : MonoBehaviour
{
    [SerializeField]
    private Vector3 angularVelocity = new Vector3(0, 5, 0);
    
    private OVRInput.Button throwButton;
    private OVRGrabbable ovrGrabbable;
    private Rigidbody rb;


    void Awake()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
        if (!ovrGrabbable)
            Debug.LogError("No ovrgrabbable at " + gameObject.name);
        rb = GetComponent<Rigidbody>();
        if (!rb)
            Debug.LogError("No rigidbody at " + gameObject.name);

        throwButton = OVRInput.Button.PrimaryIndexTrigger;
    }

    void FixedUpdate()
    {
        if (ovrGrabbable.isGrabbed && OVRInput.GetDown(throwButton, ovrGrabbable.grabbedBy.Controller))
            ThrowForward();
    }

    private void ThrowForward()
    {
        Quaternion throwDirection = ovrGrabbable.grabbedBy.throwLine.rotation;
        float throwForce = 3 + (float)3 * 2 / (this.rb.mass + 1);

        ovrGrabbable.grabbedBy.ForceRelease(ovrGrabbable);
        ovrGrabbable.GrabEnd(throwDirection * Vector3.forward * throwForce, angularVelocity);
    }
}
