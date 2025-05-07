using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   // public GameObject[] NameGame;
    public GameObject Startgame;
    public GameObject SelectLeveling;
    public GameObject CreditsButton;
    public GameObject Exit;
    //public GameObject WIP;
    public GameObject[] CreditsText;
    public GameObject BackButton;
    public bool language=false;
    public GameObject EnglishBut;
    public GameObject RussianBut;
    //public WebGetSetValue gugugaga;
    // public Text giga;
    //public Text chad;
    // public GameObject[] SendRes;
    // public GameObject[] ImportRes;
    // public GameObject[] WrongPasssword;
    // public GameObject t1;
    // public GameObject t2;
    // public GameObject t3;
    // public GameObject[] UserNotFound;
    // public GameObject[] cancel;
    // public GameObject[] sendbut;
    // public GameObject[] impbut;
    // public GameObject passfield;
    // public GameObject namefield;
    // public GameObject Scores;
    // public GameObject[] ResSav;
    // public GameObject[] ResImp;
    // public GameObject Galochka;


    // public Text[] scores;
    // public DataController dataController;

    private void Start()
    {
        language = false;
        /*gugugaga = FindObjectOfType<WebGetSetValue>();
        gugugaga.GetAllData();

        dataController= FindObjectOfType<DataController>();

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].text = dataController.getLevelScore(i).ToString();
       }*/

    }
    public void English()
    {
        language = false;

    }
    public void Russian()
    {
        language = true;
    }
    
   /* public void HEHEHEHE()
    {
        gugugaga.GetAllData();
    }*/
  /*public void ImportDataFromBase()
    {

        int j = 0;
        while (gugugaga.requestText["Users"][j] != null)
        {
            j++;
        }

        bool isExist = false;
        for (int i = 0; i < j; i++)
        {
            if (gugugaga.requestText["Users"][i]["UserName"] == giga.text)
            {
                isExist = true;
                if (gugugaga.requestText["Users"][i]["Password"] == chad.text)
                {
                    for (int k = 0; k < scores.Length; k++)
                    {
                        dataController.scores[k] = gugugaga.requestText["results"][giga.text][k];
                        scores[k].text = gugugaga.requestText["results"][giga.text][k];
                        dataController.SaveData();
                    }
                    StartCoroutine(ResultsImported());
                }
                else
                {

                    StartCoroutine(WrongPassword());
                    break;
                }
            }
        }

        if (!isExist)
        {
            StartCoroutine(NoUser());
        }
    }*/  
    /*public void SendDataToBase()
    {

        gugugaga.SendResult(giga.text, chad.text);
    }*/
    public void Begin()
    {
        SceneManager.LoadScene(1);
    }
    public void Beginlvl2()
    {
        SceneManager.LoadScene(2);
    }
    public void Beginlvl3()
    {
        SceneManager.LoadScene(3);
    }
    public void Beginlvl4()
    {
        SceneManager.LoadScene(4);
    }
    public void Beginlvl5()
    {
        SceneManager.LoadScene(5);
    }
    public void Beginlvl6()
    {
        SceneManager.LoadScene(6);
    }
    public void Beginlvl7()
    {
        SceneManager.LoadScene(7);
    }
    public void Beginlvl8()
    {
        SceneManager.LoadScene(8);
    }
    public void BeginGenLVL()
    {
        SceneManager.LoadScene(9);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SelectLevel()
    {
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        EnglishBut.SetActive(false);
        RussianBut.SetActive(false);
    }
    public void Credits()
    {
        if (language)
        {
            CreditsText[1].gameObject.SetActive(true);
        }
        else
        {
            CreditsText[0].gameObject.SetActive(true);
        }
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        EnglishBut.SetActive(false);
        RussianBut.SetActive(false);
    }
    public void Back()
    {
        // WIP.gameObject.SetActive(false);
        if (language)
        {
            CreditsText[1].gameObject.SetActive(false);
        }
        else
        {
            CreditsText[0].gameObject.SetActive(false);
        }
        BackButton.gameObject.SetActive(false);
        Startgame.gameObject.SetActive(true);
        SelectLeveling.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        //EnglishBut.SetActive(true);
        //RussianBut.SetActive(true);
    }
    public void HelpMenu()
    {
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        EnglishBut.SetActive(false);
        RussianBut.SetActive(false);
    }
   /* public IEnumerator NoUser()
    {
        UserNotFound[0].SetActive(true);
        t3.SetActive(true);
        SendRes[0].SetActive(false);
        ImportRes[0].SetActive(false);
        BackButton[0].SetActive(false);
        cancel[0].SetActive(false);
        sendbut[0].SetActive(false);
        impbut[0].SetActive(false);
        passfield.SetActive(false);
        namefield.SetActive(false);
        Scores.SetActive(false);
        yield return new WaitForSeconds(1);
        t3.SetActive(false);
        t2.SetActive(true);
        yield return new WaitForSeconds(1);
        t2.SetActive(false);
        t1.SetActive(true);
        yield return new WaitForSeconds(1);
        UserNotFound[0].SetActive(false);
        t1.SetActive(false);
        SendRes[0].SetActive(true);
        ImportRes[0].SetActive(true);
        BackButton[0].SetActive(true);



    }
   /* public IEnumerator WrongPassword()
    {
        WrongPasssword[0].SetActive(true);
        t3.SetActive(true);
        SendRes[0].SetActive(false);
        ImportRes[0].SetActive(false);
        BackButton[0].SetActive(false);
        cancel[0].SetActive(false);
        sendbut[0].SetActive(false);
        impbut[0].SetActive(false);
        passfield.SetActive(false);
        namefield.SetActive(false);
        Scores.SetActive(false);
        //
        WrongPasssword[0].SetActive(true);
        SendRes[0].SetActive(false);
        ImportRes[0].SetActive(false);
        BackButton[0].SetActive(false);
        cancel[0].SetActive(false);
        sendbut[0].SetActive(false);
        impbut[0].SetActive(false);
        yield return new WaitForSeconds(1);
        t3.SetActive(false);
        t2.SetActive(true);
        yield return new WaitForSeconds(1);
        t2.SetActive(false);
        t1.SetActive(true);
        yield return new WaitForSeconds(1);
        WrongPasssword[0].SetActive(false);
        t1.SetActive(false);
        SendRes[0].SetActive(true);
        ImportRes[0].SetActive(true);
        BackButton[0].SetActive(true);
        Scores.SetActive(true);
        //
        SendRes[0].SetActive(true);
        ImportRes[0].SetActive(true);
        BackButton[0].SetActive(true);
        WrongPasssword[0].SetActive(false);
    }
    public IEnumerator ResultsSaved()
    {
        ResSav[0].SetActive(true);
        Galochka.SetActive(true);
        yield return new WaitForSeconds(3);
        ResSav[0].SetActive(false);
        Galochka.SetActive(false);
    }
    public IEnumerator ResultsImported()
    {
        ResImp[0].SetActive(true);
        Galochka.SetActive(true);
        yield return new WaitForSeconds(3);
        ResImp[0].SetActive(false);
        Galochka.SetActive(false);
    }
   */
}
