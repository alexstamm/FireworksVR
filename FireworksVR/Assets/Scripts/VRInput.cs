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

    public float pullSpeed = 50.0f;

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
        Debug.Log("Trigger Up");
        isPullingWorld = false;
        firstPoint = Vector3.zero;
        secondPoint = Vector3.zero;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        isPullingWorld = true;
        firstPoint = handProxy.position;
    }

    public void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Grip Up");
        isSlowingTime = false;
    }

    public void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Grip Down");
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
            secondPoint = handProxy.position;
            Vector3 offset = secondPoint - firstPoint;
            player.transform.Translate(-offset * Time.deltaTime * pullSpeed, Space.World);
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
