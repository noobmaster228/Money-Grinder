using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameController movement;
    public GameObject PlayerModel; //3D модель игрока
    [SerializeField] Quaternion zeroRotation; //нет поворота
    [SerializeField] Quaternion leftRotation; //поворот налево
    [SerializeField] Quaternion rightRotation; //поворот направо
    [SerializeField] float lerpRotation; //плавность поворота
    public float speed; //—корость
    public float sideSpeed;
    float moveDirectionZ = 0f; //»зменение координаты Z
    float screenMiddle; //—ередина экрана
    public float limit;//ограничение игрока по оси Z
    bool isTimerOn; //запуск таймера
    Rigidbody rb;
    public float jumpPower;
    bool jumpRequested = false;
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
                jumpRequested = true;

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
                            jumpRequested = true;
                    }
                }
            }
            moveDirectionZ = 0f;

#if UNITY_EDITOR || UNITY_STANDALONE
            ChangeAngle(zeroRotation);
            if (Input.GetKey(KeyCode.A))
            {
                moveDirectionZ = -1f;
                ChangeAngle(leftRotation);
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirectionZ = 1f;
                ChangeAngle(rightRotation);
            }
#endif
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        if (touch.position.x < screenMiddle)
                        {
                            moveDirectionZ = -1f;
                            ChangeAngle(leftRotation);
                        }
                        else
                        {
                            moveDirectionZ = 1f;
                            ChangeAngle(rightRotation);
                        }
                    }
                    else
                    {
                        ChangeAngle(zeroRotation);
                    }
                }
                else
                {
                    ChangeAngle(zeroRotation);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (!movement.isTimerOn)
            return;
        rb.velocity = new Vector3(-speed, rb.velocity.y, 0);

        if (moveDirectionZ != 0)
        {
            float nextZ = rb.position.z + moveDirectionZ * sideSpeed * Time.fixedDeltaTime;
            float clampedZ = Mathf.Clamp(nextZ, -limit, limit);
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y, clampedZ));
        }
        if (jumpRequested && isGrounded)
        {
            Jump();
        }
    }
    public void Accelerate()
    {
        StartCoroutine(StartAcceleration(speed));
    }
    public IEnumerator StartAcceleration(float speedy) //ѕлавное ускорение в начале уровн€
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
    public void ChangeAngle(Quaternion angle) //ѕоворот персонажа
    {
        PlayerModel.transform.localRotation = Quaternion.Lerp(PlayerModel.transform.localRotation, angle, lerpRotation);
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // сбросить Y скорость
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
        jumpRequested = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}