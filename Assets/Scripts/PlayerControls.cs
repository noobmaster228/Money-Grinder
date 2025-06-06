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
    Rigidbody rb;
    public float jumpPower;
    bool isGrounded = true;
    Vector2 startTouch;
    [HideInInspector] public ParticleSystem ShieldEffect;
    [HideInInspector] public ParticleSystem SpeedEffect;
    [HideInInspector] public ParticleSystem walkEffect;
    [HideInInspector] public ParticleSystem PEffect;
    [HideInInspector] public ParticleSystem NEffect;
    [HideInInspector] public ParticleSystem ATMEffect;
    [HideInInspector] public ParticleSystem SawHitEffect;
    [HideInInspector] public ParticleSystem NoColorEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenMiddle = Screen.width / 2f; //разделение экрана на две половины
    }
    void Update() //управление влево и вправо и движение вперед
    {
        if (movement.isTimerOn)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                Jump();

#endif

            if (Application.isMobilePlatform && isGrounded)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                        startTouch = touch.position;
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Vector2 endTouch = touch.position;
                        if (endTouch.y - startTouch.y > 100f && Mathf.Abs(endTouch.x - startTouch.x) < 100f)
                            Jump();
                    }
                }
            }
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
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // сбросить Y скорость
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}