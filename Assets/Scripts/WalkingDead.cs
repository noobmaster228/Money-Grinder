using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class WalkingDead : MonoBehaviour
{

    public GameObject[] waypoints;
    public int num;

    public float minDist;
    public float speed;

    bool rand = false;
    bool go = true;

    private void Start()
    {
        num = 0;
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);

        if (go)
        {
            if (dist > minDist)
            {
                Move();
            }
            else
            {
                if (!rand)
                {
                    if (num + 1 == waypoints.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, waypoints.Length);
                }
            }
        }
    }
    public void Move()
    {
        //  gameObject.transform.LookAt(waypoints[num].transform.position);
        if (num==0)
        {
 gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
        else
        {
            gameObject.transform.position -= gameObject.transform.forward * speed * Time.deltaTime;
        }
       
    }
}

