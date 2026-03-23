using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class HandPicker : MonoBehaviour
{

    [Header("SteamVR Setup")]
    public SteamVR_Input_Sources handType; // Choose LeftHand or RightHand in the Inspector
    public SteamVR_Action_Boolean grabAction; // Assign "\actions\default\in\GrabPinch" in the Inspector
    public SteamVR_Behaviour_Pose pose; // Drag the SteamVR_Behaviour_Pose component here

    public GameObject thingBeingGrabbed = null;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.Any))
        {
            // set the game object that the script is on as the parent of another object
            thingBeingGrabbed.transform.parent = hand.transform;
            Rigidbody rb = thingBeingGrabbed.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        if (SteamVR_Input.GetStateUp("GrabPinch", SteamVR_Input_Sources.Any))
        {
            Rigidbody toThrow = thingBeingGrabbed.GetComponent<Rigidbody>();
            toThrow.isKinematic = false;
            thingBeingGrabbed.transform.parent = null;

            Hand h = GetComponent<Hand>();
            Vector3 velocity;
            Vector3 angularVelocity;
            pose.GetEstimatedPeakVelocities(out velocity, out angularVelocity);

            toThrow.linearVelocity = velocity;
            toThrow.angularVelocity = angularVelocity;

        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        thingBeingGrabbed = other.gameObject;
    }
}
