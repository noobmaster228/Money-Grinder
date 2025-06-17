using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] GameObject Startgame;
    [SerializeField] GameObject SelectLeveling;
    [SerializeField] GameObject CreditsButton;
    [SerializeField] GameObject Exit;
    [SerializeField] GameObject[] CreditsText;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject SelectLevelMenu;
    [SerializeField] GameObject ShopButton;

    [Header("Shops")]
    [SerializeField] Text RecordField;
    float Record;
    float Balance;
    float premBalance;
    [SerializeField] Text BalanceField;
    [SerializeField] Text PremBalanceField;
    [SerializeField] GameObject ShopGrid;
    [SerializeField] SkinShopUI resetShop;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        var save = SaveManager.LoadProgress();
        Record = save.Record;
        Balance = save.Balance;
        premBalance = save.PremiumBalance;
        RecordField.text = "������: " + Record.ToString("#,0").Replace(',', '\'');
        BalanceField.text = Balance.ToString("#,0").Replace(',', '\'');
        PremBalanceField.text = premBalance.ToString("#,0").Replace(',', '\'');
    }
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
        ShopButton.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }
    public void Credits()
    {
        CreditsText[0].gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        ShopButton.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        RecordField.gameObject.SetActive(true);
    }
    public void Shop()
    {
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        ShopButton.SetActive(false);
        BalanceField.gameObject.SetActive(true);
        ShopGrid.SetActive(true);
    }
    public void Back()
    {
        SelectLevelMenu.gameObject.SetActive(false);
        CreditsText[0].gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Startgame.gameObject.SetActive(true);
        SelectLeveling.gameObject.SetActive(true);
        ShopButton.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        BalanceField.gameObject.SetActive(false);
        ShopGrid.SetActive(false);
        RecordField.gameObject.SetActive(false);
    }
    public void HelpMenu()
    {
        BackButton.gameObject.SetActive(true);
        Startgame.gameObject.SetActive(false);
        SelectLeveling.gameObject.SetActive(false);
        ShopButton.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }
    public void ResetProgress()
    {
        SaveManager.ResetAllProgress();
        var save = SaveManager.LoadProgress();

        RecordField.text = "������: "+save.Record.ToString("#,0").Replace(',', '\'');
        BalanceField.text = save.Balance.ToString("#,0").Replace(',', '\'');
        PremBalanceField.text = save.PremiumBalance.ToString("#,0").Replace(',', '\'');

        resetShop.ResetShop();
    }
}
