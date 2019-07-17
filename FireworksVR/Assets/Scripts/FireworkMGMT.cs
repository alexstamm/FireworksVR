using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkMGMT : MonoBehaviour
{
    public GameObject fireworkPrefab;

    public int fireworkCountMax = 10;

    public Transform spawnLocations;
    public List<Transform> spawnLocationList;
    public List<GameObject> ActiveFireworks;

    public AnimationCurve curve;

    private float timeTracker;
    private int fireworkCount;

    void Awake()
    {
        fireworkCount = 0;
        timeTracker = 0.0f;

        foreach(Transform spawn in spawnLocations)
        {
            spawnLocationList.Add(spawn);
        }      
    }

    void Update()
    {
        if(fireworkCount < fireworkCountMax)
        {
            if(Time.frameCount % 20 == 0)
            {
                ShootFirework(fireworkCount);
                fireworkCount++;
            }
        }

        //motion
        timeTracker += Time.deltaTime;
        for(int i = 0; i < ActiveFireworks.Count; i++)
        {
            ActiveFireworks[i].transform.position = new Vector3(spawnLocationList[i].position.x, curve.Evaluate(timeTracker) * 20.0f, spawnLocationList[i].position.z);
        }

        if(timeTracker > 3.0f)
        {
            timeTracker = 0.0f;
        }
    }

    void ShootFirework(int id)
    {
        float x = spawnLocationList[id].position.x;
        float z = spawnLocationList[id].position.z;
        GameObject go = Instantiate(fireworkPrefab, new Vector3(x, 0, z), Quaternion.identity);
        ActiveFireworks.Add(go);
    }
}
