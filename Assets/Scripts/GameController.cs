using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float t; 
    public bool isTimerOn; 
    public bool isShield;
    public float multiply;
    [SerializeField] float moneybagValue;
    [SerializeField] float badMoneybagValue;
    [SerializeField] float moneyValue;
    [SerializeField] float badMoneyValue;
    [SerializeField] Vector3 startPos;
    public Vector3 endPos;
    public float MoneyCount;
    public float Points;
    public float PointsRate;
    [SerializeField] float CreditcardMoney;
    public float CreditcardCount;
    [SerializeField] ChunkPlacer triggerActivator;
    [SerializeField] WaterScroller waterSpeed;
    public Rigidbody falling;
    AudioSource itemAudio;
    public AudioSource playerAudio;
    [SerializeField] BonusSoundPlayer BonusSounds;
    [SerializeField] AudioClip ShieldBreakSound;
    public AudioClip PointCountSound;
    public AudioClip MoneyAdd;
    public AudioClip RoundEndSound;
    [SerializeField] UIManager UI;
    [SerializeField] PlayerControls playerMovement;
    void Start()
    {
        multiply = 1;
        t = 0;
        isTimerOn = false;
        transform.position = startPos;
        MoneyCount = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isShield = false;
    }
    void FixedUpdate() //Добавление очков каждые 0.5 секунды и считывание конца обучающих уровней
    {
        if (isTimerOn)
        {
            t += Time.deltaTime;
            if (t >= 0.5f)
            {
                Points += PointsRate;
                t = 0;
            }
        }
        if ((transform.position.x <= endPos.x) && (isTimerOn) && (UI.SceneNum != 9))
        {
            t = 0;
            isTimerOn = false;
            StartCoroutine(UI.ResultCount());
        }
    }
    private void OnTriggerEnter(Collider other) //при пересечении триггера считывается тег и выполняется действия в зависимости от тега
    {
        string tag = other.tag;
        switch (other.gameObject.tag)
        {
            case "+Speed":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.speed += 1;
                waterSpeed.scrollSpeed += 0.01f;
                break;
            case "-Speed":
                BonusSounds.PlayBonusSound(tag);
                playerMovement.speed -= 1;
                waterSpeed.scrollSpeed -= 0.01f;
                if (playerMovement.speed == 0)
                {
                    UI.Death();
                }
                break;
            case "+Money":
                BonusSounds.PlayBonusSound(tag);
                MoneyCount += 100;
                break;
            case "-Money":
                BonusSounds.PlayBonusSound(tag);
                MoneyCount -= 100;
                break;
            case "+Points":
                BonusSounds.PlayBonusSound(tag);
                PointsRate += 10;
                break;
            case "-Points":
                BonusSounds.PlayBonusSound(tag);
                PointsRate -= 10;
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
                playerMovement.limit = 200;
                other.tag = "Untagged";
                if (isShield)
                {
                    playerAudio.PlayOneShot(ShieldBreakSound);
                    StartCoroutine(UI.ShieldBreak());
                }
                else
                {
                    playerAudio.PlayOneShot(itemAudio.clip);
                    UI.Death();
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
                break;
            case "+Multi":
                BonusSounds.PlayBonusSound(tag);
                multiply += 0.5f;
                break;
            case "-Multi":
                BonusSounds.PlayBonusSound(tag);
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
                other.gameObject.SetActive(false);
                /*if (grayCor != null)
                { StopCoroutine(grayCor); }
                grayCor = ;*/
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
    IEnumerator ATMTransfer(float safer) //перевод денег с баланса карты на баланс денег.
    {
        safer = playerMovement.speed;
        playerMovement.speed = 0;
        yield return new WaitForSeconds(0.75f);
        MoneyCount += CreditcardCount;
        CreditcardCount = 0;
        yield return new WaitForSeconds(0.75f);
        playerMovement.speed = safer;
    }
}