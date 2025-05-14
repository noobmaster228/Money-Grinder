using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public static bool language;
    public GameObject[] NameGame;
    public GameObject[] StartGameText;
    public GameObject[] SelectLevelText;
    public GameObject[] HelpMenuText;
    public GameObject[] CreditsMenuText;
    public GameObject[] ExitText;
    public GameObject[] BackText;
    public GameObject[] HelpText;
    public GameObject[] SelectLevelMenu;
    void Start()
    {
        language = false;
    }
    public void English()
    {
        language = false;
        NameGame[0].SetActive(true);
        NameGame[1].SetActive(false);
        StartGameText[0].SetActive(true);
        StartGameText[1].SetActive(false);
        SelectLevelText[0].SetActive(true);
        SelectLevelText[1].SetActive(false);
        HelpMenuText[0].SetActive(true);
        HelpMenuText[1].SetActive(false);
        CreditsMenuText[0].SetActive(true);
        CreditsMenuText[1].SetActive(false);
        ExitText[0].SetActive(true);
        ExitText[1].SetActive(false);
        BackText[0].SetActive(true);
        BackText[1].SetActive(false);
    }
    public void Russian()
    {
        language = true;
        NameGame[0].SetActive(false);
        NameGame[1].SetActive(true);
        StartGameText[0].SetActive(false);
        StartGameText[1].SetActive(true);
        SelectLevelText[0].SetActive(false);
        SelectLevelText[1].SetActive(true);
        HelpMenuText[0].SetActive(false);
        HelpMenuText[1].SetActive(true);
        CreditsMenuText[0].SetActive(false);
        CreditsMenuText[1].SetActive(true);
        ExitText[0].SetActive(false);
        ExitText[1].SetActive(true);
        BackText[0].SetActive(false);
        BackText[1].SetActive(true);
    }
    public void HelpTextMethod()
    {
        if(language)
        {
            HelpText[1].SetActive(true);
        }
        else
        {
            HelpText[0].SetActive(true);
        }
    }

    public void SelectLevel()
    {
        if (language)
        {
            SelectLevelMenu[1].SetActive(true);
        }
        else
        {
            SelectLevelMenu[0].SetActive(true);
        }
    }
    void Update()
    {
        
    }
}
