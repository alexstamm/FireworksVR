using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRInput : MonoBehaviour

{
    //player base ref
    public GameObject player;

    //action ref
    public SteamVR_Action_Boolean WorldPull;

    //hand ref
    public SteamVR_Input_Sources handType;


    void Awake()
    {
        isHandGrabbing = false;

        WorldPull.AddOnStateDownListener(TriggerDown, handType);
        WorldPull.AddOnStateUpListener(TriggerUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Up");
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
    }

    void Update()
    {

    }

    private void SimplePull()
    {

    }

}
