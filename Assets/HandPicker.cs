using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandPicker : MonoBehaviour
{

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
            Rigidbody rb = thingBeingGrabbed.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            thingBeingGrabbed.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        thingBeingGrabbed = other.gameObject;
    }
}
