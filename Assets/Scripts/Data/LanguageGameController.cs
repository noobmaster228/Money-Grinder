using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageGameController : MonoBehaviour
{
    public bool lang;
    public GameObject[] PointsText;
    public GameObject[] StartButText;
    public GameObject[] LevelDescription;
    public GameObject[] ContinueText;
    public GameObject[] Continue2Text;
    public GameObject[] RestartText;
    public GameObject[] ExitText;
    public GameObject[] MainMenuText;
    public GameObject[] NextLevelText;
    public GameObject[] GoodjobText;

    private void Awake()
    {
        lang = LanguageController.language;
    }
    void Start()
    {
        if(lang)
        {
            PointsText[1].SetActive(true);
            StartButText[1].SetActive(true);
            LevelDescription[1].SetActive(true);
            ContinueText[1].SetActive(true);
            Continue2Text[1].SetActive(true);
            RestartText[1].SetActive(true);
            ExitText[1].SetActive(true);
            MainMenuText[1].SetActive(true);
            NextLevelText[1].SetActive(true);
            GoodjobText[1].SetActive(true);
        }
        else
        {
            PointsText[0].SetActive(true);
            StartButText[0].SetActive(true);
            LevelDescription[0].SetActive(true);
            ContinueText[0].SetActive(true);
            Continue2Text[0].SetActive(true);
            RestartText[0].SetActive(true);
            ExitText[0].SetActive(true);
            MainMenuText[0].SetActive(true);
            NextLevelText[0].SetActive(true);
            GoodjobText[0].SetActive(true);
        }
    }

}
