using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameController movement;
    [SerializeField] UIManager UI;
    [SerializeField] Quaternion zeroRotation;
    [SerializeField] Quaternion leftRotation;
    [SerializeField] Quaternion rightRotation;
    [SerializeField] float lerpRotation = 0.5f;
    public float speed; //Скорость
    float delta; //Изменение координаты Z
    float screenMiddle; //Середина экрана
    public float limit = 1.8f; //ограничение игрока по оси Z
    void Start()
    {
        screenMiddle = Screen.width / 2f; //разделение экрана на две половины
    }
    void FixedUpdate() //управление влево и вправо и движение вперед
    {
        if(movement.isTimerOn)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            delta = 0;
            if (!Application.isMobilePlatform)
            {
                ChangeAngle(zeroRotation);
                if (Input.GetKey(KeyCode.A))
                {
                    delta = -speed * Time.deltaTime;
                    ChangeAngle(leftRotation);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    delta = speed * Time.deltaTime;
                    ChangeAngle(rightRotation);
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + delta, -limit, limit));
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    float direction;

                    if (touch.position.x < screenMiddle)
                    {
                        direction = -1f;
                        ChangeAngle(leftRotation);
                    }
                    else
                    {
                        direction = 1f;
                        ChangeAngle(rightRotation);
                    }
                    delta = direction * speed * Time.deltaTime;
                }
                else
                {
                    ChangeAngle(zeroRotation);
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + delta, -limit, limit));
            }
        }
    }
    public IEnumerator StartAcceleration(float speedy) //Плавное ускорение в начале уровня
    {
        speed = 0;
        yield return new WaitForSeconds(0.25f);
        speed = 0.5f;
        yield return new WaitForSeconds(0.5f);
        speed = 1f;
        for (int i = 0; i < speedy - 1; i++)
        {
            yield return new WaitForSeconds(1f);
            speed += 1;
        }
    }
    public void ChangeAngle(Quaternion angle) //Поворот персонажа
    {
        UI.Dollar.transform.localRotation = Quaternion.Lerp(UI.Dollar.transform.localRotation, angle, lerpRotation);
    }
}