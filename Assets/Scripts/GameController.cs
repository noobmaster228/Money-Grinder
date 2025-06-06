using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float TimeCycle; //���������� ��� �������� �����
    public bool isTimerOn; //�������� ������� ������� ����
    public bool isShield; //�������� ������� ����
    public float multiply; //�������� ���������������
    [SerializeField] float moneybagValue; //�������� 1 ��������
    [SerializeField] float badMoneybagValue; //�������� ������ ��������
    [SerializeField] float moneyValue; //�������� ����� � ��������
    [SerializeField] float badMoneyValue; //�������� ������� ����� � ��������
    [SerializeField] Vector3 startPos; //������� ������ ����� �������� �������
    public Vector3 endPos; //������� ��� �������� ����
    public float MoneyCount; //������ ������
    public float Points; //���-�� ����� ������
    public float PointsRate; //���-�� ����� � ����������
    [SerializeField] float CreditcardMoney; //���-�� ����� ����������� � ��������� �����
    public float CreditcardCount; //������ ������ � ��������� ���������
    [SerializeField] ChunkPlacer triggerActivator; //������ ����������� ��������� 
    [SerializeField] WaterScroller waterSpeed; //������ ��� �������� �������� ����
    public Rigidbody falling; //���������� ����������� Rigidbody
    AudioSource itemAudio; //�������� ����� ��������
    public AudioSource playerAudio; //�������� ����� ������
    [SerializeField] BonusSoundPlayer BonusSounds; //������ ����������� ������ �������
    [SerializeField] AudioClip ShieldBreakSound; //��������� ���� ����  
    public AudioClip PointCountSound; //��������� ���������� �������� �����
    public AudioClip MoneyAdd; //��������� �������� ������� ������
    public AudioClip RoundEndSound; //��������� ����� ����
    [SerializeField] UIManager UI; //���������� UI ����
    public PlayerControls playerMovement; //������ ���������� ������
    void Start()
    {
        multiply = 1;
        TimeCycle = 0;
        isTimerOn = false;
        transform.position = startPos;
        MoneyCount = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isShield = false;
    }
    void Update() //���������� ����� ������ 0.5 ������� � ���������� ����� ��������� �������
    {
        if (isTimerOn)
        {
            TimeCycle += Time.deltaTime;
            if (TimeCycle >= 0.5f)
            {
                Points += PointsRate;
                TimeCycle = 0;
            }
        }
        if ((transform.position.x <= endPos.x) && (isTimerOn) && (UI.SceneNum != 9))
        {
            TimeCycle = 0;
            isTimerOn = false;
            StartCoroutine(UI.ResultCount());
        }
    }
    private void OnTriggerEnter(Collider other) //��� ����������� �������� ����������� ��� � ����������� �������� � ����������� �� ����
    {
        string tag = other.tag;
        switch (other.gameObject.tag)
        {
            case "+Speed":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.speed += 1;
                waterSpeed.scrollSpeed += 0.01f;
                playerMovement.SpeedEffect.Play();
                break;
            case "-Speed":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.speed -= 1;
                waterSpeed.scrollSpeed -= 0.01f;
                playerMovement.NEffect.Play();
                if (playerMovement.speed == 0)
                {
                    playerMovement.walkEffect.Stop();
                    UI.Death();
                }
                break;
            case "+Money":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.PEffect.Play();
                MoneyCount += 100;
                break;
            case "-Money":
                BonusSounds.PlayBonusSound(tag);
                MoneyCount -= 100;
                playerMovement.NEffect.Play();
                break;
            case "+Points":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.PEffect.Play();
                PointsRate += 10;
                break;
            case "-Points":
                BonusSounds.PlayBonusSound(tag);
                PointsRate -= 10;
                playerMovement.NEffect.Play();
                break;
            case "Money":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                MoneyCount += moneyValue * multiply;
                other.gameObject.SetActive(false);
                break;
            case "MoneyBag":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                MoneyCount += moneybagValue * multiply;
                other.gameObject.SetActive(false);
                break;
            case "BadMoney":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                other.gameObject.SetActive(false);
                MoneyCount -= badMoneyValue * multiply;
                break;
            case "BadMoneyBag":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                MoneyCount -= badMoneybagValue * multiply;
                other.gameObject.SetActive(false);
                break;
            case "SAW":
                itemAudio = other.GetComponent<AudioSource>();
                other.tag = "Untagged";
                if (isShield)
                {
                    playerAudio.PlayOneShot(ShieldBreakSound);
                    playerMovement.ShieldEffect.Stop();
                    StartCoroutine(UI.ShieldBreak());
                }
                else
                {
                    playerMovement.SawHitEffect.Play();
                    playerAudio.PlayOneShot(itemAudio.clip);
                    UI.Death();
                    playerMovement.limit = 200;
                }
                break;
            case "DeathPit":
                itemAudio = other.GetComponent<AudioSource>();
                playerMovement.limit = 200;
                other.tag = "Untagged";
                playerAudio.PlayOneShot(itemAudio.clip);
                UI.Death();
                break;
            case "Protection":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                other.gameObject.SetActive(false);
                isShield = true;
                UI.ShitImg.gameObject.SetActive(true);
                playerMovement.ShieldEffect.Play();
                break;
            case "+Multi":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.PEffect.Play();
                multiply += 0.5f;
                break;
            case "-Multi":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.NEffect.Play();
                if (multiply <= 0)
                {
                    multiply = 0;
                }
                else
                {
                    multiply -= 0.5f;
                }
                break;
            case "noColor":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                playerMovement.NoColorEffect.Play();
                other.gameObject.SetActive(false);
                StartCoroutine(UI.NoColor());
                break;
            case "creditcard":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                other.gameObject.SetActive(false);
                CreditcardCount += CreditcardMoney * multiply;
                break;
            case "ATM":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                StartCoroutine(ATMTransfer(playerMovement.speed));
                break;
            case "trigger":
                triggerActivator.SpawnChunk();
                break;
        }
    }
    IEnumerator ATMTransfer(float safer) //������� ����� � ������� ����� �� ������ �����.
    {
        safer = playerMovement.speed;
        playerMovement.speed = 0;
        playerMovement.walkEffect.Stop();
        yield return new WaitForSeconds(0.75f);
        playerMovement.ATMEffect.Play();
        MoneyCount += CreditcardCount;
        CreditcardCount = 0;
        yield return new WaitForSeconds(0.75f);
        playerMovement.speed = safer;
        playerMovement.walkEffect.Play();
    }
}