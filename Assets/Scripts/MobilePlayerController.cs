using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float limit = 1.8f;

    private float screenMiddle;

    void Start()
    {
        if (!Input.touchSupported || !Application.isMobilePlatform)
        {
            this.enabled = false;
            return;
        }

        screenMiddle = Screen.width / 2f;
    }
    void Update()
    {
        float delta = 0f;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            float direction;

            if (touch.position.x < screenMiddle)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }

            delta = direction * speed * Time.deltaTime;
        }
        /*if(transform.position.y <= -2.2495f)
        {
            limit = 200;
        }*/
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(transform.position.z + delta, -limit, limit)
        );
    }

}