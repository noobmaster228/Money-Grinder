using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Startgame;
    public GameObject SelectLeveling;
    public GameObject CreditsButton;
    public GameObject Exit;
    public GameObject[] CreditsText;
    public GameObject BackButton;
    public GameObject SelectLevelMenu;

    public void Startlevel(int levelnum)
    {
        SceneManager.LoadScene(levelnum);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SelectLevel()
    {
        SelectLevelMenu.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }
    public void Credits()
    {

        CreditsText[0].gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }
    public void Back()
    {
        SelectLevelMenu.gameObject.SetActive(false);
        CreditsText[0].gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Startgame.gameObject.SetActive(true);
        SelectLeveling.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
    }
    public void HelpMenu()
    {
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }
}
