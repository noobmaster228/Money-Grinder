using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float limit = 1.8f;

    private float screenMiddle;

    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        // Отключаем скрипт, если это не Android или iOS
        this.enabled = false;
        return;
#else
    screenMiddle = Screen.width / 2f;
#endif
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

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(transform.position.z + delta, -limit, limit)
        );
    }
}