using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int SceneNum;
    bool isPause;
    [SerializeField] GameObject Button;
    [SerializeField] Text MultiField;
    [SerializeField] GameObject PlayerModel;
    public GameObject ShitImg;
    [SerializeField] GameObject cont;
    [SerializeField] GameObject cont2;
    [SerializeField] GameObject rest;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject pausemenu;
    [SerializeField] GameObject goodjob;
    [SerializeField] GameObject wip;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject PauseButton;
    [SerializeField] Text CreditcardField;
    [SerializeField] Text MoneyCountField;
    [SerializeField] Text PointsField;
    float Result;
    float ResultMoney;
    [SerializeField] Text ResultField;
    [SerializeField] Text ResultMoneyField;
    [SerializeField] GameObject res;
    [SerializeField] GameObject[] badMoneyRend;
    [SerializeField] Material goodMat;
    [SerializeField] Material badMat;
    [SerializeField] Material badBagMat;
    [SerializeField] Slider colourSlider;
    public GameObject cam;
    public GameController moving;
    bool Pause2Check;
    [SerializeField] Text RecordText;
    void Start()
    {
        Button.SetActive(true);
        isPause = false;
        Result = 0;
        Pause2Check = false;
        var save = SaveManager.LoadProgress();
        RecordText.text = "Рекорд: " + save.Record.ToString();
    }
    void Update()
        {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                if (!Pause2Check)
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
                if (moving.isTimerOn)
                {
                    Pauser();
                }
                else
                {
                    Pauser2();
                }
            }
        }
        if (moving.isTimerOn)
        {
            MoneyCountField.text = moving.MoneyCount.ToString();
            PointsField.text = moving.Points.ToString();
            if (SceneNum >= 6)
            {
                MultiField.text = moving.multiply.ToString() + " X";
            }
            if (CreditcardField)
            {
                CreditcardField.text = "(" + moving.CreditcardCount.ToString() + ")";
            }
        }
    }
    public void ContinueGame()//Продолжение игры
    {
        Time.timeScale = 1f;
        moving.isTimerOn = true;
        isPause = false;
        cont.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        rest.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        pausemenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseButton.SetActive(true);
        Pause2Check = false;
    }
    public void ContinueGame2()//Продолжение игры пока игра не началась
    {
        isPause = false;
        cont2.gameObject.SetActive(false);
        rest.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        pausemenu.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Pause2Check = false;
    }
    public void Pauser()//Пауза
    {
        Time.timeScale = 0f;
        moving.isTimerOn = false;
        isPause = true;
        cont.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        rest.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseButton.SetActive(false);
        Pause2Check = false;
    }
    public void Pauser2()//Пауза пока игра не началась
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
        Pause2Check = true;
    }
    public void ExitGame()//Выход из игры
    {
        Application.Quit();
    }
    public void Restart()//Перезапуск уровня
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNum);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void MainMenuGo()//Переход в главное меню
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void NextLevel()//Переход на след уровень
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
    public void BeginGenLVL()//Включение сцены 9
    {
        SceneManager.LoadScene(9);
    }
    public void StartTimer()//Начало игры
    {
        PauseButton.SetActive(true);
        moving.isTimerOn = true;
        Button.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Death()//Смерть игрока
    {
        Destroy(moving.falling);
        GameOver.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        moving.isTimerOn = false;
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
    IEnumerator ResultCount2()//Подсчёт очков в сцене 9
    {
        yield return new WaitForSeconds(1.5f);
        moving.transform.position = moving.endPos;
        yield return new WaitForSeconds(0.5f);
        res.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Result += moving.Points;
        moving.Points = 0;
        PointsField.text = moving.Points.ToString();
        ResultField.text = Result.ToString();
        moving.playerAudio.PlayOneShot(moving.PointCountSound);
        yield return new WaitForSeconds(1f);
        ResultMoney += moving.MoneyCount;
        moving.MoneyCount = 0;
        MoneyCountField.text = moving.MoneyCount.ToString();
        ResultMoneyField.text = ResultMoney.ToString();
        moving.playerAudio.PlayOneShot(moving.MoneyAdd);
        var save = SaveManager.LoadProgress();
        float newRecord = Mathf.Max(save.Record, Result);
        float newBalance = save.Balance + ResultMoney;
        SaveManager.SaveProgressOnlyStats(newRecord, newBalance);
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        rest.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        moving.playerAudio.PlayOneShot(moving.RoundEndSound);
    }
    public IEnumerator ResultCount()//Подсчёт очков
    {
        moving.transform.position = moving.endPos;
        yield return new WaitForSeconds(0.5f);
        res.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Result += moving.Points;
        moving.Points = 0;
        PointsField.text = moving.Points.ToString();
        ResultField.text = Result.ToString();
        yield return new WaitForSeconds(0.5f);
        moving.MoneyCount *= moving.multiply;
        MoneyCountField.text = moving.MoneyCount.ToString();
        yield return new WaitForSeconds(1f);
        Result += moving.MoneyCount;
        //if (FindObjectOfType<DataController>())
        //{
        //    FindObjectOfType<DataController>().SetScore(moving.Result, SceneNum - 1);
        //    FindObjectOfType<DataController>().SaveData();
        //}
        moving.MoneyCount = 0;
        MoneyCountField.text = moving.MoneyCount.ToString();
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
    public IEnumerator ShieldBreak()//анимация Пролом щита
    {
        for (int i=0; i<3; i++)
        {
            PlayerModel.gameObject.SetActive(false);
            ShitImg.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            PlayerModel.gameObject.SetActive(true);
            ShitImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        PlayerModel.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        PlayerModel.gameObject.SetActive(true);
        moving.isShield = false;
    }
    public IEnumerator NoColor()//Отключение цвета
    {
        for (int i = 0; i < badMoneyRend.Length; i++)
        {
            foreach (var item in badMoneyRend[i].GetComponentsInChildren<MeshRenderer>())
            {
                Debug.Log(item.material + " " + badBagMat);
                if (item.material.name.Substring(6, 3) != badBagMat.name.Substring(6, 3))
                { item.material = goodMat; }
            }
        }
        cam.GetComponent<Grayscale>().enabled = true;
        colourSlider.gameObject.SetActive(true);
        colourSlider.value = 4f;
        float time = 0f;
        while (time < 4f)
        {
            colourSlider.value = Mathf.Lerp(4f, 0f, time / 4f);
            time += Time.deltaTime;
            yield return null;
        }
        colourSlider.gameObject.SetActive(false);
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
}