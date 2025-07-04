using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Essentials")]
    public int SceneNum;
    bool isPause;
    public GameController moving;
    [SerializeField] GameObject Button;
    public GameObject PlayerModel;
    bool Pause2Check;

    [Header("Pause menu")]
    [SerializeField] GameObject cont;
    [SerializeField] GameObject cont2;
    [SerializeField] GameObject rest;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject pausemenu;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject PauseButton;

    [Header("In-game stats")]
    [SerializeField] Text CreditcardField;
    [SerializeField] Text MoneyCountField;
    [SerializeField] Text PointsField;
    [SerializeField] Text MultiField;
    [SerializeField] Text RecordText;
    public GameObject ShitImg;
    [SerializeField] Slider colourSlider;

    [Header("Results")]
    [SerializeField] Text ResultField;
    float Result;
    float ResultMoney;
    [SerializeField] Text ResultMoneyField;
    [SerializeField] GameObject res;
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject goodjob;
    [SerializeField] GameObject wip;
    void Start()
    {
        Button.SetActive(true);
        isPause = false;
        Result = 0;
        Pause2Check = false;
        var save = SaveManager.LoadProgress();
        RecordText.text = "������: " + save.Record.ToString();
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
    public void ContinueGame()//����������� ����
    {
        Time.timeScale = 1f;
        moving.isTimerOn = true;
        moving.playerMovement.walkEffect.Play();
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
    public void ContinueGame2()//����������� ���� ���� ���� �� ��������
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
    public void Pauser()//�����
    {
        Time.timeScale = 0f;
        moving.isTimerOn = false;
        moving.playerMovement.walkEffect.Stop();
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
    void Pauser2()//����� ���� ���� �� ��������
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
    public void ExitGame()//����� �� ����
    {
        Application.Quit();
    }
    public void Restart()//���������� ������
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNum);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void MainMenuGo()//������� � ������� ����
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void NextLevel()//������� �� ���� �������
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
    public void BeginGenLVL()//��������� ����� 9
    {
        SceneManager.LoadScene(9);
    }
    public void StartTimer()//������ ����
    {
        PauseButton.SetActive(true);
        moving.isTimerOn = true;
        Button.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        moving.playerMovement.walkEffect.Play();
    }
    public void Death()//������ ������
    {
        Destroy(moving.playerMovement.controller);
        GameOver.gameObject.SetActive(true);
        pausemenu.gameObject.SetActive(true);
        moving.isTimerOn = false;
        moving.playerMovement.walkEffect.Stop();
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
    IEnumerator ResultCount2()//������� ����� � ����� 9
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
    public IEnumerator ResultCount()//������� �����
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
    public IEnumerator ShieldBreak()//�������� ������ ����
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerModel.gameObject.SetActive(false);
            ShitImg.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            PlayerModel.gameObject.SetActive(true);
            ShitImg.gameObject.SetActive(true);
            foreach (var ps in PlayerModel.GetComponentsInChildren<ParticleSystem>())
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            yield return new WaitForSeconds(0.5f);
        }
        PlayerModel.gameObject.SetActive(false);
        ShitImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        PlayerModel.gameObject.SetActive(true);
        foreach (var ps in PlayerModel.GetComponentsInChildren<ParticleSystem>())
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        moving.isShield = false;
    }
    public IEnumerator NoColor()//���������� �����
    {
        GrayscaleRenderFeature.IsActive = true;
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
        GrayscaleRenderFeature.IsActive = false;
    }
}