using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 targetPosition;
    void Update()
    {
        targetPosition = player.position;
        transform.position = new Vector3(targetPosition.x, -1.45f, 0);
    }
}