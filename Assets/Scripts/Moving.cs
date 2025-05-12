using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public int SceneNum;
    float t;
    float delta;
    bool isTimerOn;
    bool isShield;
    bool isPause;
    public float multiply;
    public float speed;
    public float moneybagValue;
    public float badMoneybagValue;
    public float moneyValue;
    public float badMoneyValue;
    public Vector3 startPos;
    public Vector3 endPos;
    public GameObject Button;
    public Text MultiField;
    public GameObject Dollar;
    public GameObject ShitImg;
    public GameObject cont;
    public GameObject cont2;
    public GameObject rest;
    public GameObject exit;
    public GameObject pausemenu;
    public GameObject goodjob;
    public GameObject wip;
    public GameObject MainMenu;
    public GameObject GameOver;
    public GameObject PauseButton;
    public Text CreditcardField;
    public float MoneyCount;
    public Text MoneyCountField;
    public Text PointsField;
    public float Points;
    public float PointsRate;
    public float Result;
    public Text ResultField;
    public GameObject res;
    //public WebGetSetValue DataBaseSets;
    public GameObject cam;
    public float CreditcardMoney;
    public float CreditcardCount;
    public ChunkPlacer triggerActivator;
    public WaterScroller waterSpeed;
    public Rigidbody falling;
    MobilePlayerController limits;
    AudioSource itemAudio;
    public AudioSource playerAudio;
    public BonusSoundPlayer BonusSounds;
    public AudioClip ShieldBreakSound;
    public AudioClip PointCountSound;
    public AudioClip MoneyAdd;
    public AudioClip RoundEndSound;
    public Animator animator;
    //float rotationDirection;
    //public float rotationSpeed = 100f;
    void Start()
    {
        CreditcardMoney = 50;
        //multiply = 1;
        Result = 0;
        t = 0;
        isTimerOn = false;
        Button.SetActive(true);
        transform.position = startPos;
        MoneyCount = 0;
        // PointsRate = 100;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPause = false;
        isShield = false;
        limits = GetComponent<MobilePlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                if ((t > 0) && (t < 10))
                {
                    ContinueGame();
                }
                else
                {
                    ContinueGame2();
                }
            }
            else
            {
                if (isTimerOn)
                {
                    Pauser();
                }
                else
                {
                    Pauser2();
                }
            }
        }
        if (isTimerOn)
        {

            t += Time.deltaTime;
            if (t >= 0.5f)
            {
                Points += PointsRate;
                t = 0;
            }
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            delta = 0;
            if (!Application.isMobilePlatform)
            {
                ChangeAngle(zeroRotation);
            }
            //rotationDirection = 0;
            if (Input.GetKey(KeyCode.A))
            {
                delta = -speed * Time.deltaTime;
                ChangeAngle(leftRotation);
                //rotationDirection = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                delta = speed * Time.deltaTime;
                ChangeAngle(rightRotation);
                //rotationDirection = 1f;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + delta, -1.8f, 1.8f));
            //if (rotationDirection != 0f)
            //{
            //    Dollar.transform.Rotate(Vector3.up, rotationDirection * rotationSpeed * Time.deltaTime, Space.Self);
            //}
            MoneyCountField.text = MoneyCount.ToString();
            PointsField.text = Points.ToString();
            MultiField.text = multiply.ToString();
            if (CreditcardField)
            { CreditcardField.text = "(" + CreditcardCount.ToString() + ")"; }

        }
        /*if (transform.position.y <= -2.2495f)
        {
            limits.limit = 200;
            Death();
        }*/
        if ((transform.position.x <= endPos.x) && (isTimerOn) && (SceneNum != 9))
        {
            t = 0;
            isTimerOn = false;
            StartCoroutine(ResultCount());
        }
    }
    public void StartTimer()
    {
        PauseButton.SetActive(true);
        isTimerOn = true;
        Button.SetActive(false);
        StartCoroutine(StartAcceleration(speed));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        switch (other.gameObject.tag)
        {
            case "+Speed":
                BonusSounds.PlayBonusSound(tag);
                speed += 1;
                waterSpeed.scrollSpeed += 0.01f;
                break;
            case "-Speed":
                BonusSounds.PlayBonusSound(tag);
                speed -= 1;
                waterSpeed.scrollSpeed -= 0.01f;
                if (speed == 0)
                {
                    Death();
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
                limits.limit = 200;
                other.tag = "Untagged";
                if (isShield)
                {
                    playerAudio.PlayOneShot(ShieldBreakSound);
                    StartCoroutine(ShieldBreak());
                }
                else
                {
                    playerAudio.PlayOneShot(itemAudio.clip);
                    Death();
                }
                break;
            case "DeathPit":
                itemAudio = other.GetComponent<AudioSource>();
                limits.limit = 200;
                other.tag = "Untagged";
                playerAudio.PlayOneShot(itemAudio.clip);
                Death();
                break;
            case "Protection":
                itemAudio = other.GetComponent<AudioSource>();
                playerAudio.PlayOneShot(itemAudio.clip);
                other.gameObject.SetActive(false);  
                isShield = true;
                ShitImg.gameObject.SetActive(true);
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
                if (grayCor != null)
                { StopCoroutine(grayCor); }

                grayCor = NoColor();
                StartCoroutine(grayCor);
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
                StartCoroutine(ATMTransfer(speed));
                break;
            case "trigger":
                triggerActivator.SpawnChunk();
                break;
        }


        /* if (other.gameObject.tag == "+Speed")
         {
             speed += 1;
         }
         if (other.gameObject.tag == "-Speed")
         {
             speed -= 1;
         }
         if (other.gameObject.tag == "+Money")
         {
             MoneyCount += 15;
         }
         if (other.gameObject.tag == "-Money")
         {
             MoneyCount -= 15;
         }
         if (other.gameObject.tag == "+Points")
         {
             PointsRate += 10;
         }
         if (other.gameObject.tag == "-Points")
         {
             PointsRate -= 10;
         }
         if (other.gameObject.tag == "Money")
         {
             MoneyCount += moneyValue*multiply;
             other.gameObject.SetActive(false);
         }
         if (other.gameObject.tag == "MoneyBag")
         {
             MoneyCount += moneybagValue*multiply;
             other.gameObject.SetActive(false);
         }
         if (other.gameObject.tag == "BadMoney")
         {
             other.gameObject.SetActive(false);
             MoneyCount += badMoneyValue*multiply;
         }
         if (other.gameObject.tag == "BadMoneyBag")
         {
             MoneyCount += badMoneybagValue*multiply;
             other.gameObject.SetActive(false);
         }
         if (other.gameObject.tag == "DeathPit")
         {
             if (isShield)
             {
                 StartCoroutine(ShieldBreak());
             }
             else
             {
                 GameOver.gameObject.SetActive(true);
                 rest.gameObject.SetActive(true);
                 exit.gameObject.SetActive(true);
                 pausemenu.gameObject.SetActive(true);
                 MainMenu.gameObject.SetActive(true);
                 isTimerOn = false;
                 Cursor.visible = true;
                 Cursor.lockState = CursorLockMode.None;
             }
         }
         if (other.gameObject.tag == "Protection")
         {
             other.gameObject.SetActive(false);
             isShield = true;
             ShitImg.gameObject.SetActive(true);
         }
         if (other.gameObject.tag == "+Multi")
         {
             multiply += 0.5f;
         }
         if (other.gameObject.tag == "-Multi")
         {
             if(multiply<=0)
             {
                 multiply = 0;
             }
             else
             {
                 multiply -= 0.5f;
             }

         }*/

    }
    IEnumerator grayCor;
    IEnumerator ResultCount2()
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = endPos;
        yield return new WaitForSeconds(0.5f);
        res.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Result += Points;
        Points = 0;
        PointsField.text = Points.ToString();
        ResultField.text = Result.ToString();
        playerAudio.PlayOneShot(PointCountSound);
        yield return new WaitForSeconds(0.5f);
        MoneyCount *= multiply;
        MoneyCountField.text = MoneyCount.ToString();
        yield return new WaitForSeconds(1f);
        Result += MoneyCount;
        MoneyCountField.text = MoneyCount.ToString();
        ResultField.text = Result.ToString();
        MoneyCount = 0;
        ResultField.text = Result.ToString();
        playerAudio.PlayOneShot(MoneyAdd);
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        rest.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        playerAudio.PlayOneShot(RoundEndSound);
    }
    public void Death()
    {
        Destroy(falling);
        GameOver.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        isTimerOn = false;
        if (SceneNum == 9)
        {
            StartCoroutine(ResultCount2());
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            rest.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            MainMenu.gameObject.SetActive(true);
        }
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isTimerOn = true;
        isPause = false;
        cont.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        rest.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        pausemenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseButton.SetActive(true);
    }
    public void ContinueGame2()
    {
        isPause = false;
        cont2.gameObject.SetActive(false);
        rest.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        pausemenu.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseButton.SetActive(true);
    }
    public void Pauser()
    {
        Time.timeScale = 0f;
        isTimerOn = false;
        isPause = true;
        cont.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        rest.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseButton.SetActive(false);
    }
    public void Pauser2()
    {
        isPause = true;
        cont2.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        rest.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseButton.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNum);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void MainMenuGo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    IEnumerator ResultCount()
    {
        transform.position = endPos;
        yield return new WaitForSeconds(0.5f);
        res.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        Result += Points;
        Points = 0;
        PointsField.text = Points.ToString();
        ResultField.text = Result.ToString();
        yield return new WaitForSeconds(0.5f);
        MoneyCount *= multiply;
        MoneyCountField.text = MoneyCount.ToString();
        yield return new WaitForSeconds(1f);
        Result += MoneyCount;
        if (FindObjectOfType<DataController>())
        {
            FindObjectOfType<DataController>().SetScore(Result, SceneNum - 1);
            FindObjectOfType<DataController>().SaveData();
        }



        MoneyCount = 0;
        MoneyCountField.text = MoneyCount.ToString();
        ResultField.text = Result.ToString();
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        goodjob.gameObject.SetActive(true);
        rest.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
    }
    IEnumerator ShieldBreak()
    {
        Dollar.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Dollar.gameObject.SetActive(true);
        ShitImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Dollar.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Dollar.gameObject.SetActive(true);
        ShitImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Dollar.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Dollar.gameObject.SetActive(true);
        ShitImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Dollar.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Dollar.gameObject.SetActive(true);
        isShield = false;
    }

    IEnumerator ATMTransfer(float safer)
    {
        safer = speed;
        speed = 0;
        yield return new WaitForSeconds(0.75f);
        MoneyCount += CreditcardCount;
        CreditcardCount = 0;
        yield return new WaitForSeconds(0.75f);
        speed = safer;

    }

    [SerializeField] GameObject[] badMoneyRend;
    [SerializeField] Material goodMat;
    [SerializeField] Material badMat;
    [SerializeField] Material badBagMat;
    [SerializeField] Slider colourSlider;
    IEnumerator NoColor()
    {
        for (int i = 0; i < badMoneyRend.Length; i++)
        {
            foreach (var item in badMoneyRend[i].GetComponentsInChildren<MeshRenderer>())
            {
                Debug.Log(item.material + " " + badBagMat);
                if (item.material.name.Substring(6, 3) != badBagMat.name.Substring(6, 3))
                { item.material = goodMat; }

            }

            //  badMoneyRend[i].GetComponentInChildren<MeshRenderer>().material = goodMat;
        }
        cam.GetComponent<Grayscale>().enabled = true;


        colourSlider.gameObject.SetActive(true);
        colourSlider.value = 7f;
        float time = 0f;
        while (time < 7f)
        {

            colourSlider.value = Mathf.Lerp(7f, 0f, time / 7f);
            time += Time.deltaTime;
            yield return null;
        }
        colourSlider.gameObject.SetActive(false);
        //  yield return new WaitForSeconds(7f);
        cam.GetComponent<Grayscale>().enabled = false;
        for (int i = 0; i < badMoneyRend.Length; i++)
        {
            foreach (var item in badMoneyRend[i].GetComponentsInChildren<MeshRenderer>())
            {
                if (item.material.name.Substring(6, 3) != badBagMat.name.Substring(6, 3))
                {
                    item.material = badMat;
                }
            }
        }
    }
    public void NextLevel()
    {
        if (SceneNum < 8)
        {
            SceneManager.LoadScene(SceneNum + 1);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            wip.SetActive(true);
            goodjob.gameObject.SetActive(false);
            rest.gameObject.SetActive(false);
            pausemenu.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            MainMenu.gameObject.SetActive(true);
        }
    }
    public void BeginGenLVL()
    {
        SceneManager.LoadScene(9);
    }
    IEnumerator StartAcceleration(float speedy)
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

    public Quaternion zeroRotation;
    public Quaternion leftRotation;
    public Quaternion rightRotation;
    public float lerpRotation = 0.5f;
    public void ChangeAngle(Quaternion angle)
    {
        Dollar.transform.localRotation = Quaternion.Lerp(Dollar.transform.localRotation, angle, lerpRotation);
    }
}