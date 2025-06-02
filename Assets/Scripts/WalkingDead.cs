using UnityEngine;

public class WalkingDead : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    [SerializeField] int num;
    [SerializeField] float minDist;
    [SerializeField] float speed;

    private void Start()
    {
        num = 0;
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        if (dist > minDist)
        {
            Move();
        }
        else
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
    }
    public void Move()
    {
        if (num == 0)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
        else
        {
            gameObject.transform.position -= gameObject.transform.forward * speed * Time.deltaTime;
        }
    }
}

