using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameController movement;
    public GameObject PlayerModel; //3D модель игрока
    [SerializeField] Quaternion zeroRotation; //нет поворота
    [SerializeField] Quaternion leftRotation; //поворот налево
    [SerializeField] Quaternion rightRotation; //поворот направо
    [SerializeField] float lerpRotation = 0.5f; //плавность поворота
    public float speed; //Скорость
    float delta; //Изменение координаты Z
    float screenMiddle; //Середина экрана
    public float limit = 1.8f; //ограничение игрока по оси Z
    bool isTimerOn; //запуск таймера
    void Start()
    {
        screenMiddle = Screen.width / 2f; //разделение экрана на две половины
    }
    void Update() //управление влево и вправо и движение вперед
    {
        if(movement.isTimerOn)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0); //Движение по оси X
            delta = 0; //Если нет никакого ввода то изменение коордианты Z = 0
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
                if (Input.touchCount > 0) //проверка нажатий
                {
                    Touch touch = Input.GetTouch(0);  //получение первого касания

                    float direction; //направление движения

                    if (touch.position.x < screenMiddle) //проверка стороны нажатия
                    {
                        direction = -1f;
                        ChangeAngle(leftRotation); //поворот модели налево
                    }
                    else
                    {
                        direction = 1f;
                        ChangeAngle(rightRotation); //поворот модели налево
                    }
                    delta = direction * speed * Time.deltaTime; //скорость движения направо и налево
                }
                else
                {
                    ChangeAngle(zeroRotation); //не поворачивать модель
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + delta, -limit, limit)); //движение игрока налево или направо с ограничением 
            }
        }
    }
    public void Accelerate()
    {
        StartCoroutine(StartAcceleration(speed));
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
        PlayerModel.transform.localRotation = Quaternion.Lerp(PlayerModel.transform.localRotation, angle, lerpRotation);
    }
}