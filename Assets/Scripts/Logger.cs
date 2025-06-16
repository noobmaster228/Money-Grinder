using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    Vector3 lastPos, lastScale;
    Quaternion lastRot;

    void Start()
    {
        lastPos = transform.position;
        lastRot = transform.rotation;
        lastScale = transform.localScale;
    }

    void Update()
    {
        if (transform.position != lastPos || transform.rotation != lastRot || transform.localScale != lastScale)
        {
            Debug.Log($"{name} transform изменился! batching слетит!");
            lastPos = transform.position;
            lastRot = transform.rotation;
            lastScale = transform.localScale;
        }
        if (GetComponent<MeshRenderer>().sharedMaterial.name.Contains("Instance"))
            Debug.Log($"{name}: материал стал Instance! batching слетит!");
    }
}
