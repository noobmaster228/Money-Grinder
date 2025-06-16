using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameController movement;
    public GameObject PlayerModel;
    [SerializeField] Quaternion zeroRotation;
    [SerializeField] Quaternion leftRotation;
    [SerializeField] Quaternion rightRotation;
    [SerializeField] float lerpRotation;
    public float speed;
    public float limit = 1.8f;
    private float screenMiddle;
    public CharacterController controller;
    private Vector3 velocity;
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpHeight = 2f;
    private float verticalVelocity;

    // Параметры для плавного бокового движения
    public float lateralAcceleration; // ускорение (чем больше, тем быстрее набирает)
    public float lateralMaxSpeed;  // максимальная скорость смещения по Z
    public float lateralDeceleration; // замедление если нет ввода
    private float currentLateralSpeed = 0f;
    private float targetLateral = 0f;

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
        screenMiddle = Screen.width / 2f;
    }

    void Update()
    {
        if (movement.isTimerOn)
        {
            targetLateral = 0f;

            if (!Application.isMobilePlatform)
            {
                ChangeAngle(zeroRotation);
                if (Input.GetKey(KeyCode.A))
                {
                    targetLateral = -1f;
                    ChangeAngle(leftRotation);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    targetLateral = 1f;
                    ChangeAngle(rightRotation);
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x < screenMiddle)
                    {
                        targetLateral = -1f;
                        ChangeAngle(leftRotation);
                    }
                    else
                    {
                        targetLateral = 1f;
                        ChangeAngle(rightRotation);
                    }
                }
                else
                {
                    ChangeAngle(zeroRotation);
                }
            }

            // Плавное ускорение и торможение по Z
            if (Mathf.Abs(targetLateral) > 0.01f)
            {
                currentLateralSpeed = Mathf.MoveTowards(currentLateralSpeed, targetLateral * lateralMaxSpeed, lateralAcceleration * Time.deltaTime);
            }
            else
            {
                // Плавное замедление если нет ввода
                currentLateralSpeed = Mathf.MoveTowards(currentLateralSpeed, 0f, lateralDeceleration * Time.deltaTime);
            }

            // Гравитация
            if (controller.isGrounded)
            {
                if (verticalVelocity < 0)
                    verticalVelocity = -2f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            verticalVelocity += gravity * Time.deltaTime;

            // Движение по X (всегда влево)
            Vector3 move = Vector3.left * speed;
            // Плавное боковое движение по Z
            move += Vector3.forward * currentLateralSpeed;
            move.y = verticalVelocity;
            controller.Move(move * Time.deltaTime);

            // Ограничение по Z
            Vector3 pos = transform.position;
            pos.z = Mathf.Clamp(pos.z, -limit, limit);
            transform.position = pos;
        }
    }

    public void Accelerate()
    {
        StartCoroutine(StartAcceleration(speed));
    }
    public IEnumerator StartAcceleration(float speedy)
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
    public void ChangeAngle(Quaternion angle)
    {
        PlayerModel.transform.localRotation = Quaternion.Lerp(PlayerModel.transform.localRotation, angle, lerpRotation);
    }
}
