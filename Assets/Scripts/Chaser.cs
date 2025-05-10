using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Transform player;
    public Vector3 targetPosition;
    void Start()
    {
        
    }

    void Update()
    {
        targetPosition = player.position;
        transform.position = new Vector3(targetPosition.x, -1.45f, 0);
    }
}
