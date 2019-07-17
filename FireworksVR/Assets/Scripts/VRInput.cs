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
    public SteamVR_Action_Boolean SlowTime;

    //hand ref
    public SteamVR_Input_Sources handType;

    public float pullSpeed;

    private bool isPullingWorld;
    private bool isSlowingTime;

    private Vector3 firstPoint;
    private Vector3 secondPoint;

    void Awake()
    {
        isPullingWorld = false;

        WorldPull.AddOnStateDownListener(TriggerDown, handType);
        WorldPull.AddOnStateUpListener(TriggerUp, handType);

        SlowTime.AddOnStateDownListener(GripDown, handType);
        SlowTime.AddOnStateUpListener(GripUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPullingWorld = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!isPullingWorld)
        {
            isPullingWorld = true;
            firstPoint = handProxy.position;
        }

    }

    public void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isSlowingTime = false;
    }

    public void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isSlowingTime = true;
    }


    void Update()
    {
        SimplePull();
        SimpleSlowTime();     
    }

    private void SimplePull()
    {
        if (isPullingWorld)
        {
            Vector3 offset = handProxy.position - firstPoint;
            player.transform.position += (-offset * pullSpeed) * Time.deltaTime;
            firstPoint = handProxy.position;
        }
    }

    private void SimpleSlowTime()
    {
        if (isSlowingTime)
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.3f;
            }
        }
        else
        {
            if(Time.timeScale != 1.0f)
            {
                Time.timeScale = 1.0f;
            }
        }
    }

}
