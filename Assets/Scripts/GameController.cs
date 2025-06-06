using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float TimeCycle; //Переменная для подсчёта очков
    public bool isTimerOn; //проверка запуска таймера игры
    public bool isShield; //проверка наличия щита
    public float multiply; //значение мультипликатора
    [SerializeField] float moneybagValue; //значение 1 банкноты
    [SerializeField] float badMoneybagValue; //значение плохой банкноты
    [SerializeField] float moneyValue; //значение мешка с деньгами
    [SerializeField] float badMoneyValue; //значение плохого мешка с деньгами
    [SerializeField] Vector3 startPos; //Позиция откуда игрок начинает уровень
    public Vector3 endPos; //Позиция для концовки игры
    public float MoneyCount; //баланс игрока
    public float Points; //кол-во очков игрока
    public float PointsRate; //кол-во очков в полсекунды
    [SerializeField] float CreditcardMoney; //кол-во денег подобранных с кредитной карты
    public float CreditcardCount; //баланс игрока в кредитных карточках
    [SerializeField] ChunkPlacer triggerActivator; //скрипт процедурной генерации 
    [SerializeField] WaterScroller waterSpeed; //скрипт для движения текстуры воды
    public Rigidbody falling; //управление компонентом Rigidbody
    AudioSource itemAudio; //источник звука предмета
    public AudioSource playerAudio; //источник звука игрока
    [SerializeField] BonusSoundPlayer BonusSounds; //скрипт управляющий звуком бонусов
    [SerializeField] AudioClip ShieldBreakSound; //аудиоклип лома щита  
    public AudioClip PointCountSound; //аудиоклип финального подсчёта очков
    public AudioClip MoneyAdd; //аудиоклип перевода баланса игрока
    public AudioClip RoundEndSound; //аудиоклип конца игры
    [SerializeField] UIManager UI; //управления UI игры
    public PlayerControls playerMovement; //скрипт управления игрока
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
    void Update() //Добавление очков каждые 0.5 секунды и считывание конца обучающих уровней
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
    private void OnTriggerEnter(Collider other) //при пересечении триггера считывается тег и выполняется действия в зависимости от тега
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
    IEnumerator ATMTransfer(float safer) //перевод денег с баланса карты на баланс денег.
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