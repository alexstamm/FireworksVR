using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRInput : MonoBehaviour

{
    //player base ref
    public GameObject player;

    public Transform handProxy;

    //action ref
    public SteamVR_Action_Boolean WorldPull;

    //hand ref
    public SteamVR_Input_Sources handType;

    public bool isPullingWorld;
    public float pullSpeed = 50.0f;

    private Vector3 firstPoint;
    private Vector3 secondPoint;

    void Awake()
    {
        isPullingWorld = false;
        WorldPull.AddOnStateDownListener(TriggerDown, handType);
        WorldPull.AddOnStateUpListener(TriggerUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Up");
        isPullingWorld = false;
        firstPoint = Vector3.zero;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        isPullingWorld = true;
        firstPoint = handProxy.position;
    }

    void Update()
    {
        if (isPullingWorld)
        {
            SimplePull();
        }
    }

    private void SimplePull()
    {
        secondPoint = handProxy.position;
        Vector3 offset = secondPoint - firstPoint;
        player.transform.Translate(-offset * Time.deltaTime * pullSpeed, Space.World);
    }

}
